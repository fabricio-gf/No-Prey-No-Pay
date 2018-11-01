using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ConfigData/InputConfig")]
public class InputConfig : ScriptableObject
{
    // --------------------------------- PUBLIC ATTRIBUTES ------------------------------- //
    [Header("Global")]
    public float m_triggMinRatio                        = .3f;
    public bool m_pcDebugMode                           = false;

    [Header("Locomotion")]
    public InputMgr.eXBoxButton m_dashButton            = InputMgr.eXBoxButton.TRIGG_RIGHT;

    public InputMgr.eXBoxButton m_jumpButton            = InputMgr.eXBoxButton.A;

    public InputMgr.eXBoxButton m_tossButton            = InputMgr.eXBoxButton.B;
    public InputMgr.eXBoxButton m_attackButton          = InputMgr.eXBoxButton.X;
    public InputMgr.eXBoxButton m_grabButton            = InputMgr.eXBoxButton.Y;

    [Header("Menu")]
    public InputMgr.eXBoxButton m_submitButton          = InputMgr.eXBoxButton.A;
    public InputMgr.eXBoxButton m_previousButton        = InputMgr.eXBoxButton.B;
    public InputMgr.eXBoxButton m_pauseButton           = InputMgr.eXBoxButton.START;
    public InputMgr.eXBoxButton m_changeColorButton     = InputMgr.eXBoxButton.X;
}
