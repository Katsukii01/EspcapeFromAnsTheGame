using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Sieci : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public GameObject camera;

    public GameObject l1;
    public GameObject l2;
    public GameObject l3;

    public AudioSource myFx;
    public AudioClip lostFx;
    public AudioClip winFx;

    public GameObject to;
    public GameObject panel;
    public GameObject zabierz;

    public GameObject game;
    public TMP_InputField inputField;

    public TextMeshProUGUI textMeshPro;
    private bool isPlayerInside = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.SetActive(true);
            isPlayerInside = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            panel.SetActive(false);
            isPlayerInside = false;
        }
    }

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            isPlayerInside = false; 
            panel.SetActive(false);
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
            camera.SetActive(true);
            game.SetActive(true);
            Cursor.visible = true;
        }
    }

   public void endgame(){
        Cursor.visible = false;
        camera.SetActive(false);
        game.SetActive(false);
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }

        string value = inputField.text.Trim().ToLower();
        if(value=="pentagramu"){
            win();
        }else{
            lost();
        }
    }

    void win(){ 
        textMeshPro.color = Color.green;
        textMeshPro.text = "3";
        panel.SetActive(false);
        to.SetActive(false);
        myFx.PlayOneShot(winFx);
    }

    void lost(){
         zycieZabierz();
        zabierz.SetActive(true);
        StartCoroutine(WylaczPoCzasie(zabierz, 2f));
         myFx.PlayOneShot(lostFx);
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
