using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenerController : MonoBehaviour //fenerin colliderla kaplad���m�z alan�na girdi�i an fener yans�n ��kt��� an fener kapans�n istiyoruz
{
    [SerializeField]
    SpriteRenderer fenerSprRenderer;//resmi de�i�tirece�imiz i�in resme ula�mam�z gerek
    [SerializeField]
    Sprite fenerOnSprite, fenerOffSprite;

    private void Awake()
    {
        fenerSprRenderer.sprite = fenerOffSprite;//fener a��k kalm�� olabilir o y�zden en ba�ta off haline getiriyoruz
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
            Invoke("FeneriKapat", .5f); //�noke fonksiyonu belli bir saniye sonra �al��t�ran bir fonksiyondur.
        
    }

    void FeneriKapat()
    {
        fenerSprRenderer.sprite = fenerOffSprite;

    }

}
