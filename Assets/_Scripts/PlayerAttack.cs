using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float x = 0;
    [SerializeField] private bool contador = false;

    [SerializeField] private Transform transformar;
    [SerializeField] private int damage;

    [SerializeField] private int maxHealth = 3;
    public int currentHealth;
    public float dirX = 0;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Animator animator;
    [SerializeField] private HUDManager hudManager;

    private bool canAttack = true;
    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    void Start()
    {
        currentHealth = maxHealth;
        transformar = gameObject.GetComponentInParent<Transform>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAttack) return;
        


        if (contador)
        {
            x = x + 1 * Time.deltaTime;
        }

        if (x >= 2)
        {
            contador = false;
            animator.SetBool("noAttack", true);
            x = 0;
        }


        if (Input.GetButtonDown("Attack"))
        {
            
            contador = true;
            animator.SetTrigger("Attack");
            animator.SetBool("noAttack", false);
            x = 0;

        }

        
    }

    public void TakeDamage(int damage)
    {
        if (dirX < gameObject.transform.parent.parent.GetComponent<Transform>().position.x)
        {
            gameObject.transform.parent.parent.GetComponent<ControlPlayer.PlayerController>().recibirGolpeDer();
        }
        else {
            gameObject.transform.parent.parent.GetComponent<ControlPlayer.PlayerController>().recibirGolpeIzq();
        }
        animator.SetTrigger("takeDamage");
        currentHealth -= damage;
        Debug.Log(currentHealth);
        //Bajar la vida en el HUD

        if (currentHealth <= -1) {
            hudManager.ActiveDeathScreen();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemigo"))
        {
            dirX = coll.transform.position.x;

        }
    }

    void Attack()
    {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);     
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemigo")){
                    enemy.GetComponent<Enemigo2D>().TakeDamage(damage);
                }
                
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

//(Input.GetButtonDown("Attack")
