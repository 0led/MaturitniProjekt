using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public Transform FirePoint1;
    public Transform FirePoint2; 
    public Transform WeaponHolder1;
    public Transform WeaponHolder2;
    public Text ammoText1;
    public Text ammoText2;
    public float moveSpeed = 12f;
    public float jumpForce = 17f;
    private float lastJumpTime = 0f;
    private float jumpCooldown = 0.5f;
    private bool facingRightP1 = true;
    private bool facingRightP2 = false;
    public bool isPowerUpActive = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (!GameStarter.GameHasStarted)
        return;

        if (gameObject.tag == "Player1")
        {
            HandlePlayer1Movement();
            AdjustPlayerScaleAndRotation(facingRightP1, WeaponHolder1, FirePoint1);
        }

        if (gameObject.tag == "Player2")
        {
            HandlePlayer2Movement();
            AdjustPlayerScaleAndRotation(facingRightP2, WeaponHolder2, FirePoint2);
        }
    }

    public void UpdateAmmoText(int ammo, int playerIdentifier)
    {
        if (playerIdentifier == 1 && ammoText1 != null)
        {
            ammoText1.text = ammo.ToString();
        }
        else if (playerIdentifier == 2 && ammoText2 != null)
        {
            ammoText2.text = ammo.ToString();
        }
    }

    void AdjustPlayerScaleAndRotation(bool facingRight, Transform weaponHolder, Transform firePoint)
    {
        if (!facingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            weaponHolder.localScale = new Vector3(-1, 1, 1);
            weaponHolder.eulerAngles = new Vector3(0, 180, 0);
            firePoint.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            weaponHolder.localScale = new Vector3(1, 1, 1);
            weaponHolder.eulerAngles = Vector3.zero;
            firePoint.eulerAngles = Vector3.zero;
        }
    }

    void HandlePlayer1Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.smoothDeltaTime);
            facingRightP1 = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.smoothDeltaTime);
            facingRightP1 = true;
        }
        if (Input.GetKeyDown(KeyCode.W) && Time.time - lastJumpTime > jumpCooldown)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            lastJumpTime = Time.time;
        }
    }

    void HandlePlayer2Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.smoothDeltaTime);
            facingRightP2 = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.smoothDeltaTime);
            facingRightP2 = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Time.time - lastJumpTime > jumpCooldown)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            lastJumpTime = Time.time;
        }
    }
}