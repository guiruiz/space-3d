
using System;
using UnityEngine;

public interface IPlanetEvents
{
  public event Action<GameObject> OnGravityFieldEnter;
  public event Action<GameObject> OnGravityFieldExit;
}
