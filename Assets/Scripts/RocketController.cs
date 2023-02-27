using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
  private Rigidbody rigidBody;

  int yawSpeed = 40;
  int pitchSpeed = 40;
  int rollSpeed = 40;

  private float throttle = 0;
  private float throttleSensitivity = .2f;
  private float maxThrust = 20f;
  private float maxSpeed = 30f;
  void Start()
  {
    rigidBody = this.GetComponent<Rigidbody>();

    //Set ship in orbit at alt 97m
    rigidBody.AddForce(transform.TransformDirection(Vector3.left) * 1500f);

  }

  void Update()
  {

    if (rigidBody.velocity.magnitude > maxSpeed)
    {
      rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
    }

  }
  void FixedUpdate()
  {
    FlightControl();
    ThrottleControl();
    ThrustControl();
  }

  void FlightControl()
  {
    // ROLL
    if (Input.GetKey(KeyCode.A))
    {
      transform.Rotate(-Vector3.forward * yawSpeed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      transform.Rotate(Vector3.forward * yawSpeed * Time.deltaTime);
    }

    // PITCH
    if (Input.GetKey(KeyCode.W))
    {
      transform.Rotate(-Vector3.right * pitchSpeed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.S))
    {
      transform.Rotate(Vector3.right * pitchSpeed * Time.deltaTime);
    }

    // YAW
    if (Input.GetKey(KeyCode.Q))
    {
      transform.Rotate(-Vector3.up * rollSpeed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.E))
    {
      transform.Rotate(Vector3.up * rollSpeed * Time.deltaTime);
    }
  }

  void ThrottleControl()
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

  void ThrustControl()
  {
    if (throttle > 0.02f)
    {
      rigidBody.AddForce(transform.TransformDirection(Vector3.up) * maxThrust * throttle);
    }
  }

  public float getThrottle()
  {
    return throttle;
  }
}
