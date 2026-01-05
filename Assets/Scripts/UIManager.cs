using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.DebugUI.Table;
public class UIManager : MonoBehaviour
{
    public GameObject Pause_Screen;
    public GameObject PlayerUI;
    public GameObject Setting_Screen;
    public GameObject Title_Screen;

    private bool GamePaused;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PlayGame()
    {
        PlayerUI.SetActive(true);
        if (Title_Screen != null) Title_Screen.SetActive(false);
    }
    public void StopGame()
    {
        Application.Quit();
    }
    public void SettingsScreen()
    {
        Setting_Screen.SetActive(true);
    }
    public void Back()
    {
        Setting_Screen.SetActive(false);
    }
    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused) Resume();
            else Pause();
        }
    }
    public void Pause()
    {
        if (Pause_Screen != null) Pause_Screen.SetActive(true);

        Time.timeScale = 0f;
        GamePaused = true;

        // Optional: Lock cursor for menus
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Resume()
    {
        if (Pause_Screen != null)
            Pause_Screen.SetActive(false);

        Time.timeScale = 1f; // Resume game time
        GamePaused = false;

        // Optional: Hide cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        Title_Screen.SetActive(true);
        if (Pause_Screen != null) Pause_Screen.SetActive(false);
    }
}
