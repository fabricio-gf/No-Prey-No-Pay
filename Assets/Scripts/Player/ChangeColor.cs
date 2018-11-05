using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour {

    public bool ui;
	public SpriteMeshInstance[] suit;
    public Image[] overlays;
	[Header("Colors")]
	public Color yellow;
	public Color red;
	public Color blue;
	public Color green;
	[Space(5)]

	[Range(1,4)]
	public int color;

	void OnValidate(){
        if (ui){
            foreach(Image s in overlays){
                if (color == 1)
                    s.color = yellow;
                else if (color == 2)
                    s.color = red;
                else if (color == 3)
                    s.color = blue;
                else if (color == 4)
                    s.color = green;
            }
            
        }else{
            foreach(SpriteMeshInstance s in suit){
                if (color == 1)
                    s.color = yellow;
                else if (color == 2)
                    s.color = red;
                else if (color == 3)
                    s.color = blue;
                else if (color == 4)
                    s.color = green;
            }
        }
    }

	public void ManualValidate(){
		if (ui){
            foreach(Image s in overlays){
                if (color == 1)
                    s.color = yellow;
                else if (color == 2)
                    s.color = red;
                else if (color == 3)
                    s.color = blue;
                else if (color == 4)
                    s.color = green;
            }
        }else{
            foreach(SpriteMeshInstance s in suit){
                if (color == 1)
                    s.color = yellow;
                else if (color == 2)
                    s.color = red;
                else if (color == 3)
                    s.color = blue;
                else if (color == 4)
                    s.color = green;
            }
        }
	}
}
