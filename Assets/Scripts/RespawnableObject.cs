using Fusion;
using UnityEngine;
using TMPro;

public class RespawnableObject : NetworkBehaviour
{
    public float minRespawnRadius = 5f;
    public float maxRespawnRadius = 10f;
    public float minYPosition = 3f;
    public Transform spawnPoint;

    private NetworkRunner _networkRunner;

    private void Start()
    {
        _networkRunner = FindObjectOfType<NetworkRunner>();
        if (_networkRunner == null)
        {
            Debug.LogError("NetworkRunner bulunamadý. Lütfen bir NetworkRunner objesi olduðundan emin olun.");
        }
    }

    // Çarpýþmayý algýlayan fonksiyon
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Çarpýþma algýlandý, tetikleyici obje: " + other.name);

        if (other.CompareTag("Drone"))
        {
            //Debug.Log("Drone ile çarpýþma algýlandý, obje baþka bir yere taþýnacak.");

            //var playerScoreManager = other.GetComponent<PlayerScoreManager>();
            //if (playerScoreManager != null)
            //{
            //    // Skoru artýrmak için RPC metodu çaðýrýyoruz.
            //    playerScoreManager.RPC_IncreaseScore(10);
            //}
            //else
            //{
            //    Debug.LogWarning("PlayerScoreManager bileþeni bulunamadý.");
            //}

            // Objeyi yeni pozisyona taþý
            ChangeObjectPosition();
        }
        else
        {
            Debug.Log("Çarpýþma objesi drone deðil.");
        }
    }

    // Objenin pozisyonunu RPC kullanarak deðiþtir
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    void ChangeObjectPosition_RPC(Vector3 newPosition)
    {
        transform.position = newPosition;
        Debug.Log($"Obje yeni pozisyona taþýndý: {newPosition}");
    }

    // Pozisyon deðiþtirme fonksiyonu
    void ChangeObjectPosition()
    {
        if (_networkRunner != null)
        {
            // Rastgele bir mesafe ve yön seç
            float randomDistance = Random.Range(minRespawnRadius, maxRespawnRadius);
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            Vector3 randomPosition = spawnPoint.position + randomDirection * randomDistance;

            // Y pozisyonunun minimumdan küçük olmamasýný saðla
            if (randomPosition.y < minYPosition)
            {
                randomPosition.y = minYPosition;
            }

            // RPC kullanarak yeni pozisyonu gönder
            ChangeObjectPosition_RPC(randomPosition);
        }
        else
        {
            Debug.LogError("Pozisyon deðiþtirme iþlemi baþarýsýz: NetworkRunner bulunamadý.");
        }
    }
}