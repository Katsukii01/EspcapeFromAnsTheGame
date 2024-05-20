using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossController : MonoBehaviour
{   
    Transform player;
    float speed = 0.6f;
    float attackRange = 1.6f;
    public Animator animator;
    private bool isAttacking = false;
    private bool isDead = false;
    public GameObject lifeGUI;
    public static int currentHealth;
    int maxHealth = 10;

    public Collider rightHandCollider;


    public TextMeshProUGUI life;
    private Rigidbody rb;
    private void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth=maxHealth;
    }


    private void Update()
    {
        if (!isDead && player != null)
        {
            // Obliczanie dystansu między bossem a graczem
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Sprawdzanie czy boss jest wystarczająco blisko gracza, żeby zaatakować
            if (distanceToPlayer <= attackRange)
            {
                // Atakuj gracza
                Attack();
            }
            else
            {
                // Boss goni gracza
                ChasePlayer();
            }
        }
    }

    public void changelife(){
                RectTransform affectRectTransform = lifeGUI.GetComponent<RectTransform>();
                float newScaleY = 0.1f * currentHealth; // Załóżmy, że reputation to zmienna przechowująca reputację
                Vector3 currentScale = affectRectTransform.localScale;
                affectRectTransform.localScale = new Vector3(currentScale.x, newScaleY, currentScale.z);
                life.text = ""+currentHealth*10+"%";
    }

     public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            changelife();
            // Aktualizacja paska życia, jeśli potrzebne
            if (currentHealth <= 0)
            {
                isDead=true;
                Die();
            }
        }


    void Die()
    {
        isDead = true;
        // Ustawianie animacji na śmierć
        animator.SetBool("Death", true);
    }

    void ChasePlayer()
    { 
        RightHandAttackEnd();
        isAttacking = false;
        // Ustawianie animacji na bieg
        animator.SetBool("isRunning", true);
        animator.SetBool("isAttacking", false);
        
        // Poruszanie się w stronę gracza
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        
        // Obracanie bossa w kierunku gracza
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    void Attack()
    {
        // Ustawianie animacji na atak
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
        RightHandAttack();
    }
    // Metoda wywoływana przez animację bossa podczas ataku lewej ręki

    // Metoda wywoływana przez animację bossa podczas ataku prawej ręki
    public void RightHandAttack()
    {
        if(rightHandCollider != null)
        {
            rightHandCollider.enabled = true;
        }
    }

    // Metoda wywoływana przez animację bossa po zakończeniu ataku prawej ręki
    public void RightHandAttackEnd()
    {
        if(rightHandCollider != null)
        {
            rightHandCollider.enabled = false;
        }
    }
}
