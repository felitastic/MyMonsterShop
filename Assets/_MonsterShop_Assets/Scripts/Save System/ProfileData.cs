using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProfileData 
{
    public int PlayerID;

    public ProfileData (Profile profile)
    {
        PlayerID = profile.PlayerID;
    }
}
