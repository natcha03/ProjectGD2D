using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 2.0f;
    [SerializeField] float jumpSpeed = 4.0f;
    [SerializeField] float climbSpeed = 2.0f;
    [SerializeField] Vector2 deathKick = new Vector2(0,2f);

    [SerializeField] GameObject rockAmmo;
    [SerializeField] Transform slingshot;

    Vector2 moveInput;
    Rigidbody2D rgbd2D;

    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    CircleCollider2D myCircleCollider;
    float gravityScaleAtStart;

    bool isAlive = true;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myCircleCollider = GetComponent<CircleCollider2D>();
        gravityScaleAtStart = rgbd2D.gravityScale;
    }

    void Update()
    {
        if(!isAlive) { return; };

        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if(!isAlive) { return; };
        Instantiate(rockAmmo, slingshot.position, transform.rotation);
        myAnimator.SetTrigger("Shooting");

    }

    void OnMove(InputValue value)
    {
        if(!isAlive) { return; };
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if(!isAlive) { return; };
        // if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if(!myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if(value.isPressed)
        {
            rgbd2D.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x*runSpeed , rgbd2D.velocity.y);
        rgbd2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rgbd2D.velocity.x) >  Mathf.Epsilon;
        if(playerHasHorizontalSpeed) 
        {
            myAnimator.SetBool("isRunning", true);

        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }

    void FlipSprite()
    {
        bool playHasHorizontalSpeed = Mathf.Abs(rgbd2D.velocity.x) > Mathf.Epsilon; 
        if(playHasHorizontalSpeed) 
        {
            transform.localScale = new Vector2 (Mathf.Sign(rgbd2D.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        // if not touching the ladder
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            
            rgbd2D.gravityScale = gravityScaleAtStart; 
            myAnimator.SetBool("isClimbing", false);
            return; 
        }

        Vector2 climbVelocity = new Vector2 (rgbd2D.velocity.x,moveInput.y*climbSpeed);
        rgbd2D.velocity = climbVelocity;
        rgbd2D.gravityScale = 0f;

        // check vertical Speed
        bool playHasVerticalSpeed = Mathf.Abs(rgbd2D.velocity.y) > Mathf.Epsilon; 

        myAnimator.SetBool("isClimbing", playHasVerticalSpeed);
    }

    void Die() 
    {
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazard")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            rgbd2D.velocity = deathKick;

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            StartCoroutine(informGameSession());
        }    
    }

    IEnumerator informGameSession()
    {
        // aka: come back to run the following like after the delay
        yield return new WaitForSecondsRealtime(1f); 
        // inform the GameSession
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
