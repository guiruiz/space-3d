using UnityEngine;

public class ShipFlight : MonoBehaviour
{
  private Ship ship;

  private int yawSpeed = 40;
  private int pitchSpeed = 40;
  private int rollSpeed = 40;

  private float throttleSensitivity = .4f;

  private float autoRotationSpeed = 20.0f; // Auto Rotation speed

  public void Start()
  {
    ship = GetComponent<Ship>();
  }

  public void Update()
  {
    if (ship.LandedBody == null)
    {
      RotationControl();
    }

    ThrottleControl();
  }

  public void ThrottleControl()
  {
    float t = ship.Throttle;

    if (Input.GetKey(KeyCode.X))
    {
      t = 0;
    }
    else if (Input.GetKey(KeyCode.Z))
    {
      t = 1;
    }
    else if (Input.GetKey(KeyCode.LeftShift))
    {
      t = ship.Throttle + Time.deltaTime * throttleSensitivity;
    }
    else if (Input.GetKey(KeyCode.LeftControl))
    {
      t = ship.Throttle - Time.deltaTime * throttleSensitivity;
    }

    t = Mathf.Clamp(t, 0, 1f);

    ship.Throttle = t;
  }

  public void RotationControl()
  {
    // ROLL
    if (Input.GetKey(KeyCode.A))
    {
      transform.Rotate(-Vector3.forward * rollSpeed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      transform.Rotate(Vector3.forward * rollSpeed * Time.deltaTime);
    }

    // PITCH
    if (Input.GetKey(KeyCode.W))
    {
      transform.Rotate(-Vector3.right * pitchSpeed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.S))
    {
      transform.Rotate(Vector3.right * pitchSpeed * Time.deltaTime);
    }

    // YAW
    if (Input.GetKey(KeyCode.Q))
    {
      transform.Rotate(Vector3.up * yawSpeed * Time.deltaTime);
    }
    else if (Input.GetKey(KeyCode.E))
    {
      transform.Rotate(-Vector3.up * yawSpeed * Time.deltaTime);
    }
  }

  public void RotateTo(Vector3 target)
  {
    // @TODO create rotationTarget, move rotation logic to Fixed and refactor RotationControl
    Quaternion targetRotation = Quaternion.FromToRotation(transform.up, target) * transform.rotation;

    // @TODO try rb.rotation
    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, autoRotationSpeed * Time.deltaTime);
  }
}
