using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject Pause_Screen;
    public GameObject PlayerUI;
    public GameObject Setting_Screen;
    public GameObject Title_Screen;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
    public void StopGame()
    {
        Application.Quit();
    }
    public void SettingsScreen()
    {
        Setting_Screen.SetActive(true);
    }
    public void Pause()
    {
        Pause_Screen.SetActive(true);
    }

}
/*
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //sets the ScoreUI
    public Text scoreText;
    public GameObject GameOverPanel;
    public void UpdateScoreUI(int newScore)
    {
        //Adds 1 to the Score
        scoreText.text = "Score: " + newScore;
    }
    public void ShowGameOver()
    {
        GameOverPanel.SetActive(true);
    }
}*/
