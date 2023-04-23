using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class CelestialBody : MonoBehaviour
{

  public float diameter;
  public float surfaceGravity;

  public Vector3 initialVelocity;
  [SerializeField, ReadOnly]
  public Vector3 velocity;
  [SerializeField, ReadOnly]
  public float mass = 0;

  public Rigidbody rb { get; private set; }
  private Transform meshHolder;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
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
        float sqrDst = (otherBody.rb.position - rb.position).sqrMagnitude;
        Vector3 forceDir = (otherBody.rb.position - rb.position).normalized;
        Vector3 force = forceDir * Universe.gravitationalConstant * mass * otherBody.mass / sqrDst;

        Vector3 acceleration = force / mass;
        velocity += acceleration * timeStamp;
      }
    }
  }

  void OnValidate()
  {
    mass = surfaceGravity * diameter * diameter / Universe.gravitationalConstant;
    meshHolder = transform.GetChild(0);
    meshHolder.localScale = Vector3.one * diameter;
  }

  public void UpdatePosition(float timeStep)
  {
    rb.position += velocity * timeStep;
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

        this.rb.velocity += this.transform.right * Mathf.Sqrt((Universe.gravitationalConstant * m2) / r);
      }
    }
  }

  public float getRadius()
  {
    return diameter / 2;
  }

  public Vector3 Position
  {
    get
    {
      return rb.position;
    }
  }

}
