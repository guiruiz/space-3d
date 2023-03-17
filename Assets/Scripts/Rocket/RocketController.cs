using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RocketController : MonoBehaviour
{
  public CelestialBody targetBody;
  public bool startLanded = true;

  private CelestialBody landedBody;
  private SpaceFlightController spaceFlightController;
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
    spaceFlightController = new SpaceFlightController(gameObject);
    rigidBody = this.GetComponent<Rigidbody>();

    if (targetBody)
    {
      TeleportToBody(targetBody);
    }

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
      rigidBody.velocity = landedBody.velocity;
    }
    else
    {
      Vector3 gravity = Universe.CalculateAcceleration(rigidBody.position);
      rigidBody.AddForce(gravity, ForceMode.Acceleration);
    }

    spaceFlightController.ControlShip();
  }

  // void SwitchFlightMode()
  // {
  //   if (Input.GetKey(KeyCode.O))
  //   {
  //     setFlightMode(FlightMode.Space);
  //   }
  //   else if (Input.GetKey(KeyCode.P))
  //   {
  //     setFlightMode(FlightMode.Planet);
  //   }
  // }

  // void setFlightMode(FlightMode mode, PlanetController planet = null)
  // {
  //   planetFlightController.SetPlanet(planet);
  //   if (mode == FlightMode.Space)
  //   {
  //     flightController = spaceFlightController;
  //   }
  //   else if (mode == FlightMode.Planet)
  //   {
  //     flightController = planetFlightController;
  //   }

  //   flightMode = mode;
  // }


  public FlightMode GetFlightMode()
  {
    return FlightMode.Space;
  }
  public float GetThrottle()
  {
    return spaceFlightController.GetThrottle();
  }

  public void OnGravityFieldEnter(PlanetController planet)
  {
    // disable planet flight mode switcher
    //setFlightMode(FlightMode.Planet, planet);
  }

  public void OnGravityFieldExit(PlanetController planet)
  {
    // disable planet flight mode switcher
    //setFlightMode(FlightMode.Space);
  }

  void TeleportToBody(CelestialBody body)
  {
    if (startLanded)
    {
      rigidBody.velocity = body.velocity;
      float shipHeight = GetComponent<Collider>().bounds.size.y; //@Todo find a better way
      float altitude = body.radius / 2 + shipHeight / 2;

      // Start Landed
      rigidBody.MovePosition(body.transform.position + new Vector3(0, altitude, 0));
      landedBody = body;
    }
    else
    {
      // Start Flying
      rigidBody.MovePosition(body.transform.position + (transform.position - body.transform.position).normalized * body.radius * 2);
    }
  }

  void OnTriggerEnter(Collider col)
  {
    if (col.gameObject.tag == "CelestialBody")
    {
      Debug.Log("Hiiiiiiit");
      landedBody = col.GetComponent<CelestialBody>();
    }
  }

}
