using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
   [SerializeField] private Light lightComponent;
    private bool LightState;
    private int TimesDone;

    void Start()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            if(LightState)
            {
                LightState = false;
                lightComponent.intensity = 100;
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                LightState = true;
                lightComponent.intensity = 1000;
                TimesDone += Mathf.RoundToInt(Random.Range(2f, 1f));
                if(TimesDone % 4 == 0)
                {
                    yield return new WaitForSeconds(0.3f);
                }
                else if (TimesDone % 4 == 1)
                {
                    yield return new WaitForSeconds(Random.Range(0.5f, 0.3f));
                }
                else
                {
                    yield return new WaitForSeconds(Random.Range(7f, 4f));
                }
            }

        }
    }
}
