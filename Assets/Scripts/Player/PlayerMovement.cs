using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera viewCamera;
    private Ray ray;
    private Plane groundPlane;
    private float rayDistance;
    internal Vector3 pointToLookAt;
    private void Awake()
    {
        groundPlane = new Plane(Vector3.up, Vector3.zero);
        viewCamera = Camera.main;
        print("todo::change the way of getting camera");
    }
    void Update()
    {
        ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            pointToLookAt = ray.GetPoint(rayDistance);
            transform.LookAt(pointToLookAt);
        }
    }

}
