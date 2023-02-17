using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class SkierRotator : MonoBehaviour
{
    public GameObject slope;
    public float dt_epsilon = 0.1f;
    public int numSplinePoints;

    private Spline s;
    private Transform t;

    private void Start()
    {
        SpriteShapeController splineComp = slope.GetComponent<SpriteShapeController>();
        t = GetComponent<Transform>();
        s = splineComp.spline;
        numSplinePoints = s.GetPointCount();
    }

    private void FixedUpdate()
    {
        int firstIndex = getLeftPoint();
        Vector2 firstPosition = s.GetPosition(firstIndex);
        Vector2 secondPosition = s.GetPosition(firstIndex + 1);

        Vector2[] handlePositions = {
            firstPosition,
            firstPosition + (Vector2)s.GetRightTangent(firstIndex),
            secondPosition + (Vector2)s.GetLeftTangent(firstIndex + 1),
            secondPosition
        };
        float tValue = (t.position.x - handlePositions[0].x) /
                       (handlePositions[3].x - handlePositions[0].x);
        //tValue = Mathf.Clamp(tValue, 0.1f, 0.9f);

        float tangentSlope = slopeFromHandles(handlePositions, tValue);
        t.rotation = Quaternion.Euler(0, 0, 5 + Mathf.Atan(tangentSlope) * (180 / Mathf.PI));
    }

    private int getLeftPoint()
    {
        float resDistance = 999f;
        int resIndex = 0;

        for (int i=0; i<numSplinePoints; i++)
        {
            float dist = t.position.x - s.GetPosition(i).x;
            if (dist > 0 && dist < resDistance)
            {
                resDistance = dist;
                resIndex = i;
            }
        }
        return resIndex;
    }

    private float slopeFromHandles(Vector2[] positions, float t)
    {
        float dx = parameterize(positions[0].x, positions[1].x, positions[2].x, positions[3].x, t);
        float dy = parameterize(positions[0].y, positions[1].y, positions[2].y, positions[3].y, t);
        return dy / dx;
    }

    private float parameterize(float A, float B, float C, float D, float t)
    {
        return 3 * ((D - (3 * C) + (3 * B) - A) * Mathf.Pow(t, 2) +
               ((2 * C) - (4 * B) + (2 * A)) * t + B - A);
    }
}
