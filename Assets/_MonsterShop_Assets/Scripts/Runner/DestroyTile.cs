﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTile : MonoBehaviour
{
    public float TimeTilDestroy = 0.25f;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("tile " + this.gameObject.name + " will be destroyed in " + TimeTilDestroy + "s");
            Destroy(this.gameObject, TimeTilDestroy);
        }
    }
}
