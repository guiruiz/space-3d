using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTeleporter : MonoBehaviour
{
  public CelestialBody destination;
  public bool shouldStartLanded = true;
  public int altitudeMultiplier = 1;

  private FlightController flightController;
  private Rigidbody rb;

  void Start()
  {
    rb = this.GetComponent<Rigidbody>();
    flightController = this.GetComponent<FlightController>();

    if (destination)
    {
      TeleportToBody(destination, shouldStartLanded);
    }
  }

  void Update()
  {
    if (Input.GetKey(KeyCode.R))
    {
      ResetShip();
    }
  }

  public void ResetShip()
  {

    TeleportToBody(destination, shouldStartLanded);
  }

  void TeleportToBody(CelestialBody body, bool startLanded)
  {
    flightController.velocity = body.velocity;

    if (startLanded)
    {
      // @Todo find a better way to get the height
      float shipHeight = GetComponent<Collider>().bounds.size.y;
      float altitude = body.diameter / 2 + shipHeight / 2;

      // Start Landed
      rb.position = body.transform.position + new Vector3(0, altitude, 0);
    }
    else
    {
      // Start Flying
      float altitude = body.getRadius() + 100f;
      Vector3 startPosition = body.Position + altitude * Vector3.up;
      rb.position = startPosition;
      transform.localEulerAngles = new Vector3(0f, 90f, 0f);
      // rigidBody.MovePosition(body.transform.position + (transform.position - body.transform.position).normalized * body.radius * altitudeMultiplier);
    }
  }
}
