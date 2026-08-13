using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class roomkey : MonoBehaviour
{
    private StarterAssetsInputs starterAssetsInput;
    public Player player = null;
    public string TagName = "Player";
    public BoxCollider bc = null;
    public BoxCollider my_bc = null;
    [SerializeField] private GameObject KeyShowUi = null;
    public GameObject key = null;





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
        if (player.quiz[6] == 1) { Destroy(this); }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            KeyShowUi.SetActive(true);
            if (starterAssetsInput.E)
            {
                player.quizController.setNewQuiz(player.quiz_name[10]);
                bc.enabled = true;
                my_bc.enabled = false;
                KeyShowUi.SetActive(false);
                Destroy(key);
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
