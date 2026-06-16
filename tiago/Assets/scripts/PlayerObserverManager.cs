using System;

public static class PlayerObserverManager
{
    public static Action OnCoinCollected;

    public static Action<int> OnCoinsChanged;

    public static void NotifyCoinCollected()
    {
        OnCoinCollected?.Invoke();
    }

    public static void NotifyCoinsChanged(int amount)
    {
        OnCoinsChanged?.Invoke(amount);
    }
}
