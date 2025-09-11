using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNextButtonDelay : MonoBehaviour
{
    [SerializeField] float delay = 0.3f;


    [SerializeField] Button button;


    void OnEnable()
    {
        button.enabled = false;
        StartCoroutine(Delay());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        button.enabled = true;
    }


}
