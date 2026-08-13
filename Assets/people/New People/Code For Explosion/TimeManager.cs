using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float SlowDownFactor=0.05f;
    public float SlowDownlengh = 2f;

    private void Update()
    {
        Time.timeScale += (1f / SlowDownlengh)*Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale,0f,1f);
    }
    public void SlowMotion()
    {
        Time.timeScale = SlowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
