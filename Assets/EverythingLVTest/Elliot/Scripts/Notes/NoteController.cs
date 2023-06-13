using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NoteController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode closeKey;


    [Header("UI Text")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private TMP_Text noteTextAreaUI;

    [Space(10)]
    [SerializeField] [TextArea] private string noteText;

    [Space(10)]
    [SerializeField] private UnityEvent openEvent;
   [SerializeField] private bool isOpen = false;


    public void ShowNote()
    {
        noteTextAreaUI.text = noteText;
        noteCanvas.SetActive(true);
        openEvent.Invoke();

        isOpen = true;
    }


    void DisableNote()
    {
        noteCanvas.SetActive(false);
        openEvent.Invoke();

        isOpen = false;
    }


    private void Update()
    {
        if (isOpen) 
        {
            if(Input.GetKeyDown(closeKey)) 
            {
                DisableNote();
            }        
        }
    }
}
