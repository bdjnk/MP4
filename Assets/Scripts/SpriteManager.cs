using UnityEngine;
using System.Collections;

/// <summary>
/// Credit: Simple Sprite Sheet was created by:
/// 	Jebediah Pavleas, August 2011 while he was a student at:
///		Computing and Software Systems, 
///		University of Washington, Bothell
/// 
/// ManyObject Sprite Sheet was from:
/// 	Sam Cook, Spring 2010 while he was a student at:
///		Computing and Software Systems, 
///		University of Washington, Bothell
/// 
/// Use with permission from both.
/// 
/// </summary>


//
/// <summary>
/// Input parametesr describe one sprite animation action
/// </summary>
/// 
public class SpriteActionDefinition {
	public int mRow;
	public int mBeginColumn;		// if Begin > End, the sequence will go backwards
	public int mEndColumn;
	public float mUpdatePeriod;
	public bool mShouldLoop;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="SpriteActionDefinition"/> class.
	/// </summary>
	/// <param name='rowIndex'>
	/// The row index to the animation
	/// </param>
	/// <param name='beginColumn'>
	/// Begin column index of the animation
	/// </param>
	/// <param name='endColumn'>
	/// End column index of the animation
	/// </param>
	/// <param name='period'>
	/// Period (in seconds) to show each sprite element
	/// </param>
	/// <param name='shouldLoop'>
	/// loop the animation: when looping, beginColumn must be less than endColumn.
	/// </param>
	public SpriteActionDefinition(int rowIndex, 
					int beginColumn, int endColumn, 
					float period, bool shouldLoop) 
	{
		mRow = rowIndex;
		mBeginColumn = beginColumn;
		mEndColumn = endColumn;
		mUpdatePeriod = period;
		mShouldLoop = shouldLoop;
	}
	public SpriteActionDefinition(SpriteActionDefinition copy) {
		mRow = copy.mRow;
		mBeginColumn = copy.mBeginColumn;
		mEndColumn = copy.mEndColumn;
		mUpdatePeriod = copy.mUpdatePeriod;
		mShouldLoop = copy.mShouldLoop;
	}
};


public class SpriteSheetManager {	
	/// <summary>
	/// Input parameters describing the sprite sheet
	/// </summary> 
	public Material mSpriteSheetMaterial = null; // this is expected to be on the gameObject
	public int mRowInSheet = 0;         // number of rows
	public int mColumnInSheet = 0;      // number of columns
	public int mBlankPixelsToLeft = 0;  // Number of blank pixels to the left/right of the sprite element
	public int mBlankPixelsToRight = 0; // 
	public int mBlankPixelsAbove = 0;   // Number of blank pixels above/below the sprite element
	public int mBlankPixelsBelow = 0;   // 
	
	
	/// <summary>
	/// When not null, this refers to the current sprite animation we are performing
	/// </summary>
	public SpriteActionDefinition mCurrentSpriteAction = null;
	
	/// <summary>
	/// Parameter for supporting current on-going animation
	/// </summary>
	private int mCurrentActionColumn;       // mCurrentActionRow is simply mSpriteRow
	private int mCurrentActionColumnDelta;
	private float mCurrentActionLastUpdateTime;
		

	/// <summary>
	/// The following stuff are all local
	/// </summary>
	// OK, everything in Unity3D is in UV space (meaning in normalized coordinate space between 0 to 1) 
	private float mBlankToLeftInUV = 0f;  // blank space in vertical UV 
	private float mBlankToRightInUV = 0f;  // blank space in vertical UV 
	private float mBlankAboveInUV = 0f;
	private float mBlankBelowInUV = 0f;
	private float mSpriteWidthInUV = 0f; 
	private float mSpriteHeightInUV = 0f;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="SpriteSheetManager"/> class.
	/// </summary>
	/// <param name='spriteSheetMaterial'>
	/// Sprite sheet material.
	/// </param>
	/// <param name='rowInSheet'>
	/// Row of sprite elements in sheet.
	/// </param>
	/// <param name='columnInSheet'>
	/// Column of sprite elements in sheet.
	/// </param>
	/// <param name='blankLeftPixels'>
	/// Number of blank  pixels to the left of each sprite element
	/// </param>
	/// <param name='blankRightPixels'>
	/// Number of blank pixels to the right of each sprite element
	/// </param>
	/// <param name='blankTopPixels'>
	/// Number of blank pixels to the top of each sprite element
	/// </param>
	/// <param name='blankBottomPixels'>
	/// Number of blank pixels to the top of each sprite element
	/// </param>
	public SpriteSheetManager(Material spriteSheetMaterial, 
			int rowInSheet, int columnInSheet, 
			int blankLeftPixels, int blankRightPixels,
			int blankTopPixels, int blankBottomPixels)
	{
		mSpriteSheetMaterial = spriteSheetMaterial;
		mRowInSheet = rowInSheet;
		mColumnInSheet = columnInSheet;
		mBlankPixelsToLeft = blankLeftPixels;
		mBlankPixelsToRight = blankRightPixels;
		mBlankPixelsAbove = blankTopPixels;
		mBlankPixelsBelow = blankBottomPixels;
		
		InitializeSpriteSheet();
	}
	
	public SpriteSheetManager() {
		if (null == mSpriteSheetMaterial)
			return;
		InitializeSpriteSheet();
	}
	
