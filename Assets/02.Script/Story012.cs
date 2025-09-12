using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story012 : StoryPlayer
{

    [Header("UI")]
    public Image black;
    public CanvasGroup canvasGroupQuestion;


    public Girl girl;
    public GameObject girl2;



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
            new DialogueFormat(Scenario.Me, Scenario.Me, "(저 멀리 동생이 보인다.)"),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_001;
    }

    void P_001()
    {
        StoryManager.Inst.OnEndDialogue -= P_001;

        Invoke("P_002", 1.0f);
    }

    void P_002()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "여어! 히~사시부리", () => { girl.gameObject.SetActive(true);
                girl.ChangeFace(0); }),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "엣?!",() => {
                girl.gameObject.SetActive(true);
                girl.ChangeFace(3);
                }),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "친구랑 있는데, 인사하면 어떡해!",() => {                }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "(동생의 얼굴이 빨개진다.)",() => { girl.ChangeFace(5); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "그치만, 너무 예뻐서 아는척 하고싶은걸?"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "뭐..래!!",() => { girl.ChangeFace(4); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "그런말 들으면 내가 좋아할 줄 알아?!",() => { }),

            new DialogueFormat(Scenario.Me, Scenario.UnknownPink, "애정이 가득해보이네~", ()=>{  girl.gameObject.SetActive(false); girl2.SetActive(true); } ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "애정은 무슨!..",()=>{  girl.ChangeFace(7); girl.gameObject.SetActive(true); girl2.SetActive(false); }),
            new DialogueFormat(Scenario.Me, Scenario.UnknownPink, "입꼬리가 올라갔는데~?", ()=>{  girl.gameObject.SetActive(false); girl2.SetActive(true); } ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "장..장난치지마!!",()=>{  girl.gameObject.SetActive(true); girl2.SetActive(false); girl.ChangeFace(5);}),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "(이러쿵.. 저러쿵..)" ),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_003;
    }

    void P_003()
    {
        StoryManager.Inst.OnEndDialogue -= P_003;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float time = 0f;
        Color color = Color.black;

        while (time < 1f)
        {
            time += Time.deltaTime * 0.5f;
            color.a = Mathf.Lerp(0.0f, 0.9f, time);
            black.color = color;
            yield return null;
        }
        girl2.SetActive(false);
        P_004();
    }


    void P_004()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(동생으로부터 문자가 왔다.)"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "(점심 맛있는거 해줄테니, 기다리고있어~!)"),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_005;
    }

    void P_005()
    {
        StoryManager.Inst.OnEndDialogue -= P_005;

        Invoke("P_006", 1.0f);
    }

    void P_006()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "왠일로 잘해주는거람?"),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_007;
    }

    void P_007()
    {
        StoryManager.Inst.OnEndDialogue -= P_007;

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
            color.a = Mathf.Lerp(0.9f, 1.0f, time);
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
