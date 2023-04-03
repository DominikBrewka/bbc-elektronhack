using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool IsPaused = false;
    public GameObject pauseUI;
    public FPSController controller;
    PlayerManager playerManager;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        playerManager = (PlayerManager)ManagerObject.gameStateManger.GetManager<PlayerManager>();
        controller = GameObject.Find("Player").GetComponent<FPSController>();
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        controller.isInputDisabled = true;
        playerManager.cursorLock = true;
    }

    public void Resume()
    {
        playerManager = (PlayerManager)ManagerObject.gameStateManger.GetManager<PlayerManager>();
        controller = GameObject.Find("Player").GetComponent<FPSController>();
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        controller.isInputDisabled = false;
        playerManager.cursorLock = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
