using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float mSpeed = Constants.PLAYER_TARGET_SPEED;
	public bool mLookAt = false;

	private Vector3 mDestination;
	private bool mHasDestination = false;

	// Use this for initialization
	void Start () {
		Update();
	}

	public void setSpeed(float speed) {
		mSpeed = speed;
	}

	public void setDestination(Vector3 destination) {
		mHasDestination = true;
		mDestination = destination;
	}

	public void setLookAt(bool lookAt) {
		mLookAt = lookAt;
	}
	
	// Update is called once per frame
	void Update () {
		if (!mHasDestination) {
			transform.position += transform.forward * mSpeed * Time.deltaTime;
		} else {
			if (transform.position.x != mDestination.x ||
			    transform.position.z != mDestination.z) {
				float step = mSpeed * Time.deltaTime;
				Vector3 towards = Vector3.MoveTowards(transform.position, mDestination, step);
				towards.y = transform.position.y;
				transform.position = towards;
				if (mLookAt) {
					Vector3 at = mDestination - transform.position;
					at.y = transform.forward.y;
					transform.forward = at;
				}
			}
		}
	}
}
