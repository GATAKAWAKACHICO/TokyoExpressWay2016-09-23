using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CarDamageController : MonoBehaviour {
	private GameObject car;

	private GameObject[] otherCars;
	private Transform[] otherCarsDefaultTransforms;

	private Slider hpSlider;
	public Text gameOverText;
	public GameObject mobileSingleStickControl;

	void Awake() {
		otherCars = GameObject.FindGameObjectsWithTag ("OtherCar");
		otherCarsDefaultTransforms = new Transform[otherCars.Length]; 
		for (int i = 0; i < otherCars.Length; i++) {
			otherCarsDefaultTransforms [i] = otherCars [i].transform;
		}
	}

	void Start () {
		car = GameObject.Find ("Car");
		hpSlider = GameObject.Find("Slider").GetComponent<Slider> ();
		gameOverText.text = "";
	}
	
	void FixedUpdate () {
		if (car.transform.position.y < -100.0f){
			gameOverText.text = "Game Over";
			StartCoroutine (resetGame());
		}

		if (hpSlider.value <= 0){
			gameOverText.text = "Game Over";
			StartCoroutine (resetGame());
		}
	}

	void OnCollisionEnter(Collision collision) {
		// Debug.Log (collision.relativeVelocity.magnitude);
		hpSlider.value = hpSlider.value - collision.relativeVelocity.magnitude;
		if (hpSlider.value <= 0)
			hpSlider.value = 0;
	}

	public IEnumerator resetGame(){
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
