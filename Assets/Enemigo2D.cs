using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2D : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public int direccion;
    public float speedWalk;
    public float speedRun;
    public GameObject target;
    public GameObject Visual;
    public bool atacando;
    public bool puedeAtacar = true;
    public float contadorAtaque = 0;

    public float rangoVision;
    public float rangoAtaque;
    public GameObject rango;
    public GameObject Hit;
    private Rigidbody2D rb;

    public int horizontalPush;
    public int verticalPush;

    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (puedeAtacar) {
            Comportamientos();
        }

        if (!puedeAtacar) {
            contadorAtaque += 1 * Time.deltaTime;
        }


        if (contadorAtaque > 1) {
            gameObject.GetComponentInChildren<RangoEnemigo>().puedeAtacar = true;
            gameObject.GetComponentInChildren<HitEnemigo>().puedeAtacar = true;
            puedeAtacar = true;
            contadorAtaque = 0;
        }
        
        
        
    }

    public void Comportamientos() {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangoVision && !atacando)
        {
            ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
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
                    ani.SetBool("walk", true);
                    break;

            }
        }
        else {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangoAtaque && !atacando)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    ani.SetBool("walk", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.right * speedRun * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    ani.SetBool("attack", false);
                }
                else
                {
                    ani.SetBool("walk", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.right * speedRun * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    ani.SetBool("attack", false);
                }
            }
            else
            {
                if (!atacando) {
                    if (transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                }
                ani.SetBool("walk", false);
                ani.SetBool("run", false);
            }
        }       
    }

    public void FinalAni() {
        ani.SetBool("attack", false);
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

    public void TakeDamage(int damage)
    {
        
        puedeAtacar = false;
        gameObject.GetComponentInChildren<RangoEnemigo>().puedeAtacar = false;
        gameObject.GetComponentInChildren<HitEnemigo>().puedeAtacar = false;
        if (Visual.transform.lossyScale.x == -1)
        {
            FinalAni();
            rb.AddForce(new Vector3(-horizontalPush * Time.deltaTime, verticalPush * Time.deltaTime, 0));
            Debug.Log("volando <----");
        }
        else if (Visual.transform.lossyScale.x == 1)
        {
            FinalAni();
            rb.AddForce(new Vector3(+horizontalPush * Time.deltaTime, verticalPush * Time.deltaTime, 0));
            Debug.Log("volando ---->");
        }
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {

            Debug.Log("Me mori xD");
        }

    }

}
