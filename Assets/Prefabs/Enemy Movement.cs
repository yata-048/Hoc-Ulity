using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbEnemy;
    [SerializeField] Collider2D colEnemy;
    [SerializeField] float speed = 2f;
    [SerializeField] float checkDistance = 0.2f;
    [SerializeField] LayerMask obstacleLayer; 
    float direction = 1f;
    void Awake()
    {
        if (rbEnemy == null) rbEnemy = GetComponent<Rigidbody2D>();
        if (colEnemy == null) colEnemy = GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {

        Vector2 rayOrigin = (Vector2)transform.position + new Vector2(direction * (colEnemy.bounds.extents.x + 0.05f), 0);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, checkDistance, obstacleLayer);
        Debug.DrawRay(rayOrigin, Vector2.right * direction * checkDistance, Color.red);
        if (hit.collider != null && hit.collider != colEnemy)
        {
            direction *= -1f;
        }
        rbEnemy.linearVelocity = new Vector2(speed * direction, rbEnemy.linearVelocityY);
    }
}