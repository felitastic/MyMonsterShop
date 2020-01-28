using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Runner : MonoBehaviour
{

    [Header("List of Positions to spawn the VFX")]
    public GameObject[] SpawnPosition = new GameObject[3];
    public enum Position         //One enum for each Position, needs to be the same order
    {
        ResultLevelUp,
        ResultGrowthUp,        

        //NumberofPositions needs to be the last in list!
        NumberofPositions
    }

    [Header("List of VFX prefabs")]
    public GameObject[] VFXEffect = new GameObject[4];
    public enum VFX             //One enum name for each prefab, needs to be the same order
    {
        Orb_Glow,
        Orb_Pickup,
        Runner_Death,
        Runner_Run,
        Confetti,

        //NumberofVFX needs to be the last in list!
        NumberofVFX
    }
    void Start()
    {
        if (VFXEffect.Length != (int)VFX.NumberofVFX)
        {
            Debug.LogError("Not enough VFX assigned");
        }
        if (SpawnPosition.Length != (int)Position.NumberofPositions)
        {
            Debug.LogError("Not enough Transforms assigned");
        }
    }

    public void SpawnEffect(VFX effect, Position position)
    {
        GameObject newVFX = GameObject.Instantiate(this.VFXEffect[(int)effect], transform.position, transform.rotation) as GameObject;
        newVFX.name = "" + effect;
        //newVFX.transform.position = this.SpawnPosition[(int)position].transform.position;
    }

    public void SpawnEffectViaInt(VFX effect, int position)
    {
        GameObject newVFX = GameObject.Instantiate(this.VFXEffect[(int)effect], transform.position, transform.rotation) as GameObject;
        newVFX.name = "" + effect;
        newVFX.transform.position = this.SpawnPosition[position].transform.position;
    }
}
