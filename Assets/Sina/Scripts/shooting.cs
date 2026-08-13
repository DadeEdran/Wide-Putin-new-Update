using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.UI;
public class shooting : MonoBehaviour
{
    public bool infinity = false;
    public float tempinfinity = 0;

    public int Magazine_Bullet = 1;
    //public LayerMask BloodLayer;
    //public LayerMask FireLayer;
    //public LayerMask bullethole;
    //textmesh pro
    public GameObject bullet_prefab;
    //public float shootForce,upwardForce;
    //public float impactforce = 200f;
    public float timebetweenshooting, reloadtime, timebetweenshots;
    public int magazinesize, bulletspertap;
    public bool allowbutonhold;
    public int bulletsleft, bulletsshot;
    public ParticleSystem muzzleflash;
    //public GameObject muzzleflash;
    bool shoooting, readyttoshoot, reloading;
    // public GameObject impactEffect;

    //public GameObject blood;

    //public GameObject BulletHolePatricle;
    public Transform SpawnBullet;
    public ThirdPersonShootingController ThirdPersonShootingController = null;
    //public GameObject bulletpoint;
    //public GameObject muzzleflash;
    //public TextMeshProUGUI ammunitiondisplay;

    //public float damage = 10f;

    public string nameofsound = "";

    public Animator animator = null;


    public bool allowInvoke = true;

    [SerializeField]
    private StarterAssetsInputs starterAssetsInput = null;

    public Text textui_maganizm = null;
    public Text textui_bullet = null;

    public Sprite GunPicture = null;
    public GameObject image_item = null;

    private void Awake()
    {
        bulletsleft = magazinesize;
        readyttoshoot = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        tempinfinity = timebetweenshooting;
    }

    // Update is called once per frame
    void Update()
    {
        if (infinity)
        {
            timebetweenshooting = 0.01f;

        }
        else
        {
            timebetweenshooting = tempinfinity;
        }
        //Cursor.visible = false;

        myinput();
        //if (ammunitiondisplay != null)
        //ammunitiondisplay.SetText(bulletsleft / bulletspertap + " / " + magazinesize / bulletspertap);
    }


    private void myinput()
    {

        textui_bullet.text = bulletsleft.ToString();
        textui_maganizm.text = Magazine_Bullet.ToString();
        image_item.GetComponent<Image>().sprite = GunPicture;
        if (Magazine_Bullet < 0)
        {
            textui_bullet.text = "0";
            textui_maganizm.text = "0";
            return;

        }

        if (starterAssetsInput.aim && starterAssetsInput.shoot && allowbutonhold)
        {
            shoooting = true;
        }
        else
        {
            shoooting = false;
        }


        if (starterAssetsInput.R && bulletsleft < magazinesize && !reloading) Reload();


        if (readyttoshoot && shoooting && !reloading && bulletsleft <= 0) Reload();

        if (readyttoshoot && shoooting && !reloading && bulletsleft > 0)
        {
            bulletsshot = 0;
            shoot();
        }
    }


    private void shoot()
    {

        CinemachineShake.instance.ShakeCamera(5, 0.1f);
        FindObjectOfType<AudioManager>().Play(nameofsound);
        muzzleflash.Play();
        readyttoshoot = false;




        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenmidpoint = new Vector2(Screen.width / 2f, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenmidpoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 999f))
        {
            mouseWorldPosition = hit.point;

        }
        Debug.Log(hit.collider.name);


        Vector3 aimDir = (mouseWorldPosition - SpawnBullet.position).normalized;
        Instantiate(bullet_prefab, SpawnBullet.position, Quaternion.LookRotation(aimDir, Vector3.up)).GetComponent<BulletDamage>().Player = true;

        //if (hit.collider.GetComponentInParent<Ragdoll>())
        //{
        //    foreach (var col in hit.collider.GetComponentInParent<Ragdoll>().AllColliders)
        //    {
        //        if (col.name == hit.transform.name)
        //        {
        //            Debug.Log(col.name);
        //            break;
        //        }

        //    }
        //    //hit.collider.GetComponentInParent<EnamyInfo>().Neg_Heal_E = 10;
        //    //if (hit.collider.GetComponentInParent<EnamyInfo>().ShowHeal <= 10)
        //    //{
        //    //    var enamy = hit.collider.GetComponentInParent<Ragdoll>();
        //    //     var enamyai = hit.collider.GetComponentInParent<EnemyAi>();
        //    //     enamyai.enabled = false;
        //    //     if (enamy != null)
        //    //    {
        //    //     enamy.DoRagdoll(true);
        //    //    }
        //    //}

