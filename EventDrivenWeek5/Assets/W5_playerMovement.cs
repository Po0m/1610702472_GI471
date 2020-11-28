using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W5_playerMovement : MonoBehaviour
{
    public float speedMove;
    public float jumpPower;
    public float gravity = 20f; 

    private float inputHorizontal;
    private float inputVertical;
    private bool inputJump;
    private CharacterController characterController;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    public void Setinput_Horizontal(float axis)
    {
        inputHorizontal = axis;
    }
    public void Setinput_Vertical(float axis)
    {
        inputVertical = axis;
    }
    public void SetInput_Jump(bool toSet)
    {
        inputJump = toSet;
    }


    public void UpdateMovement()
    {
        if(characterController.isGrounded)
        {
            velocity = Vector3.zero;

            Vector3 dirVertical = this.transform.forward * inputVertical;
            Vector3 dirHotizontal = this.transform.right * inputHorizontal;

            velocity = (dirVertical + dirHotizontal).normalized;
            velocity *= speedMove;


            if (inputJump)
            {
                velocity.y += jumpPower;
            }
        }
        velocity.y -= gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

}
