using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public List<GameObject> weapons;
    public Dictionary<string, GameObject> weaponList = new Dictionary<string, GameObject>();
    public GameObject currentWeapon;
    public GameObject newWeapon;
    public Transform weaponPos;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weaponList.Add("10" + i, weapons[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwapWeapon(int id)
    {
        if (currentWeapon != null) Destroy(currentWeapon);
        newWeapon = Instantiate(weaponList[id.ToString()], weaponPos);
        newWeapon.transform.position = weaponPos.position;
        currentWeapon = newWeapon;
    }
}
