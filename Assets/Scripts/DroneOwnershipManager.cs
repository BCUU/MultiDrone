using Fusion;
using UnityEngine;
using System.Linq; // Linq kütüphanesini ekleyin

public class DroneOwnershipManager : NetworkBehaviour
{
    // Sahipliði baþka bir oyuncuya devretme metodu
    public void TransferOwnershipToPlayer(PlayerRef newOwner)
    {
        if (Object.HasStateAuthority) // Sadece sahip olan oyuncu devredebilir
        {
            RPC_TransferOwnership(newOwner); // RPC ile yeni oyuncuya sahipliði devret
        }
    }

    // RPC ile sahipliði yeni oyuncuya devret
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_TransferOwnership(PlayerRef newOwner)
    {
        if (newOwner == Object.InputAuthority)
        {
            Object.RequestStateAuthority(); // Yerel oyuncu sahipliði alýr
        }
    }

    // A tuþuna basýldýðýnda sahipliði devret
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.T)||OVRInput.GetDown(OVRInput.Button.One)) // A tuþu
        {
            Debug.Log("input basýldý");
            PlayerRef nextPlayer = GetNextPlayer(); // Bir sonraki oyuncuyu al
            TransferOwnershipToPlayer(nextPlayer); // Sahipliði devret
        }
    }

    // Geçici bir metod: Bir sonraki oyuncuyu belirleme
    PlayerRef GetNextPlayer()
    {
        var players = Runner.ActivePlayers.ToList(); // Tüm oyuncularý bir listeye çeviriyoruz
        if (players.Count > 1)
        {
            return players[1]; // 2. oyuncuyu alýyoruz
        }
        return default; // Eðer oyuncu yoksa varsayýlan deðer döndür
    }
}