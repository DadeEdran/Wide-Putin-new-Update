using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class headtraking : MonoBehaviour
{
    public Transform target;
    public Rig HeadRig;
    List<lookatpoint> lookatpoints;
    public float Radius=10f;
    public float Retargetspeed = 5f;
    float RadiusSqr;
    public float MaxAngle=90f;
    void Start()
    {
        lookatpoints = FindObjectsOfType<lookatpoint>().ToList();
        RadiusSqr = Radius * Radius;
    }

    // Update is called once per frame
    void Update()
    {
        Transform tracking = null;
        foreach (lookatpoint LAP in lookatpoints)
        {
            
            Vector3 delta=LAP.transform.position - transform.position;
            if (delta.sqrMagnitude < RadiusSqr)
            {
                
                float angle = Vector3.Angle(transform.forward,delta);
                if (angle <MaxAngle)
                {
                    tracking = LAP.transform;
                    break;
                }

            }
        }
        float rigweight = 0;
        Vector3 targetpos = transform.position + (transform.forward * 2f);

        if (tracking != null)
        {
            targetpos = tracking.position;
             rigweight = 1;
        }
        target.position = Vector3.Lerp(target.position,targetpos,Time.deltaTime);
        HeadRig.weight =  Mathf.Lerp(HeadRig.weight,rigweight,Time.deltaTime*2);
    }
}
