using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollExplosion : MonoBehaviour
{
    public Rigidbody[] ragdollRigidbodies;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}