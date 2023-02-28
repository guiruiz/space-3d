using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlightController
{

  protected GameObject rocket;
  protected GameObject planet;
  protected Rigidbody rigidBody;
  protected Transform transform;

  public FlightController(GameObject rocket, GameObject planet)
  {
    this.rigidBody = rocket.GetComponent<Rigidbody>();
    this.transform = rocket.GetComponent<Transform>();

    this.rocket = rocket;
    this.planet = planet;
  }

  public abstract void ControlShip();
  public abstract float GetThrottle();
}
