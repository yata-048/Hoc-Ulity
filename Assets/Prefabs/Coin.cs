using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player=other.GetComponent<Player>();
        if (player != null)
        {
            player.GetCoin();
            Destroy(gameObject);
        }
    }
}
