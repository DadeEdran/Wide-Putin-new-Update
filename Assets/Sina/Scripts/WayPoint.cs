using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPoint : MonoBehaviour
{
    public RectTransform prefab=null;
    private RectTransform waypoint=null;

    public Transform player=null;
    private Text distanceText=null;
    public Vector3 offset = new Vector3(0, 1.25f, 0);
    bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.Find("WayPoints").transform;
        waypoint= Instantiate(prefab,canvas);
        distanceText = waypoint.GetComponentInChildren<Text>();
        start = true;

    }

    // Update is called once per frame
    void Update()
    {
        var screenpos = Camera.main.WorldToScreenPoint(transform.position+offset);
        waypoint.position = screenpos;
        waypoint.gameObject.SetActive(screenpos.z>0);
        distanceText.text = Vector3.Distance(player.position,transform.position).ToString("0")+" m";
    }

    private void OnDisable()
    {
        waypoint.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if(start)
        waypoint.gameObject.SetActive(true);
    }

}
