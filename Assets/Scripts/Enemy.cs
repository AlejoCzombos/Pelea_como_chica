using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    ChasePlayer,
    AttackPlayer,
    Death
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;


    private EnemyState enemyState = EnemyState.Idle;
    private BoxCollider2D boxCollider;

    private bool canMove = false;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        canMove = true;
        currentHealth = maxHealth;
    }
    void Update()
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250f * Time.deltaTime, 0));
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            ChangeStateEnemy(EnemyState.Death);
        }
    }

    public void die()
    {
        Debug.Log("Me mori");
        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    public void ChangeStateEnemy(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                enemyState = EnemyState.Idle;
                canMove = false;
                boxCollider.enabled = true;
                Debug.Log("Estoy Quieto");
                break;
            case EnemyState.ChasePlayer:
                enemyState = EnemyState.ChasePlayer;
                canMove = true;
                boxCollider.enabled = true;
                Debug.Log("Estoy Persiguiendo");
                break;
            case EnemyState.AttackPlayer:
                enemyState = EnemyState.AttackPlayer;
                canMove = false;
                boxCollider.enabled = true;
                Debug.Log("Estoy atacando");
                break;
            case EnemyState.Death:
                enemyState = EnemyState.Death;
                canMove = false;
                boxCollider.enabled = false;
                Debug.Log("Estoy Muerto");
                break;
            default:
                Debug.LogError("Error de Estado");
                break;
        }
    }

}
