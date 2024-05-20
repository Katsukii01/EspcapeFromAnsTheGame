using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{   public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    float knockbackForce = 3000f;
    public GameObject zabierz;

    public AudioSource myFx;
    public AudioClip lostFx;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            zycieZabierz();
            zabierz.SetActive(true);
            StartCoroutine(WylaczPoCzasie(zabierz, 2f));
            myFx.PlayOneShot(lostFx);
            
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
            playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }

    void zycieZabierz(){
        if(_CharacterPick.postac==1){
            Transform ostatnieDziecko = l1.transform.GetChild(l1.transform.childCount - 1);
                    Destroy(ostatnieDziecko.gameObject);
        }else if(_CharacterPick.postac==2){
            Transform ostatnieDziecko = l2.transform.GetChild(l2.transform.childCount - 1);
                    Destroy(ostatnieDziecko.gameObject);
        }else if(_CharacterPick.postac==3){
            Transform ostatnieDziecko = l3.transform.GetChild(l3.transform.childCount - 1);
                    Destroy(ostatnieDziecko.gameObject);
        }
    }

    IEnumerator WylaczPoCzasie(GameObject obj, float czas)
    {
        yield return new WaitForSeconds(czas);
        obj.SetActive(false);
    } 
}
