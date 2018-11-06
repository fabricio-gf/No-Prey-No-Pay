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
		anim.SetBool("inPlay", true);
	}

	public void goControls(){
		anim.SetBool("inControls", true);
	}

	public void goCredits(){
		anim.SetBool("inCredits", true);
	}

	public void goExit(){
		anim.SetBool("inExit", true);
	}

	public void goBack(){
		anim.SetBool("inPlay", false);
		anim.SetBool("inControls", false);
		anim.SetBool("inCredits", false);
		anim.SetBool("inExit", false);
	}

}
