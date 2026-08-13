using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject pedestrianprefab;
    public int pedestriansToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        int count = 0;
        while (count < pedestriansToSpawn)
        {
            GameObject obj = Instantiate(pedestrianprefab);
            Transform child = transform.GetChild(Random.Range(0,transform.childCount-1));
            obj.GetComponent<TWaypointNavigator>().currentWaypoint = child.GetComponent<TWaypoint>();
            obj.transform.position = child.position;
            obj.GetComponent<NavMeshAgent>().enabled = true;
            obj.GetComponent<CharacterNavigationController>().enabled=true;
            yield return new WaitForEndOfFrame();

            count++;
        }
    }
}
