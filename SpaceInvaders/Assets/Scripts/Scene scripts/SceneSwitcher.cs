using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string gameSceneName;
    public string creditSceneName;

    private void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadAndSetup());
    }
    
    public void LoadGameScene1()
    {
        StartCoroutine(LoadAndSetupCredits());
    }
    

    IEnumerator LoadAndSetup()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        Debug.Log("load started");

        //wait 1 frame for loading to complete
        yield return null;
        
        Player player = GameObject.FindObjectOfType<Player>();
        Debug.Log($"Found {player}");
    }
    
    IEnumerator LoadAndSetupCredits()
    {
        SceneManager.LoadScene(creditSceneName, LoadSceneMode.Single);
        Debug.Log("load started");
        
        yield return new WaitForSeconds(5f); // Wait for 5 seconds

        SceneManager.LoadScene("Main Menu");
    }
    
}
