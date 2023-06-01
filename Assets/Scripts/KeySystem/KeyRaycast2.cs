using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Keysystem
{ 
    public class KeyRaycast2 : MonoBehaviour
    {
        [SerializeField] private int rayLength = 5;
        [SerializeField] private LayerMask layerMaskInteract;
        [SerializeField] private string exluseLayerName = null;
        private KeyItemController raycastedObject;
        private NoteController _noteController;
        [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;

        [Space (10)]
        [Header("UI Image")]
        [SerializeField] private GameObject KeyInteract = null;
        [SerializeField] private GameObject TextDoor = null;
        [SerializeField] private GameObject TextNote = null;



        private bool isCrosshairActive;
        private bool doOnce;

        private string interactableTag = "InteractiveObject";
        

        private void Update()
        {
           
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, fwd, Color.magenta);

            int mask = 1 << LayerMask.NameToLayer(exluseLayerName) | layerMaskInteract.value;

            if (Physics.Raycast(transform.position,fwd ,out hit, rayLength,mask)) 
            {

                var readbleItem = hit.collider.GetComponent<NoteController>();
                if (readbleItem != null)
                {

                    _noteController = readbleItem;
                    CrosshairChange(true);
                    TextNote.SetActive(true);
                }

                else
                {
                    
                    ClearNote();
                }


                


                if (hit.collider.CompareTag(interactableTag)) 
                {
                    

                    if (!doOnce)
                    {

                        raycastedObject = hit.collider.gameObject.GetComponent<KeyItemController>();
                           
                        CrosshairChange(true);
                        TextDoor.SetActive(true);

                    }
                    
                    isCrosshairActive = true;
                    doOnce = true;


                    if(Input.GetKeyDown(openDoorKey)) 
                    {
                        raycastedObject.ObjectInteraction();    
                    }

                    

                }

                
            }

            else
            {
                TextNote.SetActive(false);
                TextDoor.SetActive(false);   
                ClearNote() ;

                if (isCrosshairActive) 
                {
                    CrosshairChange(false);
                    doOnce = false;
                }
            }

                if (_noteController != null)
                {
                    if (Input.GetKeyDown(openDoorKey))
                    {
                        _noteController.ShowNote();
                    }

                }

            



        }

        void ClearNote()
        {
            if (_noteController != null)
            {
                _noteController = null;
            }
           
        }

        void CrosshairChange(bool on)
        {
            if (on && !doOnce) 
            {
                KeyInteract.SetActive(true);
            }
            else
            {
                KeyInteract.SetActive(false);
            }
        }
    }
}