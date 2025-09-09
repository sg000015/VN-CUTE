using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public static UIManager Inst;

    void Awake()
    {
        if (Inst == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Inst = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }



}
