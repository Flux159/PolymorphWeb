using UnityEngine;
using System.Collections;

public class GamePad : MonoBehaviour
{
		public enum Axis
		{
				LeftXAxis,
				LeftYAxis,
				RightXAxis,
				RightYAxis,
				RightTrigger,
				LeftTrigger,
				DPadX,
				DPadY}
		;
		public enum Button
		{
				A,
				B,
				Y,
				X,
				L1,
				R1,
				Back,
				Start,
				LStick,
				RStick }
		;
#if UNITY_STANDALONE_WIN

	//enum Buttons{A, B, Y, X, RB, LB, Back, Start, L3, R3}
	private static string[] buttonStrings = {"joystick button 0", "joystick button 1", "joystick button 2", "joystick button 3", "joystick button 4",
		"joystick button 5", "joystick button 6", "joystick button 7", "joystick button 8", "joystick button 9"};

	private static string[] axisStrings = {"X axis", "Y axis", "4th axis", "5th axis", "10th axis", "9th axis", "6th axis", "7th axis"};
#endif
#if UNITY_STANDALONE_OSX
		private static string[] buttonStrings = {"joystick button 16", "joystick button 17", "joystick button 19", "joystick button 18", 
		"joystick button 13",
		"joystick button 14", "joystick button 10", "joystick button 9", "joystick button 11", "joystick button 12"};
	
		private static string[] axisStrings = {"X axis", "Y axis", "4th axis", "6th axis", "5th axis", "6th axis", "7th axis"};
#endif
		/*private string a_Button = "joystick button 0";
	private string b_Button = "joystick button 1";
	private string y_Button = "joystick button 3";
	private string x_Button = "joystick button 2";
	private string r_Bumper = "joystick button 5";
	private string l_Bumper = "joystick button 4";
	private string back_Button = "joystick button 6";
	private string start_Button = "joystick button 7";
	private string l_stick_Click = "joystick button 8";
	private string r_stick_Click = "joystick button 9";*/

		//enum Axes{LStickHori, LStickVert, R2, L2, RStickHori, RStickVert, DPadHori, DPadVert};
		//Must create these axis in edit->project settings -> input
		//and change or add new axes to use, use the axis in the string
		//and make sure that the senstitivty is 1, gravity 1, and dead is 0.19
		//no buttons and the type is a joystick axis
		/*private string l_stick_hori = "X axis";
	private string l_stick_vert = "Y axis";
	private string r_trigger = "10th axis";
	private string l_trigger = "9th axis";
	private string r_stick_hori = "4th axis";
	private string r_stick_vert = "5th axis";
	private string d_pad_hori = "6th axis";
	private string d_pad_vert = "7th axis";*/
		/*
	float lStickHori=0f;
	float lStickVert=0f;
	float rStickHori=0f;
	float rStickVert=0f;
	float rTrigger = 0f;
	float lTrigger = 0f;
	float dPadHori = 0f;
	float dPadVert = 0f;*/
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
		}

		public static float CopyGetAxis (Axis ax)
		{
				return Input.GetAxis (axisStrings [(int)ax]);
		}

		public static bool CopyGetButton (Button but)
		{
				return Input.GetKey (buttonStrings [(int)but]);
		}
		public static bool CopyGetButtonDown (Button but)
		{
				return Input.GetKeyDown (buttonStrings [(int)but]);
		}
}
