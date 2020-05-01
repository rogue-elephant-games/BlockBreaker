using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    private string NameFromIndex(int BuildIndex)
    {
        var path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        var slash = path.LastIndexOf('/');
        var name = path.Substring(slash + 1);
        var dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    private int sceneIndexFromName(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var sceneNameFromIndex = NameFromIndex(i);
            if (sceneNameFromIndex == sceneName)
                return i;
        }
        return -1;
    }

    public void LoadSceneByName(string sceneName) => SceneManager.LoadScene(sceneIndexFromName(sceneName));

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadSuccessScene() => LoadSceneByName("Success");
    public void LoadGameOverScene() => LoadSceneByName("Game Over");
}
