using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuButtons : MonoBehaviour {
	private GameObject pauseMenuPanel;
	public Button restartButton;
	public Button resetButton;
	public Button courseSelectButton;

	void Start(){
		pauseMenuPanel = GameObject.Find ("PauseMenuPanel");
		pauseMenuPanel.SetActive (false);
		restartButton.onClick.AddListener (onRestartButtonClick);
		resetButton.onClick.AddListener (onResetButtonClick);
		courseSelectButton.onClick.AddListener (onCourseSelectButtonClick);
	}

	public void OnClick(){
		pauseMenuPanel.SetActive (true);
		Time.timeScale = 0.0f;
	}

	public void onRestartButtonClick(){
		Time.timeScale = 1.0f;
		pauseMenuPanel.SetActive (false);
	}

	public void onResetButtonClick() {
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void onCourseSelectButtonClick() {
		SceneManager.LoadScene("MainMenu");
	}

}
