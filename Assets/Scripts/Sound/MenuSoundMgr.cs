using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundMgr : MonoBehaviour
{
    // --------------------------------- PUBLIC AUX ENUMS -------------------------------- //
    public enum eSoundEventType
    {
        Click,
        Swap
    }


    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public SoundLib m_soundLib;
    public Transform m_soundSpawnner;

    // ------------------------------- PRIVATE ATTRIBUTES -------------------------------- //
    private static MenuSoundMgr m_manager;
    private AudioSource m_bckMusicSource;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Awake()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - MenuSoundMgr : Manager must be unique!");
        Debug.Assert(m_soundLib != null, this.gameObject.name + " - MenuSoundMgr : Missing SoundLib");
        Debug.Assert(m_soundSpawnner != null, this.gameObject.name + " - MenuSoundMgr : Missing Sound Spawnner");

        m_manager = this;

        PlayBkgMusic();
    }

    // ======================================================================================
    public static void PlaySoundEvent(eSoundEventType _event)
    {
        switch (_event)
        {
            case eSoundEventType.Click:
                m_manager.StartCoroutine(m_manager.PlayAudioClip(m_manager.m_soundLib.m_menuClickSFX));
                break;
            case eSoundEventType.Swap:
                m_manager.StartCoroutine(m_manager.PlayAudioClip(m_manager.m_soundLib.m_menuSwapSFX));
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
        AudioSource source = soundObj.AddComponent<AudioSource>();

        source.clip = _soundEvent.m_clip;
        source.volume = _soundEvent.m_volume;
        source.Play();

        float timer = _soundEvent.m_clip.length;

        while (timer > 0)
        {
            yield return null;

            timer -= Time.deltaTime;
        }

        Destroy(soundObj);
    }

    private void PlayBkgMusic()
    {
        m_bckMusicSource = this.gameObject.AddComponent<AudioSource>();
        m_bckMusicSource.clip = m_soundLib.m_menuBkgMusic.m_clip;
        m_bckMusicSource.volume = m_soundLib.m_menuBkgMusic.m_volume;
        m_bckMusicSource.loop = true;
        m_bckMusicSource.Play();
    }
}
