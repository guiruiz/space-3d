using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTeleporter : MonoBehaviour
{
  public CelestialBody destination;
  public bool shouldStartLanded = true;
  public int altitudeMultiplier = 1;

  private Rigidbody rigidBody;

  void Start()
  {
    rigidBody = this.GetComponent<Rigidbody>();

    if (destination)
    {
      TeleportToBody(destination, shouldStartLanded);
    }
  }

  void Update()
  {

  }

  void TeleportToBody(CelestialBody body, bool startLanded)
  {
    // (body.radius * Vector3.up)

    rigidBody.velocity = body.velocity;

    if (startLanded)
    {
      // @Todo find a better way to get the height
      float shipHeight = GetComponent<Collider>().bounds.size.y;
      float altitude = body.diameter / 2 + shipHeight / 2;

      // Start Landed
      rigidBody.MovePosition(body.transform.position + new Vector3(0, altitude, 0));
    }
    else
    {
      // Start Flying
      float altitude = body.getRadius() + 100f;
      Vector3 startPosition = body.Position + altitude * Vector3.up;
      rigidBody.MovePosition(startPosition);

      // rigidBody.MovePosition(body.transform.position + (transform.position - body.transform.position).normalized * body.radius * altitudeMultiplier);
    }
  }
}
