﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


public class maleMove : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    private Animation anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

		float x = CrossPlatformInputManager.GetAxis("Horizontal");
		float y = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0.0f, y);

        rb.velocity = movement * 4f;
        //if (Input.GetKey(KeyCode.Space))
        //{
        //          anim.Play("walk");
        //      }
        //else
        //{
        //          anim.Play("idle");
        if (x != 0 && y != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }

        if (x != 0 || y != 0)
        {
            anim.Play("walk");
        }
        else
        {
            anim.Play("idle");
        }
    }
    
}