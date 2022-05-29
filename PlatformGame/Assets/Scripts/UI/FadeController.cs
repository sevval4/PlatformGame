using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeController : MonoBehaviour
{
    public static FadeController instance;

    [SerializeField]
    GameObject fadeImg;
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        MattanSeffafaGec();
    }
    public void SeffaftanMataGec()
    {
        fadeImg.GetComponent<CanvasGroup>().alpha = 0f;
        fadeImg.GetComponent<CanvasGroup>().DOFade(1f, 1f);//Dofade yumu8þak geçis saglar

    }
    public void MattanSeffafaGec()
    {
        fadeImg.GetComponent<CanvasGroup>().alpha = 1f;
        fadeImg.GetComponent<CanvasGroup>().DOFade(0f, 1f);

    }

}
