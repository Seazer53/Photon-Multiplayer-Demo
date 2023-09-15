using Cinemachine;
using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private Camera playCam;
    private CinemachineFreeLook freeLook;
    //private Transform stackPoint;
    private NetworkCharacterControllerPrototype _cc;
    public void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        //stackPoint = transform.GetChild(0);
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
        {
            return;
        }
        
        if (GetInput(out NetworkInputData data))
        {
            var cameraRotationY = Quaternion.Euler(0, playCam.transform.eulerAngles.y, 0);
            Vector3 move = cameraRotationY * data.direction;
            move.Normalize();
            _cc.Move(move * Runner.DeltaTime);

            /*if (data.takeObject)
            {
                foreach (var ball in BasicSpawner.Instance.balls)
                {
                    if (Vector3.Distance(transform.position, ball.transform.position) < 1.0f)
                    {
                        float randX = Random.Range(-0.25f, 0.25f);
                        float randY = Random.Range(0.25f, 0.75f);
                        float randZ = Random.Range(0.5f, 1.5f);
                        
                        ball.transform.position = stackPoint.position + new Vector3(randX, randY, randZ);
                        ball.transform.SetParent(stackPoint);

                        data.takeObject = false;
                    }
                }
            }*/
        }
    }

    public override void Spawned()
    {
        if (!Object.HasInputAuthority)
        {
            return;
        }

        playCam = Camera.main;
        freeLook = transform.GetChild(0).transform.GetChild(0).GetComponent<CinemachineFreeLook>();

        freeLook.LookAt = GetComponent<NetworkCharacterControllerPrototype>().InterpolationTarget;
        freeLook.Follow = GetComponent<NetworkCharacterControllerPrototype>().InterpolationTarget;

        freeLook.m_Orbits[0].m_Height = 5;
        freeLook.m_Orbits[0].m_Radius = 12;
        
        freeLook.m_Orbits[1].m_Height = 5;
        freeLook.m_Orbits[1].m_Radius = 18;
        
        freeLook.m_Orbits[2].m_Height = 1;
        freeLook.m_Orbits[2].m_Radius = 12;
        
        freeLook.gameObject.SetActive(true);

    }
}
