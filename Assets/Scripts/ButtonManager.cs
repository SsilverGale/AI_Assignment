using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void resetLevel()
    {
        Debug.Log("Scene Reloaded");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void closeGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}
