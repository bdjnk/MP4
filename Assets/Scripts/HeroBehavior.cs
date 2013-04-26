using UnityEngine;	
using System.Collections;
using System.Collections.Generic;

public class HeroBehavior : MonoBehaviour {
	
	SpriteSheetManager mSpriteManager = null;
	public int mRowOfSpriteElements = 0;
	public int mColumnOfSpriteElements = 0;
	public int mBlankPixelsToLeft = 0;
	public int mBlankPixelsToRight = 0;
	public int mBlankPixelsToTop = 0;
	public int mBlankPixelsToBottom = 0;
	
	// per action sprite animation information 
	private const float kWalkPeriod = 0.25f; // 0.25 second
	private const float kRunPeriod = 0.1f;  // 0.1f second update interveral
	private SpriteActionDefinition mWalkUp = new SpriteActionDefinition(0, 0, 3, kWalkPeriod, true); // Row, BeginColumn, EndColumn
	private SpriteActionDefinition mWalkDown = new SpriteActionDefinition(0, 0, 3, kWalkPeriod, true);
	
	private SpriteActionDefinition mStop = new SpriteActionDefinition(0, 0, 0, kWalkPeriod, false); // to stop
	
	// to support time ...
	private float preEggSpawnTime = -1f; // 
	private float eggSpawnInterval = 0.1f; // in seconds
	
	private GameObject eggPrefab = null;
	
    // Speed controls
    private float speed = 20f;
	private float rotateSpeed = Mathf.PI / 2f; // 90-degrees in 2 seconds
	
	private GlobalBehavior worldScript;
	
	// Use this for initialization
	void Start () {
		mSpriteManager = new SpriteSheetManager(renderer.material, 
			mRowOfSpriteElements, mColumnOfSpriteElements,
			mBlankPixelsToLeft, mBlankPixelsToRight,
			mBlankPixelsToTop, mBlankPixelsToBottom);
		worldScript = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
		eggPrefab = Resources.Load("Prefabs/Egg") as GameObject;
	}
	
	public Vector3 Position { get { return transform.position; } }
	public Vector3 Direction { get { return transform.forward; } }
	
	// Update is called once per frame
	void Update () {
		transform.position += Input.GetAxis("Vertical") * transform.forward * (speed * Time.smoothDeltaTime);
		transform.position = worldScript.WorldBoundCorrection(transform.collider.bounds);
		
		transform.RotateAround(Vector3.up, Input.GetAxis("Horizontal") * (rotateSpeed * Time.smoothDeltaTime));
		
		// Must call to see the sprite movement!
		mSpriteManager.UpdateSpriteAnimation();
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			mSpriteManager.SetSpriteAnimationAciton(mWalkUp);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			mSpriteManager.SetSpriteAnimationAciton(mWalkDown);
		}	
		if (Input.GetKeyUp(KeyCode.UpArrow)) {
			mSpriteManager.SetSpriteAnimationAciton(mStop);
		}
		if (Input.GetKeyUp(KeyCode.DownArrow)) {
			mSpriteManager.SetSpriteAnimationAciton(mStop);
		}	
		
		if (Input.GetAxis ("Fire1") > 0f) { // this is Left-Control
			
			if ((Time.realtimeSinceStartup - preEggSpawnTime) > eggSpawnInterval) {
				preEggSpawnTime = Time.realtimeSinceStartup;
					
				GameObject egg = Instantiate(eggPrefab) as GameObject;
				if (null != egg) {
					// this is stupid, figure out how to keep them layered right...
					Vector3 pos = new Vector3(transform.position.x, -0.1f, transform.position.z);
					egg.transform.position = pos;
					egg.transform.forward = transform.forward;
					
					worldScript.eggCount++;
				}
			}
		}
	}
}