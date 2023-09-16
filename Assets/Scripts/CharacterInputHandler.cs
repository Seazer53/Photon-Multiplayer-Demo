using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    private Vector2 moveInputVector = Vector2.zero;
    //private Vector2 viewInputVector = Vector2.zero;
    private bool isJumpButtonPressed;
    private bool takeObject;
    private bool dropObject;
    private bool doorOpen;

    private CharacterMovementHandler characterMovementHandler;
    private Camera camera;
    private Vector3 movement;
    
    private void Awake()
    {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
        camera = Camera.main;
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

        Transform camTransform = camera.transform;
        Vector3 forwardMovement = camTransform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalMovement = camTransform.right * Input.GetAxis("Horizontal");
        movement = Vector3.ClampMagnitude(forwardMovement + horizontalMovement, 1);

        //moveInputVector.x = Input.GetAxis("Horizontal");
        //moveInputVector.y = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            isJumpButtonPressed = true;
        }

        else
        {
            isJumpButtonPressed = false;
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            takeObject = true;
            doorOpen = true;
        }

        else
        {
            takeObject = false;
            doorOpen = false;
        }
        
        if (Input.GetKey(KeyCode.G))
        {
            dropObject = true;
        }

        else
        {
            dropObject = false;
        }
        
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //networkInputData.rotationInput = viewInputVector.x;

        networkInputData.direction = movement;
        networkInputData.isJumpPressed = isJumpButtonPressed;
        networkInputData.takeObject = takeObject;
        networkInputData.dropObject = dropObject;
        networkInputData.doorOpen = doorOpen;

        return networkInputData;

    }
    
}
