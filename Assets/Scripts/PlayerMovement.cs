using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float depthInput = Input.GetAxis("Vertical");
        float jumpInput = Input.GetAxis("Jump")*2.2f;

        //update the position
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, jumpInput * movementSpeed * Time.deltaTime, depthInput * movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
   
}
