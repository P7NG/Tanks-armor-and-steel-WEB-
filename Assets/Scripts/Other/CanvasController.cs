using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Image HealthBar;
    public GameObject TrackImage;
    public Image ReloadButton;

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);

        
    }

    public void ChangeHealthBar(float healthPercent)
    {
        HealthBar.fillAmount = healthPercent;
    }

    public void ChangeTrackImage(bool destroy)
    {
        TrackImage.SetActive(destroy);
    }

    public void ChangeFillingButton(float time)
    {
        ReloadButton.fillAmount = time;
    }
}
