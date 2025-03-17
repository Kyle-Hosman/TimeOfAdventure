using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    public SwordAttack swordAttack;

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    bool canMove = true;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.chatPanel.activeInHierarchy){
            animator.SetBool("isMoving", false); // Disable movement if chat panel is active     
            return;
        } 

        if (!canMove) return;
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success) {
                success = TryMove(new Vector2(movementInput.x, 0));
                if (!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", success);
        } else {
            animator.SetBool("isMoving", false);
        }
        
        // Update the sprite direction based on the movement input
        if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero) {
            
        // Cast a ray in the direction of movement
         int count = rb.Cast(
                direction, 
                movementFilter, 
                castCollisions, 
                moveSpeed * Time.fixedDeltaTime + collisionOffset);
            // Move the player based on the input
            if (count == 0) {
                // No collisions detected, move the player
                rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
                return true;
            } else {
                return false;}
            
        } else {
            // Dont move if no direction to move to
            return false;
        }
    }

    void OnMove(InputValue moveValue)
    {
        // Get the movement input from the player
        movementInput = moveValue.Get<Vector2>();
    }

    void OnAttack () {
        if (GameManager.Instance.chatPanel.activeInHierarchy) return; // Disable attack if chat panel is active
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack() {
        LockMovement();
        if(spriteRenderer.flipX ==true) {
            swordAttack.AttackLeft();
        } else {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack() {
        swordAttack.StopAttack();
        UnlockMovement();
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }
    
}
