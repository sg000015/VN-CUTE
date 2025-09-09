using System.Collections;
using Unity.VisualScripting;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class StoryManager : MonoBehaviour
{
    public static StoryManager Inst;

    void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }


    public Action OnEndDialogue = () => { };



#if UNITY_EDITOR
    public string testScene = "";

    [ContextMenu("Play")]
    public void LoadTestScene()
    {
        LoadScene(testScene);
    }

    [ContextMenu("ResetKey")]
    public void ResetKey()
    {
        PlayerPrefs.DeleteKey(SaveData.Puzzle_Switch);
        PlayerPrefs.DeleteKey(SaveData.Puzzle_Logic);
        PlayerPrefs.DeleteKey(SaveData.Puzzle_Pipe);
        PlayerPrefs.DeleteKey(SaveData.Puzzle_Line);
        PlayerPrefs.DeleteKey(SaveData.Puzzle_Picture);
        PlayerPrefs.DeleteKey(SaveData.Puzzle_Slide);
        PlayerPrefs.DeleteKey(SaveData.Puzzle_Dice);
        PlayerPrefs.DeleteKey(SaveData.Puzzle_Arrow);
        PlayerPrefs.DeleteKey(SaveData.Puzzle_Rotate);
        PlayerPrefs.DeleteKey(SaveData.Puzzle_Maze);
    }
#endif


    public void LoadScene(string sceneName)
    {
        Instantiate(Resources.Load<StoryPlayer>(sceneName)).Play();
    }

}
