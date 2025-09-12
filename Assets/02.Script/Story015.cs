using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story015 : StoryPlayer
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

        P_000();
    }

    // IEnumerator StartScene()
    // {
    //     float time = 0f;
    //     Color color = Color.black;

    //     while (time < 1f)
    //     {
    //         time += Time.deltaTime * 0.5f;
    //         color.a = Mathf.Lerp(1.0f, 0.7f, time);
    //         black.color = color;
    //         yield return null;
    //     }

    //     P_000();
    // }

    void P_000()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(놀이공원에 도착했다.)" ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "오빠...여기 기억나?", () => { girl.gameObject.SetActive(true);  girl.ChangeFace(5); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "우리가 자주 오던 곳이잖아~"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(그러고보니 와본 적이 있던가?)"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "그..그럼, 작년 가을에 왔던 것도 기억나?", () => { girl.gameObject.SetActive(true);  girl.ChangeFace(3); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "작년 가을? 당연하지!"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "그 날은, 내가 너한테 고백했었던..."),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_001;
    }

    void P_001()
    {
        StoryManager.Inst.OnEndDialogue -= P_001;

        StartCoroutine(P_002());
    }

    IEnumerator P_002()
    {
        float time = 0f;
        Color color = Color.black;

        while (time < 1f)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0.0f, 0.7f, time);
            black.color = color;
            yield return null;
        }

        SoundManager.Inst.PlayBGM(2);

        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(어라..?)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(잠깐만, 뭔가가 이상하다.)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(소미는 분명 내 동생인데..)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(내가 고백을 했었다고?)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(이..이 기억은 뭐지?!)"),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_003;
    }


    void P_003()
    {
        StoryManager.Inst.OnEndDialogue -= P_003;

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
            color.a = Mathf.Lerp(0.7f, 1.0f, time);
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
