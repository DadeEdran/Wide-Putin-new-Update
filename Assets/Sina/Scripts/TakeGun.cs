using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class TakeGun : MonoBehaviour
{

    public string TagName = "Player";
    [SerializeField] private GameObject KeyShowUi = null;
    public Player player = null;
    private StarterAssetsInputs starterAssetsInput;

    private void Awake()
    {
        starterAssetsInput = FindObjectOfType<StarterAssetsInputs>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            KeyShowUi.SetActive(true);
            if (starterAssetsInput.E)
            {
                if (player.quiz[4] == 2 && player.quiz[3] == 1) { player.TPSH.gun_unlock = 1; player.quiz[4] = 1; Destroy(this.gameObject); player.quizController.setNewQuiz(player.quiz_name[6]); KeyShowUi.SetActive(false); }
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
