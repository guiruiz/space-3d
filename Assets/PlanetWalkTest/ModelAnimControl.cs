using UnityEngine;

public class ModelAnimControl : MonoBehaviour
{
  public float turnSpeed = 5f;

  // Update is called once per frame
  void Update()
  {

    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");

    if (x != 0 || y != 0)
    {
      var newAngle = Mathf.Atan2(x, y) * 180 / Mathf.PI;

      float angleLerp = Mathf.LerpAngle(transform.localEulerAngles.y, newAngle, turnSpeed * Time.deltaTime);


      Vector3 q = transform.localEulerAngles;
      q.y = angleLerp;

      transform.localEulerAngles = q;

    }
  }
}
