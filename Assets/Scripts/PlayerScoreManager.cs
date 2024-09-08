using TMPro; // TextMeshPro k�t�phanesini ekleyin
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    public int playerScore = 0; // Skoru tutan de�i�ken
    public TextMeshProUGUI scoreText; // TextMeshPro UI referans�

    //void Start()
    //{
    //    UpdateScoreText(); // Ba�lang��ta skoru g�ncelle
    //}

    // Skoru art�ran fonksiyon
    public void IncreaseScore(int amount)
    {
        playerScore += amount;
        Debug.Log($"Yeni skor: {playerScore}");

        // UI'daki skoru g�ncelle
        UpdateScoreText();
    }

    // Skoru UI'da g�ncelleyen fonksiyon
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = playerScore.ToString(); // Sadece say�y� g�ster
        }
        else
        {
            Debug.LogWarning("Skor i�in TextMeshPro bile�eni atanmam��!");
        }
    }
}
