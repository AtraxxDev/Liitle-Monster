using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keysystem
{
    public class KeyDoorController : MonoBehaviour
    {
        private Animator doorAnim;
        public bool doorOpen = false;

        [Header("Animation Names")]
        [SerializeField] private string openAnimationName = "Door Open";
        [SerializeField] private string closeAnimationName = "Door Close";

        [SerializeField] private int timeToShowUI = 1;
        [SerializeField] private GameObject showDoorLockedUI = null;

        [SerializeField] private KeyInventory _keyInventory = null;

        [SerializeField] private int waitTimer = 1;
        [SerializeField] private bool pauseInteraction = false;

        public AudioSource _OpenDoor;
        public AudioSource _CloseDoor;
        public AudioSource _LockedDoor;

    

        private void Awake()
        {
            doorAnim = gameObject.GetComponent<Animator>();
        }

        private IEnumerator PauseDoorInteraction()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;
        }

        public void PlayAnimation()
        {

            if (_keyInventory.hasredKey) 
            {
                OpenDoor();
            }

            else if (_keyInventory.hasTutorialKey)
            {
                OpenDoor();
            }

            else if (_keyInventory.hasBlueKey)
            {
                OpenDoor();
            }


            else if (_keyInventory.hasYellowKey)
            {
                OpenDoor();
            }


            else if (_keyInventory.hasGreenKey)
            {
                OpenDoor();
            }

            else
            {
                Debug.Log("No abri");
                StartCoroutine(showDoorLocked());
            }
        }

     

        void OpenDoor()
        {
            if (!doorOpen && !pauseInteraction)
            {
                doorAnim.Play(openAnimationName, 0, 0.0f);
           
                _OpenDoor.Play();
                doorOpen = true;
                StartCoroutine(PauseDoorInteraction());
            }

            else if (doorOpen && !pauseInteraction)
            {
                doorAnim.Play(closeAnimationName, 0, 0.0f);
               
                _CloseDoor.Play();
                doorOpen = false;
                StartCoroutine(PauseDoorInteraction());

            }
        }

        IEnumerator showDoorLocked()
        {
            showDoorLockedUI.SetActive(true);
            _LockedDoor.Play();
            yield return new WaitForSeconds(timeToShowUI);
            showDoorLockedUI.SetActive(false);
        }

    }
}
