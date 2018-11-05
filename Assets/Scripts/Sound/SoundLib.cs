using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ConfigData/SoundLibrary")]
public class SoundLib : ScriptableObject
{
    [System.Serializable]
    public struct sSoundEvent
    {
        public AudioClip    m_clip;
        [Range(0,1)]
        public float        m_volume;
    }

    [Header("BKG Musics")]
    public sSoundEvent m_matchBkgMusic;

    [Header("Player SFX")]
    public sSoundEvent m_fistAttkSFX;
    public sSoundEvent m_saberAttkSFX;
    public sSoundEvent m_pistolAttkSFX;

    public sSoundEvent m_dashSFX;
    public sSoundEvent m_deathSFX;
}
