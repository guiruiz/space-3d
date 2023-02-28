using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDataUI : MonoBehaviour
{
  public GameObject rocket;
  public GameObject planet;
  public TMPro.TMP_Text throttleTextUI;
  public TMPro.TMP_Text altituteTextUI;
  public TMPro.TMP_Text velocityTextUI;
  public TMPro.TMP_Text speedTextUI;
  public TMPro.TMP_Text pitchTextUI;
  public TMPro.TMP_Text rollTextUI;
  public TMPro.TMP_Text yawTextUI;

  int altitudeOffset = 104; // @todo Calculate offset dinamicaly
  private float time;
  private float altitude = 0;
  private float lastAltitudeM = 0;
  float velocityRate = 10f;

  void Start()
  {
    time = Time.time;
  }

  void FixedUpdate()
  {
    UpdateThrottle();
    UpdateAltitude();
    UpdateVelocity();
    UpdateSpeed();
    UpdateRotation();
  }


  void UpdateThrottle()
  {
    RocketController rocketController = rocket.GetComponent<RocketController>();
    int throttlePercentage = (int)(rocketController.GetThrottle() * 100);

    throttleTextUI.text = "Throttle: " + throttlePercentage + "%";
  }
  void UpdateRotation()
  {
    Vector3 rotation = rocket.transform.rotation.eulerAngles;

    pitchTextUI.text = "Pitch: " + ((int)rotation.x) + "º";
    rollTextUI.text = "Roll: " + ((int)rotation.z) + "º";
    yawTextUI.text = "Yaw: " + ((int)rotation.y) + "º";
  }

  void UpdateAltitude()
  {
    Rigidbody rb1 = rocket.GetComponent<Rigidbody>();
    Rigidbody rb2 = planet.GetComponent<Rigidbody>();


    Vector3 direction = rb1.position - rb2.position;
    float distance = direction.magnitude;
    ;

    float alt = distance - altitudeOffset;
    alt = Mathf.Max(alt, 0);
    altitude = alt;

    if (alt < 1000)
    {
      alt = (int)alt;
      altituteTextUI.text = "Alt: " + alt + "m";
    }
    else
    {
      alt = alt / 1000;
      alt = (float)Mathf.Round(alt * 100f) / 100f;
      altituteTextUI.text = "Alt: " + alt.ToString("F2") + "km";
    }

  }

  void UpdateSpeed()
  {

    Rigidbody rb = rocket.GetComponent<Rigidbody>();

    speedTextUI.text = "Speed: " + rb.velocity.magnitude.ToString("F2") + "m/s";
  }

  void UpdateVelocity()
  {

    if (Time.time >= time + (1f / velocityRate))
    {
      float velocity = (altitude - lastAltitudeM) * velocityRate;

      velocityTextUI.text = "Δ Vel.: " + velocity.ToString("F2") + "m/s";

      time = Time.time;
      lastAltitudeM = altitude;
    }
  }
}
