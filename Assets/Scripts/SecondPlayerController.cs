using UnityEngine;
using Fusion;

public class SecondPlayerController : NetworkBehaviour
{
    private NetworkObjectController networkObject;

    void Update()
    {
        // Oculus kontrolc�s�ndeki A tu�una bas�ld���nda kontrol iste�i g�nderir
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (networkObject != null)
            {
                networkObject.RpcRequestControl(); // Yeni ad�yla RPC'yi �a��r�yoruz
            }
        }
    }

    public void AssignNetworkObject(NetworkObjectController obj)
    {
        networkObject = obj;
    }
}
