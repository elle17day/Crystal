using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public void OnRestartPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene()TestLevel.name);
    }
    public void StopGame()
    {
        Application.Quit();
    }
}
