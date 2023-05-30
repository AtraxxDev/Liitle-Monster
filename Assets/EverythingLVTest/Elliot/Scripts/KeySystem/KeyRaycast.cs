using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Keysystem
{ 
    public class KeyRaycast : MonoBehaviour
    {
        [SerializeField] private int rayLength = 5;
        [SerializeField] private LayerMask layerMaskInteract;
        [SerializeField] private string exluseLayerName = null;
        private KeyItemController raycastedObject;
        private NoteController _noteController;
        [SerializeField] private KeyCode openDoorKey = KeyCode.Mouse0;
        [SerializeField] private KeyCode interactNoteKey = KeyCode.Mouse0;
        [SerializeField] private Image crosshair = null;
        private bool isCrosshairActive;
        private bool doOnce;

        private string interactableTag = "InteractiveObject";
        private string noteTag = "Note";

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
                crosshair.color = Color.red;
            }
            else
            {
                crosshair.color= Color.white;
                isCrosshairActive= false;
            }
        }
    }
}