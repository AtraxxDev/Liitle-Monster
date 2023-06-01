using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keysystem
{
    public class KeyItemController : MonoBehaviour
    {
        [SerializeField] private bool redDoor = false;
        [SerializeField] private bool redKey = false;

        [SerializeField] private KeyInventory _keyInventory = null;

        private KeyDoorController doorObject;

        private void Start()
        {
            if(redDoor)
            {
                doorObject = GetComponent<KeyDoorController>();
            }
            
        }

        public void ObjectInteraction()
        {
            if (redDoor)
            {
                doorObject.PlayAnimation();
            }
            else if (redKey) 
            { 
                _keyInventory.hasredKey = true;
                gameObject.SetActive(false);
            }
        }

    }
}
