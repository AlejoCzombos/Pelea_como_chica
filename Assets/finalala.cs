using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalala : MonoBehaviour
{
    [SerializeField] private ScenesManager scenesManager;

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.CompareTag("Player")) {
            scenesManager.ChangeSceneToFinal();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
