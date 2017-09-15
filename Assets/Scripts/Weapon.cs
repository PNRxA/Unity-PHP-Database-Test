using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    //Specify data type for each stat of each weapon so they can only be that when created.
    //Basic getters and setters for weapon class...
    public int id { get; set; }
    public string weaponName { get; set; }
    public float clipSize { get; set; }
    public float damage { get; set; }
    public float fireRate { get; set; }
    public float range { get; set; }
    public float weight { get; set; }
    public string ammoType { get; set; }
    public Texture2D iconName { get; set; }

    public Weapon(int id, string weaponName, float clipSize, float damage, float fireRate, float range, float weight, string ammoType, Texture2D iconName)
    {
        this.id = id;
        this.weaponName = weaponName;
        this.clipSize = clipSize;
        this.damage = damage;
        this.fireRate = fireRate;
        this.range = range;
        this.weight = weight;
        this.ammoType = ammoType;
        this.iconName = iconName;
    }
}
