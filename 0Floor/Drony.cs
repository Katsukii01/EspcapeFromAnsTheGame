using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Drony : MonoBehaviour
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

    public GameObject plane;

    public GameObject obstacleTemplate;
    public Transform obstaclesParent;

    public GameObject game;


    public TextMeshProUGUI textMeshPro;
    private bool isPlayerInside = false;
    private bool gra = false;

   
    public RectTransform planeRectTransform;

    private float fallSpeed = 1.2f; // Speed at which the plane falls
    private float planeVerticalVelocity = 0f;

    private int passedObstacles = 0;
    private int targetObstacles = 10;

    private List<GameObject> activeObstacles = new List<GameObject>();

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
        if(gra==false){
            if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
            {
                isPlayerInside = false; 
                panel.SetActive(false);
                foreach (GameObject obj in objectsToDisable)
                {
                    obj.SetActive(false);
                }
                gra = true;
                camera.SetActive(true);
                game.SetActive(true);
                InvokeRepeating("GenerateObstaclePair", 1f, 2.5f);
                passedObstacles = 0;
            }
        }else
        {
            if(passedObstacles<10){
                HandleInput();
                ApplyPhysics();
                CheckBounds();
                CheckCollisions();
            }else{
                win();
            }
           
        }
    }
    bool canJump = true; // Dodaj zmienną do śledzenia, czy samolot może skakać

        void HandleInput()
            {
                if (canJump && Input.GetKeyDown(KeyCode.Space)) // Sprawdź, czy samolot może skakać
                {
                    planeVerticalVelocity = 0.8f;
                    canJump = false; // Ustaw wartość na false po skoku
                }
            }

            void ApplyPhysics()
            {
                if (planeVerticalVelocity <= 0)
                {
                    canJump = true; // Jeśli samolot opada, ustaw wartość na true, aby umożliwić kolejny skok
                }
                planeVerticalVelocity -= fallSpeed * 0.00167f;
                planeRectTransform.anchoredPosition += new Vector2(0, planeVerticalVelocity);
            }

    void CheckBounds()
    {
        if (planeRectTransform.anchoredPosition.y < -500 || planeRectTransform.anchoredPosition.y > 500)
        {
            lost();
            planeRectTransform.anchoredPosition = new Vector2(planeRectTransform.anchoredPosition.x, 0);
            planeVerticalVelocity = 0;
        }
    }


void GenerateObstaclePair()
{
  // Losowanie wysokości przeszkód
    float minHeight = 200f; // Minimalna wysokość jednej przeszkody
    float maxHeight = 350f; // Maksymalna wysokość jednej przeszkody

    // Suma wysokości przeszkód musi być większa lub równa 600f i mniejsza lub równa 800f
    float totalHeight = Random.Range(500f, 700f);

    // Losowanie wysokości pierwszej przeszkody
    float height1 = Random.Range(minHeight, Mathf.Min(maxHeight, totalHeight));

    // Wysokość drugiej przeszkody to reszta wysokości do sumy
    float height2 = totalHeight - height1;




    float xPos = 800f; // Start generating from the right side of the screen

    // Set the parent of the obstacles to be the same as the parent of the plane
    Transform parentTransform = plane.transform.parent;

    // Generate bottom obstacle
    GameObject bottomObstacle = Instantiate(obstacleTemplate, parentTransform);
    RectTransform bottomObstacleRectTransform = bottomObstacle.GetComponent<RectTransform>();
    bottomObstacleRectTransform.anchorMin = new Vector2(1f, 0f); // Przyczep dolną krawędź do dolnej krawędzi okna
    bottomObstacleRectTransform.anchorMax = new Vector2(1f, 0f);
    bottomObstacleRectTransform.pivot = new Vector2(1f, 0f);
    bottomObstacleRectTransform.anchoredPosition = new Vector2(xPos, 0f); // Dolna krawędź na dole ekranu
    bottomObstacleRectTransform.sizeDelta = new Vector2(bottomObstacleRectTransform.sizeDelta.x, height1);
    bottomObstacleRectTransform.rotation = Quaternion.identity; // No rotation for the bottom obstacle

    // Generate top obstacle
    GameObject topObstacle = Instantiate(obstacleTemplate, parentTransform);
    RectTransform topObstacleRectTransform = topObstacle.GetComponent<RectTransform>();
    topObstacleRectTransform.anchorMin = new Vector2(1f, 1f); // Przyczep górną krawędź do górnej krawędzi okna
    topObstacleRectTransform.anchorMax = new Vector2(1f, 1f);
    topObstacleRectTransform.pivot = new Vector2(1f, 1f);
    topObstacleRectTransform.sizeDelta = new Vector2(topObstacleRectTransform.sizeDelta.x, height2 );
    //topObstacleRectTransform.rotation = Quaternion.Euler(0f, 0f, 180f); // Rotate the top obstacle by 180 degrees
    // Przesuń górną przeszkodę w dół o jej pełną wysokość
    topObstacleRectTransform.anchoredPosition = new Vector2(xPos, -height2/64f); 

    activeObstacles.Add(topObstacle);
    activeObstacles.Add(bottomObstacle);

    // Move obstacles towards the left (negative x direction) and destroy them when they reach the end
    LeanTween.moveX(topObstacle, -800f, 4f).setEaseInOutQuad().setOnComplete(() => 
    {
        activeObstacles.Remove(topObstacle);
        Destroy(topObstacle);
        passedObstacles++;
    });

    LeanTween.moveX(bottomObstacle, -800f, 4f).setEaseInOutQuad().setOnComplete(() => 
    {
        activeObstacles.Remove(bottomObstacle);
        Destroy(bottomObstacle);
    });
}





 void CheckCollisions()
{
    foreach (GameObject obstacle in activeObstacles)
    {
        RectTransform obstacleRectTransform = obstacle.GetComponent<RectTransform>();
        if (obstacleRectTransform != null && planeRectTransform != null)
        {
            Vector3[] corners = new Vector3[4];
            planeRectTransform.GetWorldCorners(corners);
            Rect planeRect = new Rect(corners[0], corners[2] - corners[0]);

            if (RectTransformUtility.RectangleContainsScreenPoint(obstacleRectTransform, planeRect.center))
            {
                Debug.Log("Collision with obstacle!");
                lost();
            }
        }
    }
}

    void win(){ 
        gra=false;
        CancelInvoke("GenerateObstaclePair");
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }
        
        textMeshPro.color = Color.green;
        textMeshPro.text = "911";
        panel.SetActive(false);
        myFx.PlayOneShot(winFx);
        to.SetActive(false);
        camera.SetActive(false);
        game.SetActive(false);
    }

    void lost(){
        gra=false;
        CancelInvoke("GenerateObstaclePair");
        planeRectTransform.anchoredPosition = new Vector2(planeRectTransform.anchoredPosition.x, 0);
        planeVerticalVelocity = 0;

        // Destroy all obstacles
        foreach (GameObject obstacle in activeObstacles)
        {
            Destroy(obstacle);
        }
        activeObstacles.Clear();
         
        passedObstacles = 0;
        camera.SetActive(false);
        game.SetActive(false);
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true);
        }
        gra=false;
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
