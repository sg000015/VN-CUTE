using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story005 : StoryPlayer
{

    [Header("UI")]
    public Image black;
    public CanvasGroup canvasGroupQuestion;


    public Girl girl;



    public override void Play()
    {
        base.Play();

        SoundManager.Inst.PlayBGM(2);

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
            color.a = Mathf.Lerp(1.0f, 0.7f, time);
            black.color = color;
            yield return null;
        }

        P_000();
    }

    void P_000()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(밤이 되었다.)" ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "오빠...", () => { girl.gameObject.SetActive(true);
                girl.ChangeFace(5); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "벌써 잠든거야?.."),
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
            new DialogueFormat(Scenario.Me, Scenario.Girl, "..."),
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
            new DialogueFormat(Scenario.Me, Scenario.Girl, "안 될줄 알면서도..."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "해피엔딩을 꿈꾸었어요."),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_005;
    }

    void P_005()
    {
        StoryManager.Inst.OnEndDialogue -= P_005;
        ;
        Invoke("P_006", 1.0f);
    }

    void P_006()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Girl, "하지만, 역시 비극이네요..."),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_007;
    }

    void P_007()
    {
        StoryManager.Inst.OnEndDialogue -= P_007;

        StartCoroutine(P_008());
    }

    IEnumerator P_008()
    {
        float time = 0f;
        Color color = Color.black;

        while (time < 1f)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0.7f, 0.9f, time);
            black.color = color;
            yield return null;
        }


        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(잠결에 무슨 소리가 들렸지만)", ()=> {girl.gameObject.SetActive(false); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(더이상 기억나지 않는다.)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(...)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(무언가 잘못 되었던건가..?)"),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_009;
    }

    void P_009()
    {
        StoryManager.Inst.OnEndDialogue -= P_009;

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
