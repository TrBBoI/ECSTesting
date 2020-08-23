using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float speed = 10;

    private Vector3 lastPos;

    void Update()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dir += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            dir += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir += Vector3.right;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            dir += Vector3.up;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            dir += Vector3.down;
        }

        Vector3 rot = Vector3.zero;
        if (Input.GetMouseButtonDown(1))
        {
            lastPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 currPos = Input.mousePosition;

            Vector3 dirr = currPos - lastPos;
            rot.x = dirr.y * 10;
            rot.y = dirr.x * 10;

            lastPos = currPos;
        }

        transform.Rotate(rot * Time.deltaTime);
        // transform.up = Vector3.up;

        Vector3 vel = dir * speed * Time.deltaTime;
        transform.Translate(vel);
    }
}
