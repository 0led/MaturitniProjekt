using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2;
    public float jumpForce = 5;
    public Rigidbody2D _rigidbody;
    private float lastJumpTime = 0.0f;
    private float jumpCooldown = 0.5f;
    public Transform FirePoint1;
    public Transform FirePoint2;
    
    bool facingRightP1 = true;
    bool facingRightP2 = false;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

     void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (gameObject.tag == "Player1")
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
        
         if(facingRightP1)
        {
            gameObject.transform.localScale = new Vector3(0.3833f,0.3833f,0.3833f);
        }

        if(!facingRightP1)
        {
            gameObject.transform.localScale = new Vector3(-0.3833f,0.3833f,0.3833f);
            // Otočit firepoint doleva
            FirePoint1.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else {
            // Otočit firepoint doprava (standardní orientace)
            FirePoint1.transform.eulerAngles = Vector3.zero;
        }
        

        }

         if (gameObject.tag == "Player2")
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
       
        if(facingRightP2)
        {
            gameObject.transform.localScale = new Vector3(-0.3833f,0.3833f,0.3833f);
        }

        if(!facingRightP2)
        {
            gameObject.transform.localScale = new Vector3(0.3833f,0.3833f,0.3833f);
            // Otočit firepoint doleva
            FirePoint2.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else {
            // Otočit firepoint doprava (standardní orientace)
            FirePoint2.transform.eulerAngles = Vector3.zero;
        }
    }
    }
    }