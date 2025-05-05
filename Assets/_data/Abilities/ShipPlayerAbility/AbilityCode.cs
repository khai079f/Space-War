using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AbilityCode
{
    NoAbility = 0,
    Laze = 1,

}
public class AbilityCodeParser
{
    public static AbilityCode FromString(string itemName)
    {
        try
        {
            return (AbilityCode)System.Enum.Parse(typeof(AbilityCode), itemName);
        }
        catch (ArgumentException e)
        {
            Debug.LogError(e.ToString());
            return AbilityCode.NoAbility;
        }
    }
}