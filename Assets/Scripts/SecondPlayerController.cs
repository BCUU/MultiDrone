using UnityEngine;
using Fusion;

public class SecondPlayerController : NetworkBehaviour
{
    private NetworkObjectController networkObject;

    void Update()
    {
        // Oculus kontrolcüsündeki A tuþuna basýldýðýnda kontrol isteði gönderir
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (networkObject != null)
            {
                networkObject.RpcRequestControl(); // Yeni adýyla RPC'yi çaðýrýyoruz
            }
        }
    }

    public void AssignNetworkObject(NetworkObjectController obj)
    {
        networkObject = obj;
    }
}
