using Fusion;
using TMPro;
using UnityEngine;

public class PlayerScoreManager : NetworkBehaviour
{
    [Networked] public int playerScore { get; set; }

    // TextMeshPro UI objesi referans�
    public TextMeshProUGUI scoreText;

    // Obje a�da spawn edildi�inde �a�r�lan metod
    public override void Spawned()
    {
        Debug.Log("Obje spawn edildi, skoru g�ncelliyoruz.");
        UpdateScoreText();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_IncreaseScore(int amount)
    {
        Debug.Log("RPC_IncreaseScore �a�r�ld�.");
        if (Object.HasStateAuthority)
        {
            playerScore += amount;
            Debug.Log($"Yeni skor: {playerScore}");
            UpdateScoreText();
        }
        else
        {
            Debug.LogWarning("StateAuthority'ye sahip de�ilsiniz, skor art�r�lamaz.");
        }
    }


    // Skoru UI'da g�ncelleyen fonksiyon
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {playerScore}";
        }
        else
        {
            Debug.LogWarning("Score TextMeshPro bile�eni atanmam��!");
        }
    }
}
