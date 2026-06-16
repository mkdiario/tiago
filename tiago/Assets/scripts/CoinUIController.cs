using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private void OnEnable()
    {
        PlayerObserverManager.OnCoinsChanged += UpdateCoins;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinsChanged -= UpdateCoins;
    }

    private void UpdateCoins(int amount)
    {
        Debug.Log("UI recebeu: " + amount);

        coinText.text = "Moedas: " + amount;
    }
}