using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour {

    public bool ui;
    public bool hasHoverText;
	public SpriteMeshInstance[] suit;
    public Image[] overlays;
    public Transform spritesParent;
    public Text hoverText;
	[Header("Colors")]
	public Color yellow;
	public Color red;
	public Color blue;
	public Color green;
	[Space(5)]

	[Range(1,4)]
	public int color;


    private SpriteRenderer[] sprites;

    public void Awake()
    {
        if (spritesParent != null)
            sprites = spritesParent.GetComponentsInChildren<SpriteRenderer>();
    }

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
        if(hasHoverText){
            if (color == 1)
                    hoverText.color = yellow;
                else if (color == 2)
                    hoverText.color = red;
                else if (color == 3)
                    hoverText.color = blue;
                else if (color == 4)
                    hoverText.color = green;
        }

        foreach (SpriteRenderer s in sprites)
        {
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
        if(hasHoverText){
            if (color == 1)
                hoverText.color = yellow;
            else if (color == 2)
                hoverText.color = red;
            else if (color == 3)
                hoverText.color = blue;
            else if (color == 4)
                hoverText.color = green;
        }
	}
}
