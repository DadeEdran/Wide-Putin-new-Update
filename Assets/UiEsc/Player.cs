using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{


    [SerializeField] public static int tempLevel = -1;
    public int test = 1;
    public bool die_var = false;
    public bool debug = false;
    public static bool Newgame = false;
    public int level = 0;
    public int health = 10;
    public int mana = 0;
    public int Bullet_1 = 10;
    public int M_Bullet_1 = 10;
    public int Bullet_2 = 10;
    public int M_Bullet_2 = 10;
    public int Select_Gun;
    public shooting gun1 = null;
    public ThirdPersonShootingController TPSH = null;



    public bool key = false;

    public AudioManager audioManager = null;

    //public int QuizCount=5;
    public int[] quiz;
    public static int[] quiz_temp = new int[20];


    public static int colt_magazin = 0;
    public static int colt_bullet = 0;


    public static int ak_magazin = 0;
    public static int ak47_bullet = 0;


    public static int ppsh_magazin = 0;
    public static int ppsh_bullet = 0;





    public string[] quiz_name;
    public static bool ck_update_bool = false;

    public PlayerInfo playerinfo;
    public QuizController quizController;

    [Header("Level1")]
    //vido player
    public GameObject video1 = null;
    //
    public CanvasGroup die = null;
    //turn off all canves minimap , ...
    public CanvasGroup canGroup = null;
    public CanvasGroup canGroup2 = null;
    //start point
    public Transform startpoint = null;
    //TimeVideo
    public float TimeVideo = 20f;
    //Enemy Count
    public int Enemy_Count = 2;
    //Enemys
    public EnamyInfo[] enemys;
    public bool Enemy_Active_1 = true;
    public GameObject GymLocation = null;
    [Header("Level2")]
    public SaveBoda saveboda = null;
    public GameObject BodaLocation = null;
    public EnamyInfo[] Boda_Enemies;
    public int Boda_enemy_count = 4;


    // Other enemies for save and Load game / Show 2D array in inspector ! IMPORTANT 
    [System.Serializable]
    public class MultiDimensionalInt
    {
        public EnamyInfo Enemy;
        public bool Dead = false;
    }
    public MultiDimensionalInt[] AllAIEnemeis;
    // -----------------------------------




    [Header("Level3")]
    public Transform shortforoshi_start_position = null;
    public Transform shortforoshi_Out_position = null;
    public GameObject LocationShortfroshi = null;
    public static bool TalkToShortFroshi = false;
    public bool talk = false;
    [Header("Level3")]
    public int enemy_short_count = 1;
    public EnamyInfo[] enemys_short;
    public int takeshort = 5;
    public bool active_short_enemy = false;
    [Header("Level4")]
    public int enemy_white_house_count = 1;
    public EnamyInfo[] enemys_white_house;
    public bool key_enter = false;
    bool status_enemy_whitehouse = false;



    [Header("Level white house")]
    public Transform white_house_start_position = null;


    public int enemy_blueroom_count = 2;
    public EnamyInfo[] enemys_blueroom;
    public bool active_blueroom_enemy = false;


    [Header("White House Video Clip")]
    public GameObject wh_video_obj = null;
    public float TimeVideo_wh = 20f;
    public bool loading = false;
    public bool keyforredroom = false;
    public BoxCollider redroomboxcolider = null;



    public static int levelset
    {
        get { return tempLevel; }
        set { tempLevel = value; }
    }


    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }



    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        level = data.level;
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

        if (data.Quiz != null)
            quiz = new int[data.Quiz.Length];
        else
            quiz = new int[0];

        // quiz = new int[data.Quiz.Length];
        for (int i = 0; i < quiz.Length; i++)
            quiz[i] = data.Quiz[i];

    }

    public void exit()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void update_quiz_temp()
    {
        //quiz temp update

        for (int i = 0; i <= quiz.Length - 1; i++)
        {
            quiz_temp[i] = quiz[i];
        }
        //quiz temp update

        shooting a = TPSH.guncolt0.GetComponent<shooting>();
        colt_bullet = a.bulletsleft;
        colt_magazin = a.Magazine_Bullet;

    }

    public void reverse_update_quiz_temp()
    {
        if (ck_update_bool == false)
            return;
        //quiz temp update

        for (int i = 0; i <= quiz.Length - 1; i++)
        {
            quiz[i] = quiz_temp[i];
        }
        //quiz temp update

        TPSH.guncolt0.GetComponent<shooting>().bulletsleft = colt_bullet;
        TPSH.guncolt0.GetComponent<shooting>().Magazine_Bullet = colt_magazin;

    }


    private void Start()
    {



        TPSH = this.GetComponent<ThirdPersonShootingController>();


        if (level == 0)
        { //LEVEL 0 IS WHITE HOUSE 
            EnamyInfo[] tt = FindObjectsOfType<EnamyInfo>();
            for (int i = 0; i < tt.Length; i++)
            {

            }
        }

        if (debug == true)
            tempLevel = test;
        //Debug.Log(Application.persistentDataPath);

        // health= this.GetComponent<PlayerInfo>().Add_Heal;


        playerinfo = GetComponent<PlayerInfo>();

        quiz_name = new string[15];
        quiz_name[0] = "Kill 6 Enemy Around Gym";
        quiz_name[1] = "kill Some Enemies then Go Boda And Save Your Game";
        quiz_name[2] = "Go Talk to Short Shop Man ";
        quiz_name[3] = "Kill Enemies and Take " + enemy_short_count + " Shorts ";
        quiz_name[4] = "Give Shorts To Recardo";
        quiz_name[5] = "Take PPSH Gun";
        quiz_name[6] = "Go Near to WhiteHouse And kill 6 Enemies And Find Key";
        quiz_name[7] = "Go To WhiteHouse";
        quiz_name[8] = "Find and Talk To StepMom";
        quiz_name[9] = "Find The Key Inside The Box";
        quiz_name[10] = "Go To Blue Room";
        quiz_name[11] = "Go to RedRoom";







        enemys = new EnamyInfo[Enemy_Count];
        for (int i = 0; i < enemys.Length; i++)
        {
            if (GameObject.Find("Enemy" + i))
                enemys[i] = GameObject.Find("Enemy" + i).GetComponent<EnamyInfo>();
            else
                Debug.Log("Didn't find EnemyGym: " + i);
            // if (enemys[i].enemydead) Debug.Log("dead");
        }


        Boda_Enemies = new EnamyInfo[Boda_enemy_count];
        for (int i = 0; i < Boda_Enemies.Length; i++)
        {
            if (GameObject.Find("Enemy_Ai_Boda_" + i))
                Boda_Enemies[i] = GameObject.Find("Enemy_Ai_Boda_" + i).GetComponent<EnamyInfo>();
            else
                Debug.Log("Didn't find Boda_enemy: " + i);
            // if (enemys[i].enemydead) Debug.Log("dead");
        }


        enemys_short = new EnamyInfo[enemy_short_count];
        for (int i = 0; i < enemys_short.Length; i++)
        {
            if (GameObject.Find("EnemyShort" + i))
                enemys_short[i] = GameObject.Find("EnemyShort" + i).GetComponent<EnamyInfo>();
            else
            {
                Debug.Log("Didn't find EnemyShort: " + i);
                active_short_enemy = true;
            }

            // if (enemys[i].enemydead) Debug.Log("dead");
        }



        enemys_blueroom = new EnamyInfo[enemy_blueroom_count];
        for (int i = 0; i < enemys_blueroom.Length; i++)
        {
            if (GameObject.Find("EnemyBlueRoom" + i))
                enemys_blueroom[i] = GameObject.Find("EnemyBlueRoom" + i).GetComponent<EnamyInfo>();
            else
            {
                Debug.Log("Didn't find EnemyBlueRoom: " + i);
                active_blueroom_enemy = true;
            }

            // if (enemys[i].enemydead) Debug.Log("dead");
        }




        if (active_short_enemy == false)
        {
            for (int i = 0; i < enemys_short.Length; i++)
            {
                enemys_short[i].gameObject.SetActive(false);
            }
        }



        enemys_white_house = new EnamyInfo[enemy_white_house_count];
        for (int i = 0; i < enemys_white_house.Length; i++)
        {
            if (GameObject.Find("EnemyWhiteHouse" + i))
                enemys_white_house[i] = GameObject.Find("EnemyWhiteHouse" + i).GetComponent<EnamyInfo>();
            else
                Debug.Log("Didn't find EnemyWhiteHouse: " + i);
            // if (enemys[i].enemydead) Debug.Log("dead");
        }




        if (!Newgame)
        {
            PlayerData data = SaveSystem.LoadPlayer();
            level = data.level;
            health = data.health;
            playerinfo.init_Heal = health; playerinfo.init_Mana = mana; //init playerinfo

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;


            if (data.Quiz != null)
                quiz = new int[data.Quiz.Length];
            else
                quiz = new int[0];

            for (int i = 0; i < quiz.Length; i++)
                quiz[i] = data.Quiz[i];
        }





        if (Newgame)
        {
            if (level != 0)
            {
                level = 0;
                for (int i = 0; i < quiz.Length; i++)
                    quiz[i] = 0;
            }
        }

        if (tempLevel != -1)
            level = tempLevel;

        //level0
        if (ck_update_bool)
            reverse_update_quiz_temp();///////////////////////////////////////
        Level_Loader_Script.enterCK = false;
        if (level == 0)
        {
            disable_enemy_whitehouse();
            if (quiz[0] == 0) { TPSH.gun_unlock = 0; audioManager.Mute_all(); video1.SetActive(true); quiz[0] = 1; Destroy(video1, TimeVideo); canGroup.alpha = 0; canGroup2.alpha = 1; transform.position = startpoint.position; health = 10; mana = 0; Player.Newgame = false; level = 0; playerinfo.init_Heal = health; playerinfo.init_Mana = mana; quizController.setNewQuiz(quiz_name[0]); SavePlayer(); } //init level

            if (tempLevel == 0) { this.transform.position = shortforoshi_Out_position.position; }

            // When Load Game disable enemy1
            if (quiz[1] == 1)
            {
                for (int i = 0; i < enemys.Length; i++)
                {
                    enemys[i].gameObject.SetActive(false);
                }
                Enemy_Active_1 = false;
            }

            if (quiz[1] == 1 && quiz[2] == 2) { quizController.setNewQuiz(quiz_name[1]); }


            if (quiz[1] == 1 && quiz[2] == 3) { quizController.setNewQuiz(quiz_name[2]); }
            if (quiz[2] == 1 && quiz[3] == 0)
            {
                quizController.setNewQuiz(quiz_name[3]);

                // When Load Game disable enemyshort
                if (active_short_enemy == false)
                {
                    for (int i = 0; i < enemys_short.Length; i++)
                    {
                        enemys_short[i].gameObject.SetActive(true);
                    }
                    active_short_enemy = true;
                }

            }

            if (quiz[4] == 1) { TPSH.gun_unlock = 1; }

        }
        else if (level == 1)
        {
            transform.position = shortforoshi_start_position.position;
            if (quiz[1] == 1 && quiz[2] == 3) { quizController.setNewQuiz(quiz_name[2]); }
            if (quiz[2] == 1 && quiz[3] == 0) { quizController.setNewQuiz(quiz_name[3]); }

            if (quiz[3] == 1 && quiz[4] == 0) { quizController.setNewQuiz(quiz_name[4]); }
        }
        else if (level == 2)
        {


            if (quiz[5] == 1 && quiz[6] == 0)
            {
                transform.position = white_house_start_position.position;
                audioManager.Mute_all();
                wh_video_obj.SetActive(true);
                Destroy(wh_video_obj, TimeVideo_wh);
                quizController.setNewQuiz(quiz_name[8]);
                canGroup.alpha = 0; canGroup2.alpha = 1;
                SavePlayer();
                loading = true;

            }




        }
        // q0





        //quiz_temp = new int[quiz.Length];
        //reverse_update_quiz_temp();


    }



    private void Update()
    {
        if (health <= 0 && die_var == false)
        {
            die_var = true;
            die.alpha = 1;
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


        if (level == 0)//hayate bozorg
        {
            update_quiz_temp();



            // When Load Game disable enemy1
            if (Enemy_Active_1 == true)
            {
                //q 1

                if (quiz[0] == 1 && quiz[1] == 0)
                {
                    GymLocation.SetActive(true);
                    int temp_count = 0;
                    for (int i = 0; i < enemys.Length; i++)
                    {
                        if (enemys[i].enemydead) { temp_count++; }
                    }
                    quizController.ReWriteQuiz("Kill " + (Enemy_Count - temp_count).ToString() + " Enemy Around Gym");
                    quizController.setQuiz(0, false);
                    if ((Enemy_Count - temp_count) == 0) { quiz[1] = 1; quizController.ReWriteQuiz(quiz_name[0]); }
                }
            }



            //q2
            if (quiz[1] == 1 && quiz[2] == 0)
            {
                GymLocation.SetActive(false);
                BodaLocation.SetActive(true);
                quizController.setNewQuiz(quiz_name[1]);
                quiz[2] = 2;

            }

            if (quiz[1] == 1 && quiz[2] == 2) { if (saveboda.saveForsave == true) { quiz[2] = 3; SavePlayer(); quizController.setNewQuiz(quiz_name[2]); BodaLocation.SetActive(false); LocationShortfroshi.SetActive(true); ck_update_bool = true; } }

            if (quiz[2] == 1 && quiz[3] == 0)
            {
                if (active_short_enemy == true)
                {
                    int temp_count = 0;
                    for (int i = 0; i < enemys_short.Length; i++)
                    {
                        if (enemys_short[i].enemydead) { temp_count++; }
                    }
                    quizController.ReWriteQuiz("Kill Enemies " + (enemy_short_count - temp_count).ToString() + "and Take" + takeshort + " Shorts ");
                    if ((enemy_short_count - temp_count) == 0 && takeshort == 0) { quiz[3] = 1; quizController.setNewQuiz(quiz_name[4]); }
                }

            }


            ///////sadasdadadas
            if (quiz[4] == 1 && quiz[5] == 0)
            {
                if (status_enemy_whitehouse == false) active_enemy_whitehouse();
                int temp_count = 0;
                for (int i = 0; i < enemys_white_house.Length; i++)
                {
                    if (enemys_white_house[i].enemydead) { temp_count++; }
                }
                quizController.ReWriteQuiz("Go Near to WhiteHouse And kill " + (enemy_white_house_count - temp_count).ToString() + "Enemies And Find Key");
                if ((enemy_white_house_count - temp_count) == 0 && key_enter) { quiz[5] = 1; quizController.setNewQuiz(quiz_name[7]); }

            }


        }
        else if (level == 1) //short froshi
        {
            update_quiz_temp();
            if (quiz[1] == 1 && quiz[2] == 3) { if (talk == true) { quiz[2] = 1; quizController.setNewQuiz(quiz_name[3]); } }
            if (quiz[2] == 1 && quiz[3] == 0) { }

        }
        else if (level == 2) //white house
        {

            if (quiz[5] == 1 && quiz[6] == 0)
            {

            }


            if (quiz[6] == 1 && quiz[7] == 0)
            {
                int temp_count = 0;
                for (int i = 0; i < enemys_blueroom.Length; i++)
                {
                    if (enemys_blueroom[i].enemydead) { temp_count++; }
                }
                quizController.ReWriteQuiz("Go To BlueRoom And Kill " + (enemy_blueroom_count - temp_count).ToString() + "Enemies And Find Key");
                if (keyforredroom == true && enemy_blueroom_count - temp_count == 0)
                {
                    quizController.setNewQuiz(quiz_name[11]);
                    quiz[7] = 1;
                    redroomboxcolider.enabled = true;
                }


            }







        }


        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    audioManager.UnMute_all();
        //}



    }




    //public int Add_bullet_gun_1
    //{
    //    set { M_Bullet_1 = value; update_gun(); }
    //    get { return M_Bullet_1; }
    //}
    //public int Add_bullet_gun_2
    //{
    //    set { M_Bullet_2 = value; }
    //    get { return M_Bullet_2; }
    //}

    //public void update_gun()
    //{
    //    this.gun1.Magazine_Bullet =+ M_Bullet_1;
    //    M_Bullet_1 = 0;
    //}

    public void add_Magazine(int m)
    {
        gun1.Magazine_Bullet = gun1.Magazine_Bullet + m;
    }
    public void Dec_Short()
    {
        if (takeshort > 0)
        {
            takeshort--;
        }

    }


    public void disable_enemy_whitehouse()
    {
        for (int i = 0; i < enemys_white_house.Length; i++)
        {
            enemys_white_house[i].gameObject.SetActive(false);
        }
        status_enemy_whitehouse = false;
    }

    public void active_enemy_whitehouse()
    {
        for (int i = 0; i < enemys_white_house.Length; i++)
        {
            enemys_white_house[i].gameObject.SetActive(true);
        }
        status_enemy_whitehouse = false;
    }
    public void newgame()
    {
        level = 0;
        for (int i = 0; i < quiz.Length; i++)
            quiz[i] = 0;

        SavePlayer();

    }


    public void loadgame()
    {


        //PlayerData data = SaveSystem.LoadPlayer();
        //level = data.level;
        //health = data.health;
        //playerinfo.init_Heal = health; playerinfo.init_Mana = mana; //init playerinfo

        //Vector3 position;
        //position.x = data.position[0];
        //position.y = data.position[1];
        //position.z = data.position[2];
        //transform.position = position;


        //if (data.Quiz != null)
        //    quiz = new int[data.Quiz.Length];
        //else
        //    quiz = new int[0];

        //for (int i = 0; i < quiz.Length; i++)
        //    quiz[i] = data.Quiz[i];
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //die_var = false;
        //die.alpha = 0;
        //Time.timeScale = 1f;
    }






















}
