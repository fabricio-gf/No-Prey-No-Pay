using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class AnimatedSpriteMesh : MonoBehaviour {

	public SpriteMesh[] mesh;
    public int index;
    SpriteMeshInstance sprMesh;
    bool created = false;

    // Use this for initialization
    void Start () {
        initialize();
    }
	
	// Update is called once per frame
	void Update () {
		refresh();
    }

	public void initialize(){
		sprMesh = this.GetComponent<SpriteMeshInstance>();
	}

	public void refresh(){
		if(index < mesh.Length && sprMesh.spriteMesh != mesh[index]) sprMesh.spriteMesh = mesh[index];
	}
}
