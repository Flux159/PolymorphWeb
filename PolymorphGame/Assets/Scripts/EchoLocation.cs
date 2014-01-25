using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EchoLocation : MonoBehaviour {
	/// <summary>
	/// The distance the echo location ray trace will go
	/// </summary>
	private const float echoMaxDistance = 10;
	private List<Vector3> echoPositions;
	GamePad gamePad;
	public GameObject obj;
	public GameObject line;
	public GameObject line2;
	public GameObject lightSource;
/*	Vector3 lEyeForward;
	Vector3 rEyeForward;
	Transform lEye;
	Transform rEye;*/
	// Use this for initialization
	void Start () {
/*		lEye = null;
		rEye = null;*/
		echoPositions = new List<Vector3> ();
		gamePad = gameObject.GetComponent<GamePad> ();
	/*	Transform[] children = gameObject.GetComponentsInChildren<Transform>();
		foreach(Transform tChild in children)
		{
			if(tChild.gameObject.name == "CameraLeft")
				lEye = tChild;
			if(tChild.gameObject.name == "CameraRight")
				rEye = tChild;
		}*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("joystick button 0")) 
		{
			EchoLocate(obj);
		}
	}

	/*Vector3 FindForward()
	{
		if (rEye != null && lEye != null) {
						lEyeForward = lEye.forward;
						rEyeForward = rEye.forward;

						Vector3 tempFor = Vector3.Normalize(lEyeForward + rEyeForward);
			Debug.Log(tempFor.ToString() + " " + gameObject.transform.forward);
						//tempFor = tempFor / 2;
						return tempFor;
				} 
		else
			return Vector3.zero;
	}*/
	void EchoLocate(GameObject instantiate)
	{

		echoPositions.Clear ();
		Vector3 pos =gameObject.transform.position;
		Vector3 forward =gameObject.transform.forward;//FindForward ();
		float dist = echoMaxDistance;
		RaycastHit hit;
		int numHits = 0;


		while(dist > 0 && numHits < 5)
		{
			if(Physics.Raycast(pos, forward, out hit, dist))
			{
				if(hit.transform.tag == "dungeon")
				{
					//GameObject l = Instantiate(line, Vector3.zero, new Quaternion()) as GameObject;
					//LineRenderer lr = l.GetComponent<LineRenderer>();
					//lr.SetPosition(0, pos);
				//	lr.SetPosition(1, hit.transform.position);
					/*GameObject l2 = Instantiate (line2, Vector3.zero, new Quaternion ()) as GameObject;
					LineRenderer lr2 = l2.GetComponent<LineRenderer> ();
					lr2.SetPosition (0, pos);
					lr2.SetPosition (1, pos+(forward * dist));

					BoxCollider bc = hit.transform.gameObject.GetComponent<BoxCollider>();
					Vector3 closestPoint = bc.ClosestPointOnBounds(hit.transform.position);*/
					//float distance = Vector3.Distance(closestPoint, hit.point);
					float howFar = Vector3.Distance(pos, hit.point);
					pos = (hit.point)+ (.1f * -forward);
					echoPositions.Add(pos);
					forward = Vector3.Normalize(hit.normal + forward);
					dist = dist - howFar;

				}
				numHits++;
			}
			else dist--;
		}
		Generate (lightSource);
	}

	void Generate(GameObject create)
	{
		foreach (Vector3 pos in echoPositions) 
		{
			Instantiate(create, pos, new Quaternion());
		}
		echoPositions.Clear ();
	}
}
