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

        int tangentSlope = slopeFromHandles(handlePositions, transform.position.x);
        t.rotation = Quaternion.Euler(0, 0, math.atan(tangentSlope) * (180 / math.PI));
    }

    private int getLeftPoint()
    {
        return 1;
    }

    private int slopeFromHandles(Vector2[] positions, float x)
    {
        return 1;
    }
}
