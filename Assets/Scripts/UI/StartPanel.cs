using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private Button startBtn;
    private CanvasGroup canvasGroup;
    public override void ShowMe()
    {
        base.ShowMe();
        canvasGroup = GetComponent<CanvasGroup>();
        startBtn = GetComponent<Button>("StartBtn");

        FadeIn();
        startBtn.onClick.AddListener(FadeOut);
    }

    public void FadeIn()
    {
        LeanTween.alphaCanvas(canvasGroup, 1, 1f).setEase(LeanTweenType.linear);
    }


    public void FadeOut()
    {
        LeanTween.alphaCanvas(canvasGroup, 0, 0.5f).setEase(LeanTweenType.linear).setOnComplete(() =>
        {
            GameManager.Instance.StartGame();
        });
    }
}
