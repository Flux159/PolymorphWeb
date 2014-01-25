using UnityEngine;
using UnityEditor;
using System.Collections;

public class LevelGenerator : EditorWindow
{

	private string LevelName = "NewLevel";
	private Transform LevelTransform;
	private Transform FloorTransform;
	private Transform WallTransform;
	private Transform WaterTransform;
	private Transform PitTransform;
	private Transform MonsterTransform;

	public Texture2D texture;

	private Color FloorColor = new Color (128f / 255f, 128f / 255f, 128f / 255f);
	private Color RegularWallColor = new Color (64f / 255f, 64f / 255f, 64f / 255f);

	private Color EnemyRatColor = new Color (72f / 255f, 0.0f, 255f / 255f);
	private Color EnemyBatColor = new Color (178f / 255f, 0.0f, 255f / 255f);
	private Color EnemyFishColor = new Color (255f / 255f, 0.0f, 220f / 255f);
	private Color EnemyStickColor = new Color (255f / 255f, 0.0f, 110f / 255f);

	private Color StartColor = new Color (255f / 255f, 216f / 255f, 0.0f);
	private Color ExitColor = new Color (182f / 255f, 255f / 255f, 0.0f);

	private Color DoorColor = new Color (0.0f, 255f / 255f, 33f / 255f);

	private Color PitColor = new Color (0.0f, 0.0f, 0.0f);
	private Color WaterColor = new Color (0.0f, 148f / 255f, 255f / 255f);
	private Color WaterWithRegularWallColor = new Color (255f / 255f, 0.0f, 0.0f);
	private Color HoleForRatColor = new Color (255f / 255f, 106f / 255f, 0.0f);
	private Color HoleForBatColor = new Color (76f / 255f, 255f / 255f, 0.0f);
	private Color ThinWallForStickColor = new Color (0.0f, 127f / 255f, 70f / 255f);

	private GameObject wallPrefab;
	private GameObject floorPrefab;
	private GameObject ratPrefab;
	private GameObject batPrefab;
	private GameObject fishPrefab;
	private GameObject stickPrefab;
	private GameObject startPrefab;
	private GameObject exitPrefab;
	private GameObject doorPrefab;
	private GameObject pitPrefab;
	private GameObject waterPrefab;
	private GameObject wallWithHolePrefab;
	private GameObject thinWallPrefab;

	[MenuItem("Custom Tools/Level Generator")]
	static void Init ()
	{
		//Get existing open window, or if none, make a new one:
		EditorWindow.GetWindow (typeof(LevelGenerator));
	}

	/* UI STUFF! */
	Rect CreateRightSidedRect (float width, float height)
	{
		float rightX = position.width - width - 10.0f;
		float rightY = 10.0f;
		return new Rect (rightX, rightY, width, height);
	}

