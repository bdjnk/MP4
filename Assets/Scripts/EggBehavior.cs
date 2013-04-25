using UnityEngine;
using System.Collections;

public class EggBehavior : MonoBehaviour {
	
	private float speed = 100f;
	
	private GlobalBehavior worldScript;
	
	// Use this for initialization
	void Start () {
		 worldScript = GameObject.Find ("GameManager").GetComponent<GlobalBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (speed * Time.smoothDeltaTime) * transform.forward;
		
		if (!transform.collider.bounds.Intersects(worldScript.WorldBounds)) {
			Destroy(gameObject);
			if (worldScript.eggCount > 0) { // shouldn't be neccessary
				worldScript.eggCount--;
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Enemy(Clone)") {
			Destroy(gameObject);
			if (worldScript.eggCount > 0) { // shouldn't be neccessary
				worldScript.eggCount--;
			}
		}
	}
}