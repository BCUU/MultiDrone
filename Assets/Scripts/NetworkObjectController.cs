using UnityEngine;
using Fusion;

public class NetworkObjectController : NetworkBehaviour
{
    private bool isControlledByFirstPlayer = true;
    private NetworkObject netObject;

    void Start()
    {
        netObject = GetComponent<NetworkObject>();
    }

    void Update()
    {
        if (HasStateAuthority)
        {
            if (isControlledByFirstPlayer)
            {
                ControlObject();
            }
        }
        else if (!isControlledByFirstPlayer)
        {
            ControlObject();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcRequestControl()  // Metod ismi Rpc ile ba�l�yor
    {
        if (HasStateAuthority && netObject != null)
        {
            // Sahipli�i de�i�tir (OwnerShip transfer)
            Runner.SetPlayerObject(netObject.InputAuthority, netObject);
            isControlledByFirstPlayer = false; // Art�k ikinci oyuncu kontrol edecek

            netObject.ReleaseStateAuthority();
        }
    }

    private void ControlObject()
    {
        Debug.Log("Object is being controlled");
    }
}
