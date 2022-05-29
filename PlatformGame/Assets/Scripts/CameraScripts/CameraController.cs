using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerHareketController player;//örenk bir nesne oluþturdum 
    [SerializeField]
    Collider2D boundsBox;

    float halfYukseklik, halfGenislik;

    Vector2 sonPos;//vektör2 formatýnda kameranýn son pozisyonunu tutan bir deðiþken tanýmladýk
    [SerializeField]
    Transform backgrounds;

    private void Awake()
    {
        player = Object.FindObjectOfType<PlayerHareketController>();//ve playerhareketkont scriptine ulaþmýþ olduk

    }
    private void Start() //kameranýn baþlangýcta asaðý yukarý kaymamasý için yazýldý
    {
        halfYukseklik = Camera.main.orthographicSize;
        halfGenislik = halfYukseklik * Camera.main.aspect; //geniþliði buluyoruz

        sonPos = transform.position; //kameranýn pozisyonunu son pozisyona ata
    }
    private void Update()
    {
        if(player!=null) //player sahnede varsa
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x,boundsBox.bounds.min.x+halfGenislik,boundsBox.bounds.max.x-halfGenislik),                                                          //clamp max min ve kýsýtlanacak deðeri ister
                Mathf.Clamp(player.transform.position.y,boundsBox.bounds.min.y+halfYukseklik,boundsBox.bounds.max.y-halfYukseklik),
                transform.position.z); //plaeryin x ve y deðeerlerini deðiþtirmeliyiz z deðerini deðiþtirmemeliyiz z de kameranýn transformunu aldýk x ve y de playerýnkini aldýk

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
