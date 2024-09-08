using UnityEngine;

public class RespawnableObject : MonoBehaviour
{
    public float respawnRadius = 10f; // Rastgele pozisyon için yarýçap
    public Transform spawnPoint; // Baþlangýç pozisyonu için referans
    public PlayerScoreManager playerScoreManager; // Skor yöneticisi referansý

    // Çarpýþmayý algýlayan fonksiyon
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Çarpýþma algýlandý, tetikleyici obje: " + other.name);

        if (other.CompareTag("Drone")) // Drone'un tag'i "Drone" olarak ayarlanmalý
        {
            Debug.Log("Drone ile çarpýþma algýlandý, obje baþka bir yere taþýnacak.");

            // Skoru artýr
            if (playerScoreManager != null)
            {
                playerScoreManager.IncreaseScore(10); // Skoru artýr
            }
            else
            {
                Debug.LogWarning("PlayerScoreManager referansý eksik!");
            }

            // Objeyi baþka bir rastgele konuma taþý
            ChangeObjectPosition();
        }
        else
        {
            Debug.Log("Çarpýþma objesi drone deðil.");
        }
    }

    void ChangeObjectPosition()
    {
        // Rastgele bir pozisyon belirle
        Vector3 randomPosition = spawnPoint.position + Random.insideUnitSphere * respawnRadius;
        randomPosition.y = spawnPoint.position.y; // Yükseklik sabit kalabilir

        // Objeyi yeni konuma taþý
        transform.position = randomPosition;
        Debug.Log($"Obje yeni pozisyona taþýndý: {randomPosition}");
    }
}
