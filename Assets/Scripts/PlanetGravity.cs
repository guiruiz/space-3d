using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
  private float G = 30000;
  public GameObject objectToAttract;

  Rigidbody rb;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void FixedUpdate()
  {
    Attract();
  }

  void Attract()
  {
    Rigidbody rbToAttract = objectToAttract.GetComponent<Rigidbody>();

    Vector3 direction = rb.position - rbToAttract.position;
    float distance = direction.magnitude;

    float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
    Vector3 force = direction.normalized * forceMagnitude;

    rbToAttract.AddForce(force);
  }
}
