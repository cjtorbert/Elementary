using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.PLAYER_TAG) {
			LevelController.Get().onEnemyCollision();
		}
	}
}
