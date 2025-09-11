using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable]
public class SaveData
{
    public static string Puzzle_Switch = "P_Switch";
    public static string Puzzle_Logic = "P_Logic";
    public static string Puzzle_Pipe = "P_Pipe";
    public static string Puzzle_Line = "P_Line";
    public static string Puzzle_Picture = "P_Picture";
    public static string Puzzle_Slide = "P_Slide";
    public static string Puzzle_Arrow = "P_Arrow";
    public static string Puzzle_Rotate = "P_Rotate";
    public static string Puzzle_Maze = "P_Maze";
    public static string Puzzle_Dice = "P_Dice";
}



[Serializable]
public class DialogueFormat
{
    public DialogueFormat(string thumbnailPath, string name, string message, Action action = null)
    {
        this.thumbnailPath = thumbnailPath;
        this.name = name;
        this.message = message;
        this.action = action;
    }

    public string thumbnailPath;
    public string name;
    public string message;

    public Action action;
}

[Serializable]
public class CharacterFormat
{

    public CharacterFormat(string type, string animation, float time, bool isPose)
    {
        this.type = type;
        this.animation = animation;
    }

    public string type;
    public string animation;
    public float time;
}


public class Scenario
{
    public static readonly string notice = "<color=#FFF56B>알림</color>";
    public static readonly string Unknown = "<color=#FFF56B>???</color>";
    public static readonly string Me = "<color=#FFF56B>나</color>";

    public static readonly string UnknownPink = "<color=#F912FF>???</color>";
    public static readonly string Girl = "<color=#F912FF>소미</color>";


    public static readonly string Waiter = "<color=#81E9FF>종업원</color>";
    public static readonly string Hair = "<color=#6CFF85>미용사</color>";



    public static readonly string T_None = "Thumbnail/me";
    public static readonly string T_Me = "Thumbnail/me";
    public static readonly string T_Girl = "Thumbnail/girl";

}
