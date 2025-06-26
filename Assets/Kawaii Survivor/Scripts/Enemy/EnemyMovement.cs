using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header(" Elements ")] 
    private Player player;
    
    [Header(" Settings ")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;
    
    [Header(" Debug ")]
    [SerializeField] private bool gizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<Player>();

        if (player == null)
        {
            Debug.LogWarning("No player found");
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();

        TryAttack();
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadius)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!gizmos) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
