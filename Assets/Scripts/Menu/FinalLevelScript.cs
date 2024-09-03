using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelScript : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _buttonsPanel;

    public void Lose()
    {
        _buttonsPanel.SetActive(false);
        _losePanel.SetActive(true);
    }

    public void Win()
    {
        _buttonsPanel.SetActive(false);
        _winPanel.SetActive(true);
    }
}
