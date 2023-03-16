using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
  static Universe instance;

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

  public static Vector3 CalculateAcceleration(Vector3 point, CelestialBody ignoreBody = null)
  {
    Vector3 acceleration = Vector3.zero;
    foreach (var body in Instance.bodies)
    {
      if (body != ignoreBody)
      {
        // @Todo extract to Math class
        Vector3 forceDir = (body.Position - point).normalized;
        float sqrDst = (body.Position - point).sqrMagnitude;
        acceleration += forceDir * Universe.gravitationalConstant * body.mass / sqrDst;
      }
    }

    return acceleration;
  }

  public static CelestialBody[] Bodies
  {
    get
    {
      return Instance.bodies;
    }
  }

  static Universe Instance
  {
    get
    {
      if (instance == null)
      {
        instance = FindObjectOfType<Universe>();
      }
      return instance;
    }
  }

}
