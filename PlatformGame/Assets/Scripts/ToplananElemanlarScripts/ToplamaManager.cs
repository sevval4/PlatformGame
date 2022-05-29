using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToplamaManager : MonoBehaviour
{
    [SerializeField]
    bool coinmi,iksirmi;
    bool toplandimi; //ayn� �eye tekrardadn de�di�imizde herhangi bir sorun ��kmas�n

    [SerializeField]
    GameObject patlamaEfekti;
    private void OnTriggerEnter2D(Collider2D collision) //trigger �zelli�i a��k bir nesneye dokundu�umuz an �al���r
    {
        if (collision.CompareTag("Player") && !toplandimi) //dokundu�umuz nesnenin tagi player ise
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
