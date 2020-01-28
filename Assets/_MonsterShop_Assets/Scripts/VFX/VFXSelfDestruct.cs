using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSelfDestruct : MonoBehaviour
{
    public bool isTimed;
    void Update()
    {
        if (!isTimed)
            Destroy(this.gameObject, GetComponent<ParticleSystem>().main.duration);
        else
            Destroy(this.gameObject, 4.0f);
    }
}
