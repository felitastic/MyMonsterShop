﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFrame : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
