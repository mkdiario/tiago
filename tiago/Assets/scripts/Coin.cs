using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo encostou na moeda: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Jogador pegou a moeda");

            PlayerObserverManager.NotifyCoinCollected();

            Destroy(gameObject);
        }
    }
}