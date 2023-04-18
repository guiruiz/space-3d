using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BckpCameraControl : MonoBehaviour
{
  public GameObject player;
  public float heightOffset = 8f;
  public float SmoothTime = 0.01f;

  private Vector3 velocity = Vector3.zero; // Updated in runtime by ref 

  void Start()
  {

  }

  void FixedUpdate()
  {
    Vector3 newPos = player.transform.position + player.transform.up * heightOffset;
    transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, SmoothTime);
  }


  void Update()
  {
    transform.LookAt(player.transform.position, player.transform.forward);
  }
}
