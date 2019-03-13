using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Assimetric : MonoBehaviour {

	AnimatedSpriteMesh anim;
	public SpriteMesh[] dirMeshes, esqMeshes;
	public Transform reference;

	float direction = 0;

	// Use this for initialization
	void Start () {
		initialize();
	}

	public void initialize(){
		anim = this.GetComponent<AnimatedSpriteMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		refresh();
	}

	public void refresh(){
		if (reference.localScale.x != direction){
			if (reference.localScale.x > 0f) anim.mesh = dirMeshes;
			else anim.mesh = esqMeshes;

			direction = reference.localScale.x;
		}
	}
}
