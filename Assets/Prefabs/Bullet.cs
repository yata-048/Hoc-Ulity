using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbBullet;
    [SerializeField] float damage;
    [SerializeField] float lifeTime;
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyHealth enemy=hitInfo.GetComponent<EnemyHealth>();
        if(enemy!=null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
        if(hitInfo.gameObject.layer==LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }

}
