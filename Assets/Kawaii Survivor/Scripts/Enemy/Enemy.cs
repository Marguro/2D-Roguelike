using UnityEngine;

    [RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [Header(" Components ")]
    private EnemyMovement movement;
    
    [Header(" Elements ")] 
    private Player player;
    
    [Header(" Spawning ")]
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned;
    
    [Header(" Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency;
    [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;
    
    [Header(" Effects ")]
    [SerializeField] private ParticleSystem passAwayEffect;
    
    [Header(" Debug ")]
    [SerializeField] private bool gizmos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        
        player = FindAnyObjectByType<Player>();

        if (player == null)
        {
            Debug.LogWarning("No player found");
            Destroy(gameObject);
        }
        
        StartSpawnSequence();

        attackDelay = 1f / attackFrequency;
    }

    private void StartSpawnSequence()
    {
        SetRendererVisible(false);
        
        //Scale up & down the spawn indicator
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceComplete);
    }
    
    private void SpawnSequenceComplete()
    {
        SetRendererVisible(true);
        hasSpawned = true;

        movement.StorePlayer(player);
    }
    
    private void SetRendererVisible(bool isVisible)
    {
        spawnIndicator.enabled = !isVisible;
        enemyRenderer.enabled = isVisible;
    }
    void Update()
    {
        if (attackTimer >= attackDelay)
            TryAttack();
        else
            Wait();
    }
    
    private void Attack()
    {
        attackTimer = 0f;
        
        player.TakeDamage(damage);
    }
    
    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }
    
    private void PassAway()
    {
        passAwayEffect.transform.SetParent(null);
        passAwayEffect.Play();
        
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (!gizmos) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius)
            Attack();    
    }
}
