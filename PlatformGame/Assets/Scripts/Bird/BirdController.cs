using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;          //Dýþarýdaki pozisyonlarý almamýz gerek ve transform bir dizi olacaðý içn pozisyonlar isminde bir dizi oluþturduk

    public float birdSpeed;
    public float beklemeSuresi;//kuþ bir noktaya konduðu zaman bekleyecek.
    float beklemeSayac;//bekleme sürecini geriye saydýran bir sayaç oluþturduk.
    int kacinciPozisyon;
    Animator anim;

    Vector2 kusYonu;
    private void Awake() //ilk olarak pozisyonlarýn hepsini dýþarýya atmalýyýzki kuþu yalnýz hareket ettirebilelim
    {

        anim = GetComponent<Animator>();
        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;//dizideki her elemanýn parentýný n
        }
        
    }
    private void Start()
    {
        kacinciPozisyon = 0;
        transform.position = pozisyonlar[kacinciPozisyon].position; //kuþun ilk baþlayacaðý pozisyonu ayarladýk
        
    }

    private void Update()
    {
        if (beklemeSayac > 0)
        {
            beklemeSayac -= Time.deltaTime;
            anim.SetBool("ucsunMu", false);
        }
        else//ilk hareketimizi gerçekleþtiriyoruz
        {

            kusYonu = new Vector2(pozisyonlar[kacinciPozisyon].position.x - transform.position.x, pozisyonlar[kacinciPozisyon].position.y - transform.position.y);
            float angle = Mathf.Atan2(kusYonu.y, kusYonu.x) * Mathf.Rad2Deg;//radyaný dereceye çevirme iþlemi
            if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
            transform.rotation = Quaternion.Euler(0, 0, angle);//x ve y ye dokunmuyoruz z yi deðiþtiriyoruz
            transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, birdSpeed * Time.deltaTime);        //bir yerden bir yere MoveTowards fonk ile yapýyorduk.3 tane deðiþke istiyor.1-hangi baþlangýç deðer.2-target yani hedef. 3-ne kadar süre                    

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
        if (kacinciPozisyon >= pozisyonlar.Length) //dizi taþdýysa
        {
            kacinciPozisyon = 0;
        }
    }
}



