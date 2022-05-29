using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToplamaManager : MonoBehaviour
{
    [SerializeField]
    bool coinmi,iksirmi;
    bool toplandimi; //ayný þeye tekrardadn deðdiðimizde herhangi bir sorun çýkmasýn

    [SerializeField]
    GameObject patlamaEfekti;
    private void OnTriggerEnter2D(Collider2D collision) //trigger özelliði açýk bir nesneye dokunduðumuz an çalýþýr
    {
        if (collision.CompareTag("Player") && !toplandimi) //dokunduðumuz nesnenin tagi player ise
        {
            toplandimi = true;

            GameManager.instance.toplananCoinAdet++;
            UIManager.instance.CoinGuncelle();

            Destroy(gameObject);
            Instantiate(patlamaEfekti,transform.position, Quaternion.identity); 
        }
        if (iksirmi)
        {
            toplandimi = true;
            PlayerHealthController.instance.CaniArtir();
            Destroy(gameObject);
            Instantiate(patlamaEfekti, transform.position, Quaternion.identity);

        }
        {

        }
        
    }



}
