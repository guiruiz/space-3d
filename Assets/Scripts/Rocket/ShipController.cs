using UnityEngine;


public class ShipController : MonoBehaviour
{
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
}
