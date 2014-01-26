using UnityEngine;
using System.Collections;

public class TriggerMaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		BoxCollider[] objs = gameObject.GetComponentsInChildren<BoxCollider> ();
		foreach (BoxCollider bx in objs) 
		{
			if(bx.transform.parent.gameObject.name == "WaterFloor(Clone)")
				bx.isTrigger = true;
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
