using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{

    [SerializeField] private Animator animator;
    public Enemigo2D enemigo;
    public GameObject target;

    private float tiempoEntreAtaques = 3.0f;
    private float tiempoDesdeUltimoAtaque = 0.0f;
    private bool puedeAtacar = true;

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player") && puedeAtacar) {
            tiempoDesdeUltimoAtaque = 0.0f;
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("attack", true);
            enemigo.atacando = true;
            GetComponent<BoxCollider2D>().enabled = false;
            target.GetComponent<PlayerAttack>().TakeDamage(1);
            puedeAtacar = false;
        }
    }

    void Update()
    {
        tiempoDesdeUltimoAtaque+= Time.deltaTime; 
        if (!puedeAtacar && tiempoDesdeUltimoAtaque >= tiempoEntreAtaques)
        {
            puedeAtacar=true;
        }
    }
}
