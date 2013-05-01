using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalBehavior : MonoBehaviour {
	
	private Bounds worldBounds;  // this is the world bound
	
	// support for time delayed enemy spawning
	private float preEnemySpawnTime = -1f;
	private float enemySpawnInterval = 3f; // in seconds
	
	private GameObject enemyPrefab = null;
	
	private GUIText info = null;
	public int enemyCount = 0;
	public int eggCount = 0;
	
	public int initialEnemyCount = 6;
	
	// these will be used to calculate score
	public float deadline = 60 * 1f; // 60 * minutes
	public int shots = 0;
	public int hits = 0;
	
	private GameObject endBox = null;
	private Status status = Status.active;
	
	private enum Status {
		active,
		victory,
		defeat
	};
	
	// initialization
	void Start () {
		// World bound support
		worldBounds = new Bounds(Vector3.zero, Vector3.one);
		UpdateWorldBounds();
		
		enemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;
		for (int i = 0; i < initialEnemyCount; i++) {
			Instantiate(enemyPrefab);
			enemyCount++;
		}
		
		deadline = Time.time + deadline;
		
		info = GameObject.Find("InfoText").GetComponent<GUIText>();
		
		endBox = GameObject.Find("EndBox");
		endBox.SetActive(false);
	}
	
	// called once per frame
	void Update () {
		if (Input.GetButtonUp("Menu")) {
			Application.LoadLevel(0);
		}
		
		switch (status) { // finite state machine
		case Status.active:
			if (enemyCount == 0 && deadline - Time.time > 0) {
				status = Status.victory;
				eggCount = 0;
			}
			else if (deadline - Time.time < 0) {
				status = Status.defeat;
				eggCount = 0;
			}
			info.text = "enemies\t\t" + enemyCount + "\neggs\t\t\t\t\t" + eggCount
				+ "\naccuracy\t" + (100 * hits / Mathf.Max(shots, 1)) + "%"
				+ "\ntime\t\t\t\t\t" + Mathf.Max(Mathf.RoundToInt(deadline - Time.time), 0);
			break;
		case Status.victory:
			if (!endBox.activeInHierarchy) {
				endBox.SetActive(true);
				GameObject.Find("EndText").GetComponent<GUIText>().text = "Victory!";
				
				if (Application.loadedLevel == 2) {
					GameObject.Find("NextText").GetComponent<GUIText>().text = "hit spacebar to proceed";
				}
				else {
					GameObject.Find("NextText").GetComponent<GUIText>().text = "hit spacebar to return to the menu";
				}
			}
			if (Input.GetButtonUp("Jump"))
			{
				if (Application.loadedLevel == 2) {
					Application.LoadLevel(3);
				}
				else {
					Application.LoadLevel(0);
				}
			}
			break;
		case Status.defeat:
			if (!endBox.activeInHierarchy) {
				endBox.SetActive(true);
				GameObject.Find("EndText").GetComponent<GUIText>().text = "Defeat!";
				GameObject.Find("NextText").GetComponent<GUIText>().text = "hit spacebar to restart the level";
				
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				foreach (GameObject enemy in enemies) {
					Destroy(enemy);
				}
			}
			if (Input.GetButtonUp("Jump"))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
			break;
		}
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