using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class OrumcekController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar; //pozisuonlar dizisi olu�turduk
    [SerializeField]
    Slider orumcekSlider;
    public int maxSaglik;
    int gecerliSaglik;

    public float orumcekHizi;
    public float beklemeSuresi;
    float beklemeSayac;
    Animator anim;

    int kacinciPozisyon;
    Transform hedefPlayer;

    public float takipMesafesi = 5f;
    BoxCollider2D orumcekCollider;
    bool atakYapabilirmi;//belli bir s�re atak yapamams� laz�m

    Rigidbody2D rb;
    [SerializeField]
    GameObject iksirPrefab;
    private void Awake()
    {
        orumcekCollider=GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();//animator componentini verdik
        rb = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        orumcekSlider.maxValue = maxSaglik;
        SliderGuncelle();
        gecerliSaglik = maxSaglik;
        atakYapabilirmi = true;
        hedefPlayer = GameObject.Find("Player").transform;//player nesnesini bul onun transformunu hedefplayera e�itle
        foreach (Transform  pos  in pozisyonlar)
        {
            pos.parent = null;//dizinin i�indeki her elaman� bo�a ��kar
        }
    }
    private void Update()
    {
        if (!atakYapabilirmi)
            return;
        if (beklemeSayac > 0)
        {
            //�r�mcek verilen noktada duruyor
            beklemeSayac -= Time.deltaTime;
            anim.SetBool("hareketEtsinmi", false);
        }else
        {
            if(hedefPlayer.position.x>pozisyonlar[0].position.x && hedefPlayer.position.x < pozisyonlar[1].position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, hedefPlayer.position, orumcekHizi * Time.deltaTime);
                anim.SetBool("hareketEtsinmi", true);
                if (transform.position.x > hedefPlayer.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < hedefPlayer.position.x)
                {
                    transform.localScale = Vector3.one;

                }
            }
            else
            {
                anim.SetBool("hareketEtsinmi", true);
                if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < pozisyonlar[kacinciPozisyon].position.x)
                {
                    transform.localScale = Vector3.one;

                }
                transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, orumcekHizi * Time.deltaTime);
                if (Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
                {
                    beklemeSayac = beklemeSuresi;
                    PozisyonuDegistir();
                }
            }
           
        }
    }
    void PozisyonuDegistir()
    {
        kacinciPozisyon++;
        if (kacinciPozisyon >= pozisyonlar.Length)
        {
            kacinciPozisyon =0;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (orumcekCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer"))&& atakYapabilirmi) //playerlayer mask bir �eye �arpt�ysa
        {
            atakYapabilirmi = false;
            anim.SetTrigger("atakYapti");
            other.GetComponent<PlayerHareketController>().GeriTepki();
            other.GetComponent<PlayerHealthController>().CaniAzalt();
            StartCoroutine(YenidenAtakYapsin());
        }

    }
    IEnumerator YenidenAtakYapsin()
    {
        yield return new WaitForSeconds(1f);//1 saniye ataka ap�lmas�na izin verilmedi
        if(gecerliSaglik>0) 
        atakYapabilirmi = true;


    }
    //enumator fonksiyon belli bir ms�re sonra �al��an fonksiyondur
    public  IEnumerator GeriTepki() 
    {
        atakYapabilirmi = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.1f);//belli bir s�re ge�mesini sa�lad�k
        gecerliSaglik--;
        SliderGuncelle();
        if(gecerliSaglik<=0)
        {
            atakYapabilirmi = false;
            gecerliSaglik = 0;
            Instantiate(iksirPrefab, transform.position, Quaternion.identity);
            anim.SetTrigger("canVerdi");
            orumcekCollider.enabled = false;//birdaha k�l��la darbe vurulmamas� gerek
            orumcekSlider.gameObject.SetActive(false);
            Destroy(gameObject, 2f);
        }
        else
        {

            for (int i = 0; i < 5; i++)
            {                                                          //geriye do�ru gitmesi i�in
                rb.velocity = new Vector2(-transform.localScale.x + i, rb.velocity.y);
                yield return new WaitForSeconds(0.05f);


            }
            anim.SetBool("hareketEtsinmi", false);

            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            atakYapabilirmi = true;


        }

    }
    void SliderGuncelle()
    {
        orumcekSlider.value = gecerliSaglik;
    }

}
