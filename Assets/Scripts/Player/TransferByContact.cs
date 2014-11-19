using UnityEngine;
using System.Collections;

public class TransferByContact : MonoBehaviour {	
	void OnTriggerEnter(Collider other) {
		if (other.tag == PlayerController.PlayerTag()) {
			PlayerController.Get().acceptTransfer(this);
		}
	}
}
