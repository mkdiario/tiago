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

    public GameState CurrentState { get; private set; }

    [Header("Input")]
    public PlayerInput playerInput;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                Debug.Log(SceneUtility.GetScenePathByBuildIndex(i));
            }
        }
        ChangeState(GameState.Iniciando);
        LoadScene("Splash");
    }

    // =========================
    // STATE
    // =========================
    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Estado atual: " + CurrentState);
    }

    // =========================
    // SCENE CONTROL
    // =========================
    public void LoadScene(string sceneName)
    {
        Debug.Log("Tentando carregar cena: [" + sceneName + "]");

        switch (CurrentState)
        {
            case GameState.Iniciando:
            case GameState.MenuPrincipal:
            case GameState.Gameplay:
                SceneManager.LoadScene(sceneName);
                break;

            default:
                Debug.LogWarning("Transição de cena não permitida!");
                break;
        }
    }

    // =========================
    // INPUT
    // =========================
    public void AssignPlayerInput(PlayerInput input)
    {
        playerInput = input;
        Debug.Log("Input atribuído ao jogador.");
    }
}