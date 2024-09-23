using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D characterRigidbody;
    public static Animator characterAnimator;
    private float horizontalInput;
    private bool jumpInput;
    [SerializeField]private float characterSpeed = 4.5f;
    [SerializeField]private float jumpForce = 10f;

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            characterAnimator.SetBool("IsRunning", true);
        }
        else if(horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            characterAnimator.SetBool("IsRunning", true);
        }
        else if(horizontalInput == 0)
        {
            characterAnimator.SetBool("IsRunning",false);
        }


        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded == true)
        {
            characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            characterAnimator.SetBool("IsJumping", true);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);
    }
}
