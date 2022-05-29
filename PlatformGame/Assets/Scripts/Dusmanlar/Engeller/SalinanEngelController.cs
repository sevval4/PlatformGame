using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalinanEngelController : MonoBehaviour
{
    [SerializeField]
    float donmeHizi=200f;
    
    float zAngle;

    [SerializeField]
    float minZangle = -75f;
    [SerializeField]
    float maxZangle = 75f;

    private void Start()
    {
        if (Random.Range(0, 2) > 0)
            donmeHizi *= -1;
        
        
    }
    private void Update()
    {
        zAngle += Time.deltaTime * donmeHizi;
        transform.rotation = Quaternion.AngleAxis(zAngle, Vector3.forward);   //rotasyonla ilgili iþlemler Queternion sýnýfýna girer
        if (zAngle < minZangle)
        {
            donmeHizi = Mathf.Abs(donmeHizi);//pozitif yöne dönsün
        }
        if (zAngle > maxZangle)
        {
            donmeHizi = -Mathf.Abs(donmeHizi);//negatif yöne doðru dönsün
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<EdgeCollider2D>().IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player") && !other.GetComponent<PlayerHareketController>(). playerCanverdimi)
            {
                other.GetComponent<PlayerHareketController>().GeriTepki();
                other.GetComponent<PlayerHealthController>().CaniAzalt();

            }
        }
    }

}
