using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollableImage : MonoBehaviour
{
    //[SerializeField] private float paralax = 2f;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Vector2 speedMovement;
    private Material material;
    private Vector2 offset;
    private Transform camera;

    private Rect rect;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        camera = Camera.main.transform;
        offset = material.mainTextureOffset;
    }

    private void Update()
    {
        offset = (rigidbody2D.velocity.x * 0.1f) * speedMovement * Time.deltaTime;
        material.mainTextureOffset= offset;
        /*offset.x = camera.position.x / transform.localScale.x / paralax;
        offset.y = camera.position.y / transform.localScale.y / (paralax * 4f);

        material.mainTextureOffset = offset;*/
    }
}
