using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    float curHealth;

    void Awake()
    {
        curHealth=maxHealth;
    }
    public void TakeDamage(float amount)
    {
        curHealth-=amount;
        Debug.Log("Hit! -"+amount + " Health!");
        if (curHealth<=0)
        {
            Debug.Log("DEAD!");
            Destroy(gameObject);
        }
    }
}
