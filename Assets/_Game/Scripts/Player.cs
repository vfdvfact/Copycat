using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float speed = 200f;
    [SerializeField] float jumpForce = 450f;
    bool isGrounded=true;
    bool isJumping=false;
    bool isAttack=false;
    float horizontal;
    string currentAnimName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded=CheckGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");
        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
            if (Mathf.Abs(horizontal) > 0.2f)
            {
                ChangeAnim("run");
            }
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
            }

            
        }
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }

        if (Mathf.Abs(horizontal) > 0.2f)
        {
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if(isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }
    bool CheckGrounded()
    {
        RaycastHit2D hit=Physics2D.Raycast(transform.position,Vector2.down,1.1f, groundLayer);
        return hit.collider != null;
    }
    void Attack()
    {
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
    void Throw()
    {
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
    void ResetAttack()
    {
        ChangeAnim("idle");
        isAttack = false;
    }
    void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }
    void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
