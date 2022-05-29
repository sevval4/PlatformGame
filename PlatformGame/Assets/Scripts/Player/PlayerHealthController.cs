using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance; //böylece playerhealtcon a her yerden ulaþabiliriz

    public int maxSaglik, gecerliSaglik;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;
        if (UIManager.instance != null)
        {
            UIManager.instance.SliderGuncelle(gecerliSaglik, maxSaglik);
        }
       
    }

    public void CaniAzalt()
    {

        gecerliSaglik--;

        UIManager.instance.SliderGuncelle(gecerliSaglik, maxSaglik);

        if (gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;
            ///gameObject.SetActive(false);
           PlayerHareketController.instance.PlayerCanVerdi();
        }
    }

    public void CaniArtir()
    {
        gecerliSaglik++;

        if(gecerliSaglik >= maxSaglik)
        {
            gecerliSaglik = maxSaglik;

        }
        UIManager.instance.SliderGuncelle(gecerliSaglik, maxSaglik);


    }


   
}
