using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class So : MonoBehaviour

{   public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    public GameObject zabierz;
    public Animator animator;
    public GameObject start;
    public GameObject grreenlight;
    public GameObject redlight;
    bool s = true;
    public Animator a1;
    public Animator a2;
    public Animator a3;

    public AudioSource myFx;
    public AudioSource myFx2;
    public AudioClip lostFx;
    public AudioClip winFx;
    public AudioClip gameFx;

    public bool sprawdzam = false;
    public GameObject to;
    public GameObject panel1;
    public GameObject panel2;
    public TextMeshProUGUI textMeshPro;
    private bool isPlayerInside = false;
    private bool gra= false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(gra==false){
                panel1.SetActive(true);
                isPlayerInside = true;
            }else if(gra==true){
                panel2.SetActive(true);
                isPlayerInside = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(gra==false){
                panel1.SetActive(false);
                isPlayerInside = false;
            }else if(gra==true){
                panel2.SetActive(false);
                isPlayerInside = false;
            }
        }
    }


    void ChangeLookingStateRandomly()
    {
        bool currentState = animator.GetBool("looking");
        animator.SetBool("looking", !currentState);
        Invoke("ChangeToOppositeState", 2.1f);
    }

    void ChangeToOppositeState()
    {  
       if(sprawdzam==false){
         grreenlight.SetActive(false);
         redlight.SetActive(true);
         myFx2.Stop();
        }else{
         grreenlight.SetActive(true);
         redlight.SetActive(false);
         myFx2.PlayOneShot(gameFx);
      }
      sprawdzam = !sprawdzam;
    }

    void LostGame(){
            CancelInvoke("ChangeLookingStateRandomly");
            CancelInvoke("ChangeToOppositeState");
            gra=false;
            myFx2.Stop();
            myFx.PlayOneShot(lostFx);
            zycieZabierz();
            s=true;
            animator.SetBool("looking", false);
            sprawdzam=false;
            grreenlight.SetActive(false);
            redlight.SetActive(false);
            zabierz.SetActive(true);
            StartCoroutine(WylaczPoCzasie(zabierz, 2f));
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

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            if(gra==false){
                gra=true;
                isPlayerInside = false;
                panel1.SetActive(false);

                if(_CharacterPick.postac==1){
                      p1.transform.position = start.transform.position;
                }else if(_CharacterPick.postac==2){
                     p2.transform.position = start.transform.position;
                }else if(_CharacterPick.postac==3){
                     p3.transform.position = start.transform.position;
                }

            }else if(gra==true){
                CancelInvoke("ChangeLookingStateRandomly");
                CancelInvoke("ChangeToOppositeState");
                myFx2.Stop();
                myFx.PlayOneShot(winFx);
                gra=false;
                textMeshPro.color = Color.green;
                textMeshPro.text = "3";
                panel2.SetActive(false);
                isPlayerInside = false;
                to.SetActive(false);
            }
        }


        if(gra==true){
            if(s==true){
                grreenlight.SetActive(true);
                myFx2.PlayOneShot(gameFx);
                s=false;
                InvokeRepeating("ChangeLookingStateRandomly", Random.Range(3f, 5f), Random.Range(3f, 5f));
            }

            if(sprawdzam==true){
                if(_CharacterPick.postac==1){
                       bool stand = a1.GetBool("Stand");
                        if(!stand){
                            LostGame();
                        }
                }else if(_CharacterPick.postac==2){
                        bool stand = a2.GetBool("Stand");
                        if(!stand){
                            LostGame();
                        }
                }else if(_CharacterPick.postac==3){
                        bool stand = a3.GetBool("Stand");
                        if(!stand){
                            LostGame();
                        }
                }
            }
        }
    }

}
