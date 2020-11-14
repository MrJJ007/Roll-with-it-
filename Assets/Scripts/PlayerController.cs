using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

   
        CharacterController characterController;
        public float MovementSpeed = 1;
        public float Gravity = 9.8f;
        private float velocity = 0;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            // player movement - forward, backward, left, right
            float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
            float vertical = Input.GetAxis("Vertical") * MovementSpeed;
            characterController.Move((Vector3.right * horizontal + Vector3.forward * vertical) * Time.deltaTime);

            // Gravity
            if (characterController.isGrounded)
            {
                velocity = 0;
            }
            else
            {
                velocity -= Gravity * Time.deltaTime;
                characterController.Move(new Vector3(0, velocity, 0));
            }
        }
    
    //private CharacterController controller;
    //private Vector3 playerVelocity;
    //private bool groundedPlayer;
    //private float playerSpeed = .1f;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;

    //private void Start()
    //{
    //    controller = gameObject.AddComponent<CharacterController>();
    //}

    ////void Update()
    ////{
    ////    groundedPlayer = controller.isGrounded;
    ////    if (groundedPlayer )
    ////    {
    ////        playerVelocity.y = 0f;
    ////    }

    ////    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    ////    controller.Move(move * Time.deltaTime * playerSpeed);
    ////    Debug.Log(move);
    ////    if (move != Vector3.zero)
    ////    {
    ////        gameObject.transform.forward = move;
    ////    }

    ////    // Changes the height position of the player..
    ////    if (Input.GetButtonDown("Jump") && groundedPlayer)
    ////    {
    ////        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    ////    }

    ////    playerVelocity.y += gravityValue * Time.deltaTime;
    ////    controller.Move(playerVelocity * Time.deltaTime);
    ////}
    //void Update()
    //{

    //    if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        transform.position.x = transform.position.x+1 ;
    //    }
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        transform.position.x = transform.position.x -1;
    //    }
    //    if (Input.GetKey(KeyCode.UpArrow))
    //    {
    //        transform.position += (Vector3.right * playerSpeed * Time.deltaTime) / 10;
    //    }
    //    if (Input.GetKey(KeyCode.DownArrow))
    //    {
    //        transform.position += Vector3.back * playerSpeed * Time.deltaTime;
    //    }
    //}
}