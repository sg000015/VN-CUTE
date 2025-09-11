using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story008 : StoryPlayer
{

    [Header("UI")]
    public Image black;
    public CanvasGroup canvasGroupQuestion;


    public Girl girl;



    public override void Play()
    {
        base.Play();

        SoundManager.Inst.PlayBGM(1);

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
            new DialogueFormat(Scenario.Me, Scenario.Me, "(동생이 지긋이 바라보고 있다.)", ()=> { girl.gameObject.SetActive(true); girl.ChangeFace(0);}),
            new DialogueFormat(Scenario.Me, Scenario.Me, "...내 얼굴에 뭐 묻었어?"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "오빠는 머리 조금만 신경쓰면, 진짜 잘생겼는데..", () => { girl.ChangeFace(1); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "에?! 아니, 아니..! 머리가 너무 지저분하다고!", () => { girl.ChangeFace(3); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "아, 벌써 머리 자를때가 된 건가?"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "그..", () => { girl.ChangeFace(7); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "미용실 같이 가줄까?.. 싫음 말구!", () => { girl.ChangeFace(1); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "음?"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "나야 고맙지~"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "헤헷..! 그럼, 잠깐만 기다려~", () => { girl.ChangeFace(2); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(외출 준비를 해야겠다.)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(근데, 얘는 왜 나만보면 고장나는거람?)"),

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
