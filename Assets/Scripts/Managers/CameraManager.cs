﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // singleton boilerplate
    private static CameraManager _instance;
    public static CameraManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject CameraObject;
    public float mSpeed;
    public float mDelta;
    private Vector3 mRightDirection = Vector3.right;
    private Vector3 mUpDirection = Vector3.up;
    private Vector3 desiredPos;


    void Start()
    {
        desiredPos = CameraObject.transform.position;
        Debug.Log("Screen.width=" + Screen.width);
    }

    void Update()
    {
        Vector3 targetPos = desiredPos;
        Debug.Log("Input.mousePosition.x=" + Input.mousePosition.x);
        if (Input.mousePosition.x >= Screen.width - mDelta)
        {
            // Move the camera
            targetPos += mRightDirection * Time.deltaTime * mSpeed;
        }
        if (Input.mousePosition.x <= 0 + mDelta)
        {
            // Move the camera
            targetPos -= mRightDirection * Time.deltaTime * mSpeed;
        }
        if (Input.mousePosition.y >= Screen.height - mDelta)
        {
            // Move the camera
            targetPos += mUpDirection * Time.deltaTime * mSpeed;
        }
        if (Input.mousePosition.y <= 0 + mDelta)
        {
            // Move the camera
            targetPos -= mUpDirection * Time.deltaTime * mSpeed;
        }

        CameraObject.transform.position = targetPos;
    }
}
