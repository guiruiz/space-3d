using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RocketController : MonoBehaviour
{
  public GameObject planet;

  private FlightMode flightMode;
  private FlightController flightController;
  private SpaceFlightController spaceFlightController;
  private PlanetFlightController planetFlightController;

  private Rigidbody rigidBody;

  private float maxSpeed = 30f;

  void setFlightMode(FlightMode mode)
  {
    if (mode == FlightMode.Space)
    {
      flightController = spaceFlightController;
    }
    else if (mode == FlightMode.Planet)
    {
      flightController = planetFlightController;
    }

    flightMode = mode;
  }

  void Start()
  {
    spaceFlightController = new SpaceFlightController(gameObject, planet);
    planetFlightController = new PlanetFlightController(gameObject, planet);

    setFlightMode(FlightMode.Planet);


    rigidBody = this.GetComponent<Rigidbody>();
    // Set ship in orbit at alt 97m (y = 200)
    //rigidBody.AddForce(transform.TransformDirection(Vector3.left) * 1500f);
  }

  void Update()
  {
    if (Input.GetKey(KeyCode.O))
    {
      setFlightMode(FlightMode.Space);
    }
    else if (Input.GetKey(KeyCode.P))
    {
      setFlightMode(FlightMode.Planet);
    }

    // Set max speed
    // if (rigidBody.velocity.magnitude > maxSpeed)
    // {
    //   rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
    // }


  }

  void FixedUpdate()
  {
    flightController.ControlShip();
  }

  public float GetThrottle()
  {
    return flightController.GetThrottle();
  }
}
