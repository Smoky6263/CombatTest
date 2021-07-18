using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    public void SpawnEnemy()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-15, 15), 0f, Random.Range(-15, 15));

        Instantiate(enemy, randomPosition, Quaternion.Euler(0f, 0f, 0f));
    }
}
