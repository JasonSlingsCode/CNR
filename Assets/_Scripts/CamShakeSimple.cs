﻿using UnityEngine;
using System.Collections;

public class CamShakeSimple : MonoBehaviour 
{
    

    float shakeAmt = 0;
    
    public Camera mainCamera;

    void Awake()
    {

    }
    
    void OnCollisionEnter2D(Collision2D coll) 
    {
        if (coll.gameObject.tag == "Enemy")
        {
            shakeAmt = coll.relativeVelocity.magnitude * .05f;
            InvokeRepeating("CameraShake", 0, .01f);
            Invoke("StopShaking", 0.1f);
        }
        
    }
    
    void CameraShake()
    {
        if(shakeAmt>0) 
        {
            float quakeAmt = Random.value*shakeAmt*2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.x+= quakeAmt; // can also add to x and/or z
            pp.y+= quakeAmt; // can also add to x and/or z
            mainCamera.transform.position = pp;
        }
    }
    
    void StopShaking()
    {
        CancelInvoke("CameraShake");
        // mainCamera.transform.position = originalCameraPosition;
    }
    
}