using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class CelestialBody : MonoBehaviour
{
  public float radius;
  public float surfaceGravity;
  public Vector3 initialVelocity;
  Transform meshHolder;

  public Vector3 velocity { get; private set; }
  public float mass { get; private set; }
  public new Rigidbody rigidbody;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
  }

  void Awake()
  {
    velocity = initialVelocity;
  }

  public void UpdateVelocity(CelestialBody[] allBodies, float timeStamp)
  {
    foreach (var otherBody in allBodies)
    {
      if (otherBody != this)
      {
        float sqrDst = (otherBody.rigidbody.position - rigidbody.position).sqrMagnitude;
        Vector3 forceDir = (otherBody.rigidbody.position - rigidbody.position).normalized;
        Vector3 force = forceDir * Universe.gravitationalConstant * mass * otherBody.mass / sqrDst;

        Vector3 acceleration = force / mass;
        velocity += acceleration * timeStamp;
      }
    }
  }

  void OnValidate()
  {
    mass = surfaceGravity * radius * radius / Universe.gravitationalConstant;
    meshHolder = transform.GetChild(0);
    meshHolder.localScale = Vector3.one * radius;
  }

  public void UpdatePosition(float timeStep)
  {
    rigidbody.position += velocity * timeStep;
  }


  public void SetInitialVelocity(CelestialBody[] allBodies)
  {
    foreach (var otherBody in allBodies)
    {
      if (!this.Equals(otherBody))
      {
        float m2 = otherBody.mass;
        float r = Vector3.Distance(this.transform.position, otherBody.transform.position);
        this.transform.LookAt(otherBody.transform);

        this.rigidbody.velocity += this.transform.right * Mathf.Sqrt((Universe.gravitationalConstant * m2) / r);
      }
    }
  }

  public Vector3 Position
  {
    get
    {
      return rigidbody.position;
    }
  }

}
