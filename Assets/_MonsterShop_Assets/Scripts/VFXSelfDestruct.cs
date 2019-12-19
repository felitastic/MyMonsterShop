using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSelfDestruct : MonoBehaviour
{
    void Update()
    {
        Destroy(this.gameObject, GetComponent<ParticleSystem>().main.duration);
    }
}
