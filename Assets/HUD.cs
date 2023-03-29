using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class HUD : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> imagenes;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        heart();

    }

    void heart() {
        if (player.GetComponent<PlayerAttack>().currentHealth == 2)
        {
            animator.SetInteger("Hitted", 1);
            imagenes[2].gameObject.SetActive(false);

        }
        else if (player.GetComponent<PlayerAttack>().currentHealth == 1)
        {
            animator.SetInteger("Hitted", 2);
            imagenes[1].gameObject.SetActive(false);
        }
        else if (player.GetComponent<PlayerAttack>().currentHealth == 0)
        {
            animator.SetInteger("Hitted", 3);
            imagenes[0].gameObject.SetActive(false);
        }
        else if (animator.GetInteger("Hitted") == 0) {
            imagenes[0].gameObject.SetActive(true);
            imagenes[1].gameObject.SetActive(true);
            imagenes[2].gameObject.SetActive(true);

        }

    }

}
