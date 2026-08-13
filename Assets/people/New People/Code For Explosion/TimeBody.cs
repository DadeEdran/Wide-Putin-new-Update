using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
     bool IsRewinding = false;

    List<PointinTime> PointsInTime;
    Rigidbody rb;

    public float recordTime = 5f;
    public TimeManager timemanager;

    // Start is called before the first frame update
    void Start()
    {
        PointsInTime = new List<PointinTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.R))
            StopRewind();
        
    }

    private void FixedUpdate()
    {
        if (IsRewinding)
            Rewind();
        else
            Record();

    }
    void Rewind()
    {
        timemanager.SlowMotion();
        if (PointsInTime.Count > 0)
        {
            PointinTime pointtime = PointsInTime[0];
            transform.position = pointtime.position;
            transform.rotation = pointtime.rotation;
            PointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }


    }
    void Record()
    {
        if(PointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            PointsInTime.RemoveAt(PointsInTime.Count - 1);
        }
        PointsInTime.Insert(0,new PointinTime(transform.position,transform.rotation));
        
    }


    public void StartRewind()
    {
        IsRewinding = true;
        rb.isKinematic = true;
    }
    void StopRewind()
    {
        IsRewinding = false;
        rb.isKinematic = false;
    }

}
