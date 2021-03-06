using UnityEngine;

public class Level2 : Scene {
	
	private const float FUEL_TO_COMPLETE = 100;
	private const float FUEL_DECAY = 0.66f;
	private const float FUEL_PER_ITEM = 5;
	
	private float fuel = 50;
	
	private readonly Rect statusOutlineRectN;
	private readonly Rect statusOutlineRectE;
	private readonly Rect statusOutlineRectS;
	private readonly Rect statusOutlineRectW;
	private readonly GUIStyle statusOutlineStyle;
	
	private readonly GUIStyle statusStyle;
	
	public Level2() {
		//statusOutlineRect = new Rect(40, 20, Main.NATIVE_WIDTH - 80, (int) (30 * Main.GUI_RATIO_HEIGHT));
		statusOutlineRectN = new Rect(39, 19, Main.NATIVE_WIDTH - 78, 1);
		statusOutlineRectE = new Rect(Main.NATIVE_WIDTH - 39, 19, 1, (int) (30 * Main.GUI_RATIO_HEIGHT) + 1);
		statusOutlineRectS = new Rect(39, 19 + (int) (30 * Main.GUI_RATIO_HEIGHT) + 1, Main.NATIVE_WIDTH - 77, 1);
		statusOutlineRectW = new Rect(39, 20, 1, (int) (30 * Main.GUI_RATIO_HEIGHT));
		statusOutlineStyle = new GUIStyle();
		Texture2D texture = new Texture2D(1,1);
	    texture.SetPixel(0,0,new Color(1f,1f,1f,0.5f));
	    texture.Apply(); 
		statusOutlineStyle.normal.background = texture;
		
		statusStyle = new GUIStyle();
		texture = new Texture2D(1,1);
	    texture.SetPixel(0,0,new Color(0f,0f,1f,0.5f));
	    texture.Apply();
		statusStyle.normal.background = texture;
	}
	
	public override bool ControlShip {get{return true;}}
	
	public override bool ShipCollision {get{return true;}}
	
	public override bool AsteroidCollision {get{return true;}}
	
	public override int AsteroidCount {get{return 20;}}
	
	public override bool AsteroidItems {get{return true;}}
	
	public override void Begin() {
		base.Begin();
		
		Main.SpawnShip(Main.BOARD_CENTER);
	}
	
	public override void OnGUI() {
		GUI.Box(statusOutlineRectN, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectE, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectS, GUIContent.none, statusOutlineStyle);
		GUI.Box(statusOutlineRectW, GUIContent.none, statusOutlineStyle);
		
		GUI.Box(new Rect(40, 20, (Main.NATIVE_WIDTH - 80) * Mathf.Max(0, Mathf.Min(1, fuel / FUEL_TO_COMPLETE)), (int) (30 * Main.GUI_RATIO_HEIGHT)), GUIContent.none, statusStyle);
		
		if (Main.ShipCount() != 1 || fuel <= 0) {
			RestartLevel.OnGUI();
		}
	}
	
	public override void Update() {
		if (Main.ShipCount() != 1 || fuel <= 0) {
			Main.ClearShips();
			if (Main.Clicked) {
				Sounds.Click();
				Main.ChangeScenes(new Level1ToLevel2());
			}
		} else {
			fuel -= FUEL_DECAY * Time.deltaTime;
			if (fuel >= FUEL_TO_COMPLETE || Main.IsSkipToNextLevel()) {
				Main.ChangeScenes(new Level2ToLevel3());
			}
		}
	}
	
	public override void ItemGained(GameObject item) {
		if (fuel > 0) {
			fuel += FUEL_PER_ITEM;
		}
	}
	
}
