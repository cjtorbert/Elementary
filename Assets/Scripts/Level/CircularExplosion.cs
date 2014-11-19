using UnityEngine;
using System.Collections;

public class CircularExplosion : MonoBehaviour {

	public float duration;
	public float minScale;
	public float maxScale;

	private float m_startTime;

	// Use this for initialization
	void Start () {
		m_startTime = Time.time;
		gameObject.transform.localScale = new Vector3(minScale, minScale, minScale);
	}
	
	// Update is called once per frame
	void Update () {
		float t = (Time.time - m_startTime) / duration;
		float scale = minScale + t * (maxScale - minScale);
		gameObject.transform.localScale = new Vector3(scale, scale, scale);
		if (scale > maxScale) {
			Destroy(this.gameObject);
		}
	}
}
