using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlightController
{

  protected GameObject rocket;
  protected Rigidbody rigidBody;
  protected Transform transform;

  public FlightController(GameObject rocket)
  {
    this.rigidBody = rocket.GetComponent<Rigidbody>();
    this.transform = rocket.GetComponent<Transform>();

    this.rocket = rocket;
  }

  public abstract void ControlShip();
  public abstract float GetThrottle();
}
