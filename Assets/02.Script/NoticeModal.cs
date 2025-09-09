using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class NoticeModal : MonoBehaviour
{

    [SerializeField] private Text msgText;
    [SerializeField] private Button yesButton;


    public void Open(string msg, Action yesAction)
    {
        msgText.text = msg;
        this.gameObject.SetActive(true);

        yesButton.onClick.AddListener(() =>
        {
            yesAction?.Invoke();
            Close();
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            yesButton.onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Close()
    {
        Destroy(this.gameObject);
    }

}
