using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private RectTransform cloudTransform;
    [SerializeField] private float velocity;
    private void Awake()
    {
        cloudTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        cloudTransform.position -= new Vector3(velocity * Time.deltaTime,0,0);
    }
}
