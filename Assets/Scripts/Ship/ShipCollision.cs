using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollision : MonoBehaviour
{
  private Ship ship;
  private float maxLandingForce = 25f;
  private float maxLandingAngle = 20f;
  private float maxImpactForce = 30f;
  private float rebounceFactor = .5f;
  private float clippingThreshold = 0.1f;
  private float antiClippingForce = 1.0f;


  private ShipFlight shipFlight;

  void Start()
  {
    ship = GetComponent<Ship>();
    shipFlight = this.GetComponent<ShipFlight>();
  }

  void Update()
  {

  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("CelestialBody"))
    {
      CelestialBody body = collision.gameObject.GetComponent<CelestialBody>();
      float impactForce = CalculateCollisionImpact(collision);
      float impactAngle = CalculateCollisionAngle(collision);

      Debug.Log("IMPACT angle: " + impactAngle + ", force: " + impactForce);

      if (impactForce > maxImpactForce || impactAngle > maxLandingAngle)
      {
        // Explode Ship
        GetComponent<ShipTeleporter>().ResetShip();
        return;
      }

      // if (impactForce > maxLandingForce)
      // {
      //   RebounceShip(body);
      // }

      ship.LandShip(body);
    }
  }

  private void OnCollisionStay(Collision collision)
  {
    if (collision.gameObject.CompareTag("CelestialBody"))
    {
      CelestialBody body = collision.gameObject.GetComponent<CelestialBody>();

      if (body != null)
      {
        float angle = CalculateCollisionAngle(collision);
        ship.Velocity = body.velocity;

        Debug.Log("angle: " + angle);
        Vector3 normal = collision.contacts[0].normal;
        if (angle > 0)
        {
          //Rotate gradually to angle 0
          shipFlight.RotateTo(normal);
        }
      }
    }

    AvoidCliping(collision);
  }

  public float CalculateCollisionAngle(Collision collision)
  {
    Vector3 normal = collision.contacts[0].normal;
    float angle = Vector3.Angle(transform.up, normal);
    return angle;
  }

  public float CalculateCollisionImpact(Collision collision)
  {
    float impactForce = 0.0f;
    foreach (ContactPoint contact in collision.contacts)
    {
      CelestialBody body = collision.gameObject.GetComponent<CelestialBody>();
      Vector3 relativeVelocity = ship.Velocity - body.velocity;
      impactForce += relativeVelocity.magnitude * collision.rigidbody.mass; // @TODO check rb mass
    }

    return impactForce;
  }

  private void RebounceShip(CelestialBody body)
  {
    Vector3 relativeVelocity = ship.Velocity - body.velocity;
    Vector3 rebounceForce = relativeVelocity * rebounceFactor;
    ship.Velocity = body.velocity + (-rebounceForce);
    Debug.Log("Rebounce force: " + rebounceForce);
  }

  void AvoidCliping(Collision collision)
  {
    foreach (ContactPoint contact in collision.contacts)
    {
      Vector3 normal = contact.normal; // get collision normal
      Vector3 reactionForce = -normal * antiClippingForce;
      float distance = contact.separation; // get distance between colliders

      if (distance < clippingThreshold)
      {
        // apply reaction force
        ship.AddForce(reactionForce);
      }
    }
  }
}
