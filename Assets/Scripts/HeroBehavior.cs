using UnityEngine;	
using System.Collections;
using System.Collections.Generic;

public class HeroBehavior : MonoBehaviour {
	
    // Speed controls
    private const float speed = 20f;
	private float rotateSpeed = Mathf.PI / 2f; // 90-degrees in 2 seconds
	
	private SpriteSheetManager spriteManager = null;
	public int mRowOfSpriteElements = 0;
	public int mColumnOfSpriteElements = 0;
	public int mBlankPixelsToLeft = 0;
	public int mBlankPixelsToRight = 0;
	public int mBlankPixelsToTop = 0;
	public int mBlankPixelsToBottom = 0;
	
	// per action sprite animation information 
	private const float animationSpeed = speed / 80; // (0.25f) update interveral in seconds
	
	// Row, beginColumn, endColumn. Columns are read right to left.
	private SpriteActionDefinition walkUp = new SpriteActionDefinition(0, 2, 0, animationSpeed, true); 
	private SpriteActionDefinition walkDown = new SpriteActionDefinition(1, 0, 2, animationSpeed, true);
	
	private SpriteActionDefinition walkLeft = new SpriteActionDefinition(0, 1, 0, animationSpeed, true);
	private SpriteActionDefinition walkRight = new SpriteActionDefinition(0, 1, 2, animationSpeed, true);
	
	private SpriteActionDefinition stop = new SpriteActionDefinition(2, 2, 2, animationSpeed, false); // to stop
	
	private float previousDirection = 0f; // this is stupid...
	
	// to support time ...
	private float preEggSpawnTime = -1f;
	private float eggSpawnInterval = 0.1f; // in seconds
	
	private GameObject eggPrefab = null;
	
	private GlobalBehavior worldScript;
	
	// Use this for initialization
	void Start () {
		spriteManager =
			new SpriteSheetManager(renderer.material, 
			mRowOfSpriteElements, mColumnOfSpriteElements,
			mBlankPixelsToLeft, mBlankPixelsToRight,
			mBlankPixelsToTop, mBlankPixelsToBottom);
		spriteManager.SetSpriteAnimationAciton(stop);
		
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
		spriteManager.UpdateSpriteAnimation();
		
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) {
			//if (Input.GetKeyDown(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow)) {
			if (previousDirection <= 0 && Input.GetAxis("Vertical") > 0) {
				spriteManager.SetSpriteAnimationAciton(walkUp);
			}
			//if (Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)) {
			if (previousDirection >= 0 && Input.GetAxis("Vertical") < 0) {
				spriteManager.SetSpriteAnimationAciton(walkDown);
			}
		}
		else {
			spriteManager.SetSpriteAnimationAciton(stop);
		}
		
		if (Input.GetAxis("Fire1") > 0f) { // this is Left-Control
			
			if ((Time.realtimeSinceStartup - preEggSpawnTime) > eggSpawnInterval) {
				preEggSpawnTime = Time.realtimeSinceStartup;
					
				GameObject egg = Instantiate(eggPrefab) as GameObject;
				if (null != egg) {
					if (Application.loadedLevel == 2) {
						egg.transform.localScale = new Vector3(6f, 1f, 6f);
					} 	
					// this is stupid, figure out how to keep them layered right...
					Vector3 pos = new Vector3(transform.position.x, -0.1f, transform.position.z);
					egg.transform.position = pos;
					egg.transform.forward = transform.forward;
									
					worldScript.eggCount++;
					worldScript.shots++;
				}
			}
		}
		previousDirection = Input.GetAxis("Vertical");
	}
}