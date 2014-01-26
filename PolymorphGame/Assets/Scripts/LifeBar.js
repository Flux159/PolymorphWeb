var i = 0.0f;
var timeToActivate = 4.0f;

function Start() {
renderer.material.SetFloat("_Cutoff", 1.0f);
}

function Update () { 
	renderer.material.SetFloat("_Cutoff", Mathf.Lerp(0, 1.0f, 1-(i / timeToActivate)));
	i += Time.deltaTime; 
}