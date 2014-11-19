using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour {

	public GameObject spawnObject;
	public Transform spawnTargetTransform;
	public Transform minTransform;
	public Transform maxTransform;
	public float minDelay;
	public float maxDelay;
	public int spawnCount = 1;
	public float minSpawnDistance = 5;
	public float startDelay;

	// Use this for initialization
	void Start () {
		StartCoroutine(spawnObjects());
	}

	private IEnumerator spawnObjects() {
		yield return new WaitForSeconds(startDelay);
		while (LevelController.Get().isPlaying()) {
			for (int i = 0; i < spawnCount; ++i) {
				instantiateTarget();
				yield return new WaitForSeconds(0.5f + Random.Range(0f, 0.5f));
			}
			yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
		}
	}

	void instantiateTarget() {
		Vector3 minPos = minTransform.position;
		Vector3 maxPos = maxTransform.position;
		Vector3 randomPos = new Vector3(minPos.x + Random.value * (maxPos.x - minPos.x),
		                                minPos.y + Random.value * (maxPos.y - minPos.y),
		                                minPos.z + Random.value * (maxPos.z - minPos.z));
		Vector3 forward = (spawnTargetTransform.position - randomPos);
		if (Vector3.Magnitude(forward) < minSpawnDistance) {
			//We're too close. Rather than bother with the edge cases to move it away, simply abort and create another.
			instantiateTarget();
			return;
		}
		GameObject gameObject = Instantiate(spawnObject, randomPos, Quaternion.identity) as GameObject;
		gameObject.transform.forward = forward;
	}
}
