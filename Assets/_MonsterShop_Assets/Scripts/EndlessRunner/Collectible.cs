using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Controls controls;
    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controls.UpdateCollectedCount(value);
            Destroy(this.gameObject, 0.25f);
        }
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.collider.CompareTag("Player"))
    //    {
    //        controls.UpdateCollectedCount(value);
    //        Destroy(this, 0.25f);
    //    }
    //}

}
