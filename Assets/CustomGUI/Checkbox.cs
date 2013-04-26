using UnityEngine;
using System.Collections;

// Authors: Dipen Patel and Kevin Sutanto
public class Checkbox : CustomGUI 
{
	#region Constants
	//directories to corresponding textures
	//checked
    protected const string normalCheckedTextureDir = "CustomGUI/checkboxCheckedNormal";
    protected const string activeCheckedTextureDir = "CustomGUI/checkboxCheckedActive";
    protected const string hoverCheckedTextureDir = "CustomGUI/checkboxCheckedHover";
	
	//unchecked
    protected const string normalUncheckedTextureDir = "CustomGUI/checkboxUncheckedNormal";
    protected const string activeUncheckedTextureDir = "CustomGUI/checkboxUncheckedActive";
    protected const string hoverUncheckedTextureDir = "CustomGUI/checkboxUncheckedHover";
	#endregion
	
	#region Variables
	protected bool val; //checked or not
    #endregion
	
    public Checkbox()
        : base()
    {
		
    }
	
	#region Constructors
    public Checkbox(float x, float y)
        : base(x, y)
    {

    }

    public Checkbox(float x, float y, float width, float height)
        : base(x, y, width, height)
    {

    }
	
	//constructors with checkbox value (the checked-or-not boolean)
	 public Checkbox(float x, float y, bool check)
        : base(x, y)
    {
		val = check;
		InitializeStyle(); //re-initialize after value is set to reflect the corresponding state
    }

    public Checkbox(float x, float y, float width, float height, bool check)
        : base(x, y, width, height)
	{
		val = check;
		InitializeStyle(); //re-initialize after value is set to reflect the corresponding state
    }
	#endregion

    protected override bool Update()
    {
        bool pressed = GUI.Button(rect, text, style);
		if (pressed) {
			val = !val;
			if (val) { //if checked
		        NormalTexture = Resources.Load(normalCheckedTextureDir) as Texture2D;
		        HoverTexture = Resources.Load(hoverCheckedTextureDir) as Texture2D;
		        ActiveTexture = Resources.Load(activeCheckedTextureDir) as Texture2D;
			} else {
				NormalTexture = Resources.Load(normalUncheckedTextureDir) as Texture2D;
		        HoverTexture = Resources.Load(hoverUncheckedTextureDir) as Texture2D;
		        ActiveTexture = Resources.Load(activeUncheckedTextureDir) as Texture2D;
			}
		}
		return pressed;
    }
	
	#region Helper Methods / Functions
    protected override void InitializeStyle()
    {
        style = new GUIStyle();
		if (val) { //if checked
	        NormalTexture = Resources.Load(normalCheckedTextureDir) as Texture2D;
	        HoverTexture = Resources.Load(hoverCheckedTextureDir) as Texture2D;
	        ActiveTexture = Resources.Load(activeCheckedTextureDir) as Texture2D;
		} else {
			NormalTexture = Resources.Load(normalUncheckedTextureDir) as Texture2D;
	        HoverTexture = Resources.Load(hoverUncheckedTextureDir) as Texture2D;
	        ActiveTexture = Resources.Load(activeUncheckedTextureDir) as Texture2D;
		}

    }
    #endregion
	
	#region Getters & Setters
	public bool check
    {
        get
        {
            return val;
        }
        set
        {
            val = value;
        }
    }
	#endregion
}