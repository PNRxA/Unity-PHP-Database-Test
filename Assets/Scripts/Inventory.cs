using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private float scrW, scrH;
    public List<Weapon> inv = new List<Weapon>();
    private string weaponIDText = "100";
    public bool debug;

    Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();

    void Start()
    {
        //On game start, load the items from the database
        StartCoroutine(LoadItemData());
    }

    IEnumerator LoadItemData()
    {
        WWW itemDataURL = new WWW("http://localhost/databass_wubwubwub/itemdata.php");
        yield return itemDataURL;
        string textDataString = itemDataURL.text;

        //Set the weapons dictionary based on the string returned from the database
        weapons = Weapons.GetFromDB(textDataString);
    }

    void OnGUI()
    {
        //Determine screen ratio
        scrH = Screen.height / 9;
        scrW = Screen.width / 16;
        //If debugging show the inventory screen so you can add items
        if (debug)
        {
            InventoryScreen();
        }
        //If you click the debug button, enter debugging mode
        if (GUI.Button(new Rect(scrW * 15, scrH * 8, scrW, scrH), "Debug"))
        {
            debug = !debug;
        }
    }

    //Inventory screen of all items you own
    void InventoryScreen()
    {
        //Main inventory box
        GUI.Box(new Rect(0, 0, scrW * 5, scrH * 9), "Inventory");
        //The weapon ID to add to your inventory
        weaponIDText = GUI.TextField(new Rect(scrW * 10, scrW * 4, scrW * 6, scrH), weaponIDText);
        //If you click the add weapon button then add the weapon with the ID entered in the text box above 
        if (GUI.Button(new Rect(scrW * 14, scrW * 5, scrW * 2, scrH), "Add weapon"))
        {
            //Attempt to add weapon of the ID in the dictionary, if it doesn't exist then show the user the error 
            try
            {
                inv.Add(weapons[int.Parse(weaponIDText)]);
            }
            catch (System.Exception ex)
            {
                weaponIDText = ex.Message;
            }
        }
        //Don't sue me
        float itemWidth = 1;
        float itemHeight = 1;
        //For every item in your inventory, draw a box in rows
        for (int i = 0; i < inv.Count; i++)
        {
            //Specify position based on item width and height
            Rect rect = new Rect(scrW * itemWidth, scrH * itemHeight, scrW, scrH);
            //Draw box with the weapon.weaponname in the inventory at the rect's position
            GUI.Box(new Rect(rect), inv[i].weaponName);

            itemWidth++;
            //If itemWidth amount of items in row, drop a row
            if (itemWidth >= 4)
            {
                itemWidth = 1;
                itemHeight++;
            }

            //If the weaopn box contains a cursor show tooltip based on the weapon
            if (rect.Contains(point: Event.current.mousePosition))
            {
                Tooltip(inv[i]);
            }
        }
    }
    //Show a tooltip based on a weapon's stats
    void Tooltip(Weapon weapon)
    {
        string weaponInfo = "Name: " + weapon.weaponName + "\n" +
                            "Clip Size: " + weapon.clipSize + "\n" +
                            "Damage: " + weapon.damage + "\n" +
                            "Fire Rate: " + weapon.fireRate + "\n" +
                            "Range: " + weapon.range + "\n" +
                            "Weight: " + weapon.weight + "\n" +
                            "Ammo: " + weapon.ammoType;
        //Draw a box which cocntains the weapon info above
        GUI.Box(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, scrW * 5, scrH * 5), weaponInfo);
    }

}
