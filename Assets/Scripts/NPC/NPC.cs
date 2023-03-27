using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private DialogManager dialogManager;
    
    private DialogTrigger dialogTrigger;
    private bool isTrigger = false;
    private bool inDialogue = false;

    private void Awake()
    {
        dialogTrigger = gameObject.GetComponent<DialogTrigger>();
    }

    private void Update()
    {
        if (isTrigger)
        {
            if (Input.GetButtonDown("Submit"))
            {
                if (!inDialogue)
                {
                    inDialogue = true;
                    dialogManager.StartDialog(dialogTrigger.Dialog);
                }
                else if (!dialogManager.isFinish && !dialogManager.isTyping)
                {
                    StartCoroutine(dialogManager.DisplayNextSentence());

                    if (dialogManager.isFinish)
                    {
                        dialogManager.isFinish = false;
                        inDialogue = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTrigger = true;
        animator.SetBool("isOpen", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
        animator.SetBool("isOpen", false);
    }
}
