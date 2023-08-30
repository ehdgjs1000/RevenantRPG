using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public static int characterNum;

    public static float power = PlayerPrefs.GetFloat("Power");
    public static float HP = PlayerPrefs.GetFloat("HP");
    public static float luck = PlayerPrefs.GetFloat("Luck");

    public static float itemPower=100;
    public static float itemHP=100;
    public static float itemLuck = 1;

    public static float critical = 10;
    public static float criticalDmg = 150;
    public static int levelUpPoint = 100;

    public static float characterHP = 100;

}
