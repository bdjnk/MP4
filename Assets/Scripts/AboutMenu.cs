using UnityEngine;
using System.Collections;

// author of customGui: Dipen Patel
public class AboutMenu : MonoBehaviour 
{
	private const string DEFAULT_LABEL_TEXT = "";
	#region variables
	private Box box;
	private Button backButton;
	
	private Label label;
	private Label nameLabel;
	private Label member1Label;
	private Label member2Label;
	private Label member3Label;
	private Label dateLabel;
	//photos?
	#endregion

	// Use this for initialization
	void Start () 
	{
		box = new Box(0, 0);
		box.AllStyleColor = Color.white;
		box.Width = Screen.width;
		box.Height = Screen.height;
		box.Text = "Shoe Wars";
		box.Style.alignment = TextAnchor.UpperCenter;
		box.NormalTexture = Resources.Load ("Textures/shoesprite") as Texture2D;
		box.ActiveTexture = Resources.Load ("Textures/shoesprite") as Texture2D;
		box.HoverTexture = Resources.Load ("Textures/shoesprite") as Texture2D;
		box.Style.fontSize = (int)(Screen.width * 0.1f); 
	
		backButton = new Button(box.Width / 2 - box.Width * 0.3f / 2, box.Height * 0.2f);
		backButton.AllStyleColor = Color.white;
		backButton.Width = box.Width * 0.3f;
		backButton.Height = backButton.Width * 0.5f;
		backButton.Text = "Main Menu";
		backButton.HoverTexture = Resources.Load ("Textures/menu-hover") as Texture2D;
		backButton.NormalTexture = Resources.Load ("Textures/menu-normal") as Texture2D;
		backButton.ActiveTexture = Resources.Load ("Textures/menu-active") as Texture2D;
		backButton.Style.alignment = TextAnchor.MiddleCenter;
		backButton.Style.fontSize = (int)(Screen.width * 0.05f);
	
		nameLabel = new Label(box.Width / 2 - backButton.Width * 1.5f / 2, backButton.Y + backButton.Height * 0.8f);
		nameLabel.AllStyleColor = Color.white;
		nameLabel.Width = backButton.Width * 1.5f;
		nameLabel.Height = backButton.Height * 0.8f;
		nameLabel.Text = "PositiveGames";
		nameLabel.Style.alignment = TextAnchor.MiddleCenter;
		nameLabel.Style.fontSize = (int)(nameLabel.Width * 0.08f);
		
		member1Label = new Label(box.Width / 2 - backButton.Width * 1.5f / 2, nameLabel.Y + nameLabel.Height * 0.5f);
		member1Label.AllStyleColor = Color.white;
		member1Label.Width = backButton.Width * 1.5f;
		member1Label.Height = backButton.Height * 0.6f;
		member1Label.Text = "Ben Spencer";
		member1Label.Style.alignment = TextAnchor.MiddleCenter;
		member1Label.Style.fontSize = (int)(member1Label.Width * 0.06f);
		
		member2Label = new Label(box.Width / 2 - backButton.Width * 1.5f / 2, member1Label.Y + member1Label.Height * 0.5f);
		member2Label.AllStyleColor = Color.white;
		member2Label.Width = backButton.Width * 1.5f;
		member2Label.Height = backButton.Height * 0.6f;
		member2Label.Text = "Kelvin Campbell";
		member2Label.Style.alignment = TextAnchor.MiddleCenter;
		member2Label.Style.fontSize = (int)(member2Label.Width * 0.06f);
		
		member3Label = new Label(box.Width / 2 - backButton.Width * 1.5f / 2, member2Label.Y + member2Label.Height * 0.5f);
		member3Label.AllStyleColor = Color.white;
		member3Label.Width = backButton.Width * 1.5f;
		member3Label.Height = backButton.Height * 0.6f;
		member3Label.Text = "Michael Plotke";
		member3Label.Style.alignment = TextAnchor.MiddleCenter;
		member3Label.Style.fontSize = (int)(member3Label.Width * 0.06f);
		
		dateLabel = new Label(box.Width / 2 - backButton.Width * 1.5f / 2, member3Label.Y + member3Label.Height * 0.5f);
		dateLabel.AllStyleColor = Color.white;
		dateLabel.Width = backButton.Width * 1.5f;
		dateLabel.Height = backButton.Height * 0.6f;
		dateLabel.Text = "May 1, 2013";
		dateLabel.Style.alignment = TextAnchor.MiddleCenter;
		dateLabel.Style.fontSize = (int)(dateLabel.Width * 0.06f);
		
		label = new Label(box.Width / 2 - backButton.Width * 1.5f / 2, dateLabel.Y + dateLabel.Height * 0.5f);
		label.AllStyleColor = Color.white;
		label.Width = backButton.Width * 1.5f;
		label.Height = backButton.Height * 0.6f;
		label.Text = DEFAULT_LABEL_TEXT;
		label.Style.alignment = TextAnchor.MiddleCenter;
		label.Style.fontSize = (int)(label.Width * 0.06f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log("style color: " + box.NormalStyleColor);
	}
	
	void OnGUI()
	{
		box.OnGUI();
		backButton.OnGUI();
		member1Label.OnGUI();
		member2Label.OnGUI();
		member3Label.OnGUI();
		nameLabel.OnGUI();
		dateLabel.OnGUI();
		//repeatButton.OnGUI();
		label.OnGUI();
		//textBox.OnGUI();
		//checkBox.OnGUI();
		
		#region GUI Events
		if(backButton.Active)
		{
			label.Text = "Returning...";
			Application.LoadLevel(0);
		}

		#endregion
	}
}
