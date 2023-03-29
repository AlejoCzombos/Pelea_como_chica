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

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask playerLayer;


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

        if (Input.GetKeyDown(KeyCode.P)) {
            attack();
            Debug.Log("Atacando");
        }

        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250f * Time.deltaTime, 0));
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            animacionDanioIzq();
            currentHealth -= damage;
        }
        else {
            animacionDanioDer();
            currentHealth += damage;
        }   
        
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            ChangeStateEnemy(EnemyState.Death);
        }
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

    public void animacionDanioIzq() {
        animator.SetBool("GetDamage", true);
        contador = true;
        Debug.Log("Pushed");
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(horizontalPush * Time.deltaTime, verticalPush * Time.deltaTime, 0));

    }
    public void animacionDanioDer()
    {
        animator.SetBool("GetDamage", true);
        contador = true;
        Debug.Log("Pushed");
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-horizontalPush * Time.deltaTime, verticalPush * Time.deltaTime, 0));

    }

    void attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayer) {
            player.GetComponentInChildren<Transform>().gameObject.GetComponentInChildren<PlayerAttack>().TakeDamage(1);
            

        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }



}
