using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story011 : StoryPlayer
{

    [Header("UI")]
    public Image black;
    public CanvasGroup canvasGroupQuestion;


    public Girl girl;



    public override void Play()
    {
        base.Play();

        black.color = new Color(0, 0, 0, 0.9f);

        SoundManager.Inst.PlaySfx(1, 0.5f);
        SoundManager.Inst.PlayBGM(1);

        girl.gameObject.SetActive(false);
        P_000();
    }

    void P_000()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "쿨.."),
            new DialogueFormat(Scenario.Me, Scenario.Me, "쿠울..zzZ"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "흠냐..."),
            new DialogueFormat(Scenario.Me, Scenario.Me, "말랑말랑.."),

            new DialogueFormat(Scenario.Me, Scenario.UnknownPink, "에..에?!!"),
            new DialogueFormat(Scenario.Me, Scenario.UnknownPink, "잠..잠깐! 어딜 만지는거야!"),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_001;
    }

    void P_001()
    {
        StoryManager.Inst.OnEndDialogue -= P_001;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float time = 0f;
        Color color = Color.black;

        while (time < 1f)
        {
            time += Time.deltaTime * 0.5f;
            color.a = Mathf.Lerp(0.9f, 0f, time);
            black.color = color;
            yield return null;
        }

        P_002();
    }

    void P_002()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "아, 꿈이었나?.."),

            new DialogueFormat(Scenario.Me, Scenario.Me, "응? 이건 누구지?",() => {
                girl.gameObject.SetActive(true);
                girl.ChangeCloth(0);
                girl.ChangeDeco(0);
                girl.ChangeFace(4);
                }),
            new DialogueFormat(Scenario.Me, Scenario.UnknownPink, "(화난 표정의 소녀가 옆에 누워있다.)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "아, 동생인가?"),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "<size=40>(퍽!)</size>", ()=>{ SoundManager.Inst.PlaySfx(2); girl.ChangeFace(6);}),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "<size=40>바보! 멍청이! 변태!!!</size>",()=>{ girl.ChangeFace(4);}  ),

            new DialogueFormat(Scenario.Me, Scenario.Me, "으; 아퍼라~",()=>{ girl.gameObject.SetActive(false); } ),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_003;
    }

    void P_003()
    {
        StoryManager.Inst.OnEndDialogue -= P_003;

        Invoke("P_004", 1.0f);
    }

    void P_004()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "근데, 쟤는 왜 내 침대에 누워있던거람?"),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_005;
    }

    void P_005()
    {
        StoryManager.Inst.OnEndDialogue -= P_005;

        StartCoroutine(FadeOut());
    }


    IEnumerator FadeOut()
    {
        yield return null;

        float time = 0f;
        Color color = Color.black;

        while (time < 1f)
        {
            time += Time.deltaTime * 0.5f;
            color.a = Mathf.Lerp(0.0f, 1.0f, time);
            black.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        canvasGroupQuestion.gameObject.SetActive(true);
        canvasGroupQuestion.alpha = 0;

        time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * 3;
            canvasGroupQuestion.transform.localScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one, time);
            canvasGroupQuestion.alpha = Mathf.Lerp(0f, 1, time);
            yield return null;
        }
    }

    [ContextMenu("Skip")]
    void Skip()
    {
        StartCoroutine(SkipCoroutine());
    }

    IEnumerator SkipCoroutine()
    {
        canvasGroupQuestion.gameObject.SetActive(true);
        canvasGroupQuestion.alpha = 0;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * 3;
            canvasGroupQuestion.transform.localScale = Vector3.Lerp(Vector3.one * 0.5f, Vector3.one, time);
            canvasGroupQuestion.alpha = Mathf.Lerp(0, 1, time);
            yield return null;
        }
    }

}
