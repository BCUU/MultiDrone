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
            Debug.LogError("NetworkRunner bulunamad�. L�tfen bir NetworkRunner objesi oldu�undan emin olun.");
        }
    }

    // �arp��may� alg�layan fonksiyon
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�arp��ma alg�land�, tetikleyici obje: " + other.name);

        if (other.CompareTag("Drone"))
        {
            //Debug.Log("Drone ile �arp��ma alg�land�, obje ba�ka bir yere ta��nacak.");

            //var playerScoreManager = other.GetComponent<PlayerScoreManager>();
            //if (playerScoreManager != null)
            //{
            //    // Skoru art�rmak i�in RPC metodu �a��r�yoruz.
            //    playerScoreManager.RPC_IncreaseScore(10);
            //}
            //else
            //{
            //    Debug.LogWarning("PlayerScoreManager bile�eni bulunamad�.");
            //}

            // Objeyi yeni pozisyona ta��
            ChangeObjectPosition();
        }
        else
        {
            Debug.Log("�arp��ma objesi drone de�il.");
        }
    }

    // Objenin pozisyonunu RPC kullanarak de�i�tir
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    void ChangeObjectPosition_RPC(Vector3 newPosition)
    {
        transform.position = newPosition;
        Debug.Log($"Obje yeni pozisyona ta��nd�: {newPosition}");
    }

    // Pozisyon de�i�tirme fonksiyonu
    void ChangeObjectPosition()
    {
        if (_networkRunner != null)
        {
            // Rastgele bir mesafe ve y�n se�
            float randomDistance = Random.Range(minRespawnRadius, maxRespawnRadius);
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            Vector3 randomPosition = spawnPoint.position + randomDirection * randomDistance;

            // Y pozisyonunun minimumdan k���k olmamas�n� sa�la
            if (randomPosition.y < minYPosition)
            {
                randomPosition.y = minYPosition;
            }

            // RPC kullanarak yeni pozisyonu g�nder
            ChangeObjectPosition_RPC(randomPosition);
        }
        else
        {
            Debug.LogError("Pozisyon de�i�tirme i�lemi ba�ar�s�z: NetworkRunner bulunamad�.");
        }
    }
}