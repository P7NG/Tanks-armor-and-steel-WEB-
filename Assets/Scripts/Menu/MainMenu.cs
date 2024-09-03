using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenu : MonoBehaviour
{
    public YandexGame yandexGame;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject _levelPanel;
    [SerializeField] private GameObject _partPanel;
    [SerializeField] private GameObject _left;
    [SerializeField] private GameObject _right;

    private int _currentLevel;

    private void Start()
    {
        _left.GetComponent<Image>().color = Color.gray;
        
        if(yandexGame != null)
        {
            yandexGame._GameReadyAPI();
        }
    }

    public void Return(GameObject closePanel)
    {
        closePanel.SetActive(false);

        if (closePanel.name == "Levels_Panel")
        {
            _partPanel.SetActive(true);
        }
    }

    public void Open(GameObject openPanel)
    {
        openPanel.SetActive(true);

        if (openPanel.name == "Levels_Panel")
        {
            _partPanel.SetActive(false);

            for(int i = 0; i < _levels.Length; i++)
            {
                _levels[i].GetComponent<Button>().interactable = false;
            }

            for (int i = 0; i <= LevelSettings.OpenLevels; i++)
            {
                _levels[i].GetComponent<Button>().interactable = true;
            }
        }
    }


    public void ChangeLevel( GameObject button)
    {
        if (button.name == "Right")
        {
            if (_currentLevel+1 < _levels.Length)
            {
                _currentLevel++;
            }

            if(_currentLevel+1 >= _levels.Length)
            {
                button.GetComponent<Image>().color = Color.gray;
            }
        }
        else
        {
            if (_currentLevel > 0)
            {
                _currentLevel--;
            }

            if (_currentLevel <= 0)
            {
                button.GetComponent<Image>().color = Color.gray;
            }
        }

        for(int i = 0; i < _levels.Length; i++)
        {
            _levels[i].SetActive(false);
        }

        _levels[_currentLevel].SetActive(true);

        if (_currentLevel > 0 && _currentLevel < _levels.Length - 1)
        {
            _left.GetComponent<Image>().color = Color.white;
            _right.GetComponent<Image>().color = Color.white;
        }
    }

    public void OpenLevel(int Number)
    {
        SceneManager.LoadScene(Number);
    }

    public void VK()
    {
        Application.OpenURL("https://vk.com/club227162797");
    }
}
