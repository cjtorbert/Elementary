using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
	public float speed;
	public Material fireMaterial;
	public Material waterMaterial;
	public Material leafMaterial;
	private static Material[] sEnemyMaterials = null;
	private static GameObject sPlayer = null;
	private bool mAnimating = true;
	public float spawnAnimationTime;
	private float spawnTime;

	//return whether element a beats element b in the elemental circle
	public bool beatsElement(Material b) {
		Material a = renderer.sharedMaterial;
		//not very scalable; may want to rethink if add more elements or wanna play with the circle.
		if (a == fireMaterial && b == leafMaterial) return true;
		if (a == waterMaterial && b == fireMaterial) return true;
		if (a == leafMaterial && b == waterMaterial) return true;
		return false;
	}

	public bool tiesElement(Material b) {
		return (renderer.sharedMaterial == b);
	}

	public bool losesToElement(Material b) {
		return !(beatsElement(b) || tiesElement(b));
	}

	// Use this for initialization
	void Start () {
		initialize();
		int randMaterial = Constants.random.Next(0, sEnemyMaterials.Length);
		gameObject.renderer.sharedMaterial = sEnemyMaterials[randMaterial];
		speed = Random.Range(speed, Constants.PLAYER_SPEED - 1);
	}

	private void initialize() {
		spawnTime = Time.time;
		gameObject.transform.localScale = Vector3.zero;
		if (sEnemyMaterials == null) {
			sEnemyMaterials = new Material[] {fireMaterial, waterMaterial, leafMaterial};
		}
		if (sPlayer == null) {
			sPlayer = GameObject.FindWithTag(Constants.PLAYER_TAG);
		}
	}

	void Update() {
		entryAnimation();
	}

	private void entryAnimation() {
		if (mAnimating) {
			float percent = (Time.time - spawnTime) / spawnAnimationTime;
			if (percent > 1.0f) {
				percent = 1.0f;
				mAnimating = false;
			}
			gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent);
		}
	}

	void FixedUpdate() {
		moveEnemy();
	}

	void moveEnemy() {
		if (!LevelController.Get().isPlaying()) {
			rigidbody.velocity = Vector3.zero;
			return;
		}

		if (mAnimating) return;
		Vector3 forward = (sPlayer.transform.position - transform.position);
		forward.y = transform.forward.y;
		transform.forward = forward;

		rigidbody.velocity = speed * transform.forward;
	}
}