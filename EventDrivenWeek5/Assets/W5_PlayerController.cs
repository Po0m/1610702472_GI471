using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W5_PlayerController : MonoBehaviour
{
    public W5_playerMovement playerMovement;

    void Update()
    {
        playerMovement.Setinput_Horizontal(Input.GetAxis("Horizontal"));
        playerMovement.Setinput_Vertical(Input.GetAxis("Vertical"));
        playerMovement.SetInput_Jump(Input.GetKeyDown(KeyCode.Space));
    }
}
