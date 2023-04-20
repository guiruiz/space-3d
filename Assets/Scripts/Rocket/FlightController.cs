using UnityEngine;

public class FlightController : MonoBehaviour
{
  int yawSpeed = 40;
  int pitchSpeed = 40;
  int rollSpeed = 40;

  float throttleSensitivity = .4f;
  public float maxThrust = 100f;
  public float throttle { get; private set; } = 0;

  private Rigidbody rigidBody;

  public void Start()
  {
    this.rigidBody = GetComponent<Rigidbody>();
  }

  public void Update()
  {
    RotationControl();
    ThrottleControl();
  }

  public void FixedUpdate()
  {
    Thrust();
  }

  public float GetThrottle()
  {
    return throttle;
  }

  public void ThrottleControl()
  {
    float t = throttle;

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
      t = throttle + Time.deltaTime * throttleSensitivity;
    }
    else if (Input.GetKey(KeyCode.LeftControl))
    {
      t = throttle - Time.deltaTime * throttleSensitivity;
    }

    t = Mathf.Clamp(t, 0, 1f);

    throttle = t;
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

  public void Thrust()
  {
    if (throttle > 0.02f)
    {
      rigidBody.AddForce(transform.TransformDirection(Vector3.up) * maxThrust * throttle, ForceMode.Acceleration);
    }
  }
}
