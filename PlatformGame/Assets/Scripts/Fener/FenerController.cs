using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenerController : MonoBehaviour //fenerin colliderla kapladýðýmýz alanýna girdiði an fener yansýn çýktýðý an fener kapansýn istiyoruz
{
    [SerializeField]
    SpriteRenderer fenerSprRenderer;//resmi deðiþtireceðimiz için resme ulaþmamýz gerek
    [SerializeField]
    Sprite fenerOnSprite, fenerOffSprite;

    private void Awake()
    {
        fenerSprRenderer.sprite = fenerOffSprite;//fener açýk kalmýþ olabilir o yüzden en baþta off haline getiriyoruz
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fenerSprRenderer.sprite = fenerOnSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Invoke("FeneriKapat", .5f); //ýnoke fonksiyonu belli bir saniye sonra çalýþtýran bir fonksiyondur.
        
    }

    void FeneriKapat()
    {
        fenerSprRenderer.sprite = fenerOffSprite;

    }

}
