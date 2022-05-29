using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAraclarController : MonoBehaviour
{

    [SerializeField]
    bool kilicMi, mizrakMi;

    private void OnTriggerEnter2D(Collider2D collision) //istrigger fonksiyonu tetiklenmeli
    {
        if (collision.CompareTag("Player"))
        {
            if (collision != null && kilicMi)
            {
                collision.GetComponent<PlayerHareketController>().NormaliKapatKiliciAc();//fonka eriþip tetikletttik

            }

            if(collision !=null && mizrakMi)
            {
                collision.GetComponent<PlayerHareketController>().HerSeyiKapatMizrakAc();
            }
            Destroy(gameObject);
        
        }
    }
    
}
