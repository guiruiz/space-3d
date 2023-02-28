using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbiter : MonoBehaviour
{
  public float rotationSpeed = 0f;
  public float orbitSpeed = 0f;
  public GameObject pivot;

  // Update is called once per frame
  void Update()
  {
    transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    transform.RotateAround(pivot.transform.position, new Vector3(0, 1, 0), orbitSpeed * Time.deltaTime);
  }
}
