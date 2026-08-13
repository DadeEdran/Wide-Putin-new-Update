using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    //public int dec_heal = 10;
    //public string TagName = "Player";
    //PlayerInfo PlayerI;
    ////[SerializeField]
    ////private GameObject Player = null;
    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag(TagName))
    //    {

    //        PlayerI = other.GetComponent<PlayerInfo>();
    //        PlayerI.Neg_Heal = dec_heal;
    //        // gameObject.SetActive(false);

    //    }
    //    Destroy(gameObject);
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag(TagName))
    //    {
    //        PlayerI = collision.collider.GetComponent<PlayerInfo>();
    //        PlayerI.Neg_Heal = dec_heal;
    //        FindObjectOfType<AudioManager>().Play("PlayerDeath");
    //    }


    //    // do other jobs, then bullet destroys itself:
    //    Destroy(gameObject);
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag(TagName))
    //    {
    //        //gameObject.SetActive(false);
    //    }
    //}
    //// Update is called once per frame
    //void Update()
    //{

    //}
    public int damgeAmount = 10;

    public bool Enemy = false;
    public bool Player = false;
    //[SerializeField]
    //private Transform vfxhitgreen;
    //[SerializeField]
    //private Transform vfxhitred;
    public float speed = 50f;
    private Rigidbody BulletRigidBody;


    public GameObject Metal = null;
    public GameObject Wood = null;
    public GameObject Stone = null;
    public GameObject Sand = null;
    public GameObject Blood = null;

    private void Awake()
    {
        BulletRigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Destroy(gameObject, 10);
        BulletRigidBody.velocity = transform.forward * speed;
        BulletRigidBody.AddForce(transform.forward * speed);
    }



    private void OnCollisionEnter(Collision other)
    {
        //Quaternion spawnRotarion = Quaternion.FromToRotation(this.gameObject.transform.forward,this.gameObject.transform.position);
        //Instantiate(bullethole,transform.position,Quaternion.LookRotation(-transform.forward));

        ContactPoint CP = other.GetContact(0);

        if (Player && other.gameObject.tag == "Enemy")

        {
            FindObjectOfType<AudioManager>().Play("Blood");
            if (!(other.gameObject.GetComponent<EnamyInfo>().Hp <= 0))
            {

                other.gameObject.GetComponent<EnamyInfo>().TakeDamage(damgeAmount);
            }
            GameObject bullethole_ = Instantiate(Blood, CP.point, Quaternion.LookRotation(CP.normal));
            Destroy(bullethole_, 10);

            Destroy(gameObject);

        }


        if (other.gameObject.tag == "Sand")
        {
            FindObjectOfType<AudioManager>().Play("Sand");
            GameObject bullethole_ = Instantiate(Sand, CP.point, Quaternion.LookRotation(CP.normal));
            Destroy(bullethole_, 10);
        }

        if (other.gameObject.tag == "Metal")
        {
            FindObjectOfType<AudioManager>().Play("Metal");
            GameObject bullethole_ = Instantiate(Metal, CP.point, Quaternion.LookRotation(CP.normal));
            Destroy(bullethole_, 10);
        }

        if (other.gameObject.tag == "Stone")
        {
            FindObjectOfType<AudioManager>().Play("Stone");
            GameObject bullethole_ = Instantiate(Stone, CP.point, Quaternion.LookRotation(CP.normal));
            Destroy(bullethole_, 10);
        }


        if (other.gameObject.tag == "Wood")
        {
            FindObjectOfType<AudioManager>().Play("Wood");
            GameObject bullethole_ = Instantiate(Wood, CP.point, Quaternion.LookRotation(CP.normal));
            Destroy(bullethole_, 10);
        }



        if (Enemy && other.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Blood");
            other.gameObject.GetComponent<PlayerInfo>().Neg_Heal = damgeAmount;
            GameObject bullethole_ = Instantiate(Blood, CP.point, Quaternion.LookRotation(CP.normal));
            Destroy(bullethole_, 10);
        }


        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(this.transform.forward * 3000f);
        }


        if (other.gameObject.GetComponent<breaking>() != null)
        {
            breaking br = other.gameObject.GetComponent<breaking>();
            br.br = true;
        }


        BulletRigidBody.velocity = Vector3.zero;
        BulletRigidBody.AddForce(Vector3.zero);
        // Debug.Log(other.name);
        Destroy(gameObject);
    }

}
