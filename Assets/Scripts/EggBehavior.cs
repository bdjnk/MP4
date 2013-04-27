using UnityEngine;
using System.Collections;

public class EggBehavior : MonoBehaviour {
	
	private float speed = 100f;
	
	private GameObject splat;
	
	private GlobalBehavior worldScript;
	
	// Use this for initialization
	void Start () {
		worldScript = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
		
		splat = Resources.Load("Prefabs/Splat") as GameObject;
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
			
			// this is stupid, figure out how to keep them layered right...
			Vector3 pos = new Vector3(transform.position.x, 1f, transform.position.z);
			Instantiate(splat, pos, Quaternion.identity);
			
			Destroy(gameObject);
			if (worldScript.eggCount > 0) { // shouldn't be neccessary
				worldScript.eggCount--;
				worldScript.hits++;
			}
		}
	}
}