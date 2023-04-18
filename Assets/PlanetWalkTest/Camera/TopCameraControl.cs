using UnityEngine;

public class TopCameraControl : MonoBehaviour
{
  public GameObject player;
  public float zoom = 10f;
  public bool useWorldUp = true;

  void Start()
  {

  }

  void FixedUpdate()
  {
    Vector3 upPosition = player.transform.position + player.transform.up * zoom;

    transform.position = upPosition;

    Vector3 worldUp = useWorldUp ? Vector3.up : player.transform.forward;
    transform.LookAt(player.transform, worldUp);
  }


  void Update()
  {

  }

}
