using System.Linq;
using Fusion;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
    //private Vector2 viewInput;
    private Quaternion cameraRotationY;

    //private float cameraRotationX = 0;
    //private Camera localCamera;
    
    private NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    private GameController gameController;
    private Transform door;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        door = GameObject.FindWithTag("Door").transform;
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

            if (networkInputData.takeObject && gameController.balls.Count > 0)
            {
                foreach (var ball in gameController.balls)
                {
                    if (Vector3.Distance(transform.position, ball.transform.position) < 2.0f && ball.transform.parent == null)
                    {
                        float randX = Random.Range(-0.25f, 0.25f);
                        float randY = Random.Range(0.25f, 0.75f);
                        float randZ = Random.Range(0.5f, 1.5f);
                        
                        ball.transform.position = transform.position + new Vector3(randX, randY, randZ);
                        ball.transform.SetParent(transform);

                        gameController.balls.Remove(ball);
                        NetworkPlayer.collectedBalls.Add(ball);
                        break;
                    }
                }
            }

            if (networkInputData.dropObject && NetworkPlayer.collectedBalls.Count > 0)
            {
                var firstBall = NetworkPlayer.collectedBalls.Last();
                firstBall.transform.SetParent(null);
                
                float randX = Random.Range(2f, 10f);
                float randZ = Random.Range(2f, 10f);
                
                firstBall.transform.position = transform.position + new Vector3(randX, 0f, randZ);

                gameController.balls.Add(firstBall);
                NetworkPlayer.collectedBalls.Remove(firstBall);
            }

            if (networkInputData.doorOpen)
            {
                if (Vector3.Distance(transform.position, door.position) < 5.0f)
                {
                    if (door.rotation.y != 0)
                    {
                        door.rotation = new Quaternion(0, 0, 0, 0);
                    }

                    else
                    {
                        var rotationDirection = Quaternion.LookRotation(new Vector3(-90, 0, 0));
                        door.rotation = rotationDirection;
                    }
                }
            }
            
        }
    }

    public void SetViewInputVector(Vector2 viewInput)
    {
        //this.viewInput = viewInput;
    }
    
}
