using UnityEngine;
using System.Collections;

// author of customGUI: Dipen Patel
public class ShoeMenu : MonoBehaviour 
{
	private const string DEFAULT_LABEL_TEXT = "";
	#region variables
	private Box box;
	private Button playButton;
	private Button quitButton;
	private Button aboutButton;
	private Label nameLabel;
	#endregion

	// Use this for initialization
	void Start () 
	{
		Texture2D hover = Resources.Load ("Textures/menu-hover") as Texture2D;
		Texture2D normal = Resources.Load ("Textures/menu-normal") as Texture2D;
		Texture2D active = Resources.Load ("Textures/menu-active") as Texture2D;
		
		Texture2D boxTexture = Resources.Load ("Textures/shoesprite") as Texture2D;
		
		box = new Box(0, 0);
		box.AllStyleColor = Color.white;
		box.Width = Screen.width;
		box.Height = Screen.height;
		box.Text = "Shoe Wars";
		box.NormalTexture = boxTexture;
		box.HoverTexture = boxTexture;
		box.ActiveTexture = boxTexture;
		box.Style.alignment = TextAnchor.UpperCenter;
		box.Style.fontSize = (int)(Screen.width * 0.1f);
		
		nameLabel = new Label(box.Width / 2f - box.Width * 0.3f * 1.5f / 2, box.Height - 2f);
		nameLabel.AllStyleColor = Color.white;
		nameLabel.Width = box.Width * 0.3f * 1.5f;
		nameLabel.Height = box.Width * 0.3f * 0.25f;
		nameLabel.Text = "PositiveGames";
		nameLabel.Style.alignment = TextAnchor.MiddleCenter;
		nameLabel.Style.fontSize = (int)(nameLabel.Width * 0.1f);
		
		playButton = new Button(box.Width / 2f - box.Width * 0.3f * 0.5f, box.Height * 0.2f);
		playButton.AllStyleColor = Color.white;
		playButton.Width = box.Width * 0.3f;
		playButton.Height = playButton.Width * 0.5f;
		playButton.Text = "Play Game";
		playButton.Style.fontSize = (int)(Screen.width * 0.05f);
		playButton.HoverTexture = hover;
		playButton.NormalTexture = normal;
		playButton.ActiveTexture = active;
		playButton.Style.alignment = TextAnchor.MiddleCenter;
	
		quitButton = new Button(box.Width / 2f - box.Width * 0.3f * 0.5f, playButton.Y + playButton.Height);
		quitButton.AllStyleColor = Color.white;
		quitButton.Width = box.Width * 0.3f;
		quitButton.Height = quitButton.Width * 0.5f;
		quitButton.Text = "Quit";
		quitButton.Style.fontSize = (int)(Screen.width * 0.05f);
		quitButton.HoverTexture = hover;
		quitButton.NormalTexture = normal;
		quitButton.ActiveTexture = active;
		quitButton.Style.alignment = TextAnchor.MiddleCenter;
		
		aboutButton = new Button(box.Width / 2f - box.Width * 0.3f * 0.5f, quitButton.Y + quitButton.Height);
		aboutButton.AllStyleColor = Color.white;
		aboutButton.Width = box.Width * 0.3f;
		aboutButton.Height = aboutButton.Width * 0.5f;
		aboutButton.Text = "About";
		aboutButton.Style.fontSize = (int)(Screen.width * 0.05f);
		aboutButton.HoverTexture = hover;
		aboutButton.NormalTexture = normal;
		aboutButton.ActiveTexture = active;
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
