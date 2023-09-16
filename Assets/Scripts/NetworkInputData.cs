using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    // Old System
    public Vector3 direction;
    public NetworkBool takeObject;
    public NetworkBool dropObject;
    public NetworkBool doorOpen;
    
    // New System
    public Vector2 movementInput;
    public float rotationInput;
    public NetworkBool isJumpPressed;

}
