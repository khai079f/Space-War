using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ShipCode
{
    NoShip = 0,
    IroSpirit_Stealth = 1,
    F_22_Raptor = 2,
    C17A_Globemaster = 3,
    Poseidon = 4
}
public enum ShipName
{
    NoShip,
    IroSpirit_Stealth,
    F_22_Raptor,
    C17A_Globemaster,
    Poseidon
}

public class ShipCodeParser
{
    public static ShipCode FromString(string shipName)
    {
        try
        {
            return (ShipCode)System.Enum.Parse(typeof(ShipCode), shipName);
        }
        catch (ArgumentException e)
        {
            Debug.LogError(e.ToString());
            return ShipCode.NoShip;
        }
    }
}
