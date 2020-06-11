using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public enum DifficultyLevels { Easy, Medium, Hard };
    internal DifficultyLevels difficultyLevel = DifficultyLevels.Easy;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex >= SceneManager.sceneCountInBuildSettings - 2)
            currentSceneIndex = 0;


        SceneManager.LoadScene(currentSceneIndex + 1);

    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void ChooseDifficulty(DifficultyLevels desiredLevel)
    {
        difficultyLevel = desiredLevel;
    }    
    public void ChooseDifficulty(int desiredLevel)
    {
        difficultyLevel = (DifficultyLevels)desiredLevel;
        Debug.Log(difficultyLevel);
    }
}
