using ControlPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private HUDManager hudManager;

    private DialogTrigger dialogTrigger;
    private AudioSource voice;
    private bool isTrigger = false;
    private bool inDialogue = false;

    private void Awake()
    {
        dialogTrigger = gameObject.GetComponent<DialogTrigger>();
        voice = gameObject.GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        if (isTrigger)
        {
            if (Input.GetButtonDown("Attack"))
            {
                if (!inDialogue)
                {
                    inDialogue = true;
                    playerController.Active = false;
                    hudManager.canPause = false;
                    dialogManager.StartDialog(dialogTrigger.Dialog, voice);
                }
                else if (!dialogManager.isFinish && !dialogManager.isTyping)
                {
                    StartCoroutine(dialogManager.DisplayNextSentence());

                    if (dialogManager.isFinish)
                    {
                        dialogManager.isFinish = false;
                        hudManager.canPause = true;
                        playerController.Active = true;
                        inDialogue = false;
                    }
                }
            }
        }
        else
        {
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTrigger = true;
        animator.SetBool("isOpen", true);
        playerAttack.CanAttack = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
        animator.SetBool("isOpen", false);
        playerAttack.CanAttack = true;
    }
}
