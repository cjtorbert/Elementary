using UnityEngine;
using System.Collections;

public class OrbController : MonoBehaviour {

	public Transform obeliskTransform;
	public Transform homeTransform;

	public void returnToObelisk() {
		transform.parent = obeliskTransform;
		resetLocalXform();
	}

	public void transferToHome() {
		transform.parent = homeTransform;
		LevelController.Get().orbAcquired(this);
		resetLocalXform();
	}

	private void resetLocalXform() {
		transform.localPosition = new Vector3(0,0,0);
		transform.localRotation = Quaternion.Euler(0,0,0);
	}
}
