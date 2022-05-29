using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerHareketController player;//�renk bir nesne olu�turdum 
    [SerializeField]
    Collider2D boundsBox;

    float halfYukseklik, halfGenislik;

    Vector2 sonPos;//vekt�r2 format�nda kameran�n son pozisyonunu tutan bir de�i�ken tan�mlad�k
    [SerializeField]
    Transform backgrounds;

    private void Awake()
    {
        player = Object.FindObjectOfType<PlayerHareketController>();//ve playerhareketkont scriptine ula�m�� olduk

    }
    private void Start() //kameran�n ba�lang�cta asa�� yukar� kaymamas� i�in yaz�ld�
    {
        halfYukseklik = Camera.main.orthographicSize;
        halfGenislik = halfYukseklik * Camera.main.aspect; //geni�li�i buluyoruz

        sonPos = transform.position; //kameran�n pozisyonunu son pozisyona ata
    }
    private void Update()
    {
        if(player!=null) //player sahnede varsa
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x,boundsBox.bounds.min.x+halfGenislik,boundsBox.bounds.max.x-halfGenislik),                                                          //clamp max min ve k�s�tlanacak de�eri ister
                Mathf.Clamp(player.transform.position.y,boundsBox.bounds.min.y+halfYukseklik,boundsBox.bounds.max.y-halfYukseklik),
                transform.position.z); //plaeryin x ve y de�eerlerini de�i�tirmeliyiz z de�erini de�i�tirmemeliyiz z de kameran�n transformunu ald�k x ve y de player�nkini ald�k

        }
        BackgroundHareket();
    }
     void BackgroundHareket()
     {
        Vector2 aradakifark = new Vector2(transform.position.x - sonPos.x, transform.position.y - sonPos.y);
        backgrounds.position += new Vector3(aradakifark.x, aradakifark.y, 0f);
        sonPos = transform.position;


     }
}
