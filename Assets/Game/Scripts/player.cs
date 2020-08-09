﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _speed = 4f;

    [SerializeField]
    private float gravity = 9.8f;

    [SerializeField]
    private GameObject _muzzelFlash;

    [SerializeField]
    private GameObject _hitMaker;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HideCursor();
        Movement();
        if (Input.GetMouseButton(0))
        {
            _muzzelFlash.SetActive(true);

            //screen point ->(0,screen.width)
            //Ray originRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            //view point -> (0,1)
            Ray originRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;

            if (Physics.Raycast(originRay,out hit, Mathf.Infinity))
            {
                print(hit.transform.name);
                var tmp = Instantiate(_hitMaker, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(tmp, 0.8f);
            }
        }
        else
        {
            _muzzelFlash.SetActive(false);
        }
    }

    private static void HideCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Movement()
    {
        Vector3 diraction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = diraction * _speed;

        velocity.y -= gravity;

        //convert local to global
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }
}
