using System;
using NUnit.Framework;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.Rendering;
using JetBrains.Annotations;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask wall;
    [SerializeField] float speed = 10f;
    [SerializeField] float JumpForce=5f;
    [SerializeField] float groundCheckDistance;
    [SerializeField] float wallCheckDistance;
    [SerializeField] float fallingForce;
    [SerializeField] float defaultForce;
    [SerializeField] float wallJumpTime;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashTime;
    [SerializeField] public GameObject triangle;
    [SerializeField] UnityEvent shootEvent;
    [SerializeField] int maxAmmo;
    [SerializeField] float KnockbackForceX;
    [SerializeField] float KnockbackForceY;
    [SerializeField] float knockbackTime;
    [SerializeField] float shootInterval;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    [SerializeField] float reloadTime;
    bool isReloading;
    float knockbackTimer;
    float wallJumpTimer;
    float dashCooldownTimer;
    float shootIntervalTimer;
    float dashTimer;
    float wallDirection;
    float movement;
    float movementDirection = 1f;

    InputAction MoveAction; 
    InputAction JumpAction;
    InputAction AttackAction;
    InputAction DashAction;
    InputAction ReloadAction;
    bool isJump;
    bool isWall;
    int CoinCount=0;
    int AmmoAmount;

    void Awake()
    {
        MoveAction=InputSystem.actions.FindAction("Move");
        JumpAction=InputSystem.actions.FindAction("Jump");
        AttackAction=InputSystem.actions.FindAction("Attack");
        DashAction=InputSystem.actions.FindAction("Dash");
        ReloadAction=InputSystem.actions.FindAction("Reload");
        AmmoAmount=maxAmmo;
    }
    void Update()
    {
        GroundCheck();
        WallCheck();
        wallJumpTimer-=Time.deltaTime;
        dashCooldownTimer-=Time.deltaTime;
        dashTimer-=Time.deltaTime;
        knockbackTimer-=Time.deltaTime;
        shootIntervalTimer-=Time.deltaTime;
        movement=MoveAction.ReadValue<float>();
        if(movement !=0)
        {
            movementDirection=movement;
        }
        if(wallJumpTimer<=0 && dashTimer<=0  && knockbackTimer <= 0)
        {
            rb.linearVelocity=new Vector2(movement*speed,rb.linearVelocityY);
        }
        if(DashAction.WasPressedThisFrame() && dashCooldownTimer <= 0)
        {
            rb.linearVelocity=new Vector2(movementDirection*dashSpeed,rb.linearVelocityY);
            rb.gravityScale = 0f;
            dashTimer=dashTime;
            dashCooldownTimer = dashCooldown;
            Debug.Log("DASH");
        }
        if(JumpAction.WasPressedThisFrame())
        {
            if(!isJump)
            {
                rb.linearVelocityY = JumpForce;
            }
            else if(isWall)
            {
                rb.linearVelocity = new Vector2(wallDirection*speed,JumpForce);
                wallJumpTimer=wallJumpTime;
            }
        }
        if (dashTimer <= 0)
        {
            rb.gravityScale = defaultForce;
        }      
        if (rb.linearVelocityY < 0)
        {
            rb.gravityScale=fallingForce;
        }
        else
        {
            rb.gravityScale=defaultForce;
        }
        if(AttackAction.WasPressedThisFrame() && AmmoAmount>0 && shootIntervalTimer<0)
        {
            shootEvent.Invoke();
            shootIntervalTimer=shootInterval;
        }
        rb.linearVelocityY = Mathf.Clamp(rb.linearVelocityY,-20,50);

        if(ReloadAction.WasPressedThisFrame() && AmmoAmount<maxAmmo && !isReloading)
        {
            StartCoroutine(Reloading());
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
        Gizmos.DrawRay(this.transform.position,Vector2.right*wallCheckDistance);
        Gizmos.DrawRay(this.transform.position,Vector2.left*wallCheckDistance);
    }

    public void GetCoin()
    {
        CoinCount++;
        Debug.Log("Coin:" + CoinCount);
    }
    void WallCheck()
    {
        RaycastHit2D rightWall = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wall);
        RaycastHit2D leftWall = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wall);

        if (rightWall.collider != null)
        {
            isWall = true;
            wallDirection = -1;
        }
        else if (leftWall.collider != null)
        {
            isWall = true;
            wallDirection = 1;
        }
        else
        {
            isWall = false;
            wallDirection = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isWall = false;
        }
    }
    public void ReduceAmmo()
    {
        AmmoAmount--;
        Debug.Log("Ammo:" + AmmoAmount);
    }
    public void DoKnockBack()
    {
        rb.linearVelocity = new Vector2(KnockbackForceX*-movementDirection,KnockbackForceY);
        knockbackTimer = knockbackTime;
    }

    public void Shoot()
    {
        GameObject bullet =Instantiate(bulletPrefab,transform.position,Quaternion.identity);
        Rigidbody2D rbBullet=bullet.GetComponent<Rigidbody2D>();
        rbBullet.linearVelocity = new Vector2(movementDirection*bulletSpeed,0);
    }

    IEnumerator Reloading()
    {
        isReloading=true;
        Debug.Log("Reloading.");
        yield return new WaitForSeconds(reloadTime/3);
        Debug.Log("Reloading..");
        yield return new WaitForSeconds(reloadTime/3);
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime/3);
        AmmoAmount=maxAmmo;
        isReloading=false;
        Debug.Log("Reloaded");
    }
}
