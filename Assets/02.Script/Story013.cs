using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story013 : StoryPlayer
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
            new DialogueFormat(Scenario.Me, Scenario.Me, "(동생이 눈 앞에 앉아있다.)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "너는 남자친구 안 만드냐?", ()=> { girl.gameObject.SetActive(true);girl.ChangeFace(0); }),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "...", () => { girl.ChangeFace(6); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "그게 무슨 뜻인지는 알고 하는 말이야?", () => { girl.ChangeFace(6); }),

            new DialogueFormat(Scenario.Me, Scenario.Me, "뭐, 너 정도면 되게 이쁘니까?"),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "아, 아니..", () => { girl.ChangeFace(3); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "그러니까.. 으흠!!", () => { girl.ChangeFace(7); }),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "나 예뻐~?", () => { girl.ChangeFace(1); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "어디? 어디가 제일 예뻐~?", () => { girl.ChangeFace(2); }),

            new DialogueFormat(Scenario.Me, Scenario.Me, "에헴.."),
            new DialogueFormat(Scenario.Me, Scenario.Me, "그래, 내가 잘못했다~"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(동생이 방으로 들어갔다.)", () => { girl.gameObject.SetActive(false); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "..."),
            new DialogueFormat(Scenario.Me, Scenario.Me, "눈물이 보였던 것 같은데, 기분탓이겠지..?"),

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
