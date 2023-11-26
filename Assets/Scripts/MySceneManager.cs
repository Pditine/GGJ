using tools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoSingleton<MySceneManager>
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
