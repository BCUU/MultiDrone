using TMPro; // TextMeshPro kütüphanesini ekleyin
using UnityEngine;

public class PlayerScoreManager : MonoBehaviour
{
    public int playerScore = 0; // Skoru tutan deðiþken
    public TextMeshProUGUI scoreText; // TextMeshPro UI referansý

    //void Start()
    //{
    //    UpdateScoreText(); // Baþlangýçta skoru güncelle
    //}

    // Skoru artýran fonksiyon
    public void IncreaseScore(int amount)
    {
        playerScore += amount;
        Debug.Log($"Yeni skor: {playerScore}");

        // UI'daki skoru güncelle
        UpdateScoreText();
    }

    // Skoru UI'da güncelleyen fonksiyon
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = playerScore.ToString(); // Sadece sayýyý göster
        }
        else
        {
            Debug.LogWarning("Skor için TextMeshPro bileþeni atanmamýþ!");
        }
    }
}
