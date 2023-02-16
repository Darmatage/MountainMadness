using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class SkierRotator : MonoBehaviour
{
    public GameObject slope;
    public int numSplinePoints;
    public float tValue_display;

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

        Vector2[] handlePositions = {
            s.GetPosition(firstIndex),
            s.GetRightTangent(firstIndex),
            s.GetLeftTangent(firstIndex + 1),
            s.GetPosition(firstIndex + 1)
        };
        float tValue = (t.position.x - handlePositions[0].x) /
                       (handlePositions[3].x - handlePositions[0].x);

        int tangentSlope = slopeFromHandles(handlePositions, tValue);
        t.rotation = Quaternion.Euler(0, 0, math.atan(tangentSlope) * (180 / math.PI));
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

    private int slopeFromHandles(Vector2[] positions, float t)
    {
        // TODO: derive dy/dx from these terms ==>
        Vector2 term1 = math.pow(1 - t, 3) * (Vector2)(positions[0]);
        Vector2 term2 = 3 * t * math.pow(1 - t, 2) * (Vector2)(positions[1]);
        Vector2 term3 = 3 * math.pow(t, 2) * (1 - t) * (Vector2)(positions[2]);
        Vector2 term4 = math.pow(t, 3) * (Vector2)(positions[3]);

        return 0;
    }
}
