using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{

    [SerializeField] private Animator animator;
    public Enemigo2D enemigo;
    
    public bool puedeAtacar = true;

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player") && puedeAtacar) {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
            animator.SetBool("attack", true);
            enemigo.atacando = true;
            GetComponent<BoxCollider2D>().enabled = false;            
        }
    }

    
}
