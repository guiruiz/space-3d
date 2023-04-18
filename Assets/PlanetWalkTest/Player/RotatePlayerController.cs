using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerController : MonoBehaviour
{

  public GameObject planet;
  public float moveSpeed = 10f;
  public float rollSpeed = 200f;

  private Rigidbody rigidBody;
  private Rigidbody planetRigidBody;
  float hInput = 0;
  float vInput = 0;

  void Start()
  {
    rigidBody = GetComponent<Rigidbody>();
    planetRigidBody = planet.GetComponent<Rigidbody>();

    rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    rigidBody.useGravity = false;
  }

  void Update()
  {
    hInput = Input.GetAxisRaw("Horizontal");
    vInput = Input.GetAxisRaw("Vertical");


  }

  void FixedUpdate()
  {
    transform.Rotate(hInput * Vector3.up * rollSpeed * Time.deltaTime);

    rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(vInput * Vector3.forward) * moveSpeed * Time.deltaTime);


    //rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
  }
}
