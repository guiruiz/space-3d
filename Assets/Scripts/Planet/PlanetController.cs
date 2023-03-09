using System;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
  public new string name;
  private float G = 30000;
  public float rotationSpeed = 0f;
  public float orbitSpeed = 0f;
  public GameObject pivot;

  public GameObject objectToAttract;
  private bool shouldAttract = true;


  Rigidbody rb;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
  }

  void FixedUpdate()
  {
    Attract();
    Rotation();
    Orbit();
  }

  void Attract()
  {
    //if (!shouldAttract) { return; }

    if (!objectToAttract) { return; }

    Rigidbody rbToAttract = objectToAttract.GetComponent<Rigidbody>();

    Vector3 force = CalculateGravity(rbToAttract.position, rbToAttract.mass);

    rbToAttract.AddForce(force);
  }

  public Vector3 CalculateGravity(Vector3 position, float mass)
  {
    Vector3 direction = rb.position - position;
    float distance = direction.magnitude;

    float forceMagnitude = G * (rb.mass * mass) / Mathf.Pow(distance, 2);
    Vector3 force = direction.normalized * forceMagnitude;

    return force;
  }

  void Rotation()
  {
    transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
  }

  void Orbit()
  {
    if (!pivot) { return; }

    transform.RotateAround(pivot.transform.position, new Vector3(0, 1, 0), orbitSpeed * Time.deltaTime);
  }

  void OnTriggerEnter(Collider col)
  {
    if (col.gameObject.tag == "Rocket")
    {
      shouldAttract = false;
      EventManager.Instance.GravityFieldEnter(this);
    }
  }

  void OnTriggerExit(Collider col)
  {
    if (col.gameObject.tag == "Rocket")
    {
      shouldAttract = true;
      EventManager.Instance.GravityFieldExit(this);
    }
  }
}
