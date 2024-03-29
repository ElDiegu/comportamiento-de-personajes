using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Camera Parameters")]
    [SerializeField] float moveSpeed;
    [SerializeField] float zoomSpeed;
    [SerializeField] float minZoom;
    [SerializeField] float maxZoom;
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Move();
        Zoom();
    }
    void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * zInput + transform.right * xInput;

        transform.position += dir * moveSpeed * Time.deltaTime;
    }
    void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float dist = Vector3.Distance(transform.position, cam.transform.position);

        if (dist < minZoom && scrollInput > 0.0f) return;
        if (dist > maxZoom && scrollInput < 0.0f) return;

        cam.transform.position += cam.transform.forward * scrollInput * zoomSpeed;
    }
    public void FocusOnPosition(Vector3 pos)
    {
        cam.transform.position = pos;
    }
}
