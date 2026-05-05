using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Iniciando,
        MenuPrincipal,
        Gameplay
    }

    public GameState currentState;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Escuta quando a cena muda
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetState(GameState.Iniciando);
        LoadScene("splash");
    }

    // Chamado automaticamente quando uma cena carrega
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Cena carregada: " + scene.name);

        if (scene.name == "splash")
        {
            SetState(GameState.Iniciando);
        }
        else if (scene.name == "Menu")
        {
            SetState(GameState.MenuPrincipal);
        }
        else if (scene.name == "GetStarted_Scene")
        {
            SetState(GameState.Gameplay);
        }
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
        Debug.Log("Estado atual: " + currentState);
    }

    // Controle de cenas (SÓ o GameManager pode fazer isso)
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Input allocation (simples)
    public void SetupPlayerInput(PlayerInput playerInput)
    {
        Debug.Log("Input atribuído ao jogador: " + playerInput.name);
    }
    
}