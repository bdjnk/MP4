using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalBehavior : MonoBehaviour {
	
	private Bounds worldBounds;  // this is the world bound
	
	// support for time delayed enemy spawning
	private float preEnemySpawnTime = -1f;
	private float enemySpawnInterval = 3f; // in seconds
	
	private GameObject enemyPrefab = null;
	
	private bool movement = false;
	
	private GUIText info = null;
	public int enemyCount = 0;
	public int eggCount = 0;
	
	// these will be used to calculate score
	private int deadline = 60 * 2; // 60 * minutes
	public int shots = 0;
	public int hits = 0;
	//score = ((hits/shots)*100)+(deadline)/Time.timeSinceLevelLoad
	
	// initialization
	void Start () {
		// World bound support
		worldBounds = new Bounds(Vector3.zero, Vector3.one);
		UpdateWorldBounds();
		
		enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
		for (int i = 0; i < 50; i++) {
			Instantiate(enemyPrefab);
			enemyCount++;
		}
		
		info = GameObject.Find("InfoText").GetComponent<GUIText>();
	}
	
	public bool Movement { get { return movement; } }
	
	// called once per frame
	void Update () {
		if (movement) {
			SpawnAnEnemy();
		}
		
		if (Input.GetButtonUp("Jump")) {
			movement = !movement;
		}
		//left alt key added to go back to main menu
		if (Input.GetButtonUp("Fire2")) {
			Application.LoadLevel(0);
		}		
		
		info.text = "enemy count: " + enemyCount + "\negg count: " + eggCount;
	}
	
	#region game window world size bound support
	// must be called anytime the MainCamera is moved, or changed in size
	public void UpdateWorldBounds() {
		if (null != Camera.main) {
			float maxZ = Camera.main.orthographicSize;
			float maxX = Camera.main.orthographicSize * Camera.main.aspect;
			float sizeX = 2 * maxX;
			float sizeZ = 2 * maxZ;
			float sizeY = Mathf.Abs(Camera.main.farClipPlane - Camera.main.nearClipPlane);
			
			// assumes camera is looking in the negative y-axis
			Vector3 c = Camera.main.transform.position;
			c.y -= (0.5f * sizeY);
			worldBounds.center = c;
			worldBounds.size = new Vector3(sizeX, sizeY, sizeZ);
		}
	}
	
	public Bounds WorldBounds { get { return worldBounds; } }
	
	public Vector3 WorldBoundCorrection(Bounds bounds) {
		Vector3 newPosition = bounds.center;
	
		if (bounds.max.x > WorldBounds.max.x) {
			newPosition.x = WorldBounds.max.x - bounds.extents.x;
		}
		else if (bounds.min.x < WorldBounds.min.x) {
			newPosition.x = WorldBounds.min.x + bounds.extents.x;
		}
		if (bounds.max.z > WorldBounds.max.z) {
			newPosition.z = WorldBounds.max.z - bounds.extents.z;
		}
		else if (bounds.min.z < WorldBounds.min.z) {
			newPosition.z = WorldBounds.min.z + bounds.extents.z;
		}
		return newPosition;
	}
	#endregion 
	
	private void SpawnAnEnemy() {
		if ((Time.realtimeSinceStartup - preEnemySpawnTime) > enemySpawnInterval) {
			Instantiate(enemyPrefab);
			preEnemySpawnTime = Time.realtimeSinceStartup;
			enemyCount++;
		}
	}
}