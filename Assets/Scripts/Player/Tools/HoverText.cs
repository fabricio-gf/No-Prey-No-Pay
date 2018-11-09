using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverText : MonoBehaviour {

	public Transform PlayerTransform;

	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(Mathf.Sign(PlayerTransform.localScale.x)*Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
	}
}
