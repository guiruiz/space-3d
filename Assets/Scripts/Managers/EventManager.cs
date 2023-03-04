using System;
using UnityEngine;

public class EventManager : StaticInstance<EventManager>
{
  public static event Action<PlanetController> OnGravityFieldEnter;
  public static event Action<PlanetController> OnGravityFieldExit;


  public void GravityFieldEnter(PlanetController planet)
  {
    OnGravityFieldEnter?.Invoke(planet);
  }

  public void GravityFieldExit(PlanetController planet)
  {
    OnGravityFieldExit?.Invoke(planet);
  }
}
