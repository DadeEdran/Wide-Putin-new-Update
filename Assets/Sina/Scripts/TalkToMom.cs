using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;

public class TalkToMom : MonoBehaviour
{

    private StarterAssetsInputs starterAssetsInput;
    public string TagName = "Player";
    [SerializeField] private GameObject KeyShowUi = null;
    public Player player = null;

    public GameObject UI = null;

    public Text textbox=null;

    public BoxCollider boxcolider_keybox = null;

    public BoxCollider my_boxcolider = null;

    public string speak1 = "hello sir ,My daughter lost";
    public string speak2 = "you can find a key inside the box";

    int lvl = 0;
    bool tmp = false;

    



    private void Awake()
    {
        starterAssetsInput = FindObjectOfType<StarterAssetsInputs>();


    }

    private void Start()
    {
        player = FindObjectOfType<Player>();



    }

    private void Update()
    {
        if (player.loading == true &&tmp==false )
        {
            tmp = true;
            if (player.quiz[5] == 1 && player.quiz[6] == 0)
            {
                lvl = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            KeyShowUi.SetActive(true);
            if (starterAssetsInput.E)
            {

                if(player.quiz[5] == 1 && player.quiz[6] == 0)
                {
                    starterAssetsInput.E = false;
                    if (lvl == 0)
                    {
                        UI.SetActive(true);
                        textbox.text = speak1;
                        lvl++;
                    }else if (lvl == 1)
                    {
                        textbox.text = speak2;
                        lvl++;
                    }else if (lvl == 2)
                    {
                        player.quizController.setNewQuiz(player.quiz_name[9]);
                        boxcolider_keybox.enabled = true;
                        my_boxcolider.enabled = false;
                        KeyShowUi.SetActive(false);
                        lvl++;
                        UI.SetActive(false);
                        starterAssetsInput.E = false;
                    }
                  
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            KeyShowUi.SetActive(false);
        }
    }
}
