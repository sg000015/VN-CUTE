using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story007 : StoryPlayer
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

            new DialogueFormat(Scenario.Me, Scenario.UnknownPink, "어머~ 누구셔?", ()=>{  girl.gameObject.SetActive(false); girl2.SetActive(true); } ),

            new DialogueFormat(Scenario.Me, Scenario.Me, "안녕? 소미 친오빠니까 편하게 대해~",()=>{ girl2.SetActive(false); }  ),
            new DialogueFormat(Scenario.Me, Scenario.UnknownPink, "안녕하세요~",()=>{ girl2.SetActive(true); }  ),
            new DialogueFormat(Scenario.Me, Scenario.UnknownPink, "(작게) 이름이 되게 특이하네?"  ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "뭐가..?",() => { girl.gameObject.SetActive(true); girl2.SetActive(false);  }),
            new DialogueFormat(Scenario.Me, Scenario.UnknownPink, "(작게) 성이 다른 남매는 처음봤어.",() => { girl.gameObject.SetActive(false);girl2.SetActive(true); }  ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "...부모님이 달라.",()=>{ girl2.SetActive(false); girl.gameObject.SetActive(true); }  ),

            new DialogueFormat(Scenario.Me, Scenario.UnknownPink, "그게 무슨? 앗..미안!",() => { girl.gameObject.SetActive(false);girl2.SetActive(true); }  ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "...",()=>{ girl2.SetActive(false); girl.gameObject.SetActive(true); }  ),

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
            new DialogueFormat(Scenario.Me, Scenario.Girl, "(일찍 들어갈거니깐, 기다리고있어!)"),
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
            new DialogueFormat(Scenario.Me, Scenario.Me, "갑자기 왜 기다리란거람?"),
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
