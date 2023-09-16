using System;
using Fusion;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
    //private Vector2 viewInput;
    private Quaternion cameraRotationY;

    //private float cameraRotationX = 0;
    //private Camera localCamera;
    
    private NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
    }

    private void Update()
    {
        /*cameraRotationX += viewInput.y * Time.deltaTime;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);
        
        localCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);*/
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            //networkCharacterControllerPrototypeCustom.Rotate(networkInputData.rotationInput);

            /*Vector3 moveDirection = transform.forward * networkInputData.movementInput.y +
                                    transform.right * networkInputData.movementInput.x;
                                    */

            //Vector3 camMoveDirection = cameraRotationY * moveDirection;
            /*Debug.Log(moveDirection.ToString());

            moveDirection.Normalize();*/

            if (networkInputData.isJumpPressed)
            {
                networkCharacterControllerPrototypeCustom.Jump();
            }

            networkCharacterControllerPrototypeCustom.Move(networkInputData.direction);

        }
    }

    public void SetViewInputVector(Vector2 viewInput)
    {
        //this.viewInput = viewInput;
    }
    
}
