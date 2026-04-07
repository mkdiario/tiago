using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class BootSceneLoader
{
    private static string bootScenePath = "Assets/Scenes/-Boot.unity";
    private static string initialScenePath;

    static BootSceneLoader()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            initialScenePath = EditorSceneManager.GetActiveScene().path;
            
            // Se já estamos na cena Boot, não fazemos nada
            if (initialScenePath == bootScenePath)
            {
                return;
            }

            // Carrega a cena Boot
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            EditorSceneManager.OpenScene(bootScenePath, OpenSceneMode.Single);
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnRuntimeInitialize()
    {
        // Carrega a cena inicial de forma aditiva
        if (!string.IsNullOrEmpty(initialScenePath) && initialScenePath != bootScenePath)
        {
            string sceneName = Path.GetFileNameWithoutExtension(initialScenePath);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            
            // Descarrega a cena Boot após a cena inicial ser carregada
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
        // Unload the boot scene
        Scene bootScene = SceneManager.GetSceneByPath(bootScenePath);
        if (bootScene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(bootScene);
        }
    }
}
#endif


