using UnityEngine;
using System.Collections;

public class ExplosionBehavior : MonoBehaviour {
	void Start() {
		Destroy(gameObject, 0.5f);    
	}
}
