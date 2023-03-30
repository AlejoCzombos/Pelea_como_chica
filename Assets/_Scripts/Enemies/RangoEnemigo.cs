using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Enemy enemigo;

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            //animator.SetBool("walk", false);
            //animator.SetBool("run", false);
            //animator.SetBool("attack", true);
            enemigo.atacando = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
