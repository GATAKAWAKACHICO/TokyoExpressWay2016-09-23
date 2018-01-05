using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShutokoWaypoints : MonoBehaviour
{
	private GameObject car;
	private Rigidbody carRb;
	public GameObject[] otherCarPrefabs;
	public GameObject policeCarPrefab;
	public GameObject trafficJamPrefab;
	public List<GameObject> defaultWayPointList;
	private List<GameObject> dynamicWayPointList;
	private int currrentOtherCarWayPointIndex = -1;

	void Start ()
	{
		car = GameObject.Find ("Car");
		carRb = car.GetComponent<Rigidbody> ();
		dynamicWayPointList = defaultWayPointList;
	}

	void OnDrawGizmosSelected ()
	{
		/* Gizmos.color = Color.yellow;
		for (int i = 0; i < defaultWayPointList.Count - 1; i++) {
			Gizmos.DrawLine (defaultWayPointList [i].transform.position, defaultWayPointList [i + 1].transform.position);
		} */
	}

	void Update() {
		// Debug.Log (carRb.velocity.magnitude);
	}

	void FixedUpdate ()
	{
		generateOtherCarOnRoad ();
		policeCarAppear ();
		// trafficJamAppear ();
	}

	private bool isPointInsideSphere (Vector3 point, Vector3 center, float radius)
	{
		return Vector3.Distance (point, center) < radius;
	}

	private int getNearestWaypointIndexFromPlayer (List<GameObject> wayPointList)
	{
		GameObject nearestWayPoint = null;
		float minDist = Mathf.Infinity;
		int nearestWayPointIndex = 0;
		for (int i = 0; i < wayPointList.Count; i++) {
			float dist = Vector3.Distance (wayPointList [i].transform.position, car.transform.position);
			if (dist < minDist) {
				nearestWayPoint = wayPointList [i];
				nearestWayPointIndex = i;
				minDist = dist;
			}
		}
		return nearestWayPointIndex;
	}

	private void generateOtherCarOnRoad ()
	{
		GameObject[] otherCars = GameObject.FindGameObjectsWithTag ("OtherCar");
		int nearestOtherCarWayPointIndex = getNearestWaypointIndexFromPlayer (dynamicWayPointList);
		int otherCarType = Random.Range(0, otherCarPrefabs.Length);
		if (currrentOtherCarWayPointIndex != nearestOtherCarWayPointIndex) {
			if ((dynamicWayPointList [nearestOtherCarWayPointIndex].transform.position - car.transform.position).magnitude < 100.0f) {
				Quaternion nextWaypointRotation = Quaternion.LookRotation(dynamicWayPointList [nearestOtherCarWayPointIndex+1].transform.position - dynamicWayPointList [nearestOtherCarWayPointIndex].transform.position);
				if (otherCars.Length < 10) {
					Instantiate (
						otherCarPrefabs [otherCarType],
						dynamicWayPointList [nearestOtherCarWayPointIndex].transform.position,
						nextWaypointRotation
					);
				}
			}
		}
		currrentOtherCarWayPointIndex = nearestOtherCarWayPointIndex;
	}

	private void policeCarAppear(){
		GameObject policeCar = GameObject.FindGameObjectWithTag ("PoliceCar");
		if (policeCar == null && carRb.velocity.magnitude > 25.0f) {
			Instantiate (
				policeCarPrefab,
				dynamicWayPointList [getNearestWaypointIndexFromPlayer(dynamicWayPointList) - 1].transform.position,
				car.transform.rotation);
		}
	}

	private void trafficJamAppear() {
		GameObject trafficJam = GameObject.FindGameObjectWithTag ("TrafficJam");
		int nearestOtherCarWayPointIndex = getNearestWaypointIndexFromPlayer (dynamicWayPointList);
		if (trafficJam == null && carRb.velocity.magnitude > 10.0f && currrentOtherCarWayPointIndex != 0 && currrentOtherCarWayPointIndex % 5 == 0 && GameObject.FindGameObjectsWithTag ("OtherCar").Length < 5) {
			Instantiate (
					trafficJamPrefab,
					dynamicWayPointList [nearestOtherCarWayPointIndex + 1].transform.position,
					dynamicWayPointList [nearestOtherCarWayPointIndex + 1].transform.rotation);
			StartCoroutine (destroyDrafficJam (trafficJam));
		}
	}

	private IEnumerator destroyDrafficJam(GameObject trafficJam){
		yield return new WaitForSeconds(6000);
		Destroy (trafficJam);
	}

}
