using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTile : MonoBehaviour
{
    public float TimeTilDestroy = 0.5f;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject, TimeTilDestroy);
        }
    }

}
