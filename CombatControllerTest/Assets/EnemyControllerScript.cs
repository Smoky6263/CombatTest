using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{  
    [SerializeField] private float currentHealth;
    [SerializeField] private EnemySpawnerScript enemySpawnerScript;

    private float maxHealth;
    private float timeToRespawn;
    private float maxTmeToRespawn;

    private bool isDead = false;

    private void Awake()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;

        maxTmeToRespawn = 5f;
        timeToRespawn = maxTmeToRespawn;
    }

    private void Update()
    {
        if (isDead)
        {
            RespawnMe();
        }
    }

    private void RespawnMe()
    {
        timeToRespawn -= Time.deltaTime;

        if(timeToRespawn < 0)
        {
            enemySpawnerScript.SpawnEnemy();
            Destroy(gameObject);
        }

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0f)
        {
            CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
            capsuleCollider.enabled = false;
            isDead = true;
        }
    }
}
