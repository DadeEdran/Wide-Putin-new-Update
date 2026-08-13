using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;




public class ThirdPersonShootingController : MonoBehaviour
{


    [SerializeField]
    private CinemachineVirtualCamera aimVirtualCamera = null;
    private StarterAssetsInputs starterAssetsInput;

    [SerializeField]
    private float normalSensitivity = 1f;
    [SerializeField]
    private float aimSensitivity = 0.5f;
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField]
    private Transform Aim_Transform = null;

    public bool changing = false;


    private Animator animator;
    private ThirdPersonController thirdPersonController;

    [Header("Rig Setup")]
    //public Rig AimLayer = null;
    //public Rig NormalLayer = null;
    public RigBuilder rigbdr = null;

    public GameObject gunppsh0 = null;
    public GameObject gunppsh1 = null;
    public GameObject gunak470 = null;
    public GameObject gunak471 = null;
    public GameObject guncolt0 = null;
    public GameObject guncolt1 = null;

    public int Default_Gun = 0;


    bool Normal = false;
    bool Aim = false;
    bool Walk = false;
    bool run = false;
    bool Jump = false;

    int TempGun = -1;

    bool relod = false;

    public int gun_unlock = 0;





    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInput = FindObjectOfType<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        TempGun = Default_Gun;
        Def_gun();


    }

    private void Update()
    {
        if (Time.timeScale == 0f)
            return;

        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 9999f, aimColliderLayerMask))
        {
            if (starterAssetsInput.aim)
            {
                Aim_Transform.transform.position = raycastHit.point;
            }
            mouseWorldPosition = raycastHit.point;
        }

        if (!changing)
        {
            if (starterAssetsInput.aim)
            {

                //AimLayer.weight = 1f;
                //NormalLayer.weight = 0f;
                aimVirtualCamera.gameObject.SetActive(true);
                thirdPersonController.setSensitivity(aimSensitivity);
                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
                thirdPersonController.SetRotateOnMove(false);
                animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

                Aimstate();

            }
            else
            {
                //AimLayer.weight = 0f;
                //NormalLayer.weight = 1f;

                aimVirtualCamera.gameObject.SetActive(false);
                thirdPersonController.setSensitivity(normalSensitivity);
                thirdPersonController.SetRotateOnMove(true);
                animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            }
        }





        if (starterAssetsInput.Alpha1)
        {
            Default_Gun = 0; //Colt
            starterAssetsInput.Alpha1 = false;


        }
        else if (starterAssetsInput.Alpha2 && gun_unlock >= 1)
        {
            Default_Gun = 1; //AK47
            starterAssetsInput.Alpha2 = false;

        }
        else if (starterAssetsInput.Alpha3 && gun_unlock >= 2)
        {
            Default_Gun = 2; //PPSH
            starterAssetsInput.Alpha3 = false;
        }


        if (changing || relod)
        {

            changing_Gun();
        }
        else
        {
            //Debug.Log("-------------------------");
            Def_gun();
        }

        ck_speed();



    }





    void gunColt(int init, bool change_animation)
    {
        offchangegun(change_animation);
        if (init == 0)
        {
            rigbdr.layers[4].active = true; // Colt Normal
            rigbdr.layers[5].active = false; // Colt Aim
            rigbdr.layers[6].active = false; // Colt walk
            rigbdr.layers[7].active = false; // Colt Run
            rigbdr.layers[8].active = false; // Colt jump
        }
        else if (init == 1)
        {
            rigbdr.layers[4].active = false; // Colt Normal
            rigbdr.layers[5].active = true; // Colt Aim
            rigbdr.layers[6].active = false; // Colt walk
            rigbdr.layers[7].active = false; // Colt Run
            rigbdr.layers[8].active = false; // Colt jump
        }
        else if (init == 2)
        {
            rigbdr.layers[4].active = false; // Colt Normal
            rigbdr.layers[5].active = false; // Colt Aim
            rigbdr.layers[6].active = true; // Colt walk
            rigbdr.layers[7].active = false; // Colt Run
            rigbdr.layers[8].active = false; // Colt jump
        }
        else if (init == 3)
        {
            rigbdr.layers[4].active = false; // Colt Normal
            rigbdr.layers[5].active = false; // Colt Aim
            rigbdr.layers[6].active = false; // Colt walk
            rigbdr.layers[7].active = true; // Colt Run
            rigbdr.layers[8].active = false; // Colt jump
        }
        else if (init == 4)
        {
            rigbdr.layers[4].active = false; // Colt Normal
            rigbdr.layers[5].active = false; // Colt Aim
            rigbdr.layers[6].active = false; // Colt walk
            rigbdr.layers[7].active = false; // Colt Run
            rigbdr.layers[8].active = true; // Colt jump
        }

        rigbdr.layers[0].active = false; // Ak47 Normall
        rigbdr.layers[1].active = false; // Ak47 Aim
        rigbdr.layers[2].active = false; // PPSH Normal
        rigbdr.layers[3].active = false; // PPSH AIM


        ////////////////////////////
        if (gun_unlock == 0)
        {
            gunak470.SetActive(false);
            gunak471.SetActive(false);
            gunppsh0.SetActive(false);
            gunppsh1.SetActive(false);
            guncolt0.SetActive(true);
            guncolt1.SetActive(false);

        }
        else if (gun_unlock == 1)
        {
            gunak470.SetActive(false);
            gunak471.SetActive(true);
            gunppsh0.SetActive(false);
            gunppsh1.SetActive(false);
            guncolt0.SetActive(true);
            guncolt1.SetActive(false);

        }
        else if (gun_unlock == 2)
        {
            gunak470.SetActive(false);
            gunak471.SetActive(true);
            gunppsh0.SetActive(false);
            gunppsh1.SetActive(true);
            guncolt0.SetActive(true);
            guncolt1.SetActive(false);
        }

        ////////////////////////////



    }


    void gunppsh(int init, bool change_animation)
    {
        offchangegun(change_animation);
        if (init == 0)
        {
            rigbdr.layers[2].active = true; // PPSH Normal
            rigbdr.layers[3].active = false; // PPSH AIM
        }
        else if (init == 1)
        {
            rigbdr.layers[2].active = false; // PPSH Normal
            rigbdr.layers[3].active = true; // PPSH AIM
        }

        rigbdr.layers[0].active = false; // Ak47 Normall
        rigbdr.layers[1].active = false; // Ak47 Aim
        rigbdr.layers[4].active = false; // Colt Normal
        rigbdr.layers[5].active = false; // Colt Aim
        rigbdr.layers[6].active = false; // Colt walk
        rigbdr.layers[7].active = false; // Colt Run
        rigbdr.layers[8].active = false; // Colt jump


        ////////////////////////////
        if (gun_unlock == 0)
        {
        }
        else if (gun_unlock == 1)
        {
        }
        else if (gun_unlock == 2)
        {
            gunak470.SetActive(false);
            gunak471.SetActive(true);
            gunppsh0.SetActive(true);
            gunppsh1.SetActive(false);
            guncolt0.SetActive(false);
            guncolt1.SetActive(true);
        }

        ////////////////////////////


    }

    void gunak47(int init, bool change_animation)
    {
        offchangegun(change_animation);
        if (init == 0)
        {
            rigbdr.layers[0].active = true; // Ak47 Normall
            rigbdr.layers[1].active = false; // Ak47 Aim
        }
        else if (init == 1)
        {
            rigbdr.layers[0].active = false; // Ak47 Normall
            rigbdr.layers[1].active = true; // Ak47 Aim

        }


        rigbdr.layers[2].active = false; // PPSH Normal
        rigbdr.layers[3].active = false; // PPSH AIM
        rigbdr.layers[4].active = false; // Colt Normal
        rigbdr.layers[5].active = false; // Colt Aim
        rigbdr.layers[6].active = false; // Colt walk
        rigbdr.layers[7].active = false; // Colt Run
        rigbdr.layers[8].active = false; // Colt jump



        ////////////////////////////
        if (gun_unlock == 0)
        {
        }
        else if (gun_unlock == 1)
        {
            gunak470.SetActive(true);
            gunak471.SetActive(false);
            gunppsh0.SetActive(false);
            gunppsh1.SetActive(false);
            guncolt0.SetActive(false);
            guncolt1.SetActive(true);
        }
        else if (gun_unlock == 2)
        {

            gunak470.SetActive(true);
            gunak471.SetActive(false);
            gunppsh0.SetActive(false);
            gunppsh1.SetActive(true);
            guncolt0.SetActive(false);
            guncolt1.SetActive(true);
        }

        ////////////////////////////




    }

    public void FinishChanching()
    {
        changing = false;
        animator.SetLayerWeight(2, 0);
    }


    public void offchangegun(bool change_gun_animation)
    {
        //AimLayer.weight = 0f;
        //NormalLayer.weight = 0f;
        if (change_gun_animation)
        {
            animator.SetLayerWeight(2, 1);
            animator.SetTrigger("ChangeGun");
            changing = true;
            FindObjectOfType<AudioManager>().Play("ChangeGun");
        }

    }

    public void Def_gun()
    {
        //Debug.Log("States -- Normal State : "+Normal+"- Walk State : "+Walk + "- Run State : " + run+"- Jump State : " + Jump + "- Aim State : " + Aim);

        if (Default_Gun == 0) //Colt
        {
            if (Aim)
            {
                gunColt(1, privous_gun());
            }
            else if (Normal)
            {
                gunColt(0, privous_gun());
            }
            else if (Jump)
            {
                gunColt(4, privous_gun());
            }
            else if (run)
            {
                gunColt(3, privous_gun());
            }
            else if (Walk)
            {
                gunColt(2, privous_gun());
            }
            TempGun = Default_Gun;

        }
        else if (Default_Gun == 1) //Ak47
        {
            if (Aim)
            {
                gunak47(1, privous_gun());
            }
            else if (Normal)
            {
                gunak47(0, privous_gun());
            }
            else if (Jump)
            {
                gunak47(0, privous_gun());
            }
            else if (run)
            {
                gunak47(0, privous_gun());
            }
            else if (Walk)
            {
                gunak47(0, privous_gun());
            }
            TempGun = Default_Gun;

        }
        else if (Default_Gun == 2) //PPSH
        {
            if (Aim)
            {
                gunppsh(1, privous_gun());
            }
            else if (Normal)
            {
                gunppsh(0, privous_gun());
            }
            else if (Jump)
            {
                gunppsh(0, privous_gun());
            }
            else if (run)
            {
                gunppsh(0, privous_gun());
            }
            else if (Walk)
            {
                gunppsh(0, privous_gun());
            }
            TempGun = Default_Gun;
        }
    }


    public bool privous_gun()
    {
        if (Default_Gun == TempGun)
            return false;
        else
            return true;
    }

    //______________________________ States
    public void Jumpstate()
    {

        Normal = false;
        Aim = false;
        Walk = false;
        run = false;
        Jump = true;
    }


    public void Runstate()
    {

        Normal = false;
        Aim = false;
        Walk = false;
        run = true;
        Jump = false;
    }


    public void Idlestate()
    {

        Normal = true;
        Aim = false;
        Walk = false;
        run = false;
        Jump = false;
    }

    public void Aimstate()
    {

        Normal = false;
        Aim = true;
        Walk = false;
        run = false;
        Jump = false;
    }
    public void Walkstate()
    {

        Normal = false;
        Aim = false;
        Walk = true;
        run = false;
        Jump = false;
    }
    //______________________________ States



    public void ck_speed()
    {
        float speed = animator.GetFloat("Speed");

        if (speed >= 0 && speed <= 3)
        {
            Idlestate();
        }
        if (speed > 3 && speed <= 8)
        {
            Walkstate();
        }
        if (speed > 8 && speed <= 16)
        {
            Runstate();
        }
    }

    public void changing_Gun()
    {
        rigbdr.layers[0].active = false;
        rigbdr.layers[1].active = false;
        rigbdr.layers[2].active = false;
        rigbdr.layers[3].active = false;
        rigbdr.layers[4].active = false;
        rigbdr.layers[5].active = false;
        rigbdr.layers[6].active = false;
        rigbdr.layers[7].active = false;
        rigbdr.layers[8].active = false;
    }

    public void relodding()
    {
        relod = true;
        animator.SetLayerWeight(3, 1);
    }

    public void Finishrelodding()
    {
        relod = false;
        animator.SetLayerWeight(3, 0);
    }











}
