using System;
using UnityEngine;

public class ShipEvents : StaticInstance<ShipEvents>
{
  public static event Action<float> OnSetThrottle;


}
