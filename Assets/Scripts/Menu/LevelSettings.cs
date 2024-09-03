using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private Sprite _unMute;
    [SerializeField] private Sprite _mute;
    [SerializeField] private Image _muteButton;

    public static bool IsMute = false;
    public static int OpenLevels;

    [SerializeField] private bool _Mute = IsMute;

    private void Start()
    {
        StartMute();
        StartOpenLevel();
    }

    private void Update()
    {
        _Mute = IsMute;
    }

    private void StartMute()
    {
        if (!PlayerPrefs.HasKey("IsMute"))
        {
            PlayerPrefs.SetInt("IsMute", 0);
        }
        else
        {
            int mute = PlayerPrefs.GetInt("IsMute");
            if (mute == 1)
            {
                IsMute = true;
                if (_muteButton != null)
                {
                    _muteButton.sprite = _mute;
                }
            }
            else if (mute == 0)
            {
                IsMute = false;
                if (_muteButton != null) _muteButton.sprite = _unMute;
            }
        }

        //_camera.GetComponent<AudioListener>().enabled = !IsMute;
    }

    private void StartOpenLevel()
    {
        if (!PlayerPrefs.HasKey("OpenLevels"))
        {
            PlayerPrefs.SetInt("OpenLevels", 0);
            OpenLevels = 0;
        }
        else
        {
            OpenLevels = PlayerPrefs.GetInt("OpenLevels");
        }
    }

    public void OpenLevel(int levelNum)
    {
        if (levelNum > OpenLevels)
        {
            OpenLevels++;
            PlayerPrefs.SetInt("OpenLevels", OpenLevels);
        }
    }

    public void Mute()
    {
        int mute = PlayerPrefs.GetInt("IsMute");

        if (mute == 1)
        {
            IsMute = false;
            PlayerPrefs.SetInt("IsMute", 0);
            if (_muteButton != null) _muteButton.sprite = _unMute;
        }
        else if (mute == 0)
        {
            IsMute = true;
            PlayerPrefs.SetInt("IsMute", 1);
            if (_muteButton != null) _muteButton.sprite = _mute;
        }

        //_camera.GetComponent<AudioListener>().enabled = !IsMute;

    }
}
