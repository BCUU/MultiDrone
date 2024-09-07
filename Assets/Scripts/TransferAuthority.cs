using Fusion;
using UnityEngine;

public class TransferAuthority : NetworkBehaviour
{
    [Networked] public NetworkObject ControlledObject { get; set; }

    // �lk oyuncunun ba�lang��ta kontrol� olup olmad���n� belirleyen bayrak
    private bool isFirstPlayer;

    // Giri� kontrol� i�in
    void Start()
    {
        // Sadece ilk oyuncu oldu�unda kontrol yetkisini al�r
        if (Object.HasStateAuthority)
        {
            isFirstPlayer = true;
            Debug.Log("�lk oyuncu kontrol� ald�.");
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
            // �lk oyuncu Oculus denetleyicisindeki "A" tu�una basarsa kontrol� b�rakacak
            if (isFirstPlayer && OVRInput.GetDown(OVRInput.Button.One))
            {
                Debug.Log("A tu�una bas�ld�, kontrol� b�rak�yorum...");
                ReleaseControlToAnotherPlayer();
            }
        }
    }

    // Kontrol� ba�ka bir oyuncuya devretmek i�in metot
    public void ReleaseControlToAnotherPlayer()
    {
        // Yetkiyi b�rak
        Object.ReleaseStateAuthority();

        // Burada di�er oyuncuyu belirlemek gerekiyor
        PlayerRef otherPlayer = GetOtherPlayer(); // �kinci oyuncuyu belirlemek i�in bir metod kullan�yoruz

        if (otherPlayer != PlayerRef.None)
        {
            // Yetkiyi ba�ka bir oyuncuya istemek i�in RequestStateAuthority kullan�l�r
            Runner.SetPlayerObject(otherPlayer, Object);
            Debug.Log("Yetki di�er oyuncuya devredildi.");
        }
    }

    // Bu metot ikinci oyuncuyu bulur, her iki oyuncu oldu�u varsay�l�r.
    private PlayerRef GetOtherPlayer()
    {
        foreach (var player in Runner.ActivePlayers)
        {
            if (player != Runner.LocalPlayer)
            {
                return player; // Yerel oyuncu de�ilse o oyuncuya atar�z
            }
        }
        return PlayerRef.None; // E�er di�er oyuncu yoksa
    }
}
