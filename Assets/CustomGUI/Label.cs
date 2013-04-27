using UnityEngine;
using System.Collections;

// Authors: Dipen Patel and Kevin Sutanto
public class Label : CustomGUI {
	
	#region Constants
	//directories to corresponding textures
    protected new const string normalTextureDir = "CustomGUI/labelNormal";
    protected new const string activeTextureDir = "CustomGUI/labelActive";
    protected new const string hoverTextureDir = "CustomGUI/labelHover";
	#endregion
	
	
public Label()
        : base()
    {

    }
	
	#region Constructors
    public Label(float x, float y)
        : base(x, y)
    {

    }

    public Label(float x, float y, float width, float height)
        : base(x, y, width, height)
    {

    }

    protected override bool Update()
    {
		style.normal.textColor = Color.white;
		style.active.textColor = Color.white;
		style.hover.textColor = Color.white;
		GUI.Label(rect, text, style);
		return false;
    }
	#endregion
	
	
	#region Helper Methods / Functions
    protected override void InitializeStyle()
    {
        style = new GUIStyle();
		NormalTexture = Resources.Load(normalTextureDir) as Texture2D;
        HoverTexture = Resources.Load(hoverTextureDir) as Texture2D;
        ActiveTexture = Resources.Load(activeTextureDir) as Texture2D;
       
    }
    #endregion
}
