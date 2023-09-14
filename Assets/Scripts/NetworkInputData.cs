using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector3 direction;
    public NetworkBool takeObject;
    public NetworkBool dropObject;
}
