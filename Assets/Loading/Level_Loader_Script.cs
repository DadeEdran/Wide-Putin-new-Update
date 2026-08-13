using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class Level_Loader_Script : MonoBehaviour
{
    public Animator animator;
    private static bool enter = false;
    public float animation_time = 1f;
    private static string LevelNameForChange = "S1";
    Player player = null;
    private StarterAssetsInputs starterAssetsInput;
    public static int level = 0;
    public static bool enterCK
    {
        get { return enter; }
        set { enter = value; }
    }

    public static string NameLevel
    {
        get { return LevelNameForChange; }
        set { LevelNameForChange = value; }
    }

    private void Awake()
    {
        starterAssetsInput = FindObjectOfType<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInput.E && enter)
        {
            starterAssetsInput.E = false;
            LoadLevel();
            Player.tempLevel = level;
            if (LevelNameForChange == "Level_Extera" || LevelNameForChange == "Level_2")
                Player.ck_update_bool = true;

        }

    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        Time.timeScale = 1f;
    }
    public void LoadLevel()
    {

        StartCoroutine(LoadLevel(LevelNameForChange));
    }

    IEnumerator LoadLevel(string LevelIndex)
    {

        animator.SetTrigger("start");

        yield return new WaitForSeconds(animation_time);

        SceneManager.LoadScene(LevelIndex);

    }
}
