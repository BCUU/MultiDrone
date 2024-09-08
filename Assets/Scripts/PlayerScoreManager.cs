using Fusion;
using TMPro;
using UnityEngine;

public class PlayerScoreManager : NetworkBehaviour
{
    [Networked] public int playerScore { get; set; }

    // TextMeshPro UI objesi referansý
    public TextMeshProUGUI scoreText;

    // Obje aðda spawn edildiðinde çaðrýlan metod
    public override void Spawned()
    {
        Debug.Log("Obje spawn edildi, skoru güncelliyoruz.");
        UpdateScoreText();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_IncreaseScore(int amount)
    {
        Debug.Log("RPC_IncreaseScore çaðrýldý.");
        if (Object.HasStateAuthority)
        {
            playerScore += amount;
            Debug.Log($"Yeni skor: {playerScore}");
            UpdateScoreText();
        }
        else
        {
            Debug.LogWarning("StateAuthority'ye sahip deðilsiniz, skor artýrýlamaz.");
        }
    }


    // Skoru UI'da güncelleyen fonksiyon
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {playerScore}";
        }
        else
        {
            Debug.LogWarning("Score TextMeshPro bileþeni atanmamýþ!");
        }
    }
}
