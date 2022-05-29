using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoaController : MonoBehaviour
{
    [SerializeField]
    float boaYurumeHizi, boaKosmHizi;
    Animator anim;
    Rigidbody2D rb;

    [SerializeField]
    float gorusMesafesi=8f;
    [SerializeField]
    BoxCollider2D boaCollider;
    public bool oldumu;
    public LayerMask playerLayer;

    [SerializeField]
    GameObject kanamaEfekti;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        oldumu = false;

    }
    private void Update()
    {
        if (oldumu)
            return;//boa öldüyse diðer fonk çalýþtýrma 

        RaycastHit2D hit = Physics2D.Raycast(transform.position,transform.TransformDirection(Vector2.left),gorusMesafesi,playerLayer);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * gorusMesafesi, Color.green);//ýþýnýn nereye kadar gittiðini gösteren kod
        transform.localScale=new Vector3(-1,1,1); //boanýn yönünü deðiþtirme

        if (hit.collider)//gönderilen ýþýn bir þeye çarptýysa
        {
            if(hit.collider.CompareTag("Player"))//çarptýðý nesne player nesnesiyse
            {
                rb.velocity = new Vector2(-boaKosmHizi, rb.velocity.y);
                anim.SetBool("kossunMu", true);
            }
            else//eger herhangi bir seye çarpmýyorsa
            {
                rb.velocity = new Vector2(-boaYurumeHizi,rb.velocity.y);
                anim.SetBool("kossunMu", false);//yuruyuse devam etsin
            }
        }

    }
    public void BoaOldu()
    {
      
            oldumu = true;
            anim.SetTrigger("canVerdi");
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;//kinematik özelliði kapattýk
            Instantiate(kanamaEfekti, transform.position, transform.rotation);

            //BOX COLLÝDERLARI KAPATMAK ÝÇÝN
            foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
            {
                box.enabled = false;//bütün colliderlarý kapat
            }
            Destroy(gameObject, 3f);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (boaCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player"))
            {
                anim.SetTrigger("atakYapti");
                other.GetComponent<PlayerHareketController>().GeriTepki();
                other.GetComponent<PlayerHealthController>().CaniAzalt();

            }
        }
    }


}
