using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story001 : StoryPlayer
{

    [Header("UI")]
    public Image black;
    public Text text;
    public CanvasGroup canvasGroupText;
    public CanvasGroup canvasGroupQuestion;



    protected override void Excute()
    {
        base.Excute();

        videoPlayer.Play();
        videoPlayer.loopPointReached += (a) =>
        {
            P_000();
        };

        Invoke("PlayBGM", 28f);
    }

    void PlayBGM()
    {
        var audio = GetComponent<AudioSource>();
        // audio.time = 2;
        audio.Play();
    }

    void P_000()
    {
        var chat = new DialogueFormat[2]
        {
            new DialogueFormat(Scenario.T_Hero, Scenario.Unknown, "안녕? 나는 너를 위해 찾아온 히어로야!"),
            new DialogueFormat(Scenario.T_Hero, Scenario.Unknown, "무슨 고민이 있는지 말해줄래?"),
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);


        black.color = new Color(0, 0, 0, 0.8f);

        StoryManager.Inst.OnEndDialogue += P_001;
    }

    void P_001()
    {
        StoryManager.Inst.OnEndDialogue -= P_001;

        Invoke("P_002", 1f);
    }

    void P_002()
    {
        var chat = new DialogueFormat[3]
        {
            new DialogueFormat(Scenario.T_Me, Scenario.Me, "..."),
            new DialogueFormat(Scenario.T_Me, Scenario.Me, "삶이 너무 힘들어요."),
            new DialogueFormat(Scenario.T_Me, Scenario.Me, "더이상 살아갈 이유가 있을까요?")
        };

        Instantiate(Resources.Load<DialogueUI>("Dialogue")).Dialogue(chat);

        StoryManager.Inst.OnEndDialogue += P_003;
    }

    void P_003()
    {
        StoryManager.Inst.OnEndDialogue -= P_003;

        Invoke("P_004", 1f);
    }

    void P_004()
    {
        var chat = new DialogueFormat[4]
        {
            new DialogueFormat(Scenario.T_Hero, Scenario.Unknown, "중요한건 현실이 아니야."),
            new DialogueFormat(Scenario.T_Hero, Scenario.Unknown, "어떤 꿈을 가졌는지."),
            new DialogueFormat(Scenario.T_Hero, Scenario.Unknown, "어떤 행복을 품고 살아가는지!"),
            new DialogueFormat(Scenario.T_Hero, Scenario.Unknown, "어떤 생각을 하고 살아가는지가 가장 중요한걸?")
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
            color.a = Mathf.Lerp(0.8f, 0.9f, time);
            black.color = color;
            yield return null;
        }

        text.text = "히어로를 만난 순간";
        time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            canvasGroupText.alpha = time;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        while (time > 0)
        {
            time -= Time.deltaTime;
            canvasGroupText.alpha = time;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);

        text.text = "내가 태어난 의미를\n마주하는 느낌이었다.";
        time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            canvasGroupText.alpha = time;
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);

        while (time > 0)
        {
            time -= Time.deltaTime;
            canvasGroupText.alpha = time;
            yield return null;
        }

        time = 0;

        while (time < 1f)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0.8f, 1f, time);
            black.color = color;
            yield return null;
        }

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
