using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject mPlayer;
	public GameObject playerMoveTarget;
	public GameObject bullet;
	public Transform bulletSpawn;
	public Transform orbLocation;
	public Transform minTransform;
	public Transform maxTransform;

	private static PlayerController sInstance;
	private float mRotation = 0f;
	private float mLastFire = -1f;
	private InputHandler mInputHandler = InputHandler.Get();
	private InputHandler.InputResults mInputResults = new InputHandler.InputResults();
	private OrbController mCurrentOrb = null;

	public static string PlayerTag() {
		return Constants.PLAYER_TAG;
	}

	//There's only one instance of a player per game, so I'm somewhat comfortable with
	//this for the time being.
	public static PlayerController Get() {
		return sInstance;
	}

	// Use this for initialization
	void Start () {
		sInstance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (!LevelController.Get().isPlaying()) return;
		updatePlayerTargetSpeed();
		if (mInputHandler.handleInput(ref mInputResults)) {
			movePlayerTarget(ref mInputResults);
			playerFire(ref mInputResults);
		}
		movePlayer();
	}

	private void updatePlayerTargetSpeed() {
		float distance = Vector3.Magnitude(playerMoveTarget.transform.position - mPlayer.transform.position);
		Constants.PLAYER_TARGET_SPEED = Constants.MAX_PLAYER_TARGET_SPEED - distance;
	}

	//Called from TransferByContact.cs when the PlayerController encounters a trigger
	public bool acceptTransfer(MonoBehaviour trigger) {
		if (trigger.tag == Constants.ORB_TAG) {
			return acceptOrb(trigger);
		} else if (trigger.tag == Constants.HOME_TAG) {
			return transferToHome();
		}
		return false;
	}

	private bool acceptOrb(MonoBehaviour orb) {
		if (mCurrentOrb != null) {
			return false;
		}
		orb.transform.parent = orbLocation;
		orb.transform.localPosition = new Vector3(0,0,0);
		mCurrentOrb = orb.GetComponent(Constants.ORB_CONTROLLER) as OrbController;
		return true;
	}

	private bool transferToHome() {
		if (mCurrentOrb == null) {
			return false;
		}
		//determine which xform the orb should go to based on its element
		mCurrentOrb.transferToHome();
		mCurrentOrb = null;
		return true;
	}

	private void movePlayerTarget(ref InputHandler.InputResults inputResults) {
		if (inputResults.hasRotation()) {
			mRotation = (mRotation + inputResults.deltaRotation) % 360f;
			updatePlayerForwardDirection();
		}
		if (inputResults.hasTranslation()) {
			playerMoveTarget.transform.position += playerMoveTarget.transform.forward.normalized * inputResults.deltaForward;
			playerMoveTarget.transform.position += playerMoveTarget.transform.right.normalized * inputResults.deltaRight;
			validatePosition();
		}
	}

	private void validatePosition() {
		//TODO: this is somewhat crude, and should probably be cleaner / faster.
		Vector3 pos = playerMoveTarget.transform.position;
		Vector3 min = minTransform.position;
		Vector3 max = maxTransform.position;
		if (pos.x < min.x) {
			pos.x = min.x;
		} else if (pos.x > max.x) {
			pos.x = max.x;
		}

		if (pos.z < min.z) {
			pos.z = min.z;
		} else if (pos.z > max.z) {
			pos.z = max.z;
		}
		playerMoveTarget.transform.position = pos;
	}
	
	private void updatePlayerForwardDirection() {
		//Unity is a left-handed coordinate system, and forward is down conventional -z axis
		float convertedAngleToRad = (mRotation + 90) * Mathf.PI / 180;
		float x = Mathf.Cos(convertedAngleToRad);
		float z = -Mathf.Sin(convertedAngleToRad);  //due to left-handedness
		playerMoveTarget.transform.forward = new Vector3(x, 0, z);
		playerMoveTarget.transform.right = Vector3.Cross(playerMoveTarget.transform.forward, Vector3.up);
		mPlayer.transform.forward = playerMoveTarget.transform.forward;
	}
	
	private void playerFire(ref InputHandler.InputResults inputResults) {
		if (inputResults.isFiring) {
			float diff = (Time.time - mLastFire);
			if (diff > (1.0f / Constants.FIRE_RATE)) {
				mLastFire = Time.time;
				Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
			}
		}
	}
	
	private void movePlayer() {
		if (transform.position.x != playerMoveTarget.transform.position.x ||
		    transform.position.z != playerMoveTarget.transform.position.z) {
			float step = Constants.PLAYER_SPEED * Time.deltaTime;
			Vector3 towards = Vector3.MoveTowards(transform.position, playerMoveTarget.transform.position, step);
			towards.y = transform.position.y;
			transform.position = towards;
		}
	}
}
