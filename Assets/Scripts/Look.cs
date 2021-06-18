using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float mouseSensitivity = 500f;
    public Transform player;

    float rotationY = 0f;

    // Start is called before the first frame update
    public void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * this.mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * this.mouseSensitivity;

        this.rotationY -= mouseY;
        this.rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(this.rotationY, 0, 0);
        player.Rotate(Vector3.up * mouseX);
    }
}
