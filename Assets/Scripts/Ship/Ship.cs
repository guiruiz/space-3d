using UnityEngine;
public class Ship : MonoBehaviour
{
  [SerializeField, ReadOnly]
  private float _throttle = 0f;

  [SerializeField, ReadOnly]
  private float _thrust = 0f;

  [SerializeField, ReadOnly]
  private Vector3 _velocity = Vector3.zero;

  [SerializeField, ReadOnly]
  private CelestialBody _landedBody;

  private Rigidbody rb;

  void Start()
  {
    rb = this.GetComponent<Rigidbody>();
  }

  public void FixedUpdate()
  {
    UpdateVelocity();
    UpdatePosition();
  }

  void Update()
  {

    if (LandedBody != null && Throttle > 0)
    {
      LandedBody = null;
    }

    //SetMaxSpeed();
  }

  // void SetMaxSpeed()
  // {
  //   float maxSpeed = 30f;
  //   if (velocity.magnitude > maxSpeed)
  //   {
  //     velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
  //   }
  // }

  public void UpdateVelocity()
  {
    Vector3 gravity = Universe.CalculateAcceleration(rb.position);
    Vector3 v = (Velocity + gravity) * Universe.physicsTimeStep;

    Vector3 acceleration = transform.TransformDirection(Vector3.up) * (MaxThrust * Throttle);
    AddForce(gravity + acceleration);
  }

  public void UpdatePosition()
  {
    rb.position += Velocity * Universe.physicsTimeStep;
  }

  public void AddForce(Vector3 force)
  {
    float mass = 1f; // not effective
    Vector3 acceleration = force / mass;
    _velocity += acceleration * Universe.physicsTimeStep;
  }

  public Vector3 Velocity
  {
    get
    {
      return _velocity;
    }
    set
    {
      _velocity = value;
    }
  }

  public float Throttle
  {
    get
    {
      return _throttle;
    }
    set
    {
      _throttle = value;
    }
  }

  public float Thrust
  {
    get
    {
      return _thrust;
    }
    set
    {
      _thrust = value;
    }
  }


  public CelestialBody LandedBody
  {
    get
    {
      return _landedBody;
    }
    set
    {
      _landedBody = value;
    }
  }

  public float MaxThrust = 50f;


  public void LandShip(CelestialBody body)
  {
    LandedBody = body;
    Throttle = 0;
    Debug.Log("Ship Landed");
  }


}
