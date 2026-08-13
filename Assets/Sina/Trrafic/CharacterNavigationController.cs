using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterNavigationController : MonoBehaviour
{
    [SerializeField]
    Vector3 Destination;
    private NavMeshAgent navMeshAgent;
    public bool ReachedDestination = false;
    Animator animator;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destinationDirection = Destination - transform.position;
        destinationDirection.y = 200;

        float destinationDistance = destinationDirection.magnitude;

        if (destinationDistance<=200)
        {
            ReachedDestination = true;
            animator.SetBool("walk", false);
        }
        else
        {
            animator.SetBool("walk",true);
            ReachedDestination = false;
            navMeshAgent.destination = Destination;
        }

  


        
    }

    public void SetDestination(Vector3 destination)
    {
        this.Destination = destination;
        ReachedDestination = false;
    }
}
