using System.Linq;
using Fusion;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
    private Quaternion cameraRotationY;

    private NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    private GameController gameController;
    private Transform door;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        door = GameObject.FindWithTag("Door").transform;
    }
    
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
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
}
