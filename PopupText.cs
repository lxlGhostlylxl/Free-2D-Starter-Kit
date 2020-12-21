using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    Controls controls;
    bool playerClose;
    bool canvasOpen;
    public GameObject PopupCanvas;
    public float StartTime;
    float timer;

    private void Awake()
    {
        controls = new Controls();
        controls.GamePlay.Interact.started += ctx => OpenCanvas();
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            CloseCanvas();
            canvasOpen = false;
        }
    }

    public void OpenCanvas()
    {
        
        if (playerClose)
        {
            if (!canvasOpen)
            {
                timer = StartTime;
                canvasOpen = true;
            }
            PopupCanvas.SetActive(true);
        }
    }

    public void CloseCanvas()
    {
        PopupCanvas.SetActive(false);
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerClose = false;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }
}
