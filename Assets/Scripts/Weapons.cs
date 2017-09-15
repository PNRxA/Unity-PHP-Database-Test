using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Weapons
{
    /// <summary> 
    ///Function to grab weapons from a database and then store them in a dictionary by their ID
    /// </summary>
    /// <param name="input">String of weapons returned from database</param>
    /// <returns>Dictionary of weapons with ID as key</returns>
    public static Dictionary<int, Weapon> GetFromDB(string input)
    {
        //stores each weapon as a new string[] so each stat can be refrenced
        List<string[]> itemsdb = new List<string[]>();
        //Store each item as a string eg itemstat1|itemstat2|etc;
        string[] items;
        //Split the long string by ; as it indicates the end of an item's stats
        items = input.Split(';');
        //For each item, split the stats and store that array in a list (itemsdb)
        for (int y = 0; y < items.Length - 1; y++)
        {
            //Split the stats by |
            string[] itemCache = items[y].Split('|');
            //Add the array of stats into the list of arrays
            itemsdb.Add(itemCache);
        }
        //Create a dictionary so the weapons can be refrenced by their ID (int)
        Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();
        //For each item array in itemsdb, add to a dictionary and use the ID as the key
        for (int i = 0; i < itemsdb.Count; i++)
        {
            //Used to debug each weapon and stat added into the dictionary
            for (int x = 0; x < itemsdb[i].Length; x++)
            {
                //Log i (index in itemsdb) and [i][x] (specific stat of item in itemsdb)
                Debug.Log(i);
                Debug.Log(itemsdb[i][x]);
            }
            //Add weapon into the weapons dictionary by using the ID as the key and converting each string to the correct data type
            weapons[int.Parse(itemsdb[i][0])] = new Weapon(int.Parse(itemsdb[i][0]), itemsdb[i][1], float.Parse(itemsdb[i][2]), float.Parse(itemsdb[i][3]), float.Parse(itemsdb[i][4]), float.Parse(itemsdb[i][5]), float.Parse(itemsdb[i][6]), itemsdb[i][7], Resources.Load("Icons/" + itemsdb[i][8]) as Texture2D);
        }
        //Return the complete dictionary
        return weapons;
    }
}
