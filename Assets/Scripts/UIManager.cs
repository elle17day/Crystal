using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Create UI manager instance
    public static UIManager Instance;
    public GameObject Pause_Screen;
    public GameObject PlayerUI;
    public GameObject Setting_Screen;
    public GameObject Title_Screen;
    public GameObject Info_Screen;
    public GameObject GameOver;
    public GameObject EndofLevel;

    // Tower Panels
    public GameObject BasicLinePanel;
    public GameObject APPanel;
    public GameObject MultiPanel;
    public GameObject RepeaterPanel;

    private bool GamePaused;
    public bool isInUI = true;
    public GameObject Crystal;
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
    private void Start()
    {
        
    }

    public static bool gameStart()
    {
        bool isInUI = false;
        return isInUI;
    }
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name); //reset game
        PlayerUI.SetActive(true);
        if (Title_Screen != null) Title_Screen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Locks curser
    }
    public void PlayGame() // close titel screen and show Player UI
    {
        PlayerUI.SetActive(true);
        if (Title_Screen != null) Title_Screen.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void StopGame()
    {
        Application.Quit();
    }
    public void SettingsScreen()
    {
        Setting_Screen.SetActive(true);
    }

    public void InfoScreen()
    {
        Info_Screen.SetActive(true);
    }
    public void Back() //Close current panel for Setting and Info
    {
        Setting_Screen.SetActive(false);
        Info_Screen.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Press escap for pause menu
        {
            if (GamePaused) Resume();
                else Pause();
        }
    }
    public void Pause()
    {
        if (Pause_Screen != null) Pause_Screen.SetActive(true);
        Time.timeScale = 0f; //stop all movement
        GamePaused = true;

        Cursor.lockState = CursorLockMode.None; // Unlock curser in pause menu
        Cursor.visible = true;
    }

    public void Resume()
    {
        if (Pause_Screen != null) Pause_Screen.SetActive(false);

        Time.timeScale = 1f; // Resume game time
        GamePaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; //Hide cursor for gameplay
    }

    public void CloseButton() //Close Tower panels
    {
        Cursor.lockState = CursorLockMode.Locked;
        BasicLinePanel.SetActive(false);
        APPanel.SetActive(false);
        MultiPanel.SetActive(false);
        RepeaterPanel.SetActive(false);

    }
    public void MainMenu() // Return to main menu and close other panels
    {
        Title_Screen.SetActive(true);
        if (Pause_Screen != null) Pause_Screen.SetActive(false);
        if (PlayerUI != null) PlayerUI.SetActive(false);
        if (GameOver != null) GameOver.SetActive(false);
        if (EndofLevel != null) EndofLevel.SetActive(false);
    }
    public void LinePanuel()
    {
        BasicLinePanel.SetActive(true);
    }
    public void APPanuel()
    {
        APPanel.SetActive(true);
    }

    public void MultiPanuel()
    {
        MultiPanel.SetActive(true);
    }

    public void RepeaterPanuel()
    {
        RepeaterPanel.SetActive(true);
    }
}