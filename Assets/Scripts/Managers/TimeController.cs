using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
  public float timeScale = 1f;
  float fasterTimeScale = 100f;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.T))
    {
      if (Time.timeScale != 1f)
      {

        timeScale = 1f;
      }
      else
      {
        timeScale = fasterTimeScale;

      }
    }

    Time.timeScale = timeScale;
  }
}
