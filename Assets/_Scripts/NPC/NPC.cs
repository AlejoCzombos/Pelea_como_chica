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
    [SerializeField] private Enemigo2D enemigo;

    private DialogTrigger dialogTrigger;
    private AudioSource voice;
    private bool isTrigger = false;
    private bool inDialogue = false;
    private bool seHbalo = false;

    private void Awake()
    {
        dialogTrigger = gameObject.GetComponent<DialogTrigger>();
        voice = gameObject.GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        if (isTrigger && dialogTrigger.Dialog.Sentences.Length > 0 && !seHbalo)
        {
            if (Input.GetButtonDown("Attack"))
            {
                if (!inDialogue)
                {
                    seHbalo= true;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!seHbalo)
        {
            animator.SetBool("isOpen", true);
            playerAttack.CanAttack = false;
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            isTrigger = false;
            animator.SetBool("isOpen", false);
            playerAttack.CanAttack = true;
        
    }
}
