using UnityEngine;
using System.Collections;

// Authors: Dipen Patel and Kevin Sutanto
public class Box : CustomGUI 
{

	#region Constants
	//directories to corresponding textures
    protected new const string normalTextureDir = "CustomGUI/box";
    protected new const string activeTextureDir = "CustomGUI/box";
    protected new const string hoverTextureDir = "CustomGUI/box";
	#endregion
	
	
    public Box()
        : base()
    {

    }
	
	#region Constructors
    public Box(float x, float y)
        : base(x, y)
    {

    }

    public Box(float x, float y, float width, float height)
        : base(x, y, width, height)
    {

    }

    protected override bool Update()
    {
        GUI.Box(rect, text, style);
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