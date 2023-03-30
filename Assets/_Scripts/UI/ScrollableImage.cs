using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollableImage : MonoBehaviour
{

    public float[] speeds; // velocidades de movimiento de cada capa
    public Transform target; // objetivo de seguimiento, normalmente la cámara

    private Vector2[] offsets; // offset actual de cada capa
    private Renderer[] renderers; // renderers de cada capa
    private float[] lengths; // longitud de cada capa

    private void Start()
    {
        offsets = new Vector2[speeds.Length];
        renderers = new Renderer[speeds.Length];
        lengths = new float[speeds.Length];

        for (int i = 0; i < speeds.Length; i++)
        {
            renderers[i] = transform.GetChild(i).GetComponent<Renderer>();
            offsets[i] = renderers[i].material.GetTextureOffset("_MainTex");
            lengths[i] = renderers[i].bounds.size.x;
        }
    }

    private void Update()
    {
        for (int i = 0; i < speeds.Length; i++)
        {
            float distance = target.position.x * speeds[i];
            float newPosition = (distance % lengths[i]) + offsets[i].x;
            Vector2 newOffset = new Vector2(newPosition, offsets[i].y);
            renderers[i].material.SetTextureOffset("_MainTex", newOffset);
        }
    }






    /*[SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Vector2 speedMovement;
    private Material material;
    private Vector2 offset;
    private Transform camera;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        camera = Camera.main.transform;
        offset = material.mainTextureOffset;
    }

    private void Update()
    {
        offset = (rigidbody2D.velocity.x * 0.1f) * speedMovement * Time.deltaTime;
        material.mainTextureOffset += offset;
    }*/
}
