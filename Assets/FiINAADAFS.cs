using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiINAADAFS : MonoBehaviour
{
    [SerializeField] private ScenesManager scenesManager;

    private void OnTriggerEnter2D(Collision2D collision)
    {
        scenesManager.ChangeSceneToFinal();
        Debug.Log("final");
    }
}
