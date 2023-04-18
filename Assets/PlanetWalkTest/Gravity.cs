using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
  public GameObject player;
  private float rotSpeed = 50f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    Attract();
  }

  void Attract()
  {
    Transform playerTransform = player.GetComponent<Transform>();
    Rigidbody playerRigidBody = player.GetComponent<Rigidbody>();

    float gravity = 10; //@TODO read from planet
    Vector3 toCenter = (transform.position - playerTransform.position).normalized;

    playerRigidBody.AddForce(toCenter * gravity);

    Vector3 playerUp = playerTransform.up;
    Quaternion targetRotation = Quaternion.FromToRotation(playerUp, -toCenter) * playerTransform.rotation;
    playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotSpeed * Time.deltaTime);
  }
}
