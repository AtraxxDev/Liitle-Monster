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

        public AudioSource collectSound;

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
                PlayerPrefs.SetInt("TutorialKey", 1); // Guardar la llave TutorialKey en PlayerPrefs
                gameObject.SetActive(false);
            }

            if (redDoor && _keyInventory.hasredKey)
            {
                doorObject.PlayAnimation();
            }
            else if (redKey)
            {
                collectSound.Play();
                _keyInventory.hasredKey = true;
                PlayerPrefs.SetInt("RedKey", 1); // Guardar la llave RedKey en PlayerPrefs
                gameObject.SetActive(false);
            }

            if (BlueDoor && _keyInventory.hasBlueKey)
            {
                doorObject.PlayAnimation();
            }
            else if (BlueKey)
            {
                collectSound.Play();
                _keyInventory.hasBlueKey = true;
                PlayerPrefs.SetInt("BlueKey", 1); // Guardar la llave BlueKey en PlayerPrefs
                gameObject.SetActive(false);
            }

            if (YellowDoor && _keyInventory.hasYellowKey)
            {
                doorObject.PlayAnimation();
            }
            else if (YellowKey)
            {
                collectSound.Play();
                _keyInventory.hasYellowKey = true;
                PlayerPrefs.SetInt("YellowKey", 1); // Guardar la llave YellowKey en PlayerPrefs
                gameObject.SetActive(false);
            }

            if (GreenDoor && _keyInventory.hasGreenKey)
            {
                doorObject.PlayAnimation();
            }
            else if (GreenKey)
            {
                collectSound.Play();
                _keyInventory.hasGreenKey = true;
                PlayerPrefs.SetInt("GreenKey", 1); // Guardar la llave GreenKey en PlayerPrefs
                gameObject.SetActive(false);
            }
        }
    }
}
