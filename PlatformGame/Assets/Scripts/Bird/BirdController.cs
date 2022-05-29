using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;          //D��ar�daki pozisyonlar� almam�z gerek ve transform bir dizi olaca�� i�n pozisyonlar isminde bir dizi olu�turduk

    public float birdSpeed;
    public float beklemeSuresi;//ku� bir noktaya kondu�u zaman bekleyecek.
    float beklemeSayac;//bekleme s�recini geriye sayd�ran bir saya� olu�turduk.
    int kacinciPozisyon;
    Animator anim;

    Vector2 kusYonu;
    private void Awake() //ilk olarak pozisyonlar�n hepsini d��ar�ya atmal�y�zki ku�u yaln�z hareket ettirebilelim
    {

        anim = GetComponent<Animator>();
        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;//dizideki her eleman�n parent�n� n
        }
        
    }
    private void Start()
    {
        kacinciPozisyon = 0;
        transform.position = pozisyonlar[kacinciPozisyon].position; //ku�un ilk ba�layaca�� pozisyonu ayarlad�k
        
    }

    private void Update()
    {
        if (beklemeSayac > 0)
        {
            beklemeSayac -= Time.deltaTime;
            anim.SetBool("ucsunMu", false);
        }
        else//ilk hareketimizi ger�ekle�tiriyoruz
        {

            kusYonu = new Vector2(pozisyonlar[kacinciPozisyon].position.x - transform.position.x, pozisyonlar[kacinciPozisyon].position.y - transform.position.y);
            float angle = Mathf.Atan2(kusYonu.y, kusYonu.x) * Mathf.Rad2Deg;//radyan� dereceye �evirme i�lemi
            if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
            transform.rotation = Quaternion.Euler(0, 0, angle);//x ve y ye dokunmuyoruz z yi de�i�tiriyoruz
            transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, birdSpeed * Time.deltaTime);        //bir yerden bir yere MoveTowards fonk ile yap�yorduk.3 tane de�i�ke istiyor.1-hangi ba�lang�� de�er.2-target yani hedef. 3-ne kadar s�re                    

            anim.SetBool("ucsunMu", true);
            if (Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
            {
                PozisyonuDegistir();
                beklemeSayac = beklemeSuresi;
                
            }
        }
    }

    void PozisyonuDegistir()
    {
        kacinciPozisyon++;
        if (kacinciPozisyon >= pozisyonlar.Length) //dizi ta�d�ysa
        {
            kacinciPozisyon = 0;
        }
    }
}



