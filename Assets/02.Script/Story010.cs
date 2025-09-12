using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story010 : StoryPlayer
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
            color.a = Mathf.Lerp(1.0f, 0.0f, time);
            black.color = color;
            yield return null;
        }

        P_000();
    }

    void P_000()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(동생과 영화관에 왔다.)" ),
            new DialogueFormat(Scenario.Me, Scenario.Me, "갑자기 영화관으로 데려온 이유가?" ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "오늘 이렇게 잘생겼는데~ 들어가기 아쉽잖아~?!", () => { girl.gameObject.SetActive(true);
                girl.ChangeFace(2); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "지금 나 놀리는거지?" ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "데헷~"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "사실 같이 보고싶은게 있었어.", () => {girl.ChangeFace(1); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "(동생이 포스터를 보여준다.)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "'사랑의 극복'..? 로맨스는 별론데."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "<size=40>안.볼.거.야.?</size>", () => { girl.ChangeFace(4); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "으윽.. 이럴땐 참 난감하단 말이야~"),
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
        girl.gameObject.SetActive(false);
        SoundManager.Inst.PlayBGM(2);

        float time = 0f;
        Color color = Color.black;

        while (time < 1f)
        {
            time += Time.deltaTime * 0.5f;
            color.a = Mathf.Lerp(0.0f, 0.8f, time);
            black.color = color;
            yield return null;
        }


        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(영화 시청중)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(작게) 뭐야..왜 떨고있어?"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "(작게) 아..아무것도 아니야.",()=>{girl.gameObject.SetActive(true); girl.ChangeFace(5);}),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(작게) 나갈까?.."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "(작게) 앗! 응.."),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_003;
    }


    void P_003()
    {
        StoryManager.Inst.OnEndDialogue -= P_003;

        StartCoroutine(P_004());
    }

    IEnumerator P_004()
    {
        girl.gameObject.SetActive(false);

        float time = 0f;
        Color color = Color.black;

        while (time < 1f)
        {
            time += Time.deltaTime * 0.5f;
            color.a = Mathf.Lerp(0.8f, 0f, time);
            black.color = color;
            yield return null;
        }

        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(영화관을 나왔다.)"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "오빠.. 할말있어.", ()=>{ girl.gameObject.SetActive(true); girl.ChangeFace(5);}),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "나 오빠랑 사랑하는 사이로 지내고싶어..!"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "...그게 무슨 소리야."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "그러니깐..."),
            new DialogueFormat(Scenario.Me, Scenario.Me, "못 들은 걸로 할게. 먼저 들어간다."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "..."),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(머리속이 복잡하다. 잠이나 자야겠어.)",()=>{ girl.gameObject.SetActive(false);  }),
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
