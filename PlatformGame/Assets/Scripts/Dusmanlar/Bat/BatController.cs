using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    [SerializeField]
    float takipMesafesi = 8f;
    [SerializeField]
    float batHizi;
    [SerializeField]
    Transform hedefPlayer;
    Animator anim;

    Rigidbody2D rb;

    BoxCollider2D batCollider;

    public float atakYapmaSuresi;
    float atakYapmaSayaci;
    float mesafe;
    Vector2 hareketYonu;
    public int maxSaglik;
    int gecerliSaglik;

    [SerializeField]
    GameObject iksirPrefab;
    private void Awake()
    {
        hedefPlayer = GameObject.Find("Player").transform;//ismi player olan nesneyi bul ve bunun tanrsforemunu yakala hedefplayer transformuna eþitle
        anim= GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        batCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        gecerliSaglik = maxSaglik;
    }
    private void OnDrawGizmosSelected()//takip mesafsini ölçmek için
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,takipMesafesi);//takip mesafesi yarýcapýnda bir daire oluþturduk
    }
    private void Update()
    {
        if (atakYapmaSayaci < 0)
        {
            if(hedefPlayer && gecerliSaglik>0 && !PlayerHareketController.instance.playerCanverdimi)
            {
                mesafe = Vector2.Distance(transform.position, hedefPlayer.position); //yarasanýn psziyonu ile hdef playerin pozis arasýndaki mesafeyi ölç
                if (mesafe < takipMesafesi)
                {
                    anim.SetTrigger("ucusaGecti");
                    hareketYonu = hedefPlayer.position - transform.position;
                    if (transform.position.x > hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (transform.position.x < hedefPlayer.position.x)
                    {
                        transform.localScale = Vector3.one;
                    }
                    rb.velocity = hareketYonu * batHizi;

                }
            }
        }
           
        else
        {
            atakYapmaSayaci -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (batCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player"))
            {
                rb.velocity = Vector2.zero;
                atakYapmaSayaci = atakYapmaSuresi;
                anim.SetTrigger("atakYapti");
                other.GetComponent<PlayerHareketController>().GeriTepki();
                other.GetComponent<PlayerHealthController>().CaniAzalt();

            }
        }

    }
    public void CaniAzalt()
    {
        gecerliSaglik--;
        atakYapmaSayaci = atakYapmaSuresi;
        rb.velocity = Vector2.zero;
        if (gecerliSaglik <= 0)
        {
            gecerliSaglik = 0;
            batCollider.enabled=false;
            Instantiate(iksirPrefab, transform.position, Quaternion.identity);
            anim.SetTrigger("canVerdi");
            Destroy(gameObject, 3f);

        }

    }
       


}
