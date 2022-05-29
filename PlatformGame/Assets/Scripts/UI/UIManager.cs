using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    Slider playerSlider; //sliderý ve cointexti konrol edicez o yüzden  bunlara ulaþmamýz gerek

    [SerializeField]
    TMP_Text coinTxt;
    private void Awake()
    {
        instance = this;
    }
    public void SliderGuncelle(int gecerliDeger,int maxDeger)
    {
        playerSlider.maxValue = maxDeger;
        playerSlider.value = gecerliDeger;

    }

    public void CoinGuncelle()
    {
        coinTxt.text = GameManager.instance.toplananCoinAdet.ToString();
    }
}
