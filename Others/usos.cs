using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usos : MonoBehaviour
{
    public GameObject panel;
    private Animator animator;
    private bool canToggle = true;

    void Start()
    {
        animator = panel.GetComponent<Animator>();
    }

    void Update()
    {
        // Jeśli naciśnięto klawisz Tab, przełączamy widoczność panelu
        if (Input.GetKeyDown(KeyCode.Tab) && canToggle)
        {
            StartCoroutine(TogglePanelWithCooldown());
        }
    }

    IEnumerator TogglePanelWithCooldown()
    {
        canToggle = false;
        bool widoczny = animator.GetBool("VisiblePanel");

        if (widoczny)
            animator.SetBool("VisiblePanel", false);
        else
            animator.SetBool("VisiblePanel", true);

        yield return new WaitForSeconds(1.6f);

        canToggle = true;
    }
}
