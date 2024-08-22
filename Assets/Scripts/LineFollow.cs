using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollow : MonoBehaviour
{
    public Transform Ball; // Reference to the ball's transform
    private LineRenderer lineRenderer;
    private const int maxPoints = 800; // Maximum number of points to keep in the trail
    private Vector3 lastPosition;

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0; // Initialize with zero points
        lastPosition = Ball.position; // Initialize last position
    }

    void Update()
    {
        // Check if the ball has moved
        if (Vector3.Distance(Ball.position, lastPosition) > Mathf.Epsilon)
        {
            // Update last position
            lastPosition = Ball.position;

            // Increase the number of points
            int currentPointCount = lineRenderer.positionCount;
            if (currentPointCount >= maxPoints)
            {
                // Shift points to remove the oldest one
                Vector3[] positions = new Vector3[maxPoints];
                lineRenderer.GetPositions(positions);
                System.Array.Copy(positions, 1, positions, 0, maxPoints - 1);
                positions[maxPoints - 1] = Ball.position;

                // Set the new positions
                lineRenderer.positionCount = maxPoints;
                lineRenderer.SetPositions(positions);
            }
            else
            {
                // Add new point
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(currentPointCount, Ball.position);
            }
        }
    }
}
