using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUi;
    public ThirdPersonController thirdPersonController = null;
    private StarterAssetsInputs starterAssetsInput;

    private void Awake()
    {
        starterAssetsInput = FindObjectOfType<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInput.Escape)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            starterAssetsInput.Escape = false;
        }
    }

    void Resume()
    {
        thirdPersonController.LockCameraPosition = false;
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<AudioManager>().UnMute_all();

    }


    void Pause()
    {
        thirdPersonController.LockCameraPosition = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<AudioManager>().Mute_all();

    }

    private void OnGUI()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }





}
