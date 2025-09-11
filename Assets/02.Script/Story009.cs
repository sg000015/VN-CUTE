using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story009 : StoryPlayer
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
            new DialogueFormat(Scenario.Me, Scenario.Me, "(동생과 미용실에 왔다.)" ),
            new DialogueFormat(Scenario.Me, Scenario.Hair, "어서오세요~ 어떻게 해드릴까요?", ()=> { girl2.SetActive(true); } ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "오빠는 두상이 예뻐서, 투블럭에 가르마펌 하면 괜찮을듯?", () => { girl2.SetActive(false);
            girl.gameObject.SetActive(true);
            girl.ChangeFace(1);
            } ),

            new DialogueFormat(Scenario.Me, Scenario.Me, "좋아, 너만 믿는다구~!" ),
            new DialogueFormat(Scenario.Me, Scenario.Hair, "그렇게 해드릴까요~?", ()=> {girl2.SetActive(true);girl.gameObject.SetActive(false); } ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "볼륨감 많이 넣어주시고, 소프트 투블럭으로 해주세요!", () => { girl2.SetActive(false);girl.gameObject.SetActive(true);girl.ChangeFace(2);}),
            new DialogueFormat(Scenario.Me, Scenario.Me, "그럼, 부탁드리겠습니다!"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "저기서, 기다리고 있을게~"),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_001;
    }

    void P_001()
    {
        StoryManager.Inst.OnEndDialogue -= P_001;

        girl.gameObject.SetActive(false);

        Invoke("P_002", 1.0f);
    }

    void P_002()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Hair, "여자친구분이 되게 좋아하시나봐요.",()=> { girl2.SetActive(true); } ),
            new DialogueFormat(Scenario.Me, Scenario.Me, "네? 하하.."),
            new DialogueFormat(Scenario.Me, Scenario.Hair, "눈빛이 아주 초롱초롱 하시던데요~?"),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_003;
    }

    void P_003()
    {
        StoryManager.Inst.OnEndDialogue -= P_003;

        girl2.SetActive(false);
        Invoke("P_004", 1.0f);
    }

    void P_004()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(파마가 모두 끝났다.)"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "아까 미용사랑 무슨 얘기했어?",()=>{girl.gameObject.SetActive(true);girl.ChangeFace(5); }  ),
            new DialogueFormat(Scenario.Me, Scenario.Me, "너보고 여자친구인줄 알더라."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "그래서???",()=>{girl.ChangeFace(3); }  ),
            new DialogueFormat(Scenario.Me, Scenario.Me, "그냥, 동생이라했지 뭐~"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "아.. 그랬구나...",()=>{girl.ChangeFace(6); }  ),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(왠지 슬퍼보이는데, 기분 탓이려나?)"),
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
