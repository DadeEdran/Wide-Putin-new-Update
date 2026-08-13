using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickevent : MonoBehaviour
{
    public GameObject boom;

    public Transform Orgin;

    public TimeManager timemanager;
    public Ragdoll [] ragoes;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

            GameObject go= Instantiate(boom, Orgin.position+new Vector3(0f,2f,0f),Quaternion.identity);
            Destroy(go, 1.5f); // time boobmb
            timemanager.SlowMotion();
            //rago.DoRagdoll(true);
            StartCoroutine(passiveMe(1));
        }

    }

    IEnumerator passiveMe(int secs)
    {
        yield return new WaitForSeconds(secs);
        foreach (var rago in ragoes)
        {
            rago.DoRagdoll(true);
        }
        //ragoes[0].DoRagdoll(true);
    }
}
