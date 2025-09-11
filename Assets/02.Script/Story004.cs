using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story004 : StoryPlayer
{

    [Header("UI")]
    public Image black;
    public CanvasGroup canvasGroupQuestion;


    public Girl girl;
    public GameObject boy;



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
            new DialogueFormat(Scenario.Me, Scenario.Me, "(밥 먹으러 식당에 왔다.)" ),
            new DialogueFormat(Scenario.Me, Scenario.Waiter, "주문하시겠습니까?", () => { boy.SetActive(true); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "여기 커플세트로 하나 주세요." ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "지금 커플이라고 한거야..?!", () => { boy.SetActive(false); girl.gameObject.SetActive(true);
                girl.ChangeFace(3); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "당연하지!"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "커플 세트가 제일 싸잖아~"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "아?! 난..또...", () => { girl.ChangeFace(6); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "무슨 생각을 했길래 그래?"),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "아..아니!!", () => { girl.ChangeFace(5);  }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "아무 생각도 안했거든?!!", () => {  }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(음식이 나왔다.)", () => { girl.gameObject.SetActive(false);  }),

            new DialogueFormat(Scenario.Me, Scenario.Waiter, "두분 잘 어울리시네요~ 맛있게드세요.",() => { boy.SetActive(true); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "무슨 소리에요.!!", () => {boy.SetActive(false);  girl.gameObject.SetActive(true);girl.ChangeFace(4);   }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "제가 이런 바보랑 어울릴리가 없잖아요!!", () => {  }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "그렇게까지 화낼 필요는 없지 않냐.."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "헤헷!.. 고멘나사이", () => {  girl.ChangeFace(2);  }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(왠지 기뻐보이는건 기분탓인가?)", () => { girl.gameObject.SetActive(false); } ),

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
