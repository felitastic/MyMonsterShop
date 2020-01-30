using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSelfDestruct : MonoBehaviour
{
    public bool isTimed;
    public float time;
    void Update()
    {
        if (!isTimed)
            Destroy(this.gameObject, GetComponentInChildren<ParticleSystem>().main.duration);        
        else
            Destroy(this.gameObject, time);

    }
}
