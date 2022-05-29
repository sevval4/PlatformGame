using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHareketController : MonoBehaviour
{
    public static PlayerHareketController instance;
    Rigidbody2D rb;

    [SerializeField]
    GameObject normalPlayer, kilicPlayer,mizrakPlayer;

    [SerializeField]
    Transform zeminKontrolNoktasi;

    [SerializeField]
    Animator normalAnim,kilicAnim,mizrakAnim;

    [SerializeField]
    SpriteRenderer normalSprite,kilicSprite,mizrakSprite;

    [SerializeField]
    GameObject kilicVurusBoxObje;

    public LayerMask zeminMaske;    //sadece zemin old�u�unu anlad���nda �arpma ger�ekle�sin

    public float hareketHizi;
    public float ziplamaGucu;
    bool zemindemi; //zemindeyse ziplamal� 
    bool ikinciKezZiplasinmi;

    [SerializeField]
    float geriTepkiSuresi, geriTepkiGucu;
    float geriTepkiSayaci;

    bool yonSagdami;
    public bool playerCanverdimi;
    bool kiliciVurdumu;
    public float atakSuresi;
    public float atakSogumaSuresi;
    [SerializeField]
    GameObject atilacakMizrak;
    [SerializeField]
    Transform mizrakCikisNoktasi;

    public void Awake()
    {
        instance = this;
        kiliciVurdumu = false;
        rb = GetComponent<Rigidbody2D>();
        playerCanverdimi = false; //ba�ta can� var bu y�zden bu ifade false
        atakSuresi = 0f;
        atakSogumaSuresi = 0.3f;

        kilicVurusBoxObje.SetActive(false); 
    }

    public void Update()
    {
        if (playerCanverdimi)
            return;//karakterin can� yoksa geriye d�n yani update fonksiyonundan ��k
        
        if (geriTepkiSayaci <= 0)
        {
            HareketEt();
            Zipla();
            YonuDegistir();
            if (normalPlayer.activeSelf)
            {
                normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 1f);
            }
            if (kilicPlayer.activeSelf)
            {
                kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, 1f);
            }

            if (mizrakPlayer.activeSelf)//mizrak player aktivse
            {
                mizrakSprite.color = new Color(mizrakSprite.color.r,mizrakSprite.color.g,mizrakSprite.color.b,1f);
            }

            if(atakSuresi >0)
            {
                atakSuresi -= Time.deltaTime;
            }
            if(atakSuresi < 0)
            {
                atakSuresi = 0;
            }

            if(Input.GetMouseButtonDown(0)  && kilicPlayer.activeSelf)
            {
               if(atakSuresi == 0)
                {
                    kiliciVurdumu = true;
                    atakSuresi = atakSogumaSuresi;
                }
                kilicVurusBoxObje.SetActive(true); 
            }
            else
            {
                kiliciVurdumu=false;    
            }
            if(Input.GetKeyDown(KeyCode.X) && mizrakPlayer.activeSelf)
            {
                mizrakAnim.SetTrigger("mizrakAtti");
                Invoke("MizragiFirlat", .15f);
            }
        }
        else
        {
            geriTepkiSayaci -= Time.deltaTime;

            if (yonSagdami)
            {
                rb.velocity = new Vector2(-geriTepkiGucu,rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(geriTepkiSayaci, rb.velocity.y);
            }
        }

        if (normalPlayer.activeSelf)//sahnede normal player aktivse bu �al��mal�.
        {

            normalAnim.SetBool("zemindemi", zemindemi); //zeminde olup olmad���n� kontrol ediyoruz
            normalAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x)); //geriye giderken animasyonun olmas� i�in math fonk kullan�ld�
        }    
        if (kilicPlayer.activeSelf)//sahnede  kilic player aktivse kili� animasyonu  �al��mal�.
        {

            kilicAnim.SetBool("zemindemi", zemindemi); //zeminde olup olmad���n� kontrol ediyoruz
            kilicAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x)); //geriye giderken animasyonun olmas� i�in math fonk kullan�ld�
         
        }
        if (mizrakPlayer.activeSelf)
        {
            mizrakAnim.SetBool("zemindemi", zemindemi);
            mizrakAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
        }


        if (kiliciVurdumu && kilicPlayer.activeSelf)
        {
            kilicAnim.SetTrigger("kiliciVurdu");
        }

    }
    void MizragiFirlat()
    {
        GameObject mizrak = Instantiate(atilacakMizrak, mizrakCikisNoktasi.position, mizrakCikisNoktasi.rotation);
        mizrak.transform.localScale = transform.localScale;
        mizrak.GetComponent<Rigidbody2D>().velocity = mizrakCikisNoktasi.right * transform.localScale.x * 7f;
        Invoke("HerSeyiKapatNormaliAc", .1f);

    }
    void HareketEt()
    {
        float h = Input.GetAxis("Horizontal"); //h=horizontal, sa� ve sol tu�lara bast���m�zda +1 ile -1 aras�nda de�erler elde etmemizi sa�lad�k
        rb.velocity = new Vector2(h * hareketHizi, rb.velocity.y); //bulmu� oldu�umuz bu h de�eri ile hareket h�z�n� �arp�yoruz b�ylece rigidbody nin velocitysinin x teki h�z�n� ayarlam�� olduk, y deki h�z�na dokunmuyoruz rb.velocity.y diyerek ayn� kalmas�n� sa�lad�k
       
    }

    void YonuDegistir()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // x de�eri -1 olacak
            yonSagdami = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one; //one=(1,1,1) de�i�iklik olmayacak
            yonSagdami=true;    
        }
    }
    public void GeriTepki()
    {
        geriTepkiSayaci = geriTepkiSuresi;

        if (normalPlayer.activeSelf)
        {
            normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, .5f);
        }
        if (kilicPlayer.activeSelf)
        {
            kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, .5f);
        }
        if (mizrakPlayer.activeSelf)
        {

            mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite.color.b, .5f);
        }
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    void Zipla() //ilk olarak bir ���n g�ndermemiz gerek
    {
        zemindemi=(Physics2D.OverlapCircle(zeminKontrolNoktasi.position, .2f, zeminMaske)); //Overlapcircle fonksiyonuyla �ember �eklinde bir ���n g�ndeririz. (���n� nerden g�ndermemiz gerekiyor,ne kadarl�k yar��ap,hangi maske)

        if(Input.GetButtonDown("Jump") && (zemindemi || ikinciKezZiplasinmi)) //space tu�una bas�ld���nda ve zem�ndem� true oldu�udna ya da ikizkezz�plas�nmi true oldu�unda �al���r
        {
            if (zemindemi)
            {
                ikinciKezZiplasinmi = true;
            }
            else
            {
                ikinciKezZiplasinmi = false;
            }
            rb.velocity = new Vector2(rb.velocity.x,ziplamaGucu);


        }

        
    }

    public void PlayerCanVerdi()
    {
        rb.velocity = Vector2.zero;//h�z�n� 0'a �ekiyoruz
        playerCanverdimi = true;

        if (normalPlayer.activeSelf)//sahnede normal player aktivse bu �al��mal�.
        {

            normalAnim.SetTrigger("canVerdi");//animat�r� tetiklettik
        }
        if (kilicPlayer.activeSelf)//sahnede  kilic player aktivse kili� animasyonu  �al��mal�.
        {
            kilicAnim.SetTrigger("canVerdi");//animat�r� tetiklettik
        }
        if (mizrakPlayer.activeSelf)
        {

            mizrakAnim.SetTrigger("caniniVerdi");
        }

        StartCoroutine(PlayerYokEtSahneYenile());
    }

    IEnumerator PlayerYokEtSahneYenile()
    {
        yield return new WaitForSeconds(2f);//2 saniye karakter beklesin
        GetComponentInChildren<SpriteRenderer>().enabled = false;//alt nesnelerdeki componentler i�erisinden sprite rendereri bul ve enabled false yap. direkt playeri yok edersem sahne yenilenmez o y�zden sprite rendereri kapatt�k
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//aktif olan sahnenin index numaras�n� al ve buraya ata. B�ylece sahnemizi yeniden y�klemi� oluruz.

    }

    public void NormaliKapatKiliciAc()
    {
        normalPlayer.SetActive(false);//normal playeri kapatt�k 
        kilicPlayer.SetActive(true);
        mizrakPlayer.SetActive(false);
    }

    public void HerSeyiKapatMizrakAc()
    {
        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(true);
    }
    public void HerSeyiKapatNormaliAc()
    {
        normalPlayer.SetActive(true);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
    }
}
