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

    public GameObject BasicLinePanel;
    public GameObject APPanel;
    public GameObject MultiPanel;
    public GameObject RepeaterPanel;

    public GameObject BasicCirclePanel;
    public GameObject IcePanel;
    public GameObject FirePanel;
    public GameObject PoisenPanel;

    public GameObject XButton;

    private bool GamePaused;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        if (PlayerUI != true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GamePaused) Resume();
                else Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (BasicLinePanel != null) BasicLinePanel.SetActive(true);
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
        Cursor.lockState = CursorLockMode.None;
        //if (BasicCirclePanel != null) 
            BasicCirclePanel.SetActive(false);
        //if (FirePanel != null)
            FirePanel.SetActive(false);
        //if (IcePanel != null) 
            IcePanel.SetActive(false);
        if (PoisenPanel != null) PoisenPanel.SetActive(false);
        if (BasicLinePanel != null) BasicLinePanel.SetActive(false);
        if (APPanel != null) APPanel.SetActive(false);
        if (MultiPanel != null) MultiPanel.SetActive(false);
        if (RepeaterPanel != null) RepeaterPanel.SetActive(false);

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
    public void IcePanuel()
    {
        IcePanel.SetActive(true);
        if (BasicCirclePanel != null) BasicCirclePanel.SetActive(false);
    }
    public void FirePanuel()
    {
        FirePanel.SetActive(true);
        if (BasicCirclePanel != null) BasicCirclePanel.SetActive(false);
    }
    public void PoisenPanuel()
    {
        PoisenPanel.SetActive(true);
        if (BasicCirclePanel != null) BasicCirclePanel.SetActive(false);
    }
}
