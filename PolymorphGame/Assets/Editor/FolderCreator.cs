//Unity Editor FolderCreator.cs
//Generate folders in our project

//Add a menu item to create folders in the project

using UnityEditor;
using UnityEngine;
using System.IO;

//[CustomEditor(typeof(FolderCreator))]
public class FolderCreator : Editor
{
	//Use "Custom Tools/Make Project Folders #&_g" for a shortcut too
	[MenuItem("Custom Tools/Make Project Folders")]
	static void MakeFolders ()
	{
		//		Debug.Log ("Making Project Folders.");
		//Create directories for the project
		Directory.CreateDirectory (Application.dataPath + "/Audio");
		Directory.CreateDirectory (Application.dataPath + "/Materials");
		Directory.CreateDirectory (Application.dataPath + "/Meshes");
		Directory.CreateDirectory (Application.dataPath + "/GUISkins");
		Directory.CreateDirectory (Application.dataPath + "/Fonts");
		Directory.CreateDirectory (Application.dataPath + "/Textures");
		Directory.CreateDirectory (Application.dataPath + "/Resources");
		Directory.CreateDirectory (Application.dataPath + "/Scripts");
		Directory.CreateDirectory (Application.dataPath + "/Shaders");
		Directory.CreateDirectory (Application.dataPath + "/Scenes");
		Directory.CreateDirectory (Application.dataPath + "/Packages");
		Directory.CreateDirectory (Application.dataPath + "/Prefabs");
		Directory.CreateDirectory (Application.dataPath + "/Plugins");
		Directory.CreateDirectory (Application.dataPath + "/Physics");
	}
	
}
