using System.Collections;
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
    //public Transform WeaponHolder1;
    //public Transform WeaponHolder2;
    //public Transform Player1Sprite;
    //public Transform Player2Sprite;
    
    private bool facingRightP1 = true;
    private bool facingRightP2 = false;
    
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
        Vector3 scale;
        Vector3 rotation;

    if (facingRightP1)
    {
        scale = new Vector3(0.3833f, 0.3833f, 0.3833f);
        rotation = Vector3.zero;
    }
    else
    {
        scale = new Vector3(-0.3833f, 0.3833f, 0.3833f);
        rotation = new Vector3(0, 180, 0);
    }

    gameObject.transform.localScale = scale;
    FirePoint1.transform.eulerAngles = rotation;
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
    
        Vector3 scale;
        Vector3 rotation;

    if (facingRightP2)
    {
        scale = new Vector3(-0.3833f, 0.3833f, 0.3833f); // Záměrně záporná hodnota pro X, protože Player2 je v zrcadlovém obrazu k Player1
        rotation = Vector3.zero;
    }
    else
    {
        scale = new Vector3(0.3833f, 0.3833f, 0.3833f);
        rotation = new Vector3(0, 180, 0);
    }

    gameObject.transform.localScale = scale;
    FirePoint2.transform.eulerAngles = rotation;
    }
}


/*using System.Collections;
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
    //public Transform WeaponHolder1;
    //public Transform WeaponHolder2;
    //public Transform Player1Sprite;
    //public Transform Player2Sprite;
    
    private bool facingRightP1 = true;
    private bool facingRightP2 = false;
    
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
            //Player1Sprite.transform.eulerAngles = new Vector3(0, 180, 0);
            //WeaponHolder1.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else {
            // Otočit firepoint doprava (standardní orientace)
            FirePoint1.transform.eulerAngles = Vector3.zero;
            //Player1Sprite.transform.eulerAngles = Vector3.zero;
           // WeaponHolder1.transform.eulerAngles = Vector3.zero;
        }
       /*
        if (gameObject.CompareTag("Player1"))
        {
            // Otočení WeaponHolder1 podle orientace hráče
            WeaponHolder1.transform.localEulerAngles = facingRightP1 ? Vector3.zero : new Vector3(0, 180, 0);
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
            //Player2Sprite.transform.eulerAngles = new Vector3(0, 180, 0);
           // WeaponHolder2.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else {
            // Otočit firepoint doprava (standardní orientace)
            FirePoint2.transform.eulerAngles = Vector3.zero;
            //Player2Sprite.transform.eulerAngles = Vector3.zero;
           // WeaponHolder2.transform.eulerAngles = Vector3.zero;
       
        }
/*
        if (gameObject.CompareTag("Player2"))
        {
            // Otočení WeaponHolder2 podle orientace hráče
            WeaponHolder2.transform.localEulerAngles = facingRightP2 ? Vector3.zero : new Vector3(0, 180, 0);
        }
*/