using Fusion;
using UnityEngine;
using System.Linq; // Linq k�t�phanesini ekleyin

public class DroneOwnershipManager : NetworkBehaviour
{
    // Sahipli�i ba�ka bir oyuncuya devretme metodu
    public void TransferOwnershipToPlayer(PlayerRef newOwner)
    {
        if (Object.HasStateAuthority) // Sadece sahip olan oyuncu devredebilir
        {
            RPC_TransferOwnership(newOwner); // RPC ile yeni oyuncuya sahipli�i devret
        }
    }

    // RPC ile sahipli�i yeni oyuncuya devret
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_TransferOwnership(PlayerRef newOwner)
    {
        if (newOwner == Object.InputAuthority)
        {
            Object.RequestStateAuthority(); // Yerel oyuncu sahipli�i al�r
        }
    }

    // A tu�una bas�ld���nda sahipli�i devret
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.T)||OVRInput.GetDown(OVRInput.Button.One)) // A tu�u
        {
            Debug.Log("input bas�ld�");
            PlayerRef nextPlayer = GetNextPlayer(); // Bir sonraki oyuncuyu al
            TransferOwnershipToPlayer(nextPlayer); // Sahipli�i devret
        }
    }

    // Ge�ici bir metod: Bir sonraki oyuncuyu belirleme
    PlayerRef GetNextPlayer()
    {
        var players = Runner.ActivePlayers.ToList(); // T�m oyuncular� bir listeye �eviriyoruz
        if (players.Count > 1)
        {
            return players[1]; // 2. oyuncuyu al�yoruz
        }
        return default; // E�er oyuncu yoksa varsay�lan de�er�d�nd�r
����}
}