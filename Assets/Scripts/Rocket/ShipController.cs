using UnityEngine;


public class ShipController : MonoBehaviour
{
  public CelestialBody landedBody;
  private Rigidbody rb;

  void Awake()
  {
    EventManager.OnGravityFieldEnter += OnGravityFieldEnter;
    EventManager.OnGravityFieldExit += OnGravityFieldExit;
  }

  void OnDestroy()
  {
    EventManager.OnGravityFieldEnter -= OnGravityFieldEnter;
    EventManager.OnGravityFieldExit -= OnGravityFieldExit;
  }

  void Start()
  {
    rb = this.GetComponent<Rigidbody>();

    // Set ship in orbit at alt 97m (y = 200)
    //rigidBody.AddForce(transform.TransformDirection(Vector3.left) * 1500f);
  }

  void Update()
  {
    // Set max speed
    // float maxSpeed = 30f;
    // if (rigidBody.velocity.magnitude > maxSpeed)
    // {
    //   rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxSpeed);
    // }


  }

  void FixedUpdate()
  {
    if (landedBody)
    {
      //rigidBody.velocity = landedBody.velocity;
    }

    //Vector3 gravity = Universe.CalculateAcceleration(rb.position);
    //rb.AddForce(gravity + (landedBody.velocity - rb.velocity), ForceMode.Acceleration);


    //Vector3 velocity = (rb.velocity + gravity) * Universe.physicsTimeStep;
    //rb.position += velocity * Universe.physicsTimeStep;

  }

  public void OnGravityFieldEnter(PlanetController planet)
  {
    // setFlightMode(FlightMode.Planet, planet);
  }

  public void OnGravityFieldExit(PlanetController planet)
  {
    // setFlightMode(FlightMode.Space);
  }

  void OnTriggerEnter(Collider col)
  {

    if (col.gameObject.tag == "CelestialBody")
    {
      //rb.velocity = landedBody.velocity;
    }
  }

  //public float decelerationFactor = 0.5f; // Deceleration factor to control how much the ship should decelerate upon collision
  // private void OnCollisionEnter(Collision collision)
  // {
  //   Debug.Log("asdasd");
  //   if (collision.gameObject.CompareTag("CelestialBody")) // Check if colliding with the planet
  //   {
  //     Vector3 collisionNormal = collision.contacts[0].normal; // Get the normal of the collision point

  //     // Calculate deceleration force
  //     Vector3 decelerationForce = -rb.velocity.normalized * decelerationFactor * rb.mass;

  //     // Apply deceleration force to the ship
  //     rb.AddForce(decelerationForce, ForceMode.Force);
  //   }
  // }
}
