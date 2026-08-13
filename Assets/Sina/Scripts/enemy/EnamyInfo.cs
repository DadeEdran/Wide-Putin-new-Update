using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using UnityEngine.AI;

public class EnamyInfo : MonoBehaviour
{
    //GameObject go;
    //bool s = true;
    //[Range(0, 100)]
    //[SerializeField] private int HealPlayer_E = 50;

    //public int Add_Heal_E
    //{
    //    get { return HealPlayer_E; }
    //    set { if (HealPlayer_E < 100) { HealPlayer_E = HealPlayer_E + value; } }
    //}

    //public int Neg_Heal_E
    //{
    //    get { return HealPlayer_E; }
    //    set { if (!(HealPlayer_E <= 0)) { HealPlayer_E = HealPlayer_E - value; } }
    //}
    //public int ShowHeal
    //{
    //    get { return HealPlayer_E; }
    //}
    //private void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        if (s)
    //        {
    //            GetComponentInChildren<Ragdoll>().DoRagdoll(true);
    //            s = false;
    //        }
    //        else
    //        {
    //            GetComponentInChildren<Ragdoll>().DoRagdoll(false);
    //            s = true;
    //        }
    //    }



    //}
    
    public int Hp = 100;
    [SerializeField]
    private Animator animator=null;
    [SerializeField]
    private Slider HealthBar = null;
    [SerializeField]
    private CanvasGroup HealBar=null;
    [SerializeField]
    private Transform PlayerFollow=null;
    
    [SerializeField]
    Collider Collider1 = null;
    [SerializeField]
    Collider Collider2 = null;




    public GameObject Bullet_prefab=null;
    public GameObject AttackPoint=null;
    public Transform attackpoint=null;
    public bool Gun = false;
    public float spread;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public bool enemydead = false;

    public bool drop = false;
    bool drop_test = false;
    public GameObject Drop_Object = null;

    public PlayerInfo playerinfo=null;

   // public Rig norrig = null;
    //public Rig aimrig = null;
  //  public Rig wlkrig = null;
    public RigBuilder rigbdr=null;
    public float offsety = 200f;

    public NavMeshAgent NMA = null;



    public void TakeDamage(int damageAmount)
    {
        Hp -= damageAmount;
        if (Hp <= 0)
        {
            Collider1.enabled = false;
            Collider2.enabled = false;
            HealBar.alpha = 0;
            animator.SetBool("dead",true);
            Ragdoll rg = this.GetComponent<Ragdoll>();
            rg.enabled = true;
            animator.enabled = false;
            animator.avatar = null;
            enemydead = true;
            NMA.enabled = false;
            if (drop && drop_test == false)
            {
                Instantiate(Drop_Object, this.transform.position+new Vector3(0,2,0),Quaternion.identity);
                drop_test = true;
            }
               
            
            // play death animation
        }
        else
        {
            if(!(Hp <= 0))
            {
                animator.SetTrigger("damage");
            }
            //play hit animation
            
        }
    }

    public void ChangeRotation()
    {
        
        this.transform.LookAt(new Vector3(PlayerFollow.position.x, offsety, PlayerFollow.position.z));
    }


    public void set_rig_aim()
    {
        //norrig.weight = 0;
        //aimrig.weight = 1f;
        //wlkrig.weight = 0;
        //Debug.Log("aim");
        
        rigbdr.layers[0].active = false;
        rigbdr.layers[1].active = true;
        rigbdr.layers[2].active = false;
        if (rigbdr.layers.Count > 3)
        {
            rigbdr.layers[3].active = false;
        }

    }

    public void set_rig_normal()
    {
        //norrig.weight = 1f;
        //aimrig.weight = 0;
        //wlkrig.weight = 0;
        //Debug.Log("normal");
        rigbdr.layers[0].active = true;
        rigbdr.layers[1].active = false;
        rigbdr.layers[2].active = false;
        if (rigbdr.layers.Count > 3)
        {
            rigbdr.layers[3].active = false;
        }

    }

    public void set_rig_wlkrig()
        
    {
        //norrig.weight = 0;
        //aimrig.weight = 0;
        //wlkrig.weight = 1f;
        //Debug.Log("walk");
        rigbdr.layers[0].active = false;
        rigbdr.layers[1].active = false;
        rigbdr.layers[2].active = true;
        if (rigbdr.layers.Count > 3)
        {
            rigbdr.layers[3].active = false;
        }
    }

    public void set_rig_running()
    {
        rigbdr.layers[0].active = false;
        rigbdr.layers[1].active = false;
        rigbdr.layers[2].active = false;
        if (rigbdr.layers.Count>3)
        {
            rigbdr.layers[3].active = true;
        }
        else
        {
            rigbdr.layers[2].active = true;
        }
        
    }

    private void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(!(Hp <= 0))
        {
            if (animator.GetBool("isAttacking"))
            {
                
                ChangeRotation();
            }
        }



        HealthBar.value = Hp;

    }

    public void fire()
    {
        if (!alreadyAttacked)
        {
            shoot();//shoot
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void shoot()
    {
        ///Attack code here
        //Rigidbody rb = Instantiate(projectile, AttackPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        //rb.AddForce(transform.TransformDirection(Vector3.down) * 32f, ForceMode.Impulse);
        //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        ///End of attack code
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        Vector3 targetpoint;
        RaycastHit hit;
        if (Physics.Raycast(AttackPoint.transform.position, AttackPoint.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            targetpoint = hit.point;
            Debug.DrawRay(AttackPoint.transform.position, AttackPoint.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 2f, false);

            //Debug.Log("Did Hit" + hit.distance);
        }
        else
        {
            Debug.DrawRay(AttackPoint.transform.position, AttackPoint.transform.TransformDirection(Vector3.forward) * 1000, Color.white, 2f, false);
            //Debug.Log("Did not Hit");
            targetpoint = hit.point;
        }

        Vector3 directionwithoutspread = targetpoint - attackpoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);


        Vector3 dirrectionwithspread = directionwithoutspread + new Vector3(x, 0, y);

        GameObject currentbullet = Instantiate(Bullet_prefab, attackpoint.position, Quaternion.identity);

        currentbullet.transform.forward = dirrectionwithspread.normalized;
        currentbullet.GetComponent<BulletDamage>().Enemy = true;


        //currentbullet.GetComponent<Rigidbody>().AddForce(dirrectionwithspread.normalized * 32, ForceMode.Impulse);

        //currentbullet.GetComponent<Rigidbody>().AddForce(fpscam.transform.up * upwardForce, ForceMode.Impulse);
        // Destroy(currentbullet, 2f);
    }


    public void Attack_Near() {
        playerinfo.Neg_Heal = 10;   
    }

}
