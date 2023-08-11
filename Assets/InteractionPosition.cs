using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class InteractionPosition : MonoBehaviour
{
    public int vertexCount = 36; 
    public float radius = 2.0f; 

    public LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = vertexCount;
        DrawCircle();
    }

    private void DrawCircle()
    {
        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        for (int i = 0; i < vertexCount; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, 0f, z);

            lineRenderer.SetPosition(i, pos);

            theta += deltaTheta;
        }
    }
}
