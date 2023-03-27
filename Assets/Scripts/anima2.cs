using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anima2 : MonoBehaviour
{

    public float x;
    public bool contador;
    private Animator animator;
    public int damage;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        damage = 40;
        contador = false;
        animator = gameObject.GetComponent<Animator>();
        animator.SetInteger("combo", -1); 

    }

    // Update is called once per frame
    void Update()
    {

        if (contador) {
            x = x + 1 * Time.deltaTime;
        }

        if (x >= 2) {
            contador = false;
            animator.SetBool("noAttack", true);
            animator.SetInteger("combo", -1);
            x = 0;
        }


        if (Input.GetKeyDown(KeyCode.G) && animator.GetInteger("combo") == -1) {

            Attack();
            contador = true;
            animator.SetTrigger("Attack");
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 0);
            x = 0;

        }

        if (Input.GetKeyDown(KeyCode.G) && x > 0.15f && animator.GetInteger("combo") == 0)
        {
            Attack();
            contador = true;
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 1);
            x = 0;
        }

        if (Input.GetKeyDown(KeyCode.G) && x > 0.15f && animator.GetInteger("combo") == 1)
        {
            Attack();
            contador = true;
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 2);
            x = 0;
        }

        if (Input.GetKeyDown(KeyCode.G) && x > 0.15f && animator.GetInteger("combo") == 2)
        {
            Attack();
            contador = true;
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 0);
            x = 0;
        }

    }

    void Attack() {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies) {
            if (enemy.GetComponent<Enemy_mov>().currentHealth > 0) {
                enemy.GetComponent<Enemy_mov>().TakeDamage(damage);
            }
           
        }
    }

    void OnDrawGizmosSelected() {

        if (attackPoint == null) {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
