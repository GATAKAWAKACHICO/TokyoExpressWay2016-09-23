using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScoreController : MonoBehaviour {
	private GameObject car;
	private Vector3 startCarPosition;
	private Text scoreText;
	private int score;

	void Start () {
		score = 0;
		scoreText = gameObject.GetComponent<Text>();
		scoreText.text = String.Format ("score: {0}", score);
		car = GameObject.Find ("Car");
		startCarPosition = car.transform.position;
	}

	void Update() {
		if (score < 0)
			score = 0;
		scoreText.text = String.Format ("score: {0}", score);
	}

	void FixedUpdate () {
		score = (int) Vector3.Distance (startCarPosition, car.transform.position);
	}

}
