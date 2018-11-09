using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputCtlr))]
public class ShowNbWins : MonoBehaviour
{
    public GameObject[] m_winObjs;
    private PlayerInputCtlr m_input;
	// Use this for initialization
	void Start () {
        m_input = this.gameObject.GetComponent<PlayerInputCtlr>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        int nbWins = MatchReferee.GetNbWins((int) m_input.m_nbPlayer - 1);

        if (nbWins == 0)
            return;

        foreach (GameObject go in m_winObjs)
            go.SetActive(false);

        if (nbWins <= m_winObjs.Length)
            m_winObjs[nbWins - 1].SetActive(true);
	}
}
