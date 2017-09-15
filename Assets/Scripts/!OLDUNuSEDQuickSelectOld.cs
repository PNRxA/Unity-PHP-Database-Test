using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSelectOld : MonoBehaviour
{
    public Vector2 mousePos;
    public string direction;
    public bool showSelectMenu;
    public float scrH, scrW;
    public Inventory inventory;
    public Weapon selectedWeapon;
    public Weapon equippedWeapon;
	

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scrH = Screen.height / 9;
            scrW = Screen.width / 16;
            showSelectMenu = !showSelectMenu;
        }

        if (showSelectMenu)
        {

            mousePos = Input.mousePosition;
            if (-mousePos.y + Screen.height >= scrH * 0 && -mousePos.y + Screen.height <= scrH * 4)
            {
                if (mousePos.x >= scrW * 7.5f && mousePos.x <= scrW * 8.5f)
                {
                    direction = "Up";
                    Debug.Log(direction);
                }
            }
            if (-mousePos.y + Screen.height >= scrH * 4 && -mousePos.y + Screen.height <= scrH * 5)
            {
                if (mousePos.x >= scrW * 8.5f && mousePos.x <= scrW * 16f)
                {
                    direction = "Right";
                    Debug.Log(direction);
                }
            }
            if (-mousePos.y + Screen.height >= scrH * 5 && -mousePos.y + Screen.height <= scrH * 9)
            {
                if (mousePos.x >= scrW * 7.5f && mousePos.x <= scrW * 8.5f)
                {
                    direction = "Down";
                    Debug.Log(direction);
                }
            }
            if (-mousePos.y + Screen.height >= scrH * 4 && -mousePos.y + Screen.height <= scrH * 5)
            {
                if (mousePos.x >= scrW * 0f && mousePos.x <= scrW * 7.5f)
                {
                    direction = "Left";
                    Debug.Log(direction);
                }
            }


            if (Input.GetMouseButtonDown(0))
            {
                if (direction != "")
                {
                    switch (direction)
                    {
                        case "Up":
                            selectedWeapon = inventory.inv[0];
                            Debug.Log(selectedWeapon.weaponName);
                            break;
                        case "Right":
                            selectedWeapon = inventory.inv[1];
                            Debug.Log(selectedWeapon.weaponName);
                            break;
                        case "Down":
                            selectedWeapon = inventory.inv[2];
                            Debug.Log(selectedWeapon.weaponName);
                            break;
                        case "Left":
                            selectedWeapon = inventory.inv[3];
                            Debug.Log(selectedWeapon.weaponName);
                            break;

                    }
                }
            }
        }
    }

    void OnGUI()
    {
        if (showSelectMenu)
        {
            SelectMenu();
        }
    }

    void SelectMenu()
    {
        //Dead
        GUI.Box(new Rect(scrW * 7.5f, scrH * 4, scrW, scrH), "");
        if (selectedWeapon != null)
        {
            GUI.DrawTexture(new Rect(scrW * 7.5f, scrH * 4, scrW, scrH), selectedWeapon.iconName);
        }
        //Up
        GUI.Box(new Rect(scrW * 7.5f, scrH * 0, scrW, scrH * 4), "");
        //Right
        GUI.Box(new Rect(scrW * 8.5f, scrH * 4, scrW * 7.65f, scrH), "");
        //Down
        GUI.Box(new Rect(scrW * 7.5f, scrH * 5, scrW, scrH * 4), "");
        //Left
        GUI.Box(new Rect(scrW * 0f, scrH * 4, scrW * 7.5f, scrH), "");
    }
}
