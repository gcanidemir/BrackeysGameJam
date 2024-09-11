using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsControls : MonoBehaviour
{
    public GameObject player;
    private bool inArea =false;
    private Rigidbody2D rb;
    void Start()
    {

    rb = player.GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            inArea = false;
        }
    }

    public void ToggleGuns(){
        bool hasActiveChild = false;

        foreach (Transform child in player.transform)
        {

            if (child.gameObject.activeSelf)
            {
                hasActiveChild = true;
                break;
            }

        }

        if (hasActiveChild){
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            foreach (Transform child in player.transform)
            {

                child.gameObject.SetActive(false);

            }

        }
        else{
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            foreach (Transform child in player.transform)
            {

                child.gameObject.SetActive(true);

            }

        }
        
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.E) && inArea){
            ToggleGuns();
        }

        
    }
}
