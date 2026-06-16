using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    private int coins = 0;

    private void OnEnable()
    {
        Debug.Log("PlayerCoins inscrito");
        PlayerObserverManager.OnCoinCollected += AddCoin;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinCollected -= AddCoin;
    }

    private void AddCoin()
    {
        Debug.Log("AddCoin foi chamado");

        coins++;

        PlayerObserverManager.NotifyCoinsChanged(coins);
    }
}