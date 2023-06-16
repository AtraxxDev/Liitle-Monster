using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsChange : MonoBehaviour
{
    public GameObject change;
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds (5.0f);
        change.SetActive (true);
    }
}
