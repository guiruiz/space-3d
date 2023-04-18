using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{

  public GameObject planet;
  public float moveSpeed = 10f;

  private Rigidbody rigidBody;
  private Rigidbody planetRigidBody;

  private Vector3 moveDir;
  void Start()
  {
    rigidBody = GetComponent<Rigidbody>();
    planetRigidBody = planet.GetComponent<Rigidbody>();

    rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    rigidBody.useGravity = false;
  }

  void Update()
  {
    moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

  }
  void FixedUpdate()
  {
    rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
  }


}
