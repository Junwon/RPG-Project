﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    void Start()
    {
        GetComponent<NavMeshAgent>().destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
