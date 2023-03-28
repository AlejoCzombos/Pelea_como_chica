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
    [SerializeField] private Animator animator;
    [SerializeField] private float x = 0;
    [SerializeField] private bool contador = false;


    [SerializeField] private GameObject player;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private float horizontalPush;
    [SerializeField] private float verticalPush;


    private EnemyState enemyState = EnemyState.Idle;
    private BoxCollider2D boxCollider;

    private bool canMove = false;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        contador = false;
        animator = gameObject.GetComponent<Animator>();
        canMove = true;
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (contador)
        {
            x = x + 1 * Time.deltaTime;
        }

        if (x >= 1.5f)
        {
            contador = false;
            animator.SetBool("GetDamage", false);
            x = 0;
        }

        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250f * Time.deltaTime, 0));
    }

    public void TakeDamage(int damage)
    {
        animacionDanio();
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

    public void animacionDanio() {
        animator.SetBool("GetDamage", true);
        contador = true;
        Debug.Log("Pushed");
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(horizontalPush * Time.deltaTime, verticalPush * Time.deltaTime, 0));

    }

}
