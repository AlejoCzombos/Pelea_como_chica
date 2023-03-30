using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackgeound : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSize;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition= cameraTransform.position;
        Sprite sprite= GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSize = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition= cameraTransform.position;

        /*if (cameraTransform.position.x - transform.position.x >= textureUnitSize)
        {
            float offtenPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSize;
            transform.position = new Vector3(cameraTransform.position.x + offtenPositionX, transform.position.x);
        }*/
    }
}
