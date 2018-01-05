using UnityEngine;
using System.Collections;

public class TollGateController : MonoBehaviour {
	private GameObject car;
	private GameObject etcIndicator;
	private bool etcCard;
	private AudioSource[] audioSources;
	private bool etcWarning;
	private GameObject barLeft;
	private GameObject barRight;
	private Quaternion barLeftRotationDefault;
	private Quaternion barRightRotationDefault;

	void Start() {
		car = GameObject.Find ("Car");
		etcIndicator = GameObject.Find ("ETCIndicator");
		etcCard = car.GetComponent<CarEquipmentController> ().etcCard;
		audioSources = etcIndicator.GetComponents<AudioSource>();
		etcWarning = false;
		barLeft = gameObject.transform.Find ("BarLeftBase").gameObject;
		barRight = gameObject.transform.Find ("BarRightBase").gameObject;
		barLeftRotationDefault = barLeft.transform.rotation;
		barRightRotationDefault = barLeft.transform.rotation;
	}

	void FixedUpdate () {
		if (!etcCard && !etcWarning) {
			if ((car.transform.position - gameObject.transform.position).magnitude < 100.0f) {
				audioSources[1].Play();
				etcWarning = true;
			}
		}
		if (etcCard) {
			if ((car.transform.position - gameObject.transform.position).magnitude < 10.0f) {
				// open bar
				barLeft.transform.rotation = Quaternion.Euler (-90, -90, 90);
				barRight.transform.rotation = Quaternion.Euler (90, -90, 90);
			} else {
				barLeft.transform.rotation = barLeftRotationDefault;
				barRight.transform.rotation = barRightRotationDefault;
			}
		}
	}
}
