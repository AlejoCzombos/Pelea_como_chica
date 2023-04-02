using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditosFInal : MonoBehaviour
{
    public float speed = 10.0f; // Velocidad de movimiento del texto
    public RectTransform rectTransform; // RectTransform del objeto de texto

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Obtener RectTransform del objeto de texto
    }

    void Update()
    {
        // Mover el objeto de texto hacia arriba a la velocidad especificada
        rectTransform.anchoredPosition += Vector2.up * speed * Time.deltaTime;
    }
}
