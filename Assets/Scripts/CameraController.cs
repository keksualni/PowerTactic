﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float CameraSpeed = 20f;
    public float ScreenPadding = 10f;
    public float ScrollSpeed = 15f;
    public float CameraMaxYPos = 50f;
    public float CameraMinYPos = 5f;

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y > Screen.height - ScreenPadding)
        {
            pos.z += CameraSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.mousePosition.y < ScreenPadding)
        {
            pos.z -= CameraSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.mousePosition.x < ScreenPadding)
        {
            pos.x -= CameraSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d")  || Input.mousePosition.x > Screen.width - ScreenPadding)
        {
            pos.x += CameraSpeed * Time.deltaTime;
        }

        pos.y -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        pos.y = Mathf.Clamp(pos.y, CameraMinYPos, CameraMaxYPos);

        transform.position = pos;
    }
}
