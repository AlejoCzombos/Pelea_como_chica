using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiINAADAFS : MonoBehaviour
{
    [SerializeField] private ScenesManager scenesManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        scenesManager.ChangeSceneToFinal();
    }
}
