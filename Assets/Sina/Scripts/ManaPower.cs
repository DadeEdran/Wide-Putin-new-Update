using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
//using UnityEngine.UI;


public class ManaPower : MonoBehaviour
{
    //public Text TextTimer;

    float CurrentTime = 0;
    float StartingTime = 10;
    bool Timer = false;

    bool set_ck = false;

    public shooting colt = null;
    public shooting ak = null;
    public shooting ppsh = null;

    public GameObject postprocess = null;

    [SerializeField]
    private StarterAssetsInputs starterAssetsInput = null;

    public void set_infinity()
    {
        colt.infinity = true;
        postprocess.SetActive(true);
        set_ck = true;
        CurrentTime = StartingTime;
        Timer = true;

    }

    public void set_infinity_false()
    {
        colt.infinity = false;
        postprocess.SetActive(false);
        set_ck = false;
    }

    private void Update()
    {

        if (starterAssetsInput.F)
        {
            starterAssetsInput.F = false;
            if (Timer == false)
            {
                set_infinity();
            }

        }


        if (CurrentTime >= 0 && Timer == true)
        {
            CurrentTime -= 1 * Time.deltaTime;
        }

        if (CurrentTime <= 0 && Timer == true)
        {
            Timer = false;
            CurrentTime = StartingTime;
            set_infinity_false();
        }

        //FreshTextUi();





    }

    //public void FreshTextUi()
    //{
    //TextTimer.text = CurrentTime.ToString("00.0");
    //}
}
