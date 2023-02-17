using UnityEngine;
using UnityEngine.U2D;

public class SkierRotator : MonoBehaviour
{
    public GameObject slope; // sprite shape path to target
    private int numSplinePoints;

    private Spline s;
    private Transform t;

    private void Start()
    {
        s = slope.GetComponent<SpriteShapeController>().spline;
        t = GetComponent<Transform>();
        numSplinePoints = s.GetPointCount();
    }

    private void FixedUpdate()
    {
        // find the nearest bezier point to the LEFT of the skier
        int firstIndex = GetLeftPoint();
        Vector2 firstPosition = s.GetPosition(firstIndex);
        Vector2 secondPosition = s.GetPosition(firstIndex + 1);

        // create a list of bezier handle positions
        // => these are equivalent to A, B, C, and D, in newton's cubic bezier formula
        Vector2[] handlePositions = {
            firstPosition,
            firstPosition + (Vector2)s.GetRightTangent(firstIndex),
            secondPosition + (Vector2)s.GetLeftTangent(firstIndex + 1),
            secondPosition
        };

        // calculate t (to sample the active bezier curve)
        float tValue = (t.position.x - handlePositions[0].x) /
                       (handlePositions[3].x - handlePositions[0].x);

        // calculate the slope by sampling the curve, then convert to a rotation
        float tangentSlope = SlopeFromHandles(handlePositions, tValue);
        t.rotation = Quaternion.Euler(0, 0, 5 + Mathf.Atan(tangentSlope) * (180 / Mathf.PI));
    }

    private int GetLeftPoint()
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

    private float SlopeFromHandles(Vector2[] positions, float t)
    {
        float dx = Parameterize(positions[0].x, positions[1].x, positions[2].x, positions[3].x, t);
        float dy = Parameterize(positions[0].y, positions[1].y, positions[2].y, positions[3].y, t);
        return dy / dx;
    }

    private float Parameterize(float A, float B, float C, float D, float t)
    {
        // https://en.wikipedia.org/wiki/Bezier_curve#Derivative
        // => optimized with a little algebra
        return 3 * ((D - (3 * C) + (3 * B) - A) * Mathf.Pow(t, 2) +
               ((2 * C) - (4 * B) + (2 * A)) * t + B - A);
    }
}
