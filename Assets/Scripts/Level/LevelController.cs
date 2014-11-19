using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

	public int orbCount;
	public GUIText gameOverText;

	private static LevelController sInstance;
	private HashSet<MonoBehaviour> acquiredOrbs = new HashSet<MonoBehaviour>();
	private bool mIsPlaying = true;

	public bool isPlaying() {
		return mIsPlaying;
	}

	public static LevelController Get() {
		return sInstance;
	}

	// Use this for initialization
	void Start () {
		sInstance = this;
		gameOverText.text = "";
	}

	void Update() {
		if (!mIsPlaying && InputHandler.Get().captureRestart()) {
			Application.LoadLevel(0);
		}
	}

	public void orbAcquired(MonoBehaviour orb) {
		if (acquiredOrbs.Add(orb)) {
			if (acquiredOrbs.Count >= orbCount) {
				gameOver(true);
			}
		}
	}

	public void onEnemyCollision() {
		gameOver(false);
	}

	private void gameOver(bool win) {
		if (win) {
			gameOverText.text = Constants.WIN_TEXT;
		} else {
			gameOverText.text = Constants.LOSE_TEXT;
		}
		mIsPlaying = false;
	}
}
