using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Home : MonoBehaviour
{
    [Header("List of Positions to spawn the VFX")]
    public GameObject[] SpawnPosition = new GameObject[(int)Position.NumberofPositions];  
    public enum Position         //One enum for each Position, needs to be the same order
    {
        PetLeft,
        PetMiddle,
        PetRight,
        CageLeft,
        CageMiddle,
        CageRight,
        EggHatching,
        EggGlow,
        RarityBanner,

        //NumberofPositions needs to be the last in list!
        NumberofPositions
    }

    [Header("List of VFX prefabs")]
    public GameObject[] VFXEffect = new GameObject[(int)VFX.NumberofVFX];    
    public enum VFX             //One enum name for each prefab, needs to be the same order
    {
        Cage_falls,
        Confetti,
        Pet,
        EggShells,
        TapEgg,
        EggGlow,
        EpicBanner,
        LegendaryBanner,

        //NumberofVFX needs to be the last in list!
        NumberofVFX
    }
    void Start()
    {
        GameManager.Instance.vfx_home = this;

        if (VFXEffect.Length != (int)VFX.NumberofVFX)
        {
            Debug.LogError("Not enough VFX assigned");
        }
        if (SpawnPosition.Length != (int)Position.NumberofPositions)
        {
            Debug.LogError("Not enough Transforms assigned");
        }
    }

    /// <summary>
    /// Spawn VFX using the enum lists
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="position"></param>
    public void SpawnEffect(VFX effect, Position position)
    {
        GameObject newVFX = GameObject.Instantiate(this.VFXEffect[(int)effect], transform.position, transform.rotation) as GameObject;
        newVFX.name = "" + effect;
        newVFX.transform.position = SpawnPosition[(int)position].transform.position;
        newVFX.transform.SetParent(SpawnPosition[(int)position].transform);
        //print("Spawned VFX " + newVFX.name + " under " + SpawnPosition[(int)position].name);
    }

    /// <summary>
    /// Spawn VFX via int; pet 0-2; cage 3-5;  
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="position"></param>
    public void SpawnEffectViaInt(VFX effect, int position)
    {
        GameObject newVFX = GameObject.Instantiate(this.VFXEffect[(int)effect], transform.position, transform.rotation) as GameObject;
        newVFX.name = "" + effect;
        newVFX.transform.position = SpawnPosition[(int)position].transform.position;
        newVFX.transform.SetParent(SpawnPosition[(int)position].transform);
        //print("Spawned VFX " + newVFX.name + " under " + SpawnPosition[(int)position].name);
    }

    public void SpawnEffektAtPosition(VFX effect, Vector3 position)
    {
        GameObject newVFX = GameObject.Instantiate(this.VFXEffect[(int)effect], transform.position, transform.rotation) as GameObject;
        newVFX.name = "" + effect;
        newVFX.transform.position = position;
        //newVFX.transform.SetParent(SpawnPosition[(int)position].transform);
    }
}
