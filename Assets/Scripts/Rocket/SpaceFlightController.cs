using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFlightController : FlightController
{
  public SpaceFlightController(GameObject rocket) : base(rocket)
  { }

  int yawSpeed = 40;
  int pitchSpeed = 40;
  int rollSpeed = 40;

  float throttle = 0;
  float throttleSensitivity = .2f;
  float maxThrust = 20f;

  override public void ControlShip()
  {
    //rigidBody.isKinematic = false;

    FlightControl();
    ThrottleControl();
    ThrustControl();
  }

  override public float GetThrottle()
  {
    return throttle;
  }

  public void FlightControl()
  {
    // ROLL
    if (Input.GetKey(KeyCode.Q))
    {
      transform.Rotate(Vector3.forward * rollSpeed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.E))
    {
      transform.Rotate(-Vector3.forward * rollSpeed * Time.deltaTime);
    }

    // PITCH
    if (Input.GetKey(KeyCode.W))
    {
      transform.Rotate(Vector3.right * pitchSpeed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.S))
    {
      transform.Rotate(-Vector3.right * pitchSpeed * Time.deltaTime);
    }

    // YAW
    if (Input.GetKey(KeyCode.A))
    {
      transform.Rotate(-Vector3.up * yawSpeed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      transform.Rotate(Vector3.up * yawSpeed * Time.deltaTime);
    }
  }

  public void ThrottleControl()
  {
    float t = throttle;

    if (Input.GetKey(KeyCode.X))
    {
      t = 0;
    }
    else if (Input.GetKey(KeyCode.Z))
    {
      t = 1;
    }
    else if (Input.GetKey(KeyCode.LeftShift))
    {
      t = throttle + Time.deltaTime * throttleSensitivity;
    }
    else if (Input.GetKey(KeyCode.LeftControl))
    {
      t = throttle - Time.deltaTime * throttleSensitivity;
    }

    t = Mathf.Clamp(t, 0, 1f);


    throttle = t;
  }

  public void ThrustControl()
  {
    if (throttle > 0.02f)
    {
      rigidBody.AddForce(transform.TransformDirection(Vector3.forward) * maxThrust * throttle);
    }
  }


}
