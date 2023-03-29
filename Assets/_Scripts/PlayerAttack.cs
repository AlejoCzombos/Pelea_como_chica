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

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Animator animator;

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
        animator.SetInteger("combo", -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (transformar.lossyScale == new Vector3(-1, 1, 1))
        {
            damage = -20;
        }

        if (transformar.lossyScale == new Vector3(1, 1, 1))
        {
            damage = 20;
        }

        if (!canAttack) return;

        if (contador)
        {
            x = x + 1 * Time.deltaTime;
        }

        if (x >= 2)
        {
            contador = false;
            animator.SetBool("noAttack", true);
            animator.SetInteger("combo", -1);
            x = 0;
        }


        if (Input.GetButtonDown("Attack") && animator.GetInteger("combo") == -1)
        {
            Attack();
            contador = true;
            animator.SetTrigger("Attack");
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 0);
            x = 0;

        }

        if (Input.GetButtonDown("Attack") && x > 0.15f && animator.GetInteger("combo") == 0)
        {
            Attack();
            contador = true;
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 1);
            x = 0;
        }

        if (Input.GetButtonDown("Attack") && x > 0.15f && animator.GetInteger("combo") == 1)
        {
            Attack();
            contador = true;
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 2);
            x = 0;
        }

        if (Input.GetButtonDown("Attack") && x > 0.15f && animator.GetInteger("combo") == 2)
        {
            Attack();
            contador = true;
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 0);
            x = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        gameObject.transform.parent.parent.GetComponent<ControlPlayer.PlayerController>().recibirGolpe();
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (currentHealth <= 0) {
            Debug.Log("Me mori xD");
        }

    }

    void Attack()
    {
       
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);     
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
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
