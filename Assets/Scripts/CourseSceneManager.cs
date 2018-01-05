using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CourseSceneManager : MonoBehaviour {

	public void OnClick() {
		SceneManager.LoadScene(gameObject.name);
	}

}
