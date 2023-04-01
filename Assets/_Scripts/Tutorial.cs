using ControlPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField] private GameObject imagenTutorial;
    [SerializeField] private Animator animator;
    [SerializeField] private DialogTrigger dialogTrigger;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private HUDManager hudManager;

    private bool seUso = false;
    private bool IsTrigguer = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!seUso)
        {
            seUso = true;
            animator.SetBool("isOpen", true);
            dialogManager.StartDialog(dialogTrigger.Dialog, null);
            playerController.Active = false;
            hudManager.canPause = false;
            playerAttack.CanAttack = false;
            StartCoroutine(mostrarImagenes());
        }
    }
    IEnumerator mostrarImagenes()
    {
        yield return new WaitForSeconds(1.0f);
        imagenTutorial.SetActive(true);
    }
    private void Update()
    {
        if (seUso)
        {
            if (Input.GetButtonDown("Attack"))
            {
                playerController.Active = true;
                hudManager.canPause = true;
                playerAttack.CanAttack = true;
                StartCoroutine(dialogManager.DisplayNextSentence());
                imagenTutorial.SetActive(false);
                dialogManager.isFinish = false;
                gameObject.GetComponent<BoxCollider2D>().isTrigger= false;
            }
        }
    }
}
