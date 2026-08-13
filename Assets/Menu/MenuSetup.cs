using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuSetup : MonoBehaviour
{
    public GameObject btn_newgame = null;
    public Text Continue = null;

    public void PlayGame()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (!File.Exists(path))
        {
            Player.Newgame = true;
            SceneManager.LoadScene(0);
        }
        else
        {
            if (Player.Newgame)
            {
                Player.Newgame = true;
                SceneManager.LoadScene(0);
            }
            else
            {
                PlayerData data = SaveSystem.LoadPlayer();
                SceneManager.LoadScene(data.level);
            }

        }



    }
    public void QuitGame()
    {
        Application.Quit();
    }


    public void NewGame()
    {
        Player.Newgame = true;
        PlayGame();
    }


    private void Start()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (!File.Exists(path))
        {
            btn_newgame.SetActive(false);
            Continue.text = "PLAY";
        }
        else
        {
            PlayerData data = SaveSystem.LoadPlayer();
            if (data.Quiz[0] != 0)
            {
                btn_newgame.SetActive(true);
                Continue.text = "CONTINUE";

            }
            else
            {
                btn_newgame.SetActive(false);
                Continue.text = "PLAY";
            }
        }

    }




}

