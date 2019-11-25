#pragma strict
var pr:Projector;
var FramesPerSecond:int = 30;
var causticsTextures:Texture[] = null;
//@script ExecuteInEditMode()
function Start () {
	pr = GetComponent.<Projector>();
}

function Update () {
	var causticsIndex:int = (Time.time * FramesPerSecond) % causticsTextures.Length;
	pr.material.SetTexture("_MainTex", causticsTextures[causticsIndex]);
}