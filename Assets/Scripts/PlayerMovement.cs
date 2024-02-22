﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12;
    public float jumpForce = 15;
    public Rigidbody2D _rigidbody;
    private float lastJumpTime = 0.0f;
    private float jumpCooldown = 0.5f;
    public Transform FirePoint1;
    public Transform FirePoint2; 
    private bool facingRightP1 = true;
    private bool facingRightP2 = false;
    public Transform WeaponHolder1;
    public Transform WeaponHolder2;
    
    public bool GetFacingRightP1()
    {
        return facingRightP1;
    }

    public bool GetFacingRightP2()
    {
        return facingRightP2;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameObject.tag == "Player1")
        {
            HandlePlayer1Movement();
            AdjustPlayer1ScaleAndRotation();
        }

        if (gameObject.tag == "Player2")
        {
            HandlePlayer2Movement();
            AdjustPlayer2ScaleAndRotation();
        }
    }

    void HandlePlayer1Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.smoothDeltaTime);
            facingRightP1 = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.smoothDeltaTime);
            facingRightP1 = true;
        }
        if (Input.GetKeyDown(KeyCode.W) && Time.time - lastJumpTime > jumpCooldown)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            lastJumpTime = Time.time;
        }
    }

    void AdjustPlayer1ScaleAndRotation()
    {
        if (!facingRightP1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            FirePoint1.eulerAngles = new Vector3(0, 180, 0);
            WeaponHolder1.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            FirePoint1.eulerAngles = Vector3.zero;
            WeaponHolder1.eulerAngles = Vector3.zero;
        }

    FirePoint1.localScale = Vector3.one;
    WeaponHolder1.localScale = Vector3.one;
    }

    void HandlePlayer2Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * speed * Time.smoothDeltaTime);
            facingRightP2 = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * speed * Time.smoothDeltaTime);
            facingRightP2 = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Time.time - lastJumpTime > jumpCooldown)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            lastJumpTime = Time.time;
        }
    }

    void AdjustPlayer2ScaleAndRotation()
    {
        if (!facingRightP2)
        {
            transform.localScale = new Vector3(1, 1, 1);
            WeaponHolder2.localScale = new Vector3(-1, 1, 1);
            FirePoint2.eulerAngles = new Vector3(0, 180, 0);
            WeaponHolder2.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            WeaponHolder2.localScale = new Vector3(-1, 1, 1);
            FirePoint2.eulerAngles = Vector3.zero;
            WeaponHolder2.eulerAngles = Vector3.zero;
        }
    
    FirePoint2.localScale = Vector3.one;
    WeaponHolder2.localScale = Vector3.one;
}
}