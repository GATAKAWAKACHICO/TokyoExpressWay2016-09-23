using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarEquipmentController : MonoBehaviour {
	public bool etcCard;
	private GameObject etcIndicator;
	private AudioSource[] audioSources;
	private RawImage raw;

	void Start () {
		etcIndicator = GameObject.Find ("ETCIndicator");
		audioSources = etcIndicator.GetComponents<AudioSource>();
		raw = etcIndicator.GetComponent<RawImage> ();
		// Debug.Log(raw.uvRect);
		// x:0.50, y:0.00, width:0.50, height:1.00
		if (etcCard) {
			raw.uvRect = new Rect(1.0f, 0.0f, 0.5f, 1.0f);
			audioSources[0].PlayDelayed(5.0f);
		} else {
			raw.uvRect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
			audioSources[1].PlayDelayed(5.0f);
		}
	}

}
