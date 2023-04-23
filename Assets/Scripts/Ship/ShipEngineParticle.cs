using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEngineParticle : MonoBehaviour
{
  public ParticleSystem particles;
  public float minSpeed = 0f;
  public float maxSpeed = 20.0f;

  private Ship ship;

  void Start()
  {
    ship = GetComponent<Ship>();
    particles.gameObject.SetActive(false);
  }

  void Update()
  {
    ParticleSystem.MainModule mainModule = particles.main;

    if (ship.Throttle > 0)
    {
      float speed = MathUtils.Map(ship.Throttle, 0, 1, minSpeed, maxSpeed);
      mainModule.startSpeed = speed;
      particles.gameObject.SetActive(true);

    }
    else
    {
      particles.gameObject.SetActive(false);
    }
  }
}
