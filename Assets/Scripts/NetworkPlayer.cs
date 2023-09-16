using System.Collections.Generic;
using Cinemachine;
using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }
    public static List<GameObject> collectedBalls = new();
    private CinemachineFreeLook freeLook;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            
            freeLook = transform.GetChild(0).GetComponent<CinemachineFreeLook>();

            freeLook.LookAt = GetComponent<NetworkCharacterControllerPrototypeCustom>().InterpolationTarget;
            freeLook.Follow = GetComponent<NetworkCharacterControllerPrototypeCustom>().InterpolationTarget;

            freeLook.m_Orbits[0].m_Height = 5;
            freeLook.m_Orbits[0].m_Radius = 12;
        
            freeLook.m_Orbits[1].m_Height = 5;
            freeLook.m_Orbits[1].m_Radius = 18;
        
            freeLook.m_Orbits[2].m_Height = 1;
            freeLook.m_Orbits[2].m_Radius = 12;
        
            freeLook.gameObject.SetActive(true);
            
            Debug.Log("Spawned local player");
        }

        else
        {
            Debug.Log("Spawned remote player");
        }
        
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }
}