	void OnGUI ()
	{
		GUILayout.BeginArea (CreateRightSidedRect (200, position.height));

		GUI.Label (new Rect (3, 3, 200, 20), "Add a Texture:");
		texture = (Texture2D)EditorGUI.ObjectField (new Rect (0, 0, 200, 60), "", texture, typeof(Texture2D), false);

		GUI.Label (new Rect (0, 80, 200, 20), "Wall Prefab");
		wallPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 100, 200, 20), wallPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 120, 200, 20), "FloorPrefab");
		floorPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 140, 200, 20), floorPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 160, 200, 20), "RatPrefab");
		ratPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 180, 200, 20), ratPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 200, 200, 20), "BatPrefab");
		batPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 220, 200, 20), batPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 240, 200, 20), "FishPrefab");
		fishPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 260, 200, 20), fishPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 280, 200, 20), "StickPrefab");
		stickPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 300, 200, 20), stickPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 320, 200, 20), "StartPrefab");
		startPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 340, 200, 20), startPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 360, 200, 20), "ExitPrefab");
		exitPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 380, 200, 20), exitPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 400, 200, 20), "DoorPrefab");
		doorPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 420, 200, 20), doorPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 440, 200, 20), "PitPrefab");
		pitPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 460, 200, 20), pitPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 480, 200, 20), "WaterPrefab");
		waterPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 500, 200, 20), waterPrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 520, 200, 20), "WallWithHolePrefab");
		wallWithHolePrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 540, 200, 20), wallWithHolePrefab, typeof(GameObject), false);

		GUI.Label (new Rect (0, 560, 200, 20), "ThinWallPrefab");
		thinWallPrefab = (GameObject)EditorGUI.ObjectField (new Rect (0, 580, 200, 20), thinWallPrefab, typeof(GameObject), false);

		if (GUI.Button (new Rect (0, 620, 200, 20), "Generate Level")) {
			GenerateLevel ();
		}

		GUILayout.EndArea ();

		if (texture) {
			GUI.Label (new Rect (25, 45, 100, 15), "Preview:");
			EditorGUI.DrawPreviewTexture (new Rect (25, 60, 100, 100), texture);
		} else {
			GUI.Label (new Rect (25, 45, 100, 15), "No Texture Found.");
		}


	}

	void GenerateLevel ()
	{
		LevelTransform = new GameObject ().transform;
		LevelTransform.name = LevelName;

		for (int i = 0; i < texture.width; i++) {
			for (int j = 0; j < texture.height; j++) {
				Color currentPixel = texture.GetPixel (i, j);

				//Debug.Log("GOT CURRENT PIXEL = " + Mathf.Round(currentPixel.r*255f) + "/" + Mathf.Round(currentPixel.g*255f) + "/" + Mathf.Round(currentPixel.b*255f));
				//Debug.Log("WALL = " + Mathf.Round(RegularWallColor.r*255f) + "/" + Mathf.Round(RegularWallColor.g*255f) + "/" + Mathf.Round(RegularWallColor.b*255f));

				if (ColorEquals (FloorColor, currentPixel)) {
					//Debug.Log ("Should be making Floor");
					GenerateFloor (i, j);
				} else if (ColorEquals (RegularWallColor, currentPixel)) {
					//Debug.Log ("Should be making wall");
					GenerateWall (i, j);
				} else if (ColorEquals (EnemyRatColor, currentPixel)) {
					GenerateRat (i, j);
				} else if (ColorEquals (EnemyBatColor, currentPixel)) {
					GenerateBat (i, j);
				} else if (ColorEquals (EnemyFishColor, currentPixel)) {
					GenerateFish (i, j);
				} else if (ColorEquals (EnemyStickColor, currentPixel)) {
					//TODO: DO THE STICK
					//GenerateStick (i, j);
				} else if (ColorEquals (StartColor, currentPixel)) {
					GenerateStart (i, j);
				} else if (ColorEquals (ExitColor, currentPixel)) {
					GenerateExit (i, j);
				} else if (ColorEquals (DoorColor, currentPixel)) {
					GenerateDoor (i, j);
				} else if (ColorEquals (PitColor, currentPixel)) {
					GeneratePit (i, j);
				} else if (ColorEquals (WaterColor, currentPixel)) {
					GenerateWater (i, j);
				} else if (ColorEquals (WaterWithRegularWallColor, currentPixel)) {
					GenerateWaterWithWalls (i, j);
				} else if (ColorEquals (HoleForRatColor, currentPixel)) {
					GenerateHoleForRat (i, j);
				} else if (ColorEquals (HoleForBatColor, currentPixel)) {
					GenerateHoleForBat (i, j);
				} else if (ColorEquals (ThinWallForStickColor, currentPixel)) {
					GenerateThinWall (i, j);
				}
			}
		}
		Debug.Log ("Generating Level!");
	}

	bool ColorEquals (Color a, Color b)
	{
		//Debug.Log ("Matching colors " + a + " and " + b);
		bool match = ((Mathf.Round (a.r * 255f) == Mathf.Round (b.r * 255f))
			&& (Mathf.Round (a.g * 255f) == Mathf.Round (b.g * 255f))
			&& (Mathf.Round (a.b * 255f) == Mathf.Round (b.b * 255f)));
		return match;
	}

	void GenerateFloor (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		if (!LevelTransform.FindChild ("Floors")) {
			FloorTransform = new GameObject ().transform;
			FloorTransform.name = "Floors";
			FloorTransform.parent = LevelTransform;
		}

		//Add floor prefab at location
		GameObject go = (GameObject)Instantiate (floorPrefab, newPosition, Quaternion.identity);
		go.transform.parent = FloorTransform;
	}

	void GenerateWall (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		//Add floor and Wall at location
		GenerateFloor (i, j);

		if (!LevelTransform.FindChild ("Walls")) {
			WallTransform = new GameObject ().transform;
			WallTransform.name = "Walls";
			WallTransform.parent = LevelTransform;
		}
		
		GameObject go = (GameObject)Instantiate (wallPrefab, newPosition, Quaternion.identity);
		go.transform.parent = WallTransform;
	}

	void GenerateRat (int i, int j)
	{
		GenerateFloor (i, j);
		
		if (!LevelTransform.FindChild ("Monsters")) {
			MonsterTransform = new GameObject ().transform;
			MonsterTransform.name = "Monsters";
			MonsterTransform.parent = LevelTransform;
		}

		Vector3 newPosition = new Vector3 (i, 1.0f, j);

		GameObject go = (GameObject)Instantiate (ratPrefab, newPosition, Quaternion.identity);
		go.transform.parent = MonsterTransform;
	}

	void GenerateBat (int i, int j)
	{
		GenerateFloor (i, j);
		
		if (!LevelTransform.FindChild ("Monsters")) {
			MonsterTransform = new GameObject ().transform;
			MonsterTransform.name = "Monsters";
			MonsterTransform.parent = LevelTransform;
		}

		Vector3 newPosition = new Vector3 (i, 3.0f, j);

		GameObject go = (GameObject)Instantiate (batPrefab, newPosition, Quaternion.identity);
		go.transform.parent = MonsterTransform;
	}

	void GenerateFish (int i, int j)
	{
		GenerateFloor (i, j);
		GenerateWater (i, j);
				
		if (!LevelTransform.FindChild ("Monsters")) {
			MonsterTransform = new GameObject ().transform;
			MonsterTransform.name = "Monsters";
			MonsterTransform.parent = LevelTransform;
		}

		Vector3 newPosition = new Vector3 (i, 1.0f, j);
		GameObject go = (GameObject)Instantiate (fishPrefab, newPosition, Quaternion.identity);
		go.transform.parent = MonsterTransform;
	}
	/*
	void GenerateStick (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		GameObject go = (GameObject) Instantiate(floorPrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
		go = (GameObject) Instantiate(stickPrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
	}
	*/
	
	void GenerateStart (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		GameObject go = (GameObject)Instantiate (floorPrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
		go = (GameObject)Instantiate (startPrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
	}

	void GenerateExit (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		GameObject go = (GameObject)Instantiate (floorPrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
		go = (GameObject)Instantiate (exitPrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
	}

	void GenerateDoor (int i, int j)
	{
		GenerateFloor (i, j);

		Vector3 newPosition = new Vector3 (i, 3.0f, j);
		GameObject go = (GameObject)Instantiate (doorPrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
	}

	void GeneratePit (int i, int j)
	{
		if (!LevelTransform.FindChild ("Pits")) {
			PitTransform = new GameObject ().transform;
			PitTransform.name = "Pits";
			PitTransform.parent = LevelTransform;
		}
		
		Vector3 newPosition = new Vector3 (i, -0.5f, j);

		GameObject go = (GameObject)Instantiate (pitPrefab, newPosition, Quaternion.identity);
		go.transform.parent = PitTransform;
	}

	void GenerateWater (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);
		
		if (!LevelTransform.FindChild ("Waters")) {
			WaterTransform = new GameObject ().transform;
			WaterTransform.name = "Waters";
			WaterTransform.parent = LevelTransform;
		}

		GameObject go = (GameObject)Instantiate (waterPrefab, newPosition, Quaternion.identity);
		go.transform.parent = WaterTransform;
	}

	void GenerateWaterWithWalls (int i, int j)
	{
		GenerateWater (i, j);
		GenerateWall (i, j);
	}

	void GenerateHoleForRat (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		GameObject go = (GameObject)Instantiate (floorPrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
		newPosition.y += 2.0f;
		go = (GameObject)Instantiate (wallWithHolePrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
	}

	void GenerateHoleForBat (int i, int j)
	{
		GenerateFloor (i, j);

		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		GameObject go = (GameObject)Instantiate (floorPrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;

		newPosition.y += 1.0f;
		go = (GameObject)Instantiate (wallWithHolePrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
	}

	void GenerateThinWall (int i, int j)
	{
		GenerateFloor (i, j);

		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		GameObject go = (GameObject)Instantiate (thinWallPrefab, newPosition, Quaternion.identity);
		go.transform.parent = LevelTransform;
	}

}
