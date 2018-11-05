using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCity : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void goPlay(){
		anim.SetTrigger("play");
	}

	public void goControls(){
		anim.SetTrigger("controls");
	}

	public void goCredits(){
		anim.SetTrigger("credits");
	}

	public void goExit(){
		anim.SetTrigger("exit");
	}

	public void goBack(){
		anim.SetTrigger("back");
	}

}
