using UnityEngine;

[CreateAssetMenu(menuName = "ConfigData/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("------------- LOCOMOTION PARAMS -------------")]
    [Header("Physics")]
    public float m_gravityRatio             = 5.0f;
    public float m_gravityMaxSpeed          = 6.0f;

    [Header("Jump")]
    public float m_jumpMaxSpeed             = 12;

    [Header("Fall")]
    [Range(0, 1)]
    public float m_ratioToWalk              = 0.62f;

    [Header("Walk")]
    public float m_walkAcc                  = 10;
    public float m_walkMaxSpeed             = 20;

    [Header("Wall Eject (Exclusive Event)")]
    public float m_slideMaxSpeed            = 4;
    public float m_ejectDist                = 1;
    public float m_ejectMaxSpeed            = 15;

    [Header("Dash (Exclusive Event)")]
    public float m_dashDist                 = 1;
    public float m_dashMaxSpeed             = 40;

    [Header("---------- LOCOMOTION FINE TUNNING ----------")]
    [Header("Fine Tunning Thresholds")]
    [Range(.01f,1)]
    public float m_walkMinSpeedRatio        = .1f;
    [Range(.01f, 1f)]
    public float m_ejectMinSpeedRatio       = .3f;
    [Range(.01f, 1f)]
    public float m_dashMinSpeedRatio        = .2f;
}
