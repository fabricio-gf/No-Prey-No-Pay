using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWatcher : MonoBehaviour {

	public AnimatedSpriteMesh handB, handF;
	public GameObject saber, pistol;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)) PickPistol();

		if (Input.GetKeyDown(KeyCode.S)) PickSaber();

		if (Input.GetKeyDown(KeyCode.D)) Drop();
	}

	public void PickPistol(){
		handB.index = 1;
		pistol.SetActive(true);
		saber.SetActive(false);
	}

	public void PickSaber(){
		handB.index = 1;
		saber.SetActive(true);
		pistol.SetActive(false);
	}

	public void Drop(){
		handB.index = 0;
		pistol.SetActive(false);
		saber.SetActive(false);
	}
}
