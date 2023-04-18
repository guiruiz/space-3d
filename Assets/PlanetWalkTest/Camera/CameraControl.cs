using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
  public GameObject player;
  public float SmoothTime = 0.01f;

  private Vector3 velocity = Vector3.zero; // Updated in runtime by ref 

  void Start()
  {

  }

  void FixedUpdate()
  {
    Vector3 newPos = player.transform.position + player.transform.up * distanceFromTarget;
    //transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, SmoothTime);
  }


  void Update()
  {

    OrbitCamera(defaultSmoothTime);
    ZoomCamera();

    //transform.LookAt(player.transform.position, player.transform.forward);
  }

  [SerializeField]
  private float distanceFromTarget = 8f;
  [SerializeField]
  private Vector2 zoomMinMax = new Vector2(5, 20);
  [SerializeField]
  private float mouseSensitivity = 3f;
  [SerializeField]
  private float zoomSensitivity = 5f;
  [SerializeField]
  private float defaultSmoothTime = 0.2f;

  [SerializeField]
  private float rotationH = 0;
  [SerializeField]
  private float rotationV = 90;


  private Vector3 currentRotation;
  private Vector3 smoothVelocity = Vector3.zero;


  void OrbitCamera(float smoothTime)
  {

    float mouseHorizontal = 0;
    float mouseVertical = 0;

    if (Input.GetMouseButton(1))
    {
      mouseHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity;
      mouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity;
    }

    rotationH += mouseHorizontal;
    rotationV += mouseVertical;


    Vector3 nextRotation = new Vector3(rotationV, rotationH, 0);

    // Apply damping between rotation changes
    //currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
    //transform.eulerAngles = currentRotation;

    transform.RotateAround(player.transform.position, nextRotation, mouseVertical * 50f * Time.deltaTime);
    transform.RotateAround(player.transform.position, nextRotation, mouseHorizontal * 50f * Time.deltaTime);

    // Substract forward vector of the GameObject to point its forward vector to the target
    //transform.position = player.transform.position - transform.forward * distanceFromTarget;
  }

  void ZoomCamera()
  {
    float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
    float newZoom = distanceFromTarget + mouseScroll * zoomSensitivity * -1;
    distanceFromTarget = Mathf.Clamp(newZoom, zoomMinMax.x, zoomMinMax.y);
  }
}
