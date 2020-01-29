using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageVFX : MonoBehaviour
{
    public void SpawnCageVFX()
    {
        int pos = 0;
        switch (GameManager.Instance.curMonsterID)
        {
            case 0:
                pos = 3;
                break;
            case 1:
                pos = 4;
                break;
            case 2:
                pos = 5;
                break;
            default:
                pos = 4;
                break;
        }
        GameManager.Instance.vfx_home.SpawnEffectViaInt(VFX_Home.VFX.Cage_falls, pos);
    }
}
