using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipController : MonoBehaviour
{
  private CelestialBody landedBody;
  private Rigidbody rigidBody;

  void Awake()
  {
    EventManager.OnGravityFieldEnter += OnGravityFieldEnter;
    EventManager.OnGravityFieldExit += OnGravityFieldExit;
  }

  void OnDestroy()
  {
    EventManager.OnGravityFieldEnter -= OnGravityFieldEnter;
    EventManager.OnGravityFieldExit -= OnGravityFieldExit;
  }

  void Start()
  {
    rigidBody = this.GetComponent<Rigidbody>();

    // Set ship in orbit at alt 97m (y = 200)
    //rigidBody.AddForce(transform.TransformDirection(Vector3.left) * 1500f);
  }

  void Update()
  {
    // Set max speed
    // float maxSpeed = 30f;
    // if (rigidBody.velocity.magnitude > maxSpeed)
    // {
    //   rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
    // }
  }

  void FixedUpdate()
  {
    if (landedBody)
    {
      //rigidBody.velocity = landedBody.velocity;
    }
    else
    {
      Vector3 gravity = Universe.CalculateAcceleration(rigidBody.position);
      rigidBody.AddForce(gravity, ForceMode.Acceleration);
    }

  }

  public void OnGravityFieldEnter(PlanetController planet)
  {
    // setFlightMode(FlightMode.Planet, planet);
  }

  public void OnGravityFieldExit(PlanetController planet)
  {
    // setFlightMode(FlightMode.Space);
  }

  void OnTriggerEnter(Collider col)
  {
    if (col.gameObject.tag == "CelestialBody")
    {
      landedBody = col.GetComponent<CelestialBody>();
    }
  }
}
