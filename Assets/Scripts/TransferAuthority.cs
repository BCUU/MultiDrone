using Fusion;
using UnityEngine;

public class TransferAuthority : NetworkBehaviour
{
    [Networked] public NetworkObject ControlledObject { get; set; }

    // Ýlk oyuncunun baþlangýçta kontrolü olup olmadýðýný belirleyen bayrak
    private bool isFirstPlayer;

    // Giriþ kontrolü için
    void Start()
    {
        // Sadece ilk oyuncu olduðunda kontrol yetkisini alýr
        if (Object.HasStateAuthority)
        {
            isFirstPlayer = true;
            Debug.Log("Ýlk oyuncu kontrolü aldý.");
        }
        else
        {
            isFirstPlayer = false;
        }
    }

    void Update()
    {
        if (Object.HasStateAuthority)
        {
            // Ýlk oyuncu Oculus denetleyicisindeki "A" tuþuna basarsa kontrolü býrakacak
            if (isFirstPlayer && OVRInput.GetDown(OVRInput.Button.One))
            {
                Debug.Log("A tuþuna basýldý, kontrolü býrakýyorum...");
                ReleaseControlToAnotherPlayer();
            }
        }
    }

    // Kontrolü baþka bir oyuncuya devretmek için metot
    public void ReleaseControlToAnotherPlayer()
    {
        // Yetkiyi býrak
        Object.ReleaseStateAuthority();

        // Burada diðer oyuncuyu belirlemek gerekiyor
        PlayerRef otherPlayer = GetOtherPlayer(); // Ýkinci oyuncuyu belirlemek için bir metod kullanýyoruz

        if (otherPlayer != PlayerRef.None)
        {
            // Yetkiyi baþka bir oyuncuya istemek için RequestStateAuthority kullanýlýr
            Runner.SetPlayerObject(otherPlayer, Object);
            Debug.Log("Yetki diðer oyuncuya devredildi.");
        }
    }

    // Bu metot ikinci oyuncuyu bulur, her iki oyuncu olduðu varsayýlýr.
    private PlayerRef GetOtherPlayer()
    {
        foreach (var player in Runner.ActivePlayers)
        {
            if (player != Runner.LocalPlayer)
            {
                return player; // Yerel oyuncu deðilse o oyuncuya atarýz
            }
        }
        return PlayerRef.None; // Eðer diðer oyuncu yoksa
    }
}
