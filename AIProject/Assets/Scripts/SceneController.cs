using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// basic scene controller - NH
public class SceneController : MonoBehaviour
{
    public static SceneController sceneController;

    [SerializeField] int currentSceneIndex;

    private void Awake()
    {
        if (sceneController == null)
        {
            sceneController = this;
        } else
        {
            Destroy(gameObject);
        }

        // grabs the current scene's index
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += updateCurrentSceneIndex;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= updateCurrentSceneIndex;
    }

    void updateCurrentSceneIndex(Scene scene, LoadSceneMode mode)
    {
        currentSceneIndex = scene.buildIndex;
    }

    // feed the desired scene index to load         
    public void loadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // feed the desired scene name (a string -- use QUOTES) to load
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // adds 1 to the current scene index and loads it
    public void loadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // reload the current scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void delayedNextLoad(float delay)
    {
        Invoke("loadNextScene", delay);
    }

    public void delayedReload(float delay)
    {
        Invoke("ReloadScene", delay);
    }
}