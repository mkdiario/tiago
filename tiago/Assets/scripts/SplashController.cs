using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2f);

        GameManager.Instance.ChangeState(GameManager.GameState.MenuPrincipal);
        GameManager.Instance.LoadScene("MenuPrincipal");
    }
}