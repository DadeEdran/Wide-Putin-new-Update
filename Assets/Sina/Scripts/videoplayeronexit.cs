using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class videoplayeronexit : MonoBehaviour
{
    public CanvasGroup screen;
    public CanvasGroup screen2;
    private void OnDestroy()
    {
        screen.alpha = 1;
        screen2.alpha = 0;
        FindObjectOfType<AudioManager>().UnMute_all();

    }
}
