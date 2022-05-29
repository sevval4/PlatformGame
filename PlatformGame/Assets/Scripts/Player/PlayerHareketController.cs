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

    public LayerMask zeminMaske;    //sadece zemin oldýuðunu anladýðýnda çarpma gerçekleþsin

    public float hareketHizi;
    public float ziplamaGucu;
    bool zemindemi; //zemindeyse ziplamalý 
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
        playerCanverdimi = false; //baþta caný var bu yüzden bu ifade false
        atakSuresi = 0f;
        atakSogumaSuresi = 0.3f;

        kilicVurusBoxObje.SetActive(false); 
    }

    public void Update()
    {
        if (playerCanverdimi)
            return;//karakterin caný yoksa geriye dön yani update fonksiyonundan çýk
        
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

        if (normalPlayer.activeSelf)//sahnede normal player aktivse bu çalýþmalý.
        {

            normalAnim.SetBool("zemindemi", zemindemi); //zeminde olup olmadýðýný kontrol ediyoruz
            normalAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x)); //geriye giderken animasyonun olmasý için math fonk kullanýldý
        }    
        if (kilicPlayer.activeSelf)//sahnede  kilic player aktivse kiliç animasyonu  çalýþmalý.
        {

            kilicAnim.SetBool("zemindemi", zemindemi); //zeminde olup olmadýðýný kontrol ediyoruz
            kilicAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x)); //geriye giderken animasyonun olmasý için math fonk kullanýldý
         
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
        float h = Input.GetAxis("Horizontal"); //h=horizontal, sað ve sol tuþlara bastýðýmýzda +1 ile -1 arasýnda deðerler elde etmemizi saðladýk
        rb.velocity = new Vector2(h * hareketHizi, rb.velocity.y); //bulmuþ olduðumuz bu h deðeri ile hareket hýzýný çarpýyoruz böylece rigidbody nin velocitysinin x teki hýzýný ayarlamýþ olduk, y deki hýzýna dokunmuyoruz rb.velocity.y diyerek ayný kalmasýný saðladýk
       
    }

    void YonuDegistir()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // x deðeri -1 olacak
            yonSagdami = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one; //one=(1,1,1) deðiþiklik olmayacak
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
    void Zipla() //ilk olarak bir ýþýn göndermemiz gerek
    {
        zemindemi=(Physics2D.OverlapCircle(zeminKontrolNoktasi.position, .2f, zeminMaske)); //Overlapcircle fonksiyonuyla çember þeklinde bir ýþýn göndeririz. (ýþýný nerden göndermemiz gerekiyor,ne kadarlýk yarýçap,hangi maske)

        if(Input.GetButtonDown("Jump") && (zemindemi || ikinciKezZiplasinmi)) //space tuþuna basýldýðýnda ve zemþndemþ true olduðudna ya da ikizkezzýplasýnmi true olduðunda çalýþýr
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
        rb.velocity = Vector2.zero;//hýzýný 0'a çekiyoruz
        playerCanverdimi = true;

        if (normalPlayer.activeSelf)//sahnede normal player aktivse bu çalýþmalý.
        {

            normalAnim.SetTrigger("canVerdi");//animatörü tetiklettik
        }
        if (kilicPlayer.activeSelf)//sahnede  kilic player aktivse kiliç animasyonu  çalýþmalý.
        {
            kilicAnim.SetTrigger("canVerdi");//animatörü tetiklettik
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
        GetComponentInChildren<SpriteRenderer>().enabled = false;//alt nesnelerdeki componentler içerisinden sprite rendereri bul ve enabled false yap. direkt playeri yok edersem sahne yenilenmez o yüzden sprite rendereri kapattýk
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//aktif olan sahnenin index numarasýný al ve buraya ata. Böylece sahnemizi yeniden yüklemiþ oluruz.

    }

    public void NormaliKapatKiliciAc()
    {
        normalPlayer.SetActive(false);//normal playeri kapattýk 
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
