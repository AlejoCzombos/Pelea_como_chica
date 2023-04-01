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
        private float[] yOffsetFactors; // factores de velocidad para cada capa en función de la distancia vertical relativa al centro de la pantalla
        private void Start()
        {
            offsets = new Vector2[speeds.Length];
            renderers = new Renderer[speeds.Length];
            lengths = new float[speeds.Length];
            yOffsetFactors = new float[speeds.Length];

            for (int i = 0; i < speeds.Length; i++)
            {
                renderers[i] = transform.GetChild(i).GetComponent<Renderer>();
                offsets[i] = renderers[i].material.GetTextureOffset("_MainTex");
                lengths[i] = renderers[i].bounds.size.x;

                // Calcular el factor de velocidad para cada capa en función de la distancia vertical relativa al centro de la pantalla
                float yDistance = Mathf.Abs(target.position.y - Camera.main.transform.position.y);
                yOffsetFactors[i] = Mathf.Clamp01(yDistance / Camera.main.orthographicSize) * speeds[i];
            }
        }

        private void Update()
        {
            for (int i = 0; i < speeds.Length; i++)
            {
                float distance = target.position.x * speeds[i];
                float newPosition = (distance % lengths[i]) + offsets[i].x;
                // Calcular la posición vertical en función del factor de velocidad para cada capa
                float yOffset = target.position.y * yOffsetFactors[i];
                Vector2 newOffset = new Vector2(newPosition, offsets[i].y + yOffset);
                renderers[i].material.SetTextureOffset("_MainTex", newOffset);
            }
        }
    }
