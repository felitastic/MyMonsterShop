using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTile : MonoBehaviour
{
    public float TimeTilDestroy = 0.25f;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("destroying tile in " + TimeTilDestroy);
            Destroy(this.gameObject, TimeTilDestroy);
        }
    }
}
