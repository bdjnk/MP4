using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
	private float speed = 40f;
	private float rotateSpeed = Mathf.PI * 2;
	
	private float safe = 30f;
	
	private Texture normal;
	private Texture run;
	private Texture stunned;
	
	private int stunCount;
	private float endStunTime = -1f; // 
	private float stunInterval = 5f; // in seconds
	
	private GameObject explosion;
	
	private GlobalBehavior worldScript;

	void Start () {
		worldScript = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
		
		explosion = Resources.Load("Prefabs/Explosion") as GameObject;
		
		Bounds bounds = worldScript.WorldBounds;
		Vector3 random = Random.insideUnitSphere;
		random.x *= bounds.size.x * 0.5f;  // half of the size;
		random.z *= bounds.size.z * 0.5f;
		random.y = 0f;
		transform.position = random + bounds.center;
		
		speed = Random.value * 20f + 20f;
		
		normal = Resources.Load("Textures/normal") as Texture;
		run = Resources.Load("Textures/run") as Texture;
		stunned = Resources.Load("Textures/stunned") as Texture;
		
		stunCount = 0;
		
		NewDirection();
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.rigidbody.WakeUp();
		
		GameObject hero = GameObject.Find("Hero");
		Vector3 heroToMe = transform.position - hero.transform.position;
		float angleToMe = Vector3.Dot(Vector3.Normalize(heroToMe), hero.transform.forward);
		float distance = Vector3.Magnitude(heroToMe);
		
		if (renderer.material.mainTexture == stunned) {
			transform.RotateAround(Vector3.up, (Mathf.PI / 20f) * Time.smoothDeltaTime);
			
			if (Time.realtimeSinceStartup >= endStunTime) {
				renderer.material.mainTexture = normal;
			}
		}
		else if (renderer.material.mainTexture == run) {
			if (angleToMe < -0.3f || distance > safe + 1f) { // behind the hero or far away (thus safe)
				renderer.material.mainTexture = normal;
			}
			else {
				// perpendicular is the left hand vector, so an angle of < 90 is on the left, and > 90 is on the right
				Vector3 perpendicular = new Vector3(-hero.transform.forward.z, 0f, hero.transform.forward.x);
				float angleToMeFromPerp = Vector3.Dot(Vector3.Normalize(heroToMe), perpendicular);
				float forwardDiffAngle = Vector3.Dot(transform.forward, hero.transform.forward);
				
				float whichSide = (angleToMeFromPerp > 0) ? -1 : 1; // > 0 = < 90, spin left. < 0 = > 90, spin right.
				
				transform.RotateAround(Vector3.up, rotateSpeed * whichSide * forwardDiffAngle * Time.smoothDeltaTime);
				if (worldScript.WorldBoundCorrection(transform.collider.bounds) != transform.collider.bounds.center) {
					NewDirection();
				}
				transform.position += transform.forward * speed * Time.smoothDeltaTime;
			}
		}
		else { // texture == normal
			if (angleToMe > -0.1f && distance < safe) { // in front of the hero and close (danger!)
				renderer.material.mainTexture = run;
			}
			else {
				if (worldScript.Movement) {
					if (worldScript.WorldBoundCorrection(transform.collider.bounds) != transform.collider.bounds.center) {
						NewDirection();
					}
					transform.position += (speed * Time.smoothDeltaTime) * transform.forward;
				}
			}
		}
	}
	
	private void NewDirection() {
		Vector2 random = Random.insideUnitCircle;
		random.Normalize();
		
		Bounds bounds = transform.collider.bounds;
		
		if (bounds.max.x > worldScript.WorldBounds.max.x) {
			random.x = -Mathf.Abs(random.x);
		}
		else if (bounds.min.x < worldScript.WorldBounds.min.x) {
			random.x = Mathf.Abs(random.x);
		}
		else if (bounds.max.z > worldScript.WorldBounds.max.z) {
			random.y = -Mathf.Abs(random.y);
		}
		else if (bounds.min.z < worldScript.WorldBounds.min.z) {
			random.y = Mathf.Abs(random.y);
		}
		
		Vector3 newForward = new Vector3(random.x, 0f, random.y); // NOTICE!! 
		transform.forward = newForward;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Egg(Clone)") {
			stunCount++;
			endStunTime = Time.realtimeSinceStartup + stunInterval;
			if (stunCount >= 3) {
				// this is stupid, figure out how to keep them layered right...
				Vector3 pos = new Vector3(transform.position.x, 1f, transform.position.z);
				Instantiate(explosion, pos, Quaternion.identity);
				Destroy(gameObject);
				if (worldScript.enemyCount > 0) { // shouldn't be neccessary
					worldScript.enemyCount--;
				}
			}
			else {
				renderer.material.mainTexture = stunned;
			}
		}
	}
}
