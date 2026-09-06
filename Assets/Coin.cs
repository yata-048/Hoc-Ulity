using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player=other.GetComponent<Player>();
        player.GetCoin();
        Destroy(gameObject);

    }
}
