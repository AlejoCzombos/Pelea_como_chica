using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private Queue<string> sentences;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Animator animator;
    [SerializeField] private float wordsPerSeconds;
 
    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        animator.SetBool("isOpen", true);
        nameText.text = dialog.Name;
        sentences.Clear();

        foreach (string sentence in dialog.Sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }
    public void EndDialog()
    {
        animator.SetBool("isOpen", false);
        Debug.Log("Fin de la conversacion");
    }

    public IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char caracter in sentence)
        {
            dialogText.text += caracter;
            yield return new WaitForSeconds( 1 / wordsPerSeconds);
        }
        yield return new WaitForSeconds(1.0f);
    }
}
