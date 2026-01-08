using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<< Updated upstream
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.DebugUI.Table;
=======
using UnityEngine.UI;
>>>>>>> Stashed changes
public class UIManager : MonoBehaviour
{
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

<<<<<<< Updated upstream
    public GameObject BasicCirclePanel;
    public GameObject IcePanel;
    public GameObject FirePanel;
    public GameObject PoisenPanel;

    public GameObject XButton;

    private bool GamePaused;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
=======
    private bool GamePaused;
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
    
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name); //reset game
        PlayerUI.SetActive(true);
        if (Title_Screen != null) Title_Screen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Locks curser
>>>>>>> Stashed changes
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
    public void Back() //Close current panel
    {
        Setting_Screen.SetActive(false);
        Info_Screen.SetActive(false);
    }
    private void Update()
    {
<<<<<<< Updated upstream
        if (PlayerUI != true)
=======
        //HealthC = Crystal.GetComponent<Crystal>().

        if (Input.GetKeyDown(KeyCode.Escape)) // Press escap for pause menu
>>>>>>> Stashed changes
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GamePaused) Resume();
                else Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)) // show tower panels
        {
            if (BasicLinePanel != null) BasicLinePanel.SetActive(true);
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
    public void MainMenu() // Return to main menu and close other panels
    {
        Title_Screen.SetActive(true);
        if (Pause_Screen != null) Pause_Screen.SetActive(false);
        if (PlayerUI != null) PlayerUI.SetActive(false);
        if (GameOver != null) GameOver.SetActive(false);
        if (EndofLevel != null) EndofLevel.SetActive(false);
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
