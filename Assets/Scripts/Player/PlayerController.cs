using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public float GroundDistance;
    [SerializeField] private float speed, jumpForce;
    [SerializeField] private LayerMask ground;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    private PlayerActions playerActions;
    private Rigidbody2D player;
    private Collider2D collider;
    private Animator animator;
    private bool jump = false;
    bool Grounded;

    private void Awake() 
    {
        playerActions = new PlayerActions();
        player = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() 
    {
        playerActions.Enable();
    }

    private void OnDisable() 
    {
        playerActions.Disable();
    }

    private void Start() 
    {
        playerActions.Land.Jump.performed += _ => Jump();
        playerActions.Land.Shoot.performed += _ => Shoot();
    }

    

    private void Update() 
    {
        // Read the movment value from player actions 
        float moveInput = playerActions.Land.Move.ReadValue<float>();
        // Moving the player 
        animator.SetFloat("Speed",Mathf.Abs(moveInput));
        if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0); // Flipped
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0); // Normal
        }
        Vector3 currentPosition = transform.position;
        currentPosition.x += moveInput * speed * Time.deltaTime;
        transform.position = currentPosition;
        animator.SetBool("isGround", Grounded);

    }

    private void Jump()
    {
        if (Grounded)
        {
            animator.SetTrigger("Jump");
            animator.SetBool("isGround",false);
            player.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(bullet,5f);
    }

void OnCollisionStay2D(Collision2D collider)
{
    CheckIfGrounded ();
}

void OnCollisionExit2D(Collision2D collider)
{
    Grounded = false;
}

private void CheckIfGrounded()
{
    RaycastHit2D[] hits;

    //We raycast down 1 pixel from this position to check for a collider
    Vector2 positionToCheck = transform.position;
    hits = Physics2D.RaycastAll (positionToCheck, new Vector2 (0, -1), 0.01f);

    //if a collider was hit, we are grounded
    if (hits.Length > 0) {
        Grounded = true;
    }
}
}
