using UnityEngine;
using System.Collections;

// Authors: Dipen Patel and Kevin Sutanto
public class Button : CustomGUI 
{
	#region Constants
	//directories to corresponding textures
    protected new const string normalTextureDir = "CustomGUI/buttonNormal";
    protected new const string activeTextureDir = "CustomGUI/buttonActive";
    protected new const string hoverTextureDir = "CustomGUI/buttonHover";
	#endregion
	
    public Button()
        : base()
    {

    }
	
	#region Constructors
    public Button(float x, float y)
        : base(x, y)
    {

    }

    public Button(float x, float y, float width, float height)
        : base(x, y, width, height)
    {

    }

    protected override bool Update()
    {
        return  GUI.Button(rect, text, style);
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
