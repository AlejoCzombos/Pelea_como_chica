using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ScenesManager scenesManager;
    [SerializeField] private Image BigImage;
    [SerializeField] private Image smallImage;

    private float timeSinceLastClick;
    private float timeSinceClicks = 0.3f;
    private int currentSelectedCharacter;

    private void Start()
    {
        animator.SetInteger("SelCharacter", 1);
    }

    private void Update()
    {
        timeSinceLastClick += Time.deltaTime;
        if (timeSinceLastClick < timeSinceClicks)
        {
            return;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            timeSinceLastClick = 0.0f;
            currentSelectedCharacter = currentSelectedCharacter + 1;
        }else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            timeSinceLastClick = 0.0f;
            currentSelectedCharacter = currentSelectedCharacter - 1;
        }
        currentSelectedCharacter = Mathf.Clamp(currentSelectedCharacter, 0, 2);
        animator.SetInteger("SelCharacter", currentSelectedCharacter);
        if (Input.GetAxisRaw("Submit") != 0)
        {
            if (currentSelectedCharacter == 1)
            {
                scenesManager.ChangeSceneToGame();
            }
        }
        /*if (animator.GetCurrentAnimatorStateInfo(0).IsName("CharacterSelector_Juana") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            smallImage.color = new Color(1, 1, 1, 0);
            BigImage.color = new Color(1, 1, 1, 1);
            Debug.Log("if");
        }
        if(animator.GetInteger("SelCharacter") != 1) { 
            Debug.Log("else");
            smallImage.color = new Color(1, 1, 1, 1);
        }*/
    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
