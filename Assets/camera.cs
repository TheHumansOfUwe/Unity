using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
public bool right = false;
 public    float x;
 public    float timeElapsed;
 public    float lerpDuration = 0.6f;
 public    float startValue = 0;
 public    float endValue = 0;
  public   float valueToLerp;
    void Update()
    {
        if (right)
        {

            if (timeElapsed < lerpDuration)
            {
                valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
            }

            transform.rotation = Quaternion.Euler(0, valueToLerp, 0);
            print(Mathf.Round(timeElapsed));
            if(Mathf.Round(timeElapsed*10) == 6){
                transform.rotation = Quaternion.Euler(0, endValue, 0);
                right = false;
                timeElapsed = 0.0f;
                print("Bazinga");
            }
            return;
        }

        if (Input.GetKeyDown("right"))
        {
            right = true;
            startValue = endValue;
            endValue -= 90;
        }
        else if (Input.GetKeyDown("left"))
        {
            right = true;
            startValue = endValue;
            endValue += 90;
        }
    }
}
