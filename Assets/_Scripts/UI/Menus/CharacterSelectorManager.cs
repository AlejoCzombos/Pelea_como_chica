using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterSelectorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ScenesManager scenesManager;
    [SerializeField] private Image pascualaImage;
    [SerializeField] private Image pascualaNameImagen;
    [SerializeField] private Image macachaImage;
    [SerializeField] private Image machucaNameImagen;
    [SerializeField] private Image BigImage;
    [SerializeField] private Image smallImage;

    private float timeSinceLastClick;
    private float timeSinceClicks = 0.3f;
    private int currentSelectedCharacter;

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
            timeSinceLastClick = 0.0f;
            switch (currentSelectedCharacter)
            {
                case 0:
                    StartCoroutine(Shake(pascualaImage, pascualaNameImagen));
                    break;
                case 1:
                    scenesManager.ChangeSceneToGame();
                    break;
                case 2:
                    StartCoroutine(Shake(macachaImage, machucaNameImagen));
                    break;
                default:
                    break;
            }
        }
    }
    IEnumerator Shake(Image image, Image nameImage)
    {
        yield return image.rectTransform.transform.DOPunchRotation(new Vector3(0,0,5.0f), 0.1f);
        yield return nameImage.rectTransform.transform.DOPunchRotation(new Vector3(0,0,3.0f), 0.1f);
    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
