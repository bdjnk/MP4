using UnityEngine;
using System.Collections;

// Authors: Dipen Patel and Kevin Sutanto
public abstract class CustomGUI
{
    #region Constants
	protected const float DEFAULT_X = 0;
	protected const float DEFAULT_Y = 0;
	protected const float DEFAULT_WIDTH = 100f;
	protected const float DEFAULT_HEIGHT = 100f;
	//directories to corresponding textures
    protected const string normalTextureDir = "CustomGUI/normal";
    protected const string activeTextureDir = "CustomGUI/active";
    protected const string hoverTextureDir = "CustomGUI/hover";
    #endregion

    #region Variables
	protected GUIStyle style;
	protected Rect rect;
	protected string text;
	protected bool visible;
	protected bool active;
    #endregion

    #region Constructors
	public CustomGUI ()
        : this(DEFAULT_X, DEFAULT_Y)
	{
       
	}

	public CustomGUI (float x, float y)
        : this(x, y, DEFAULT_WIDTH, DEFAULT_HEIGHT)
	{

	}

	public CustomGUI (Rect rectangle): this(rectangle.x, rectangle.y, rectangle.width, rectangle.height)
	{

	}

	public CustomGUI (float x, float y, float width, float height)
	{
		InitializeStyle ();
		rect = new Rect (x, y, width, height);    
		text = "";
		visible = true;
		active = false;
	}
    #endregion

    #region Helper Methods / Functions
	protected virtual void InitializeStyle ()
	{
		style = new GUIStyle ();
		NormalTexture = Resources.Load (normalTextureDir) as Texture2D;
		HoverTexture = Resources.Load (hoverTextureDir) as Texture2D;
		ActiveTexture = Resources.Load (activeTextureDir) as Texture2D;
        
	}
    #endregion


	/// <summary>
	/// This method must be called in the owner's script OnGUI() method.
	/// 
	/// </summary>
	public void OnGUI ()
	{
		if (visible) {
			active = Update ();
		}
	}

	/// <summary>
	/// This method must be overriden by child class
	/// </summary>
	/// <returns></returns>
	protected abstract bool Update ();

    #region Getters & Setters

	public float X {
		get {
			return Rectangle.x;
		}
		set {
			Rectangle = new Rect (value, Rectangle.y, Rectangle.width, Rectangle.height);
		}
	}

	public float Y {
		get {
			return Rectangle.y;
		}
		set {
			Rectangle = new Rect (Rectangle.x, value, Rectangle.width, Rectangle.height);
		}
	}

	public float Width {
		get {
			return Rectangle.width;
		}
		set {
			Rectangle = new Rect (Rectangle.x, Rectangle.y, value, Rectangle.height);
		}
	}

	public float Height {
		get {
			return Rectangle.height;
		}
		set {
			Rectangle = new Rect (Rectangle.x, Rectangle.y, Rectangle.width, value);
		}
	}

	public Rect Rectangle {
		get {
			return rect;
		}
		set {
			rect = value;
		}
	}

	public  GUIStyle Style {
		get {
			return style;
		}

		set {
			style = value;
		}
	}
	public Color AllStyleColor {
		set {
			style.normal.textColor = value;
			style.active.textColor = value;
			style.hover.textColor = value;
		}
	}
	
	public Color NormalStyleColor {
		get {
			return style.normal.textColor;
		}
		set {
			style.normal.textColor = value;
		}
	}
	
	public Color ActiveStyleColor {
		get {
			return style.active.textColor;
		}
		set {
			style.active.textColor = value;
		}
	}
	public Color HoverStyleColor {
		get {
			return style.hover.textColor;
		}
		set {
			style.hover.textColor = value;
		}
	}
	
	
	public Texture2D NormalTexture {
		get {
			return style.normal.background;
		}
		set {
			style.normal.background = value;
		}
	}

	public Texture2D HoverTexture {
		get {
			return style.hover.background;
		}
		set {
			style.hover.background = value;
		}
	}

	public Texture2D ActiveTexture {
		get {
			return style.active.background;
		}
		set {
			style.active.background = value;
		}
	}

	public bool Active {
		get {
			return active;
		}
		set {
			active = value;
		}
	}

	public bool Visible {
		get {
			return visible;
		}

		set {
			visible = value;
		}
	}

	public string Text {
		get {
			return text;
		}

		set {
			text = value;
		}
	}
    #endregion

}
