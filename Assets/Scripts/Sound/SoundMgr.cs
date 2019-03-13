using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : RuntimeMonoBehaviour
{
    // --------------------------------- PUBLIC AUX ENUMS -------------------------------- //
    public enum eSoundEventType
    {
        FistAttk,
        SaberAttk,
        PistolAttk,

        Dash,
        Death
    }

    
    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public SoundLib         m_soundLib;
    public Transform        m_soundSpawnner;
    
    // ------------------------------- PRIVATE ATTRIBUTES -------------------------------- //
    private static SoundMgr m_manager;
    private AudioSource     m_bckMusicSource;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Awake()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - SoundMgr : Manager must be unique!");
        Debug.Assert(m_soundLib != null, this.gameObject.name + " - SoundMgr : Missing SoundLib");
        Debug.Assert(m_soundSpawnner != null, this.gameObject.name + " - SoundMgr : Missing Sound Spawnner");

        m_manager = this;

        m_bckMusicSource                = this.gameObject.AddComponent<AudioSource>();
        m_bckMusicSource.playOnAwake    = false;
        m_bckMusicSource.loop           = true;
        m_bckMusicSource.clip           = m_soundLib.m_matchBkgMusic.m_clip;
        m_bckMusicSource.volume         = m_soundLib.m_matchBkgMusic.m_volume;
    }
    // ======================================================================================
    public static void PlaySoundEvent(eSoundEventType _event)
    {
        switch (_event)
        {
            case eSoundEventType.FistAttk:
                m_manager.StartCoroutine(m_manager.PlayAudioClip(m_manager.m_soundLib.m_fistAttkSFX));
                break;
            case eSoundEventType.SaberAttk:
                m_manager.StartCoroutine(m_manager.PlayAudioClip(m_manager.m_soundLib.m_saberAttkSFX));
                break;
            case eSoundEventType.PistolAttk:
                m_manager.StartCoroutine(m_manager.PlayAudioClip(m_manager.m_soundLib.m_pistolAttkSFX));
                break;


            case eSoundEventType.Dash:
                m_manager.StartCoroutine(m_manager.PlayAudioClip(m_manager.m_soundLib.m_dashSFX));
                break;
            case eSoundEventType.Death:
                m_manager.StartCoroutine(m_manager.PlayAudioClip(m_manager.m_soundLib.m_deathSFX));
                break;
        }
    }

    // ======================================================================================
    // PRIVATE MEMBERS
    // ======================================================================================
    private IEnumerator PlayAudioClip(SoundLib.sSoundEvent _soundEvent)
    {
        GameObject soundObj = new GameObject();
        soundObj.transform.parent = m_soundSpawnner;
        AudioSource source  = soundObj.AddComponent<AudioSource>();

        source.clip = _soundEvent.m_clip;
        source.volume = _soundEvent.m_volume;
        source.Play();

        float timer = _soundEvent.m_clip.length;

        while (timer > 0)
        {
            yield return null;

            if (this.IsActive())
            {
                timer -= GameMgr.DeltaTime;
                if (!source.isPlaying)
                    source.UnPause();
            }
            else
                source.Pause();
        }

        Destroy(soundObj);
    }

    // ======================================================================================
    protected override void OnPlay()
    {
        base.OnPlay();
        if (!m_bckMusicSource.isPlaying)
            m_bckMusicSource.Play();
    }

    // ======================================================================================
    protected override void OnPause()
    {
        base.OnPause();
        if (!GameMgr.IsRoundEnd)
            m_bckMusicSource.Pause();
    }
}
