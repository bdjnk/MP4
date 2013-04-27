using UnityEngine;
using System.Collections;

// author of customGui: Dipen Patel
public class ShoeMenu : MonoBehaviour 
{
	private const string DEFAULT_LABEL_TEXT = "";
	#region variables
	private Box box;
	private Button playButton;
	private Button quitButton;
	private Button aboutButton;
	//private Label label;
	private Label nameLabel;
	#endregion

	// Use this for initialization
	void Start () 
	{
		box = new Box(0, 0);
		box.Width = Screen.width;
		box.Height = Screen.height;
		box.Text = "Shoe Wars";
		//box.Style.normal.textColor = Color.white;
		box.NormalTexture = Resources.Load ("Textures/shoesprite") as Texture2D;
		box.ActiveTexture = Resources.Load ("Textures/shoesprite") as Texture2D;
		box.HoverTexture = Resources.Load ("Textures/shoesprite") as Texture2D;
		box.Style.alignment = TextAnchor.UpperCenter;
		box.Style.fontSize = (int)(Screen.width * 0.1f);
		
		nameLabel = new Label(box.Width/2f - box.Width * 0.3f*1.5f/2, box.Height - 2f);
		nameLabel.Width = box.Width * 0.3f * 1.5f;
		nameLabel.Height = box.Width * 0.3f * 0.25f;
		nameLabel.Text = "PositiveGames";
		nameLabel.Style.alignment = TextAnchor.MiddleCenter;
		nameLabel.Style.fontSize = (int)(nameLabel.Width * 0.1f);
		
		playButton = new Button(box.Width/2f - box.Width*0.3f/2f, box.Height * 0.25f);
		playButton.Width = box.Width * 0.3f;
		playButton.Height = playButton.Width * 0.25f;
		playButton.Text = "Play Game";
		playButton.HoverTexture = Resources.Load ("Textures/menu-run") as Texture2D;
		playButton.Style.alignment = TextAnchor.MiddleCenter;
	
		quitButton = new Button(box.Width/2f - box.Width*0.3f/2f, playButton.Y + playButton.Height * 1.2f);
		quitButton.Width = box.Width * 0.3f;
		quitButton.Height = quitButton.Width * 0.25f;
		quitButton.Text = "Quit";
		quitButton.HoverTexture = Resources.Load ("Textures/menu-quit") as Texture2D;
		quitButton.Style.alignment = TextAnchor.MiddleCenter;
		
		aboutButton = new Button(box.Width/2f - box.Width*0.3f/2f, quitButton.Y + quitButton.Height * 1.2f);
		aboutButton.Width = box.Width * 0.3f;
		aboutButton.Height = aboutButton.Width * 0.25f;
		aboutButton.Text = "About";
		aboutButton.HoverTexture = Resources.Load ("Textures/menu-about") as Texture2D;
		aboutButton.Style.alignment = TextAnchor.MiddleCenter;

	}
	
	// Update is called once per frame
	void Update () 
	{
		// empty
	}
	
	void OnGUI()
	{
		box.OnGUI();
		playButton.OnGUI();
		quitButton.OnGUI();
		aboutButton.OnGUI();
		nameLabel.OnGUI();

		#region GUI Events
		if(playButton.Active)
		{
			//label.Text = "Loading Game";
			Application.LoadLevel(2);
		}
		else if(quitButton.Active)
		{
			//label.Text = "Exit Game";
			Application.Quit();
		}
		else if(aboutButton.Active)
		{
			//label.Text = "About Positive Games";
			Application.LoadLevel(1);
		}
		#endregion
	}
}
