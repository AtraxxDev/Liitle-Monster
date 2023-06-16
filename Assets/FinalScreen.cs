using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreen : MonoBehaviour
{
    public GameObject Fade;
    public GameObject LinetExt;
    public AudioSource Monster;
    public AudioSource shout;
    public AudioSource Crouch;
    public AudioSource Music;
    public string SceneName;

    //public Animator fadeScreenAnimator;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Fade.SetActive(true);
            //fadeScreenAnimator.SetTrigger("FadeTrigger");
            StartCoroutine(Final());
        }
    }


    IEnumerator Final()
    {
        yield return new WaitForSeconds(4.0f);
        Music.Pause();
        LinetExt.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        Monster.Play();
        yield return new WaitForSeconds(3.0f);
        shout.Play();
        LinetExt.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        Crouch.Play();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneName);

    }
}