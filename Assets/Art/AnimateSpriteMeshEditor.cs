using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AnimateSpriteMeshEditor : MonoBehaviour {

    public AnimatedSpriteMesh anim;

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<AnimatedSpriteMesh>();
        anim.initialize();
    }
	
	// Update is called once per frame
	void Update () {
        anim.refresh();
    }
}
