using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
  public const float gravitationalConstant = 0.0001f;
  public const float physicsTimeStep = 0.01f;

  CelestialBody[] bodies;

  void Awake()
  {
    bodies = FindObjectsOfType<CelestialBody>();
    Time.fixedDeltaTime = Universe.physicsTimeStep;


    // @Todo set vel auto or manually?
    // foreach (var body in bodies)
    // {
    //   body.SetInitialVelocity(bodies);
    // }
  }
  void FixedUpdate()
  {
    foreach (var body in bodies)
    {
      body.UpdateVelocity(bodies, Universe.physicsTimeStep);
    }

    foreach (var body in bodies)
    {
      body.UpdatePosition(Universe.physicsTimeStep);
    }
  }


}
