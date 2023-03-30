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

    //IA
    private int rutina;
    private float cronometro;
    private int direccion;
    [SerializeField] private float speedWalk;
    [SerializeField] private float speedRun;
    [SerializeField] private GameObject target;
    public bool atacando;
    [SerializeField] private float rangoVision;
    [SerializeField] private float rangoAtaque;
    [SerializeField] private GameObject rango;
    [SerializeField] private GameObject Hit;


    private Animator animator;
    private EnemyState enemyState = EnemyState.Idle;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private bool canMove = false;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        contador = false;
        canMove = true;
        currentHealth = maxHealth;
    }
    void Update()
    {
        Comportamientos();

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
           // ChangeStateEnemy(EnemyState.Death);
        }
    }

    public void Comportamientos() {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangoVision && !atacando)
        {
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    break;

                case 1:
                    direccion = Random.Range(0, 2);
                    rutina++;
                    break;

                case 2:

                    switch (direccion)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector3.right * speedWalk * Time.deltaTime);
                            break;

                        case 1:
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.right * speedWalk * Time.deltaTime);
                            break;


                    }
                    break;

            }
        }
        else {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangoAtaque && !atacando)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    transform.Translate(Vector3.right * speedRun * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    transform.Translate(Vector3.right * speedRun * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else {
                if (!atacando) {
                    if (transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    //animator.SetBool("walk", false);
                    //animator.SetBool("run", false);
                }
            }
        }
    }

    public void FinalAni() {
        //animator.SetBool("attack", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue() {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    /*
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

        */

    public void animacionDanioIzq() {
        animator.SetBool("GetDamage", true);
        contador = true;
        Debug.Log("Pushed");
        rb.AddForce(new Vector3(horizontalPush * Time.deltaTime, verticalPush * Time.deltaTime, 0));

    }
    public void animacionDanioDer()
    {
        animator.SetBool("GetDamage", true);
        contador = true;
        Debug.Log("Pushed");
        rb.AddForce(new Vector3(-horizontalPush * Time.deltaTime, verticalPush * Time.deltaTime, 0));

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
