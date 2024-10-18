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
    [SerializeField]private int _currentHealth;
    [SerializeField]private int _maxHealth = 5;

    private bool isAttacking;

    [SerializeField] private Transform attackHitBox;
    [SerializeField] private float attackRadius = 1;
    private AudioSource _audioSource;

    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce);
        _currentHealth = _maxHealth;

        GameManager.instance.SetHealthBar(_maxHealth);
    }

    void Update()
    {

        Movement();
        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded && !isAttacking)
        {
            Jump();
        }
            
        if(Input.GetButtonDown("Attack") && GroundSensor.isGrounded && !isAttacking)
        {
            //Attack();
            StartAttack();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.Pause();
        }
    }
       
    // Update is called once per frame
    void FixedUpdate()
    {
        /*if(isAttacking)
        {
            characterRigidbody.velocity = new Vector2(0, characterRigidbody.velocity.y);
        }
        else
        {
            characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);
        }*/
       
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);
    }
    
    void Movement()
    {
        if(isAttacking && horizontalInput == 0)
        {
            horizontalInput = 0;
        }
    else
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
        if(horizontalInput < 0)
    {
        if(!isAttacking)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        characterAnimator.SetBool("IsRunning", true);
    }
    else if(horizontalInput > 0)
    {
        if(!isAttacking)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        characterAnimator.SetBool("IsRunning", true);
    }
    else
    {
        characterAnimator.SetBool("IsRunning", false);
    }
    
}
    void Jump()
    {
        characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        characterAnimator.SetBool("IsJumping", true);
        SoundManager.instance.PlaySFX(_audioSource, SoundManager.instance._jumpAudio);
    }
    
    
    /*void Attack()
    {

        if(horizontalInput == 0)
        {
            characterAnimator.SetTrigger("Attack");
        }

        else
        {
            characterAnimator.SetTrigger("RunAttack");
        }

        StartCoroutine(AttackAnimation());
        
        
    }

    IEnumerator AttackAnimation()
    {
        isAttacking = true;

        yield return new WaitForSeconds(0.1f);
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag("Mimico"))
            {
                //Destroy(enemy.gameObject);
                Rigidbody2D enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidBody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);
                Enemy script = enemy.GetComponent<Enemy>();
                script.TakeDamage();
            }
        }

        yield return new WaitForSeconds(0.4f);

        isAttacking = false;
    }*/

    void StartAttack()
    {
        isAttacking = true;
        characterAnimator.SetTrigger("Attack");
    }

    void Attack()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);
        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag("Mimico"))
            {
                //Destroy(enemy.gameObject);
                Rigidbody2D enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidBody.AddForce(transform.right + transform.up * 2, ForceMode2D.Impulse);
                Enemy script = enemy.GetComponent<Enemy>();
                script.TakeDamage();
                SoundManager.instance.PlaySFX(_audioSource, SoundManager.instance._attackAudio);
            }
        }
    }

    void EndAttack()
    {
        isAttacking = false;
    }

    void TakeDamage()
    {
        _currentHealth--;

        GameManager.instance.UpdateHealthBar(_currentHealth);

        if(_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            characterAnimator.SetTrigger("IsHurt");
        }
    }

    void Die()
    {
        characterAnimator.SetTrigger("IsDead");
        Destroy(gameObject, 1f);
        SoundManager.instance.PlaySFX(_audioSource, SoundManager.instance._deathAudio);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            //characterAnimator.SetTrigger("IsHurt");
            //Destroy(gameObject, 0.4f);
            TakeDamage();
        } 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitBox.position, attackRadius);
    }
}
