using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public static class AuthGenerator
{
    public static string Generate()
    {
        StringBuilder code = new StringBuilder();
        for (int i = 0; i < 6; i++)
        {
            if (R())
            {
                code.Append(Random.Range(0, 9));
            }
            else
            {
                code.Append(GetRandomCharacter());
            }
        }
        return code.ToString();
    }

    private static bool R()
    {
        int yn = Random.Range(0, 2);
        if (yn >= 1)
        {
            return true;
        }
        return false;
    }

    public static char GetRandomCharacter()
    {
        string st = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return st[Random.Range(0,st.Length)];
    }
}
