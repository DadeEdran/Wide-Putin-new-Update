using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour  
{
    public CanvasGroup canvasgroup;
    public float opacity=0;
    public float speedopacity=0.4f;
	

    //Heal Pictures
    public Sprite heal_0;
    public Sprite heal_10;
    public Sprite heal_20;
    public Sprite heal_30;
    public Sprite heal_40;
    public Sprite heal_50;
    public Sprite heal_60;
    public Sprite heal_70;
    public Sprite heal_80;
    public Sprite heal_90;
    public Sprite heal_100;
    [Range(0, 100)]
    [SerializeField] private int HealPlayer = 10;

    //Mana Pictures
    public Sprite Mana_0;
    public Sprite Mana_1;
    public Sprite Mana_2;
    public Sprite Mana_3;
    public Sprite Mana_4;

    [Range(0, 4)]
    [SerializeField] private int ManaPlayer = 0;




    //[SerializeField] private GameObject HealBarUi;
    [SerializeField] private GameObject HealBarUiImage=null;
    [SerializeField] private GameObject ManaBarUiImage=null;

     Player player = null;

    public Text HealShowText =null;
    public Text ManaShowText=null;




    public int Add_Heal {
        get { return HealPlayer; }
        set { if (HealPlayer < 100) { HealPlayer = HealPlayer + value; } }
    }


    public int Neg_Heal
    {
        get { return HealPlayer; }
        set { if (!(HealPlayer<=0)) { HealPlayer = HealPlayer - value; canvasgroup.alpha = 1f; int tmp = Random.Range(0, 3); FindObjectOfType<AudioManager>().Play("Damage"+tmp); } }
    }



    public int Add_Mana
    {
        get { return ManaPlayer; }
        set { if (ManaPlayer < 4) { ManaPlayer = ManaPlayer + value;  } }
    }


    public int Neg_Mana
    {
        get { return ManaPlayer; }
        set { if (!(ManaPlayer <= 0)) { ManaPlayer = ManaPlayer - value; } }
    }


    public int init_Heal
    {
        set { HealPlayer = value; }
        get { return HealPlayer; }
    }
    public int init_Mana
    {
        set { ManaPlayer = value; }
        get { return ManaPlayer; }
    }


    // Text txt;




    // Start is called before the first frame update
    void Start()
    {
        player=GetComponent<Player>();
        //init_Heal = player.health;
        //init_Mana = player.mana;
       //txt= HealBarUi.GetComponent<Text>();

        //txt.text = life.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        Manachack();
        HealChack();
        if (canvasgroup.alpha > 0f)
        {
           canvasgroup.alpha = canvasgroup.alpha - speedopacity * Time.deltaTime;
        }



        //txt.text = life.ToString()+"%";


    }






    void Manachack()
    {
        player.mana = init_Mana;
        ManaShowText.text = init_Mana.ToString();
        switch (ManaPlayer)
        {
            case 0:
                ManaBarUiImage.GetComponent<Image>().sprite = Mana_0;
                break;
            case 1:
                ManaBarUiImage.GetComponent<Image>().sprite = Mana_1;
                break;
            case 2:
                ManaBarUiImage.GetComponent<Image>().sprite = Mana_2;
                break;
            case 3:
                ManaBarUiImage.GetComponent<Image>().sprite = Mana_3;
                break;
            case 4:
                ManaBarUiImage.GetComponent<Image>().sprite = Mana_4;
                break;
            default:
                break;
        }
    }

    void HealChack()
    {
        player.health = init_Heal;
        HealShowText.text = init_Heal.ToString()+"%";
        switch (HealPlayer)
        {
            case 0:
                HealBarUiImage.GetComponent<Image>().sprite = heal_0;
                break;
            case 10:
                HealBarUiImage.GetComponent<Image>().sprite = heal_10;
                break;
            case 20:
                HealBarUiImage.GetComponent<Image>().sprite = heal_20;
                break;
            case 30:
                HealBarUiImage.GetComponent<Image>().sprite = heal_30;
                break;
            case 40:
                HealBarUiImage.GetComponent<Image>().sprite = heal_40;
                break;
            case 50:
                HealBarUiImage.GetComponent<Image>().sprite = heal_50;
                break;
            case 60:
                HealBarUiImage.GetComponent<Image>().sprite = heal_60;
                break;
            case 70:
                HealBarUiImage.GetComponent<Image>().sprite = heal_70;
                break;
            case 80:
                HealBarUiImage.GetComponent<Image>().sprite = heal_80;
                break;
            case 90:
                HealBarUiImage.GetComponent<Image>().sprite = heal_90;
                break;
            case 100:
                HealBarUiImage.GetComponent<Image>().sprite = heal_100;
                break;
            default:
                break;
        }
    }

}




//[SerializeField] private GameObject test;
//playerInfo info = test.GetComponent<PlayerInfo>();
//if (Input.GetKeyDown(KeyCode.G)) { info.Add_Heal = 10; }
//if (Input.GetKeyDown(KeyCode.F)) { info.Neg_Heal = 10; }