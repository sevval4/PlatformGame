using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtakController : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D kilicVurusBox;

    [SerializeField]
    GameObject parlamaEfekti;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))//kilicvurusbox dusmanlayer katmanýndan herhangi bi nesneye deðdiyse
        {
            if (other.CompareTag("Orumcek"))
            {
                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                    StartCoroutine(other.GetComponent<OrumcekController>().GeriTepki());
                }

            }
              
            

        }
        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))//kilicvurusbox dusmanlayer katmanýndan herhangi bi nesneye deðdiyse
        {
            if (other.CompareTag("Bat"))
            {


                if (parlamaEfekti)
                {
                    Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                    other.GetComponent<BatController>().CaniAzalt();
                }
            }

        }


    }

}
