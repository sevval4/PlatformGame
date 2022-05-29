using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlokController : MonoBehaviour
{
    public Transform altPoint; //altpoint noktasýndan ýþýn göndercez onu aldýk
    Animator anim;
    Vector3 hareketYonu = Vector3.up;//yukarýya 1 brmlik bir hareket yaptýrmamýz gerek
    Vector3 orijinalPos;//yukarýdan aþaðýya ineceði için ilk poziyonu tutmalýyýz
    Vector3 animPos;//animasyon poziyonu
    bool animasyonBaslasinmi; //ýþýnla karakterimiz çarptýðýnda baþlamasý gerek
    bool hareketEtsinmi=true;

    public LayerMask playerLayer;//sadece altýndan player geçtiði zaman çalýþsýn

    public GameObject coinPrefab;
    Vector3 coinPos;

    private void Awake()
    {
        anim = GetComponent<Animator>(); // anim deðiþkeninin içerisine blokun üzerindeki animatorudirekt almýþ oluyoruz.
    }

    private void Start()
    {
        orijinalPos = transform.position; //ilk poziynu bir deðiþkende tuttuk
        animPos = transform.position; //animpos u artrmak için direkt artýrma iþlemi yapýlmadýðýndan bir deðiþkene atýyoruz ardýndan artýrma iþlemi yapabiliriz (transform.position.y diyerek yapamayýz)
        animPos.y += 0.15f;
        coinPos = transform.position;
        coinPos.y += 1f;

    }
    private void Update()
    {
        CarpismayýKontrolEt();
        AnimasyonuBaslat();
    }
    void CarpismayýKontrolEt()
    {
        if (hareketEtsinmi)
        {
            RaycastHit2D hit = Physics2D.Raycast(altPoint.position, Vector2.down, 0.1f, playerLayer);
            // Debug.DrawRay(altPoint.position, Vector2.down, Color.green);//sahnede nerden nereye kadar ýþýn gönderdiðimizi görmek için
            if (hit && hit.collider.gameObject.tag == "Player") //hit kýsmý çarpýþma var mý ve carptýðýmýz nesnenin etkiketi player mý
            {
                anim.Play("mat");
                animasyonBaslasinmi = true;
                hareketEtsinmi = false;

                Instantiate(coinPrefab,coinPos,Quaternion.identity);

             }

        }

    }
    void AnimasyonuBaslat() //ýþýn karakterimize çarparsa animasyon baþlayacak
    {
        if (animasyonBaslasinmi)
        {
            transform.Translate(hareketYonu * Time.smoothDeltaTime);//smoothdeltatime daha yumuþak
            if (transform.position.y >= animPos.y)
            {
                hareketYonu=Vector3.down;
            } else if (transform.position.y <= orijinalPos.y)
            {
                animasyonBaslasinmi = false;
            }
        }


    }
}
