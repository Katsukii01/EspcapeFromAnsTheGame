using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Analiza : MonoBehaviour
{
    public GameObject[] objectsToDisable;
    public GameObject camera;

    public GameObject dead;
    public GameObject panel;

    public GameObject game;
    public TMP_InputField inputField;

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
        string value = inputField.text.Trim().ToLower();
        if(value=="lnx" || value=="1*lnx"){
            win();
        }else{
            lost();
        }
    }

    void win(){ 
         _Menu.levels_complete[_Menu.level-2] = true;
         _Menu.SaveProgress();
        _Menu.level=_Menu.level+1;
        SceneManager.LoadSceneAsync(_Menu.level);  
    }

    void lost(){
         dead.SetActive(true);
    }
}
