using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlokController : MonoBehaviour
{
    public Transform altPoint; //altpoint noktas�ndan ���n g�ndercez onu ald�k
    Animator anim;
    Vector3 hareketYonu = Vector3.up;//yukar�ya 1 brmlik bir hareket yapt�rmam�z gerek
    Vector3 orijinalPos;//yukar�dan a�a��ya inece�i i�in ilk poziyonu tutmal�y�z
    Vector3 animPos;//animasyon poziyonu
    bool animasyonBaslasinmi; //���nla karakterimiz �arpt���nda ba�lamas� gerek
    bool hareketEtsinmi=true;

    public LayerMask playerLayer;//sadece alt�ndan player ge�ti�i zaman �al��s�n

    public GameObject coinPrefab;
    Vector3 coinPos;

    private void Awake()
    {
        anim = GetComponent<Animator>(); // anim de�i�keninin i�erisine blokun �zerindeki animatorudirekt alm�� oluyoruz.
    }

    private void Start()
    {
        orijinalPos = transform.position; //ilk poziynu bir de�i�kende tuttuk
        animPos = transform.position; //animpos u artrmak i�in direkt art�rma i�lemi yap�lmad���ndan bir de�i�kene at�yoruz ard�ndan art�rma i�lemi yapabiliriz (transform.position.y diyerek yapamay�z)
        animPos.y += 0.15f;
        coinPos = transform.position;
        coinPos.y += 1f;

    }
    private void Update()
    {
        Carpismay�KontrolEt();
        AnimasyonuBaslat();
    }
    void Carpismay�KontrolEt()
    {
        if (hareketEtsinmi)
        {
            RaycastHit2D hit = Physics2D.Raycast(altPoint.position, Vector2.down, 0.1f, playerLayer);
            // Debug.DrawRay(altPoint.position, Vector2.down, Color.green);//sahnede nerden nereye kadar ���n g�nderdi�imizi g�rmek i�in
            if (hit && hit.collider.gameObject.tag == "Player") //hit k�sm� �arp��ma var m� ve carpt���m�z nesnenin etkiketi player m�
            {
                anim.Play("mat");
                animasyonBaslasinmi = true;
                hareketEtsinmi = false;

                Instantiate(coinPrefab,coinPos,Quaternion.identity);

             }

        }

    }
    void AnimasyonuBaslat() //���n karakterimize �arparsa animasyon ba�layacak
    {
        if (animasyonBaslasinmi)
        {
            transform.Translate(hareketYonu * Time.smoothDeltaTime);//smoothdeltatime daha yumu�ak
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
