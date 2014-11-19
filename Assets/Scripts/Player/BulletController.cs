using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject explosion;
	private bool mActive = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag == Constants.ENEMY_TAG) {
			handleEnemyCollision(other);
		}
	}

	private void handleEnemyCollision(Collider other) {
		EnemyController enemy = other.GetComponent<EnemyController>();
		if (enemy.losesToElement(renderer.sharedMaterial)) {
			if (!mActive) {
				mActive = true;
				renderer.sharedMaterial = other.renderer.sharedMaterial;
			}
			Destroy(other.gameObject);
		} else {
			if (enemy.tiesElement(renderer.sharedMaterial)) {
				Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
			}
			Destroy(this.gameObject);
		}
	}
}