        //}






        //Debug.Log(hit.transform.name);
        //if (hit.transform.name == "WhiteHouseGaurd_Armature")
        //{

        //hit.collider.GetComponentInParent<EnamyInfo>().Neg_Heal_E = 10;
        //Debug.Log(hit.collider.GetComponentInParent<EnamyInfo>().ShowHeal);

        //if (hit.collider.GetComponentInParent<EnamyInfo>().ShowHeal<=10)
        //{

        // var enamy = hit.collider.GetComponent<Ragdoll>();
        // var enamyai = hit.collider.GetComponentInParent<EnemyAi>();
        // enamyai.enabled = false;
        // if (enamy != null)
        //{
        // enamy.DoRagdoll(true);
        //}
        //}

        //}
        //Debug.Log(hit.transform.name);
        //    Target target = hit.transform.GetComponent<Target>();
        //    if (target != null)
        //    {
        //        target.takeDamage(damage);

        //    }
        //    if (hit.rigidbody != null)
        //    {
        //        hit.rigidbody.AddForce(-hit.normal * impactforce);
        //    }
        //}
        //else
        //    targetpoint = ray.GetPoint(75);
        ////Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //Vector3 directionwithoutspread = targetpoint - attackpoint.position;

        //float x = Random.Range(-spread,spread);
        //float y = Random.Range(-spread, spread);


        //Vector3 dirrectionwithspread = directionwithoutspread + new Vector3(x, y, 0);

        //GameObject currentbullet = Instantiate(bullet,attackpoint.position,Quaternion.identity);

        //currentbullet.transform.forward = dirrectionwithspread.normalized;


        //currentbullet.GetComponent<Rigidbody>().AddForce(dirrectionwithspread.normalized*shootForce,ForceMode.Impulse);

        //currentbullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.up * upwardForce, ForceMode.Impulse);
        //Destroy(currentbullet, 2f);
        //if (impactEffect != null)
        //{
        //    if ((FireLayer.value & (1 << hit.transform.gameObject.layer)) > 0)
        //    {
        //        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //        Destroy(impactGO, 2f);
        //    }

        //}

        //if (BulletHolePatricle != null)
        //{
        //    GameObject impactGO = Instantiate(BulletHolePatricle, hit.point, Quaternion.LookRotation(hit.normal));
        //    Destroy(impactGO, 2f);
        //}


        //if (blood != null)
        //{

        //    if ((BloodLayer.value & (1 << hit.transform.gameObject.layer))>0)
        //    {
        //        GameObject impactGO = Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
        //        Destroy(impactGO, 2f);
        //    }

        //}

        //if (bulletpoint != null)
        //{
        //    if ((bullethole.value & (1 << hit.transform.gameObject.layer)) > 0)
        //    {
        //        GameObject bulletp = Instantiate(bulletpoint, hit.point, Quaternion.LookRotation(hit.normal));
        //        Destroy(bulletp, 2f);
        //    }


        //}

        //gameobjz
        //if (muzzleflash != null)
        //{
        //   // GameObject mflash= Instantiate(muzzleflash, attackpoint.position, Quaternion.identity);
        //   GameObject mflash = Instantiate(muzzleflash, attackpoint.position, Quaternion.LookRotation(hit.normal));
        //   Destroy(mflash, 1f);
        //}


        if (!infinity) { bulletsleft--; }

        bulletsshot++;

        Debug.Log(bulletsleft + " : " + bulletsshot);

        if (allowInvoke)
        {
            Invoke("Resetshot", timebetweenshooting);
            allowInvoke = false;
        }


        if (bulletsshot < bulletspertap && bulletsleft > 0)
            Invoke("Shoot", timebetweenshots);
    }


    private void Resetshot()
    {
        readyttoshoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        FindObjectOfType<AudioManager>().Play("RealodGun");
        if (nameofsound == "AK47" || nameofsound == "PPSH")
        {
            animator.SetTrigger("RelodNormal");
        }
        else
        {
            animator.SetTrigger("RelodColt");
        }
        ThirdPersonShootingController.relodding();
        reloading = true;
        Invoke("ReloadFinished", reloadtime);
    }

    private void ReloadFinished()
    {
        if (Magazine_Bullet >= 0) { Magazine_Bullet -= 1; }

        bulletsleft = magazinesize;
        reloading = false;
    }

    private void OnGUI()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

}
