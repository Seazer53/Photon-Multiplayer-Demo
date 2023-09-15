using System;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    private Vector2 moveInputVector = Vector2.zero;
    //private Vector2 viewInputVector = Vector2.zero;
    private bool isJumpButtonPressed;

    private CharacterMovementHandler characterMovementHandler;

    private void Awake()
    {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*viewInputVector.x = Input.GetAxis("Mouse X");
        viewInputVector.y = Input.GetAxis("Mouse Y") * -1;

        characterMovementHandler.SetViewInputVector(viewInputVector);*/
        
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            isJumpButtonPressed = true;
        }

        else
        {
            isJumpButtonPressed = false;
        }

    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //networkInputData.rotationInput = viewInputVector.x;

        networkInputData.movementInput = moveInputVector;

        networkInputData.isJumpPressed = isJumpButtonPressed;

        return networkInputData;

    }
    
}
