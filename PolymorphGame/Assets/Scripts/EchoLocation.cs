using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EchoLocation : MonoBehaviour {
	/// <summary>
	/// The distance the echo location ray trace will go
	/// </summary>
	private const float echoMaxDistance = 10;
	private List<Vector3> echoPositions;
	public GameObject lightSource;


	// Use this for initialization
	void Start () {

		echoPositions = new List<Vector3> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GamePad.CopyGetButtonDown(GamePad.Button.A)) 
		{
			EchoLocate(lightSource);
		}
	}

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
				if(hit.transform.tag != "Player" || hit.transform.tag != "MainCamera")
				{

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
		Generate (instantiate);
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
