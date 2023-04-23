using UnityEngine;

public class FlightController : MonoBehaviour
{
  public float maxThrust = 100f;

  [SerializeField, ReadOnly]

  private float throttle = 0;

  [SerializeField, ReadOnly]
  public Vector3 velocity = Vector3.zero;

  private Rigidbody rb;
  private int yawSpeed = 40;
  private int pitchSpeed = 40;
  private int rollSpeed = 40;

  private float throttleSensitivity = .4f;


  public void Start()
  {
    this.rb = GetComponent<Rigidbody>();
  }
  void Awake()
  {
  }

  public void Update()
  {
    RotationControl();
    if (landedBody == null)
    {
    }
    else
    {
      if (throttle > 0)
      {
        landedBody = null;
      }
    }

    ThrottleControl();
  }

  public void FixedUpdate()
  {
    UpdateVelocity();
    UpdatePosition();
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

  public void AddForce(Vector3 force, float mass = 1f)
  {
    Vector3 acceleration = force / mass;
    velocity += acceleration * Universe.physicsTimeStep;
  }

  public void UpdateVelocity()
  {
    Vector3 gravity = Universe.CalculateAcceleration(rb.position);
    Vector3 v = (velocity + gravity) * Universe.physicsTimeStep;

    Vector3 t = transform.TransformDirection(Vector3.up) * (maxThrust * throttle);
    AddForce(gravity + t);
  }

  public void UpdatePosition()
  {
    rb.position += velocity * Universe.physicsTimeStep;
  }

  public float rotationSpeed = 20.0f; // Rotation speed

  void RotateTo(Vector3 target)
  {
    // @TODO create rotationTarget, move rotation logic to Fixed and refactor RotationControl
    Quaternion targetRotation = Quaternion.FromToRotation(transform.up, target) * transform.rotation;

    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
  }

  public float CalculateCollisionImpact(Collision collision)
  {
    float impactForce = 0.0f;
    foreach (ContactPoint contact in collision.contacts)
    {
      CelestialBody body = collision.gameObject.GetComponent<CelestialBody>();
      Vector3 relativeVelocity = velocity - body.velocity;
      impactForce += relativeVelocity.magnitude * collision.rigidbody.mass;
    }

    return impactForce;
  }

  public float CalculateCollisionAngle(Collision collision)
  {
    Vector3 normal = collision.contacts[0].normal;
    float angle = Vector3.Angle(transform.up, normal);
    return angle;
  }

  private float maxLandingForce = 25f;
  private float maxLandingAngle = 20f;
  private float maxImpactForce = 30f;
  private float rebounceFactor = .5f;

  [SerializeField, ReadOnly]
  private CelestialBody landedBody;

  private float clippingThreshold = 0.1f;
  private float antiClippingForce = 1.0f;


  private void LandShip(CelestialBody body)
  {
    landedBody = body;
    throttle = 0;
    Debug.Log("Ship Landed");
  }

  private void RebounceShip(CelestialBody body)
  {
    Vector3 relativeVelocity = velocity - body.velocity;
    Vector3 rebounceForce = relativeVelocity * rebounceFactor;
    velocity = body.velocity + (-rebounceForce);
    Debug.Log("Rebounce force: " + rebounceForce);
  }

  private void OnCollisionEnter(Collision collision)
  {

    if (collision.gameObject.CompareTag("CelestialBody"))
    {
      CelestialBody body = collision.gameObject.GetComponent<CelestialBody>();
      float impactForce = CalculateCollisionImpact(collision);
      float impactAngle = CalculateCollisionAngle(collision);



      Debug.Log("IMPACT angle: " + impactAngle + ", force: " + impactForce);
      if (impactForce > maxImpactForce || impactAngle > maxLandingAngle)
      {
        // Explode Ship
        GetComponent<ShipTeleporter>().ResetShip();
        return;
      }

      // if (impactForce > maxLandingForce)
      // {
      //   RebounceShip(body);
      // }

      LandShip(body);
    }
  }

  private void OnCollisionStay(Collision collision)
  {
    if (collision.gameObject.CompareTag("CelestialBody"))
    {
      CelestialBody body = collision.gameObject.GetComponent<CelestialBody>();

      if (body != null)
      {
        float angle = CalculateCollisionAngle(collision);
        velocity = body.velocity;


        Debug.Log("angle: " + angle);
        Vector3 normal = collision.contacts[0].normal;
        if (angle > 0)
        {

          //Rotate gradually to angle 0
          RotateTo(normal);
        }
      }


    }

    AvoidCliping(collision);
  }


  void AvoidCliping(Collision collision)
  {
    foreach (ContactPoint contact in collision.contacts)
    {
      Vector3 normal = contact.normal; // Get collision normal
      Vector3 moveForce = -normal * antiClippingForce; // Calculate move direction away from the collision
      float distance = contact.separation; // Get distance between colliders

      if (distance < clippingThreshold)
      {
        // Apply force to move the object away from the collision
        AddForce(moveForce);
      }
    }
  }
}
