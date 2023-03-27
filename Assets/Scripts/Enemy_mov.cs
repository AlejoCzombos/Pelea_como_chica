using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_mov : MonoBehaviour
{

    [SerializeField] private GameObject player;

    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {

        //gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250f * Time.deltaTime, 0));


    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            die();
        }
    }

    public void die() {
        Debug.Log("Me mori");
        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

}
