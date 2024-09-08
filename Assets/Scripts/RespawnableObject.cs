using UnityEngine;

public class RespawnableObject : MonoBehaviour
{
    public float respawnRadius = 10f; // Rastgele pozisyon i�in yar��ap
    public Transform spawnPoint; // Ba�lang�� pozisyonu i�in referans
    public PlayerScoreManager playerScoreManager; // Skor y�neticisi referans�

    // �arp��may� alg�layan fonksiyon
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�arp��ma alg�land�, tetikleyici obje: " + other.name);

        if (other.CompareTag("Drone")) // Drone'un tag'i "Drone" olarak ayarlanmal�
        {
            Debug.Log("Drone ile �arp��ma alg�land�, obje ba�ka bir yere ta��nacak.");

            // Skoru art�r
            if (playerScoreManager != null)
            {
                playerScoreManager.IncreaseScore(10); // Skoru art�r
            }
            else
            {
                Debug.LogWarning("PlayerScoreManager referans� eksik!");
            }

            // Objeyi ba�ka bir rastgele konuma ta��
            ChangeObjectPosition();
        }
        else
        {
            Debug.Log("�arp��ma objesi drone de�il.");
        }
    }

    void ChangeObjectPosition()
    {
        // Rastgele bir pozisyon belirle
        Vector3 randomPosition = spawnPoint.position + Random.insideUnitSphere * respawnRadius;
        randomPosition.y = spawnPoint.position.y; // Y�kseklik sabit kalabilir

        // Objeyi yeni konuma ta��
        transform.position = randomPosition;
        Debug.Log($"Obje yeni pozisyona ta��nd�: {randomPosition}");
    }
}