	/// <summary>
	/// Updates the sprite animation. Should be called per update.
	/// </summary>
	public void UpdateSpriteAnimation() {
		if ((mCurrentSpriteAction == null) || (mCurrentSpriteAction.mBeginColumn == mCurrentSpriteAction.mEndColumn))
			return;  // no action defined or required
		
		if ((Time.realtimeSinceStartup - mCurrentActionLastUpdateTime) > mCurrentSpriteAction.mUpdatePeriod) {
			mCurrentActionLastUpdateTime = Time.realtimeSinceStartup;
			ComputeNextAndContinue();					
			Vector2 offset = mSpriteSheetMaterial.mainTextureOffset;
			offset.x = ComputeXOffset(mCurrentActionColumn);
			mSpriteSheetMaterial.mainTextureOffset = offset;	
		}
	}
	
	/// <summary>
	/// Sets the sprite animation aciton. The manager maintains a ** LINK ** to the passed
	/// in action parameter!! This means, any changes in the action parameter will alter
	/// the behavior of the sprite animation (e.g., you can increase the animaiton rate by
	/// decrease the mUpdatePeriod
	/// </summary>
	/// <param name='anim'>
	/// The desired animation.
	/// </param>
	public void SetSpriteAnimationAciton(SpriteActionDefinition anim) {
		mCurrentSpriteAction = anim; 
		if (mCurrentSpriteAction.mShouldLoop) {
			// make sure being is always smaller then end
			if (mCurrentSpriteAction.mEndColumn < mCurrentSpriteAction.mBeginColumn) {
				int tmp = mCurrentSpriteAction.mEndColumn;
				mCurrentSpriteAction.mEndColumn = mCurrentSpriteAction.mBeginColumn;
				mCurrentSpriteAction.mBeginColumn = tmp;
			}
			mCurrentActionColumnDelta = 1;
		} else {
			if (mCurrentSpriteAction.mEndColumn > mCurrentSpriteAction.mBeginColumn)
				mCurrentActionColumnDelta = 1;
			else
				mCurrentActionColumnDelta = -1;
		}
		mCurrentActionColumn = mCurrentSpriteAction.mBeginColumn;
		mCurrentActionLastUpdateTime = Time.realtimeSinceStartup;
		Vector2 offset = mSpriteSheetMaterial.mainTextureOffset;
		offset.x = ComputeXOffset(mCurrentActionColumn);
		offset.y = ComputeYOffset(mCurrentSpriteAction.mRow);
		mSpriteSheetMaterial.mainTextureOffset = offset;
	}
	
	
	#region private utility functions
	private void ComputeNextAndContinue() {		
		mCurrentActionColumn += mCurrentActionColumnDelta;
		
		// check for the end
		if (mCurrentSpriteAction.mShouldLoop) { // end > begin is guaranteed!!
			if (mCurrentActionColumnDelta > 0) {
				// counting up in index
				if (mCurrentActionColumn > mCurrentSpriteAction.mEndColumn) {
					mCurrentActionColumnDelta = -1;
					mCurrentActionColumn = mCurrentSpriteAction.mEndColumn + mCurrentActionColumnDelta;
				} 
			} else {
				// count down in index
				if (mCurrentActionColumn < mCurrentSpriteAction.mBeginColumn) {
					mCurrentActionColumnDelta = 1;
					mCurrentActionColumn = mCurrentSpriteAction.mBeginColumn + mCurrentActionColumnDelta;
				} 
			}
		} else {
			// not loop, 
			if (mCurrentActionColumnDelta > 0) {
				// end > begin
				if (mCurrentActionColumn > mCurrentSpriteAction.mEndColumn) {
					mCurrentActionColumn = mCurrentSpriteAction.mBeginColumn;
				}
			} else {
				// end < begin
				if (mCurrentActionColumn < mCurrentSpriteAction.mEndColumn) {
					mCurrentActionColumn = mCurrentSpriteAction.mBeginColumn;
				}
			}
		}
	}
	
	private void InitializeSpriteSheet()
	{
		mBlankToLeftInUV = mBlankPixelsToLeft / (float) mSpriteSheetMaterial.mainTexture.width;
		mBlankToRightInUV = mBlankPixelsToRight / (float) mSpriteSheetMaterial.mainTexture.width;
		mBlankAboveInUV = mBlankPixelsAbove / (float) mSpriteSheetMaterial.mainTexture.height;
		mBlankBelowInUV = mBlankPixelsBelow / (float) mSpriteSheetMaterial.mainTexture.height;
		mSpriteWidthInUV = (1f/(float) mColumnInSheet) - mBlankToLeftInUV - mBlankToRightInUV;
		mSpriteHeightInUV = (1f/(float) mRowInSheet) - mBlankAboveInUV - mBlankBelowInUV;
		
		// for now, let's just initialize to the first sprite element
		Vector2 offset;
		offset.x = ComputeXOffset(0);
		offset.y = ComputeYOffset(0);
		mSpriteSheetMaterial.mainTextureOffset = offset;
		
		Vector2 scale = new Vector2(mSpriteWidthInUV, mSpriteHeightInUV);
		mSpriteSheetMaterial.mainTextureScale = scale;
		// Debug.Log ("Texture dimension:" + mSpriteSheetMaterial.mainTexture.width.ToString() + "x" + 
		//		mSpriteSheetMaterial.mainTexture.height.ToString() + ". Set offset to: " + offset);
	}
	
	private float ComputeXOffset(int c) {
		float useC = mColumnInSheet - 1 - c;
		return useC * (mBlankToLeftInUV + mSpriteWidthInUV + mBlankToRightInUV) + mBlankToLeftInUV;
	}
	
	private float ComputeYOffset(int r) {
		float useR = mRowInSheet - 1 - r;
		return useR * ( mBlankAboveInUV + mSpriteHeightInUV + mBlankBelowInUV) + mBlankAboveInUV;
	}
	#endregion

}
