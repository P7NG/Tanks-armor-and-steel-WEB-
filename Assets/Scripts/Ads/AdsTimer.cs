using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class AdsTimer : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameObject _adsPanel;
    [SerializeField] private YandexGame yandexGame;
   

    UnityAction methodDelegate;
    public void TimerMenu()
    {
        if (yandexGame.infoYG.fullscreenAdInterval < yandexGame.CurrentTimeAd)
        {
            yandexGame.CloseFullscreenAd = new UnityEvent();
            methodDelegate = System.Delegate.CreateDelegate(typeof(UnityAction), this, "ChangeSceneMenu") as UnityAction;
            yandexGame.CloseFullscreenAd.AddListener(methodDelegate);
            YandexGame.FullscreenShow();
        }
        else
        {
            ChangeSceneMenu();
        }
    }

    public void TimerReload()
    {
        if (yandexGame.infoYG.fullscreenAdInterval < yandexGame.CurrentTimeAd)
        {
            yandexGame.CloseFullscreenAd = new UnityEvent();
            methodDelegate = System.Delegate.CreateDelegate(typeof(UnityAction), this, "ChangeSceneThis") as UnityAction;
            yandexGame.CloseFullscreenAd.AddListener(methodDelegate);
            YandexGame.FullscreenShow();
        }
        else
        {
            ChangeSceneThis();
        }
    }

    public void ChangeSceneMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeSceneThis()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
