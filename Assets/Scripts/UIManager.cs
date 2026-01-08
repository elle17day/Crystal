using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Create UI manager instance
    public static UIManager Instance;
    public GameObject Pause_Screen;
    public GameObject PlayerUI;
    public GameObject Setting_Screen;
    public GameObject Title_Screen;

    public GameObject BasicLinePanel;
    public GameObject APPanel;
    public GameObject MultiPanel;
    public GameObject RepeaterPanel;

    public GameObject XButton;
    private bool GamePaused;
    private void Awake()
    {
        // If there is no instance yet, this is the one
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {

            Destroy(gameObject);
        }
    }
    

    
    public void RestartGame()
    {
        PlayerUI.SetActive(true);
        if (Title_Screen != null) Title_Screen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void PlayGame()
    {
        PlayerUI.SetActive(true);
        if (Title_Screen != null) Title_Screen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (BasicLinePanel != null) BasicLinePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        if (Pause_Screen != null) Pause_Screen.SetActive(false);

        Time.timeScale = 1f; // Resume game time
        GamePaused = false;

        // Optional: Hide cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CloseButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        BasicLinePanel.SetActive(false);
        APPanel.SetActive(false);
        MultiPanel.SetActive(false);
        RepeaterPanel.SetActive(false);

    }
    public void MainMenu()
    {
        Title_Screen.SetActive(true);
        if (Pause_Screen != null) Pause_Screen.SetActive(false);
        if (PlayerUI != null) PlayerUI.SetActive(false);
    }
    public void APPanuel()
    {
        APPanel.SetActive(true);
        if (BasicLinePanel != null) BasicLinePanel.SetActive(false);
    }

    public void MultiPanuel()
    {
        MultiPanel.SetActive(true);
        if (BasicLinePanel != null) BasicLinePanel.SetActive(false);
    }

    public void RepeaterPanuel()
    {
        RepeaterPanel.SetActive(true);
        if (BasicLinePanel != null) BasicLinePanel.SetActive(false);
    }
}