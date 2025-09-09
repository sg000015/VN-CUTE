using UnityEngine;


public static class Notice
{

    public static void Show(string message, System.Action yesAction = null)
    {
        NoticeModal modal = Resources.Load<NoticeModal>("Notice");

        MonoBehaviour.Instantiate(modal).Open(message, yesAction);
    }

}