using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anima2 : MonoBehaviour
{

    public float x;
    public bool contador;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        contador = false;
        animator = gameObject.GetComponent<Animator>();

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


        if (Input.GetKeyDown(KeyCode.G) && !contador) {
            contador = true;
            animator.SetTrigger("Attack");
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 0);
            x = 0;

        }

        if (Input.GetKeyDown(KeyCode.G) && x > 0.2f && animator.GetInteger("combo") == 0)
        {
            contador = true;
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 1);
            x = 0;
        }

        if (Input.GetKeyDown(KeyCode.G) && x > 0.2f && animator.GetInteger("combo") == 1)
        {
            contador = true;
            animator.SetBool("noAttack", false);
            animator.SetInteger("combo", 2);
            x = 0;
        }

    }
}
