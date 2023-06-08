using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keysystem
{
    public class KeyItemController : MonoBehaviour
    {

        [Space(10)]

        [SerializeField] private bool TutorialDoor = false;
        [SerializeField] private bool TutorialKey = false;

        [Space(10)]

        [SerializeField] private bool redDoor = false;
        [SerializeField] private bool redKey = false;

        [Space(10)]

        [SerializeField] private bool BlueDoor = false;
        [SerializeField] private bool BlueKey = false;

        [Space(10)]

        [SerializeField] private bool YellowDoor = false;
        [SerializeField] private bool YellowKey = false;

        [Space(10)]

        [SerializeField] private bool GreenDoor = false;
        [SerializeField] private bool GreenKey = false;

        

        [SerializeField] private KeyInventory _keyInventory = null;

        private KeyDoorController doorObject;

        private void Start()
        {
            if (TutorialDoor)
            {
                doorObject = GetComponent<KeyDoorController>();
            }

            if (redDoor)
            {
                doorObject = GetComponent<KeyDoorController>();
            }

            if (BlueDoor)
            {
                doorObject = GetComponent<KeyDoorController>();
            }

            if (YellowDoor)
            {
                doorObject = GetComponent<KeyDoorController>();
            }

            if (GreenDoor)
            {
                doorObject = GetComponent<KeyDoorController>();
            }


        }

        public void ObjectInteraction()
        {
            if (TutorialDoor && _keyInventory.hasTutorialKey)
            {
                doorObject.PlayAnimation();
            }
           
            else if (TutorialKey)
            {
                _keyInventory.hasTutorialKey = true;
                gameObject.SetActive(false);
            }


            if (redDoor && _keyInventory.hasredKey)
            {
                doorObject.PlayAnimation();
            }
            else if (redKey) 
            { 
                _keyInventory.hasredKey = true;
                gameObject.SetActive(false);
            }


            if (BlueDoor && _keyInventory.hasBlueKey)
            {
                doorObject.PlayAnimation();
            }
            else if (BlueKey)
            {
                _keyInventory.hasBlueKey = true;
                gameObject.SetActive(false);
            }

            if (YellowDoor && _keyInventory.hasYellowKey)
            {
                doorObject.PlayAnimation();
            }
            else if (YellowKey)
            {
                _keyInventory.hasYellowKey = true;
                gameObject.SetActive(false);
            }


            if (GreenDoor && _keyInventory.hasGreenKey)
            {
                doorObject.PlayAnimation();
            }
            else if (GreenKey)
            {
                _keyInventory.hasGreenKey = true;
                gameObject.SetActive(false);
            }
        }

    }
}
