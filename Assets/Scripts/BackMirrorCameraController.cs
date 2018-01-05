using UnityEngine;
using System.Collections;

public class BackMirrorCameraController : MonoBehaviour {
	
	/*
	 * https://docs.unity3d.com/ScriptReference/Camera.OnPreCull.html
	 */

	private Camera camera;

	void Start () {
		camera = gameObject.GetComponent<Camera>();
	}
	
	void OnPreCull() {
		camera.ResetWorldToCameraMatrix();
		camera.ResetProjectionMatrix();
		camera.projectionMatrix = camera.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
	}

	void OnPreRender() {
		GL.SetRevertBackfacing(true);
	}

	void OnPostRender() {
		GL.SetRevertBackfacing(false);
	}

}
