using UnityEngine;
using System.Collections;

public abstract class InputHandler {
	public struct InputResults {
		public float deltaRotation;
		public float deltaForward;
		public float deltaRight;
		public bool isFiring;
		
		public void clear() {
			deltaRotation = deltaForward = deltaRight = 0;
			isFiring = false;
		}
		
		public bool hasRotation() {
			return (deltaRotation != 0);
		}
		
		public bool hasTranslation() {
			return (deltaForward != 0 || deltaRight != 0);
		}
		
		public bool hasResults() {
			return (hasRotation() || hasTranslation() || isFiring);
		}
	}

	private static InputHandler sInstance = null;

	public static InputHandler Get() {
		//Check for other build targets first, then by default return KeyboardInputHandler.
		if (sInstance == null) {
			sInstance = new KeyboardInputHandler();
		}
		return sInstance;
	}

	protected InputHandler() {}

	public bool handleInput(ref InputResults results) {
		results.clear();
		captureRotation(ref results);
		captureTranslation(ref results);
		captureFiring(ref results);
		return results.hasResults();
	}

	protected abstract void captureRotation(ref InputResults results);
	protected abstract void captureTranslation(ref InputResults results);
	protected abstract void captureFiring(ref InputResults results);
	public abstract bool captureRestart();
}

public class KeyboardInputHandler : InputHandler {
	protected override void captureRotation(ref InputResults results) {
		//handle change in rotation
		if (Input.GetKey(KeyCode.LeftArrow)) {
			results.deltaRotation -= Constants.PLAYER_ROTATE_SPEED * Time.deltaTime;
		} if (Input.GetKey(KeyCode.RightArrow)) {
			results.deltaRotation += Constants.PLAYER_ROTATE_SPEED * Time.deltaTime;
		}
	}
	protected override void captureTranslation(ref InputResults results) {
		//handle change in translation
		if (Input.GetKey(KeyCode.W)) {
			results.deltaForward += Constants.PLAYER_TARGET_SPEED*Time.deltaTime;
		} if (Input.GetKey(KeyCode.S)) {
			results.deltaForward -= Constants.PLAYER_TARGET_SPEED*Time.deltaTime;
		} if (Input.GetKey(KeyCode.A)) {
			results.deltaRight -= Constants.PLAYER_TARGET_SPEED*Time.deltaTime;
		} if (Input.GetKey(KeyCode.D)) {
			results.deltaRight += Constants.PLAYER_TARGET_SPEED*Time.deltaTime;
		}
	}
	protected override void captureFiring(ref InputResults results) {
		results.isFiring = Input.GetKey(KeyCode.Space) || Input.GetKey (KeyCode.Keypad0);
	}
	public override bool captureRestart() {
		return Input.GetKey(KeyCode.R);
	}
}
