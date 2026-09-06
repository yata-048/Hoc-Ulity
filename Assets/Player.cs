using System;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask ground;
    [SerializeField] float speed = 10f;
    [SerializeField] float JumpForce=5f;
    [SerializeField] float groundCheckDistance;
    float movement;
    InputAction MoveAction; 
    InputAction JumpAction;
    InputAction AttackAction;
    bool isJump;
    int CoinCount=0;
    void Awake()
    {
        MoveAction=InputSystem.actions.FindAction("Move");
        JumpAction=InputSystem.actions.FindAction("Jump");
        AttackAction=InputSystem.actions.FindAction("Attack");

    }
    void Update()
    {
        GroundCheck();

        movement=MoveAction.ReadValue<float>();
        
        rb.linearVelocity=new Vector2(movement*speed,rb.linearVelocityY);
        
        if(JumpAction.WasPressedThisFrame() && !isJump)
        {
            rb.linearVelocityY=JumpForce;
        }
        if(AttackAction.WasPressedThisFrame())
        {
            Debug.Log("ATTACK!!!!");
        }
        
    }
    void GroundCheck()
    {
        RaycastHit2D ray = Physics2D.Raycast(this.transform.position,Vector2.down,groundCheckDistance,ground);
        if(ray.collider == null ) { isJump = true; return;} 
        isJump = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position,Vector2.down*groundCheckDistance);
    }

    public void GetCoin()
    {
        CoinCount++;
        Debug.Log("Coin:" + CoinCount);
    }

}
