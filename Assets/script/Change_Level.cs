using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Level : MonoBehaviour
{
    [SerializeField] private string newLevel=null;
    [SerializeField] private GameObject uiElement=null;
    [SerializeField] private GameObject minimap=null;
    public Animator transition;
    public float transition_time = 1f;
    private bool state=true;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiElement.SetActive(state);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                state = false;
               
                StartCoroutine(Loadlevel());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiElement.SetActive(false);
        }
    }
    IEnumerator Loadlevel()
    {
        uiElement.SetActive(false);
        minimap.SetActive(false);
        //play animation
        transition.SetTrigger("Start");
        // wait
        yield return new WaitForSeconds(transition_time);
        // Load scene
        SceneManager.LoadScene(newLevel);
    }


}
