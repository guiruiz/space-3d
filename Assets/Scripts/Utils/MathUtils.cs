using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils
{

  public static float Map(float value, float minValue, float maxValue, float newMin, float newMax)
  {
    // Calculate the normalized value within the source range
    float normalizedValue = Mathf.InverseLerp(minValue, maxValue, value);

    // Map the normalized value to the target range
    float mappedValue = Mathf.Lerp(newMin, newMax, normalizedValue);

    // Return the mapped value
    return mappedValue;
  }
}
