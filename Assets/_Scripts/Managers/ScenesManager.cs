using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    
    public void LoadNewGame()
    {
        //SceneManager.LoadScene(Scene.Part_1.ToString());
        MenuTrigger.isMainMenu = true;

        StartCoroutine(LoadNewGameAsync());
    }
    
    private IEnumerator LoadNewGameAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scene.Part_1.ToString());
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        SceneManager.LoadScene(Scene.Part_1.ToString());
    }
    
    private IEnumerator LoadSceneAsync(Scene scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene.ToString());
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        SceneManager.LoadScene(scene.ToString());
    }
    
    public void LoadNextScene()
    {
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public enum Scene
    {
        MainMenu,
        Part_1,
        Part_2
    }
}
