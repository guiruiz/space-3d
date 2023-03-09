using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class Path : MonoBehaviour
{

  public GameObject rocket;
  public GameObject pathPointModel;
  public bool shouldDisplay = true;

  private int pathPointsCount = 10;
  private int pointOffset = 5;
  private GameObject[] pathPoints;

  void Start()
  {
    StartPathPoints();

    //CreatePhysicsScene();
  }

  void Update()
  {
    UpdatePathPoints();

    //SimulateTrajectory(rocket.transform.position, rocket.GetComponent<Rigidbody>().velocity);
  }

  void StartPathPoints()
  {
    pathPoints = new GameObject[pathPointsCount];
    for (int i = 0; i < pathPointsCount; i++)
    {
      Vector3 position = Vector3.zero;
      Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);
      var spawned = Instantiate(pathPointModel, rocket.transform.position, spawnRotation, transform);
      pathPoints[i] = spawned;
    }
  }



  void UpdatePathPoints()
  {
    Rigidbody rocketRb = rocket.GetComponent<Rigidbody>();


    Vector3 lastPosition = rocket.transform.position;
    Vector3 lastVelocity = rocketRb.velocity;
    for (int i = 0; i < pathPointsCount; i++)
    {
      GameObject point = pathPoints[i];
      //point.transform.position = rocket.transform.position + rocket.transform.TransformDirection(Vector3.forward) * (i + 1) * pointOffset;

      Vector3 newVelocity = CalculateFinalVelocity(lastPosition, rocketRb.mass, lastVelocity);

      Vector3 newPosition = lastPosition;
      newPosition += newVelocity * Time.fixedDeltaTime * 2;

      point.transform.position = newPosition;


      lastPosition = newPosition;
      lastVelocity = newVelocity;
      point.SetActive(shouldDisplay);
    }
  }


  // private Scene simulationScene;
  // private PhysicsScene physicsScene;
  // [SerializeField] private LineRenderer line;
  // [SerializeField] private int maxPhysicsFrameIterations = 100;
  // public GameObject prefab;
  // private readonly Dictionary<Transform, Transform> spawnedObjects = new Dictionary<Transform, Transform>();

  // void CreatePhysicsScene()
  // {
  //   simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
  //   physicsScene = simulationScene.GetPhysicsScene();

  //   GameObject[] celestialBodies = GameObject.FindGameObjectsWithTag("CelestialBody");
  //   foreach (GameObject obj in celestialBodies)
  //   {
  //     var ghostObj = Instantiate(obj, obj.transform.position, obj.transform.rotation);
  //     ghostObj.GetComponent<Renderer>().enabled = false;
  //     ghostObj.GetComponent<PlanetController>().objectToAttract = null;
  //     SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);
  //     if (!ghostObj.isStatic) spawnedObjects.Add(obj.transform, ghostObj.transform);
  //   }
  // }

  // void UpdateSpawned()
  // {
  //   foreach (var item in spawnedObjects)
  //   {
  //     item.Value.position = item.Key.position;
  //     item.Value.rotation = item.Key.rotation;
  //   }
  // }

  // public void SimulateTrajectory(Vector3 pos, Vector3 intialVelocity)
  // {
  //   var ghostObj = Instantiate(prefab, pos, Quaternion.identity);
  //   //ghostObj.GetComponent<Renderer>().enabled = false;
  //   SceneManager.MoveGameObjectToScene(ghostObj.gameObject, simulationScene);



  //   var ghostObjRb = ghostObj.GetComponent<Rigidbody>();
  //   ghostObj.GetComponent<Rigidbody>().velocity = CalculateFinalVelocity(ghostObjRb.position, ghostObjRb.mass, intialVelocity);

  //   line.positionCount = maxPhysicsFrameIterations;

  //   for (int i = 0; i < maxPhysicsFrameIterations; i++)
  //   {
  //     physicsScene.Simulate(Time.fixedDeltaTime);
  //     line.SetPosition(i, ghostObj.transform.position);
  //   }

  //   Destroy(ghostObj);
  // }

  Vector3 CalculateFinalVelocity(Vector3 position, float mass, Vector3 intialVelocity)
  {

    Vector3 finalVelocity = Vector3.zero;

    finalVelocity += intialVelocity;

    GameObject[] celestialBodies = GameObject.FindGameObjectsWithTag("CelestialBody");
    foreach (var body in celestialBodies)
    {
      PlanetController controller = body.GetComponent<PlanetController>();
      finalVelocity += controller.CalculateGravity(position, mass);
    }
    return finalVelocity;
  }

}