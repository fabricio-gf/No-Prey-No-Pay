using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AssimetricEditor : MonoBehaviour {

	public Assimetric script;

	// Use this for initialization
	void Start () {
		script = this.GetComponent<Assimetric>();
		script.initialize();
	}
	
	// Update is called once per frame
	void Update () {
		script.refresh();
	}
}
