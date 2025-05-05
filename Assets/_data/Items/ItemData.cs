using System;
using System.Collections.Generic;
using UnityEngine;
public enum ItemCode
{
    NoItem = 0,
    Iron = 1,
    Rock = 2,
    Titan = 3,
    FireBullet = 4,
    ManaCore_1 = 5,
    ManaCore_2 = 6
}

public enum ItemName
{
    NoItem,
    Iron,
    Rock,
    Titan,
    FireBullet,
    ManaCore_1,
    ManaCore_2
}
public enum ItemType
{
    NoType = 0,
    Resoucre = 1,
    Equiment = 2
} 

public enum ObjectType
{
    NoType = 0,
    Junk = 1,
    Enemy = 2,
    Boss = 3,
    Ship = 4
}
public class ItemCodeParser
{
    public static ItemCode FromString(string itemName)
    {
        try
        {
            return (ItemCode)System.Enum.Parse(typeof(ItemCode), itemName);
        }
        catch(ArgumentException e)
        {
            Debug.LogError(e.ToString());
            return ItemCode.NoItem;
        }
    }
}
