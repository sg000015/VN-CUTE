using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story014 : StoryPlayer
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
            new DialogueFormat(Scenario.Me, Scenario.Me, "(동생과 공원에 왔다.)" ),
            new DialogueFormat(Scenario.Me, Scenario.Me, "아까 표정이 안좋던데.. 무슨 일 있어?" ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "있잖아...교통사고 당했던 날 기억나?", () => {  girl.gameObject.SetActive(true); girl.ChangeFace(5); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "응. 기억하지." ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "어..? 기억이 난다고?!", () => {   girl.ChangeFace(3); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "당연하지, 내가 다친건데 기억을 못 할리가?.." ),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(어라? 기억이 나질 않는다.)"),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "오빠, 지금 놀이공원으로 가자.", () => {   girl.ChangeFace(4); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "갑자기..?"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "서둘러야겠다, 빨리 따라와!",() => {   girl.ChangeFace(3); }),

            new DialogueFormat(Scenario.Me, Scenario.Me, "(동생을 따라가야겠다.)", () => { girl.gameObject.SetActive(false);  }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(갑자기 왜 텐션이 올라간거지?)"),

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
