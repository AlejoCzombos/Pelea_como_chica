using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Dialog dialog;
    
    public void TrigguerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}
