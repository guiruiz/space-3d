using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

  public float currentTimeScale = 1f;

  void Update()
  {
    float newTimeScale = default;

    if (Input.GetKeyDown(KeyCode.Keypad1))
    {
      newTimeScale = 1f;
    }
    else if (Input.GetKeyDown(KeyCode.Keypad2))
    {
      newTimeScale = 10f;
    }
    else if (Input.GetKeyDown(KeyCode.Keypad3))
    {
      newTimeScale = 50f;
    }
    else if (Input.GetKeyDown(KeyCode.Keypad4))
    {
      newTimeScale = 100f;
    }

    if (newTimeScale != default)
    {
      currentTimeScale = newTimeScale;
      Time.timeScale = newTimeScale;
    }
  }
}
