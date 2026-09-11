using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    void Start()
    {
            Instantiate(enemyPrefab,transform.position,quaternion.identity);
    }

}
