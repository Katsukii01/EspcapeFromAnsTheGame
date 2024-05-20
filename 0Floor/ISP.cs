using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ISP : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public GameObject camera;

    public Animator animator;
    public Button button;

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
    int los = 2; 

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

    public void losuj(){
        los = Random.Range(1, 5);
        animator.SetBool("rest", false);
        button.interactable = false;
        if(los==1 || los==2 ){
            animator.SetBool("2", true);
        }else if(los==5 || los==4){
            animator.SetBool("5", true);
        }else{
            animator.SetBool("3", true);
        }
        Invoke("endgame", 11f);  
    }

   public void endgame(){
        Cursor.visible = false;
        camera.SetActive(false);
        game.SetActive(false);
        button.interactable = true;
        animator.SetBool("2", false);
        animator.SetBool("3", false);
        animator.SetBool("5", false);
        animator.SetBool("rest", true);

        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }

        if(los==1 || los==2){
           lost(); 
        }else{
            win();
        }
    }

    void win(){ 
        textMeshPro.color = Color.green;
        textMeshPro.text = "4";
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
