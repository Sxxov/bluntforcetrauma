using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveViaCharacterController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    
    // Update is called once per frame
    public void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        this.controller.Move(move * speed * Time.deltaTime);
    }
}
