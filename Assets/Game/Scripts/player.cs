using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _speed=4f;

    [SerializeField]
    private float gravity = 9.8f;


    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 diraction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = diraction * _speed;

        velocity.y -= gravity;

        //convert local to global
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime );
    }
}
