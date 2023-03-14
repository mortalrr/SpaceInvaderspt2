using UnityEngine.SceneManagement;
using UnityEngine;

public class ScoreScreen : MonoBehaviour
{
    public KeyCode nextSceneKey;

    void Update()
    {
        if (Input.GetKeyDown(nextSceneKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
