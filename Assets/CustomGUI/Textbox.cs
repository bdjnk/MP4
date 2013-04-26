using UnityEngine;
using System.Collections;

// Authors: Dipen Patel and Kevin Sutanto
public class Textbox : CustomGUI 
{
	#region Constants
	//directories to corresponding textures
    protected new const string normalTextureDir = "CustomGUI/textboxNormal";
    protected new const string activeTextureDir = "CustomGUI/textboxActive";
    protected new const string hoverTextureDir = "CustomGUI/textboxHover";
	#endregion
	
	#region Variables
	protected int maxLength = -1; //maximum length of text allowed by the textbox
	#endregion
	
	public Textbox()
        : base()
    {

    }
	
	#region Constructors
    public Textbox(float x, float y)
        : base(x, y)
    {

    }

    public Textbox(float x, float y, float width, float height)
        : base(x, y, width, height)
    {

    }
	
	//constructors with maximum length
	public Textbox(float x, float y, int maxlength)
        : base(x, y)
    {
		maxLength = maxlength;
    }

    public Textbox(float x, float y, float width, float height, int maxlength)
        : base(x, y, width, height)
    {
		maxLength = maxlength;
    }


    protected override bool Update()
    {
		//if maximum length is specified
        if (maxLength != -1)
		{
			text = GUI.TextField(rect, text, maxLength, style);
		}
		//maximum length unspecified
		else 
		{
			text = GUI.TextField(rect, text, style); 
		}
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
	
	#region Getters & Setters
	public int Maxlength
    {
        get
        {
            return maxLength;
        }
        set
        {
            maxLength = value;
        }
    }
	#endregion
}
