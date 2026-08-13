using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class SubtitleConversation : MonoBehaviour
{
    public string[] subtitle;
    public string[] subtitle_gun;
    public string TagName = "Player";
    public int counter = -1;
    [SerializeField] private GameObject KeyShowUi = null;
    public CanvasGroup suntitle_vavasgroup = null;
    public Text txt1 = null;
    public Animator animator = null;
    public bool test_q = false;
    public Player player = null;

    public Transform gunLocation = null;
    public GameObject gun = null;
    private StarterAssetsInputs starterAssetsInput;

    private void Awake()
    {
        starterAssetsInput = FindObjectOfType<StarterAssetsInputs>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (counter <= subtitle.Length && counter >= 0)
        {
            if (player.quiz[4] == 0 && player.quiz[3] == 1)
            {
                txt1.text = subtitle_gun[counter];
            }
            else
            {
                txt1.text = subtitle[counter];
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
                if (counter == subtitle.Length - 1)
                {
                    counter = -1;
                    suntitle_vavasgroup.alpha = 0;
                    if (test_q == true)
                    {
                        player.talk = true;

                        if (player.quiz[4] == 0 && player.quiz[3] == 1) { player.quiz[4] = 2; gun.SetActive(true); player.quizController.setNewQuiz(player.quiz_name[5]); }
                    }


                }
                else
                {
                    animator.SetTrigger("Talk");
                    counter++;
                    suntitle_vavasgroup.alpha = 1;
                    test_q = true;

                }

                starterAssetsInput.E = false;
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
