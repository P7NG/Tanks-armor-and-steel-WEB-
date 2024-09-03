using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public Clip[] _clips;
    private AudioSource _source;
    private float _volume;

    private void Start()
    {
        _source = gameObject.GetComponent<AudioSource>();
        _volume = _source.volume;
    }

    private void Update()
    {
        float x = 1;
        if (LevelSettings.IsMute)
        {
            x = 0;
        }
        else
        {
            x = 1;
        }
        
        _source.volume = _volume * x;
    }

    public void PlaySoundOneShot(string name)
    {
        for(int i = 0; i < _clips.Length; i++)
        {
            if(_clips[i].Name == name)
            {
                _source.PlayOneShot(_clips[i].AudioClip);
            }
        }
    }

    public void SlowSoundStart(string name)
    {
        for (int i = 0; i < _clips.Length; i++)
        {
            if (_clips[i].Name == name)
            {
                StartCoroutine(SlowChangeVolume(1));
            }
        }
    }

    IEnumerator SlowChangeVolume(int dir)
    {
        if (dir == 1)
        {
            while (_source.volume < 0.08)
            {
                yield return new WaitForSeconds(0.01f);
                _source.volume += 0.0001f;
            }
        }
        else if(dir == -1)
        {
            while (_source.volume > 0)
            {
                yield return new WaitForSeconds(0.01f);
                _source.volume -= 0.001f;
            }
        }
    }

    public void StopSoundSlow()
    {
        StartCoroutine(SlowChangeVolume(-1));
    }

    public void StopSound()
    {
        _source.Stop();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        AudioListener.pause = silence;
        // Or / And
        AudioListener.volume = silence ? 0 : 1;
    }
}


[System.Serializable]
public class Clip
{
    public string Name;
    public AudioClip AudioClip;
}
