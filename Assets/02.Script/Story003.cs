using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story003 : StoryPlayer
{

    [Header("UI")]
    public Image black;
    public CanvasGroup canvasGroupQuestion;


    public Girl girl;



    public override void Play()
    {
        base.Play();

        SoundManager.Inst.PlayBGM(0);

        girl.gameObject.SetActive(false);

        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        float time = 0f;
        Color color = Color.black;

        while (time < 1f)
        {
            time += Time.deltaTime * 0.5f;
            color.a = Mathf.Lerp(1.0f, 0f, time);
            black.color = color;
            yield return null;
        }

        P_000();
    }

    void P_000()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(알람소리가 들린다.)", ()=> { SoundManager.Inst.PlaySfx(3,0.5f); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "얘는 핸드폰만 두고 어딜 갔다냐?"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(핸드폰을 켰더니, 나랑 찍은 사진이 보인다.)"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "뭐야! 남의 핸드폰을 왜 만져!!", () => { girl.gameObject.SetActive(true);
                girl.ChangeFace(4); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "아니, 알림이 계속 울려서..."),
            new DialogueFormat(Scenario.Me, Scenario.Me, "근데, 내 사진을 배경화면으로 한 거야?"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "그건..! 당연히...", () => { girl.ChangeFace(5); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "(작게) 잘생겼으니까...", () => { girl.ChangeFace(7); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "응? 뭐라했어?", () => { }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "아..아니야!!", () => { girl.ChangeFace(5); girl.gameObject.SetActive(true); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(동생이 도망가버렸다.)", () => { girl.gameObject.SetActive(false); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "웬일로 화를 안 내는거지?"),

        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_001;
    }

    void P_001()
    {
        StoryManager.Inst.OnEndDialogue -= P_001;

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
