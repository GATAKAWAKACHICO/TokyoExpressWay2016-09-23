using UnityEngine;
using System.Collections;

public class NavController : MonoBehaviour {
	private GameObject car;
	private NavMeshAgent agent;
	public Transform target;

	void Start () {
		car = GameObject.Find ("Car");
		agent = GetComponent<NavMeshAgent>();
	}

	void FixedUpdate(){
		agent.destination = target.position;
		if ((gameObject.transform.position - car.transform.position).magnitude > 100.0f) {
			Destroy (gameObject);
		}
	}
}
