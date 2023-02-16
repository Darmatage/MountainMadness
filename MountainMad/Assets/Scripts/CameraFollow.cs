using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform follow_transform;
    public BoxCollider2D map_bounds;

    private float xMin, xMax, yMin, yMax;
    private float camY, camX;
    private float camOrthsize;
    private float cameraRatio;
    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        xMin = map_bounds.bounds.min.x;
        xMax = map_bounds.bounds.max.x;
        yMin = map_bounds.bounds.min.y;
        yMax = map_bounds.bounds.max.y;
        
        mainCam = GetComponent<Camera>();
        camOrthsize = mainCam.orthographicSize;
        cameraRatio = (xMax + camOrthsize) / 2.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camY = Mathf.Clamp(follow_transform.position.y, yMin + camOrthsize, yMax - camOrthsize);
        camX = Mathf.Clamp(follow_transform.position.x, xMin + camOrthsize, xMax - camOrthsize);
        transform.position = new Vector3(follow_transform.position.x, follow_transform.position.y, transform.position.z);
    }
}
