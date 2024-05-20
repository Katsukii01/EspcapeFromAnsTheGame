using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class stop_menu : MonoBehaviour
{
    bool stan = false;
    public GameObject menu;
     public GameObject camera;
     public GameObject[] objectsToDisable;

    public void Update()
    {
        // Jeśli naciśnięto klawisz F1, przełączamy się między kamerami
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!stan){
                 open();
            }else if(stan){
                close();
            }
           
        }
    }
   public void open(){
        menu.SetActive(true);
        stan=true;
        Cursor.visible = true;
        camera.SetActive(true);
        
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
   }

   public void close(){
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }

        camera.SetActive(false);

        menu.SetActive(false);
        stan=false;
        Cursor.visible = false;
   }

    public void nowa_gra(){
        Cursor.visible = false;
        FloorHellController.ending=0;
        _CharacterPick.postac=0;
        _Menu.level=2;
        SceneManager.LoadSceneAsync(0);
   }

   public  void wyjdz(){
        Application.Quit();
   }
}
