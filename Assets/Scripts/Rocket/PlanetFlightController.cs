using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFlightController : FlightController
{
  public PlanetFlightController(GameObject rocket) : base(rocket)
  { }

  private PlanetController planet;
  float throttle = .5f;
  float maxSpeed = 30;
  int yawSpeed = 50;

  override public void ControlShip()
  {
    FlightControl();
    RotationControl();
    AltitudeControl();

    // Set max speed
    if (rigidBody.velocity.magnitude > maxSpeed)
    {
      rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
    }
  }

  override public float GetThrottle()
  {
    return throttle;
  }

  public void RotationControl()
  {
    if (!planet) { return; }

    Vector3 gravityUp = (transform.position - planet.transform.position).normalized;
    Vector3 rocketUp = transform.up;

    Quaternion targetRotation = Quaternion.FromToRotation(rocketUp, gravityUp) * transform.rotation;
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50 * Time.deltaTime);
  }

  public void FlightControl()
  {
    if (Input.GetKey(KeyCode.A))
    {
      //rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(-Vector3.left) * horizontalSpeed * Time.deltaTime);
      transform.Rotate(-Vector3.up * yawSpeed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      //rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(Vector3.left) * horizontalSpeed * Time.deltaTime);
      transform.Rotate(Vector3.up * yawSpeed * Time.deltaTime);
    }

    if (Input.GetKey(KeyCode.W))
    {
      // rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(-Vector3.forward) * horizontalSpeed * Time.deltaTime);
      rigidBody.AddForce(transform.TransformDirection(Vector3.forward) * maxSpeed);
    }
    else if (Input.GetKey(KeyCode.S))
    {
      //rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(Vector3.left) * horizontalSpeed * Time.deltaTime);
      rigidBody.AddForce(transform.TransformDirection(-Vector3.forward) * maxSpeed);
    }

    if (Input.GetKeyUp(KeyCode.X)) // it set as W, might fix the bug, @todo: enable n try out.
    {
      rigidBody.velocity = Vector3.zero;
    }

  }

  public void AltitudeControl()
  {
    if (Input.GetKey(KeyCode.LeftShift))
    {
      rigidBody.AddForce(transform.TransformDirection(Vector3.up) * maxSpeed);
      throttle = .75f;
    }
    else if (Input.GetKey(KeyCode.LeftControl))
    {
      rigidBody.AddForce(transform.TransformDirection(-Vector3.up) * maxSpeed);
      throttle = .25f;
    }

    if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftControl))
    {

      rigidBody.velocity = Vector3.zero;
      throttle = .5f;
    }
  }


  public void SetPlanet(PlanetController planet)
  {
    this.planet = planet;
  }
}
