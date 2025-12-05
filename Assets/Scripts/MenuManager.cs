using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private bool isPauseMenu;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!isPauseMenu) SetMainPanel();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMainPanel()
    {
        if (isPauseMenu)
        {
            if (pausePanel) pausePanel.SetActive(true);
            if (mainPanel) mainPanel.SetActive(false);
        }
        else
        {
            if (mainPanel) mainPanel.SetActive(true);
            if (pausePanel) pausePanel.SetActive(false);
        }

        settingsPanel.SetActive(false);
    }

    public void SetSettingsPanel()
    {
        settingsPanel.SetActive(true);
        if (isPauseMenu)
            if (pausePanel) pausePanel.SetActive(false);
            else if (mainPanel) mainPanel.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
        // SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        SetMainPanel();
    }

    public void UnPause()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SwitchPause()
    {
        if (Time.timeScale > 0.0f)
            Pause();
        else
            UnPause();
    }
}