using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSelect : MonoBehaviour
{
    public Vector2 mousePos;
    public string direction;
    public bool showSelectMenu;
    public float scrH, scrW;
    public Inventory inventory;
    public Weapon selectedWeapon;
    public Weapon equippedWeapon;
    int invSize;
    public WeaponHandler player;

    public Texture bulletTexture;
    public int maxClip, currentClip, maxRow;
    public float ammoStartX, ammoStartY, ammoSpacingX, ammoSpacingY, ammoSizeX, ammoSizeY, amStartX;

    void Start()
    {
        //Set initial screen ratios
        scrH = Screen.height / 9;
        scrW = Screen.width / 16;
        //Current clip becomes max clip
        currentClip = maxClip;
        //Temporary amStartX becomes user set ammoStartX (used to be able to drop a line for more bullets)
        amStartX = ammoStartX;
    }

    void Update()
    {
        //Class code
        /*
        if (currentClip > 30)
        {
            currentClip -= 30;
            rows++;
        }
        else if (currentClip <= 0 && rows > 0)
        {
            rows--;
            currentClip += 30;
        }
        */


        //Debug.Log(Input.GetAxis("Horizontal").ToString() + Input.GetAxis("Vertical").ToString() + ":)");

        //Show the quick select menu when pressing the jump key
        if (Input.GetButtonDown("Jump"))
        {
            //Set the screen ratios in case they've changed since the game started
            scrH = Screen.height / 9;
            scrW = Screen.width / 16;
            //Show or hide the quick select menu
            showSelectMenu = !showSelectMenu;
            //The invSize becomes the actual size of the inventory
            invSize = inventory.inv.Count;
        }

        if (showSelectMenu)
        {

        }
    }

    void OnGUI()
    {
        //If the showSelectMenu bool is true show the quickselect menu otherwise show the bullet count
        if (showSelectMenu)
        {
            SelectMenu();
        }
        else
        {
            BulletMenu();
        }
        HUD();
    }
    //Shows the amount of bullets in a clip visually
    void BulletMenu()
    {
        int r = 1; //Row
        int cr = 1; //Current row[bullet] number
        int c2 = 0; //C in the loop but resets every maxRow
        //For each column until reaching the end of the currentClip...
        for (int c = 0; c < currentClip; c++)
        {
            //Draw a bullet in this column
            GUI.DrawTexture(new Rect(amStartX * scrW + c2 * (ammoSpacingX * scrW), ammoStartY * scrH + r * (ammoSpacingY * scrH), ammoSizeX * scrW, ammoSizeY * scrH), bulletTexture);
            //C2 increases
            c2++;
            //If the column number is above the maxRow amount
            if (cr > maxRow)
            {   
                //Drop a row
                r++;
                //Start at the beginning of the line
                amStartX = ammoStartX;
                //Reset the c2 and cr variables so that a new line of bullets is made
                c2 = 0;
                cr = 0;
            }

            cr++;
        }
        //Class code
        /*
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < 30; c++)
                {
                    GUI.DrawTexture(new Rect(amStartX * scrW + c2 * (ammoSpacingX * scrW), ammoStartY * scrH  + r * (ammoSpacingY * scrH), ammoSizeX * scrW, ammoSizeY * scrH), bulletTexture);
                }
            }
            for (int c = 0; c < currentClip; c++)
            {
                GUI.DrawTexture(new Rect(amStartX * scrW + c2 * (ammoSpacingX * scrW), ammoStartY * scrH  + rows * (ammoSpacingY * scrH), ammoSizeX * scrW, ammoSizeY * scrH), bulletTexture);
            }
         */

    }
    //Quick select menu
    void SelectMenu()
    {
        //Tooltip
        Rect cursorPos = new Rect(scrW * 6.5f * Input.GetAxisRaw("Horizontal") + (scrW * 7.5f), scrH * 3 * -Input.GetAxisRaw("Vertical") + (scrH * 4), scrW, scrH);
        GUI.Box(cursorPos, "cusor");

        //This code is used for all positions
        //Top position
        Rect topPos = new Rect(scrW * 7.5f, scrH, scrW, scrH);
        //Create  a box with the top position
        GUI.Box(topPos, "");
        //Populate that box with a texture if there is an item in it
        PopulateInvIcons(0, topPos, cursorPos);


        /* Center of screen
        GUI.Box(new Rect(scrW * 0, scrH * 4, scrW * 8, scrH), "");
        GUI.Box(new Rect(scrW * 8, scrH * 4, scrW * 8, scrH), "");
		*/

        //Top right
        Rect topRightPos = new Rect(scrW * 14, scrH, scrW, scrH);
        GUI.Box(topRightPos, "");
        PopulateInvIcons(1, topRightPos, cursorPos);

        //Middle right
        Rect middleRightPos = new Rect(scrW * 14, scrH * 4, scrW, scrH);
        GUI.Box(middleRightPos, "");
        PopulateInvIcons(2, middleRightPos, cursorPos);

        //Bottom right
        Rect bottomRightPos = new Rect(scrW * 14, scrH * 7, scrW, scrH);
        GUI.Box(bottomRightPos, "");
        PopulateInvIcons(3, bottomRightPos, cursorPos);

        //Bottom middle
        Rect bottomMiddlePos = new Rect(scrW * 7.5f, scrH * 7, scrW, scrH);
        GUI.Box(bottomMiddlePos, "");
        PopulateInvIcons(4, bottomMiddlePos, cursorPos);

        //Buttom left
        Rect bottomLeftPos = new Rect(scrW, scrH * 7, scrW, scrH);
        GUI.Box(bottomLeftPos, "");
        PopulateInvIcons(5, bottomLeftPos, cursorPos);

        //Middle left
        Rect middleLeftPos = new Rect(scrW, scrH * 4, scrW, scrH);
        GUI.Box(middleLeftPos, "");
        PopulateInvIcons(6, middleLeftPos, cursorPos);

        //Top Left
        Rect topLeftPos = new Rect(scrW, scrH, scrW, scrH);
        GUI.Box(topLeftPos, "");
        PopulateInvIcons(7, topLeftPos, cursorPos);
    }
    //Populates the icons of each box in the quickselect based on weapon index, rect position and the cursor position.
    void PopulateInvIcons(int index, Rect pos, Rect cursorPos)
    {
        //If the quickselect box contains the selection rectangle (moved through arrows or controller) or the cursor then select the item
        if (pos.Contains(cursorPos.position) && invSize > index || pos.Contains(Event.current.mousePosition) && invSize > index)
        {
            //Prove code is hit
            Debug.Log("test");
            //Change the equipped weapon to the one in the inventory with the correct index
            equippedWeapon = inventory.inv[index];
            player.SwapWeapon(equippedWeapon.id);
            //Set newClip variable with the new gun's clipsize
            int newClip = Mathf.RoundToInt(equippedWeapon.clipSize);
            //Set max and current clip so bullet display is accurate
            maxClip = newClip;
            currentClip = newClip;
            //Hide quick select menu
            showSelectMenu = false;
        }
        //If the inventory size is larger than the index specified, show the item icon in that inventory slot
        if (invSize > index)
        {
            //Draw icon to the rectangle with the correct icon of that inventory index
            GUI.DrawTexture(pos, inventory.inv[index].iconName);
        }
    }

    void HUD()
    {
        //If there is an equipped weapon, draw it.
        if (equippedWeapon != null)
        {
            //Draw the weapon texture of the equipped weapon
            GUI.DrawTexture(new Rect(0, 0, scrW, scrH), equippedWeapon.iconName);
        }
    }
}
