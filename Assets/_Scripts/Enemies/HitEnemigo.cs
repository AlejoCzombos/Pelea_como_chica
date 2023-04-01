using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo : MonoBehaviour
{
    public GameObject target;

    public bool puedeAtacar = true;

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player") && puedeAtacar)
        {
            target.GetComponent<PlayerAttack>().TakeDamage(1);

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
