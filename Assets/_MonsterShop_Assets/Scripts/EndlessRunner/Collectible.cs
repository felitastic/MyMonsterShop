using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            Controls.gold += 1;
            Destroy(this, 0.25f);
        }
    }
}
