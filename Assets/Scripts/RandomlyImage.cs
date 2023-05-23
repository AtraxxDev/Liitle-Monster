using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomlyImage : MonoBehaviour
{
    public GameObject image;
    public float minTime;
    public float maxTime;
    public AudioClip[] sounds;
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(ShowImage());
        
    }

    IEnumerator ShowImage()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            image.SetActive(true);
            var sound = sounds[Random.Range(0, sounds.Length)];
            audioSource.PlayOneShot(sound);
            yield return new WaitForSeconds(minTime);
            image.SetActive(false);
        }
    }
}
