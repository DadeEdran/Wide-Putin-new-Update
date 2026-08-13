using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class tutorial_controller : MonoBehaviour
{
    // Start is called before the first frame update

    public string title = "name";
    public string description = "Description";
    CanvasGroup cgroup_tutorial = null;
    public Sprite Mana = null;
    public Sprite Heal = null;
    public Sprite Magazine = null;
    public GameObject Image = null;
    public Text text1 = null;
    public Text text2 = null;
    public GameObject uikeyshow = null;


    private StarterAssetsInputs starterAssetsInput;

    private void Awake()
    {
        starterAssetsInput = FindObjectOfType<StarterAssetsInputs>();
    }

    private void Start()
    {
        cgroup_tutorial = GetComponent<CanvasGroup>();
    }

    public void set_tutorial(int set)
    {
        if (set == 0)
        {
            Image.GetComponent<Image>().sprite = Heal;
        }
        else if (set == 1)
        {
            Image.GetComponent<Image>().sprite = Mana;
        }
        else if (set == 2)
        {
            Image.GetComponent<Image>().sprite = Magazine;
        }
        text1.text = title;
        text2.text = description;


        cgroup_tutorial.alpha = 1.0f;

    }

    public void dis_tutorial()
    {
        cgroup_tutorial.alpha = 0f;
    }

    private void Update()
    {
        if (starterAssetsInput.Enter) { dis_tutorial(); Time.timeScale = 1f; }
    }

}
