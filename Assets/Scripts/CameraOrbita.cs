using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbita : MonoBehaviour
{
    private Vector2 angle = new Vector2(180 * Mathf.Deg2Rad, 0);
    private new Camera camera;
    private Vector2 nearPlaneSize;

   public Transform follow;
   public float maxDistance;
   public Vector2 sensitivity;


    void Start()
    {
        camera = GetComponent<Camera>();

        CalculateNearPlaneSize();
    }

    private void CalculateNearPlaneSize()
    {
        float height = Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad / 2) * camera.nearClipPlane;
        float width = height * camera.aspect;

        nearPlaneSize = new Vector2(width, height);
    }

    private Vector3[] GetCameraCollisionPoints(Vector3 direciton)
    {
        Vector3 position = follow.position;
        Vector3 center = position + direciton * (camera.nearClipPlane + 0.2f);

        Vector3 right = transform.right * nearPlaneSize.x;
        Vector3 up = transform.up * nearPlaneSize.y;

        return new Vector3[]
        {
            center - right + up,
            center - right + up,
            center - right - up,
            center + right - up
        };
    }




    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");

        if (horizontal != 0)
        {
            angle.x += horizontal * Mathf.Deg2Rad * sensitivity.x;
        }

        float vertical = Input.GetAxis("Mouse Y");

        if (vertical != 0)
        {
            angle.y += vertical * Mathf.Deg2Rad * sensitivity.y;
            angle.y = Mathf.Clamp(angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
        }

    }

    void LateUpdate()
    {
        Vector3 direction = new Vector3(Mathf.Cos(angle.x) * Mathf.Cos(angle.y), -Mathf.Sin(angle.y), -Mathf.Sin(angle.x) * Mathf.Cos(angle.y));


        RaycastHit hit;
        float distance = maxDistance;

        Vector3[] points = GetCameraCollisionPoints(direction);

        foreach (Vector3 point in points)
        {

            if (Physics.Raycast(point, direction, out hit, maxDistance))
            {
                distance = Mathf.Min((hit.point - follow.position).magnitude, distance);
            }
        }

        transform.position = follow.position + direction * distance;
        transform.rotation = Quaternion.LookRotation(follow.position - transform.position);
    }

}