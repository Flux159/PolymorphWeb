using UnityEngine;
using UnityEditor;
using System.Collections;

public class LevelGenerator : EditorWindow
{

	public Texture2D texture;

//	public Color FloorColor = new Color (128f / 255f, 128f / 255f, 128f / 255f);
//	public Color WallColor = new Color (138f / 255f, 43f / 255f, 0.0f);
//
//	public Color EnemyRatColor = new Color (72f / 255f, 0.0f, 255f / 255f);
//	public Color EnemyBatColor = new Color (178f / 255f, 0.0f, 255f / 255f);
//	public Color EnemyFishColor = new Color (255f / 255f, 0.0f, 220f / 255f);
//	public Color EnemyStickColor = new Color (255f / 255f, 0.0f, 110f / 255f);
//
//	public Color StartColor = new Color (255f / 255f, 216f / 255f, 0.0f);
//	public Color ExitColor = new Color (182f / 255f, 255f / 255f, 0.0f);
//
//	public Color DoorColor = new Color (0.0f, 255f / 255f, 33f / 255f);
//
//	public Color PitColor = new Color (0.0f, 0.0f, 0.0f);
//	public Color WaterColor = new Color (0.0f, 148f / 255f, 255f / 255f);
//	public Color WaterWithWallColor = new Color (255f, 0.0f, 0.0f);
//	public Color HoleForRatColor = new Color (255f / 255f, 106f / 255f, 0.0f);
//	public Color HoleForBatColor = new Color (76f / 255f, 255f / 255f, 0.0f);
//	public Color ThinWallForStickColor = new Color (0.0f, 127f / 255f, 70f / 255f);

	//This isn't working - check this and the "FloatEquals, NewFloatEquals, and ColorEquals methods"
	//Along with "GenerateLevel" method
	public Color FloorColor = new Color (128f, 128f, 128f);
	public Color WallColor = new Color (138f, 43f, 0f);
	
	public Color EnemyRatColor = new Color (72f, 0.0f, 255f);
	public Color EnemyBatColor = new Color (178f, 0.0f, 255f);
	public Color EnemyFishColor = new Color (255f, 0.0f, 220f);
	public Color EnemyStickColor = new Color (255f, 0.0f, 110f);
	
	public Color StartColor = new Color (255f, 216f, 0.0f);
	public Color ExitColor = new Color (182f, 255f, 0.0f);
	
	public Color DoorColor = new Color (0.0f, 255f, 33f);
	
	public Color PitColor = new Color (0.0f, 0.0f, 0.0f);
	public Color WaterColor = new Color (0.0f, 148f, 255f);
	public Color WaterWithWallColor = new Color (255f, 0.0f, 0.0f);
	public Color HoleForRatColor = new Color (255f, 106f, 0.0f);
	public Color HoleForBatColor = new Color (76f, 255f, 0.0f);
	public Color ThinWallForStickColor = new Color (0.0f, 127f, 70f);

	public GameObject wallPrefab;
	public GameObject floorPrefab;
	public GameObject ratPrefab;
	public GameObject batPrefab;
	public GameObject fishPrefab;
	public GameObject stickPrefab;
	public GameObject startPrefab;
	public GameObject exitPrefab;
	public GameObject doorPrefab;
	public GameObject pitPrefab;
	public GameObject waterPrefab;
	public GameObject wallWithHolePrefab;
	public GameObject thinWallPrefab;

	private float EPSILON = 0.002f;

	[MenuItem("Custom Tools/Level Generator")]
	static void Init ()
	{
		//Get existing open window, or if none, make a new one:
		LevelGenerator levelGenerator = (LevelGenerator)EditorWindow.GetWindow (typeof(LevelGenerator));
	}

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
		for (int i = 0; i < texture.width; i++) {
			for (int j = 0; j < texture.height; j++) {
				Color currentPixel = texture.GetPixel (i, j);

				if (ColorEquals (FloorColor, currentPixel)) {
//					Debug.Log ("Should be making Floor");
					GenerateFloor (i, j);
				} else if (ColorEquals (WallColor, currentPixel)) {
//					Debug.Log ("Should be making wall");
					GenerateWall (i, j);
				} else if (ColorEquals (EnemyRatColor, currentPixel)) {
					GenerateRat (i, j);
				} else if (ColorEquals (EnemyBatColor, currentPixel)) {
					GenerateBat (i, j);
				} else if (ColorEquals (EnemyFishColor, currentPixel)) {
					GenerateFish (i, j);
				} else if (ColorEquals (EnemyStickColor, currentPixel)) {
					GenerateStick (i, j);
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
				} else if (ColorEquals (WaterWithWallColor, currentPixel)) {
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

	bool FloatEquals (float a, float b)
	{
		return (((a - EPSILON) < b) && ((a + EPSILON) > b));
	}

	bool NewFloatEquals (float a, float b)
	{
		return ((int)(a * 255f)) == ((int)(b * 255f));
	}

	bool ColorEquals (Color a, Color b)
	{
//		if (FloatEquals (a.r, b.r) && FloatEquals (a.g, b.g) && FloatEquals (a.b, b.b)) {
//			return true;
//		}
		if (NewFloatEquals (a.r, b.r) && NewFloatEquals (a.g, b.g) && NewFloatEquals (a.b, b.b)) {
			return true;
		}

		return false;
	}

	void GenerateFloor (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		//Add floor prefab at location
		Instantiate (floorPrefab, newPosition, Quaternion.identity);
	}

	void GenerateWall (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		//Add floor and Wall at location
		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (wallPrefab, newPosition, Quaternion.identity);
	}

	void GenerateRat (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (ratPrefab, newPosition, Quaternion.identity);
	}

	void GenerateBat (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (batPrefab, newPosition, Quaternion.identity);
	}

	void GenerateFish (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (fishPrefab, newPosition, Quaternion.identity);
	}

	void GenerateStick (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (stickPrefab, newPosition, Quaternion.identity);
	}

	void GenerateStart (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (startPrefab, newPosition, Quaternion.identity);
	}

	void GenerateExit (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (exitPrefab, newPosition, Quaternion.identity);
	}

	void GenerateDoor (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (doorPrefab, newPosition, Quaternion.identity);
	}

	void GeneratePit (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (pitPrefab, newPosition, Quaternion.identity);
	}

	void GenerateWater (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (waterPrefab, newPosition, Quaternion.identity);
	}

	void GenerateWaterWithWalls (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (waterPrefab, newPosition, Quaternion.identity);
		Instantiate (wallPrefab, newPosition, Quaternion.identity);
	}

	void GenerateHoleForRat (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);
		newPosition.y += 1.0f;

		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (wallWithHolePrefab, newPosition, Quaternion.identity);
	}

	void GenerateHoleForBat (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (wallWithHolePrefab, newPosition, Quaternion.identity);
	}

	void GenerateThinWall (int i, int j)
	{
		Vector3 newPosition = new Vector3 (i, 0.0f, j);

		Instantiate (floorPrefab, newPosition, Quaternion.identity);
		Instantiate (thinWallPrefab, newPosition, Quaternion.identity);
	}

}
