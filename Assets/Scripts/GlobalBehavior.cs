using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalBehavior : MonoBehaviour {
	
	private Bounds worldBounds;  // this is the world bound
	
	// support for time delayed enemy spawning
	private float preEnemySpawnTime = -1f;
	private float enemySpawnInterval = 3f; // in seconds
	
	private GameObject enemyPrefab = null;
	
	private bool movement = true;
	
	//private GUIText info = null;
	private Box info;
	public int enemyCount = 0;
	public int eggCount = 0;
	
	public int initialEnemyCount = 6;
	
	// these will be used to calculate score
	private int deadline = 60 * 2; // 60 * minutes
	public int shots = 0;
	public int hits = 0;
	//score = ((hits/shots)*100)+deadline/Time.timeSinceLevelLoad
	private GameObject scoreCube = null;
	
	private GameObject enemyHundreds;
	private GameObject enemyTens;
	private GameObject enemyOnes;
	
	private GameObject eggHundreds;
	private GameObject eggTens;
	private GameObject eggOnes;
	
	private GameObject scoreHundreds;
	private GameObject scoreTens;
	private GameObject scoreOnes;
	//private GameObject scoreTens = null;
	//private GameObject scoreOnes = null;
	
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
		
		//info = GameObject.Find("InfoText").GetComponent<GUIText>();
		info = new Box(5f,5f,115f,104f);
		info.AllStyleColor = Color.yellow;
		Texture2D scoreSheet = Resources.Load ("Textures/score") as Texture2D;
		info.NormalTexture = scoreSheet;
		info.HoverTexture = scoreSheet;
		info.ActiveTexture = scoreSheet;
		scoreCube = Resources.Load ("Prefabs/NumberCube") as GameObject;	
		//scoreTens = Resources.Load ("Prefabs/NumberCube") as GameObject;
		//scoreOnes = Resources.Load ("Prefabs/NumberCube") as GameObject;
		enemyHundreds = (GameObject) Instantiate (scoreCube);
		enemyTens = (GameObject) Instantiate (scoreCube);
		enemyOnes = (GameObject) Instantiate (scoreCube);
		enemyHundreds.transform.position = new Vector3 (worldBounds.min.x+35f,0.1f,worldBounds.max.z - 13f);
		enemyTens.transform.position = new Vector3 (worldBounds.min.x+43f,0.1f,worldBounds.max.z - 13f);
		enemyOnes.transform.position = new Vector3 (worldBounds.min.x+51f,0.1f,worldBounds.max.z - 13f);
		
		eggHundreds = (GameObject) Instantiate (scoreCube);
		eggTens = (GameObject) Instantiate (scoreCube);
		eggOnes = (GameObject) Instantiate (scoreCube);
		eggHundreds.transform.position = new Vector3 (worldBounds.min.x+35f,0.1f,worldBounds.max.z - 30f);
		eggTens.transform.position = new Vector3 (worldBounds.min.x+43f,0.1f,worldBounds.max.z - 30f);
		eggOnes.transform.position = new Vector3 (worldBounds.min.x+51f,0.1f,worldBounds.max.z - 30f);
		
		scoreHundreds = (GameObject) Instantiate (scoreCube);
		scoreTens = (GameObject) Instantiate (scoreCube);
		scoreOnes = (GameObject) Instantiate (scoreCube);
		scoreHundreds.transform.position = new Vector3 (worldBounds.min.x+35f,0.1f,worldBounds.max.z - 47f);
		scoreTens.transform.position = new Vector3 (worldBounds.min.x+43f,0.1f,worldBounds.max.z - 47f);
		scoreOnes.transform.position = new Vector3 (worldBounds.min.x+51f,0.1f,worldBounds.max.z - 47f);
	}
	
	public bool Movement { get { return movement; } }
	
	// called once per frame
	void Update () {
		/*
		if (movement) {
			SpawnAnEnemy();
		}
		
		if (Input.GetButtonUp("Jump")) {
			movement = !movement;
		}
		*/
		if (Input.GetButtonUp("Fire2")) {
			Application.LoadLevel(0);
		}
		
		float lshots = Mathf.Max(shots, 1);
		float score = Mathf.Min(((hits / lshots * 100) * (deadline / Time.time)), 100);
		
		if (enemyCount == 0) {
			// add end level dialog
			//info.text = "Victory!";
			//info.style = style.normal.textColor = Color.red;
			//info.Text = "Victory!";
		}
		else {
			//info.style.normal.textColor = Color.red;
			//info.text = "enemy count: " + enemyCount + "\negg count: " + eggCount + "\nscore: " + score;
			//info.Text = "enemy count: " + enemyCount + "\negg count: " + eggCount + "\nscore: " + score;
			UpdateMainScore(score);
		}
		UpdateEnemyScore (enemyCount);
		UpdateEggScore(eggCount);
		
	}
	
	void OnGUI(){
		info.OnGUI();
	}
	
	public void UpdateEnemyScore(int enemy){
		int nOnes = enemy % 10;
		float nOnesTextureOffset = nOnes /10f;
		enemyOnes.renderer.material.mainTextureOffset = new Vector2(nOnesTextureOffset,0.0f);
		enemy = enemy/10;
		if (enemy > 0){
			int nTens = enemy % 10;
			float nTensTextureOffset = nTens /10f;
			enemyTens.renderer.material.mainTextureOffset = new Vector2(nTensTextureOffset,0.0f);
			enemy = enemy/10;
		} else { 
			enemyTens.renderer.material.mainTextureOffset = new Vector2(0.0f,0.0f);
		}
		if (enemy > 0){
			int nHundreds = enemy % 10;
			float nHundredsTextureOffset = nHundreds /10f;
			enemyHundreds.renderer.material.mainTextureOffset = new Vector2(nHundredsTextureOffset,0.0f);
			enemy = enemy/10;
		} else { 
			enemyHundreds.renderer.material.mainTextureOffset = new Vector2(0.0f,0.0f);
		}
		
	  	//scoreHundreds;
	  	//scoreTens;
	  	//scoreOnes;
	}
	public void UpdateEggScore(int eggs){
		int nOnes = eggs % 10;
		float nOnesTextureOffset = nOnes /10f;
		eggOnes.renderer.material.mainTextureOffset = new Vector2(nOnesTextureOffset,0.0f);
		eggs = eggs/10;
		if (eggs > 0){
			int nTens = eggs % 10;
			float nTensTextureOffset = nTens /10f;
			eggTens.renderer.material.mainTextureOffset = new Vector2(nTensTextureOffset,0.0f);
			eggs = eggs/10;
		} else { 
			eggTens.renderer.material.mainTextureOffset = new Vector2(0.0f,0.0f);
		}
		if (eggs > 0){
			int nHundreds = eggs % 10;
			float nHundredsTextureOffset = nHundreds /10f;
			eggHundreds.renderer.material.mainTextureOffset = new Vector2(nHundredsTextureOffset,0.0f);
			eggs = eggs/10;
		} else { 
			eggHundreds.renderer.material.mainTextureOffset = new Vector2(0.0f,0.0f);
		}
	}
	public void UpdateMainScore(float score){
		int scoreInt = (int) score;
		int nOnes = (int) scoreInt % 10;
		float nOnesTextureOffset = nOnes /10f;
		scoreOnes.renderer.material.mainTextureOffset = new Vector2(nOnesTextureOffset,0.0f);
		scoreInt = scoreInt/10;
		if (scoreInt > 0){
			int nTens = scoreInt % 10;
			float nTensTextureOffset = nTens /10f;
			scoreTens.renderer.material.mainTextureOffset = new Vector2(nTensTextureOffset,0.0f);
			scoreInt = scoreInt/10;
		} else { 
			scoreTens.renderer.material.mainTextureOffset = new Vector2(0.0f,0.0f);
		}
		if (scoreInt > 0){
			int nHundreds = scoreInt % 10;
			float nHundredsTextureOffset = nHundreds /10f;
			scoreHundreds.renderer.material.mainTextureOffset = new Vector2(nHundredsTextureOffset,0.0f);
			scoreInt = scoreInt/10;
		} else { 
			scoreHundreds.renderer.material.mainTextureOffset = new Vector2(0.0f,0.0f);
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