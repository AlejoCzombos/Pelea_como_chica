using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private Queue<string> sentences;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogText;
    [SerializeField] private Animator animator;
    [SerializeField] private float wordsPerSeconds;

    public bool isFinish = false;
    public bool isTyping = false;

    private AudioSource voice;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog, AudioSource voiceAudio)
    { 
        voice = voiceAudio;
        animator.SetBool("isOpen", true);
        nameText.text = dialog.Name;
        sentences.Clear();

        foreach (string sentence in dialog.Sentences)
        {
            sentences.Enqueue(sentence);
        }
        StartCoroutine(DisplayNextSentence());
    }

    public IEnumerator DisplayNextSentence()
    {
        if (sentences.Count == 0) {
            isFinish = true;
            EndDialog();
            yield break;
        }
        string sentence = sentences.Dequeue();
        yield return TypeSentence(sentence);
    }
    public void EndDialog()
    {
        animator.SetBool("isOpen", false);
    }

    public IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char caracter in sentence)
        {
            dialogText.text += caracter;
            voice.Play();
            yield return new WaitForSeconds( 1 / wordsPerSeconds);
        }
        yield return new WaitForSeconds(1.0f);
        isTyping = false;
    }
}
