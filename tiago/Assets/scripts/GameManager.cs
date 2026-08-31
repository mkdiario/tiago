using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

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

    // =========================
    // PONTUAÇÃO DOS JOGADORES
    // =========================

    public int Player1Stars { get; private set; }
    public int Player2Stars { get; private set; }

    // =========================
    // OBSERVER
    // =========================

    public static event Action<int, int> OnStarsChanged;
    public static event Action<int> OnGameFinished;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

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

    // ==========================================
    // QUANDO UMA CENA É CARREGADA
    // ==========================================

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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

            // Evita carregar a GUI várias vezes
            if (!SceneManager.GetSceneByName("GUI").isLoaded)
            {
                SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
            }

            // Começa uma nova partida
            ResetGame();
        }
    }

    // ==========================================
    // ESTADO DO JOGO
    // ==========================================

    public void SetState(GameState newState)
    {
        currentState = newState;

        Debug.Log("Estado atual: " + currentState);
    }

    // ==========================================
    // CONTROLE DE CENAS
    // ==========================================

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ==========================================
    // ESTRELAS
    // ==========================================

    public void AddStar(int playerIndex)
    {
        if (playerIndex == 0)
        {
            Player1Stars++;

            Debug.Log("Jogador 1 pegou uma estrela: " + Player1Stars);

            OnStarsChanged?.Invoke(0, Player1Stars);
        }
        else if (playerIndex == 1)
        {
            Player2Stars++;

            Debug.Log("Jogador 2 pegou uma estrela: " + Player2Stars);

            OnStarsChanged?.Invoke(1, Player2Stars);
        }
    }

    // ==========================================
    // FINAL DA PARTIDA
    // ==========================================

    public void FinishGame()
    {
        int winner = GetWinner();

        Debug.Log("Vencedor: " + winner);

        // Observer avisa a GUI
        OnGameFinished?.Invoke(winner);
    }

    public int GetWinner()
    {
        if (Player1Stars > Player2Stars)
        {
            return 0;
        }

        if (Player2Stars > Player1Stars)
        {
            return 1;
        }

        // Empate
        return -1;
    }

    // ==========================================
    // RESET
    // ==========================================

    public void ResetGame()
    {
        Player1Stars = 0;
        Player2Stars = 0;

        OnStarsChanged?.Invoke(0, 0);
        OnStarsChanged?.Invoke(1, 0);
    }

    // ==========================================
    // INPUT
    // ==========================================

    public void SetupPlayerInput(PlayerInput playerInput)
    {
        Debug.Log(
            "Input atribuído ao jogador: " +
            playerInput.name +
            " | Player Index: " +
            playerInput.playerIndex
        );
    }
}
