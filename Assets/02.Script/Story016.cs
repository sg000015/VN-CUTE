using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story016 : StoryPlayer
{

    [Header("UI")]
    public Image black;
    public CanvasGroup canvasGroupQuestion;

    public Image background;
    public Sprite sprite;



    public Girl girl;



    public override void Play()
    {
        base.Play();

        SoundManager.Inst.PlayBGM(3, 0.8f);

        girl.gameObject.SetActive(false);

        P_000();
    }

    void P_000()
    {
        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(다급한 목소리가 들린다.)" ),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "오빠!!! 괜찮아?!!", () => { girl.gameObject.SetActive(true);  girl.ChangeFace(3); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "소미야.. 미안...정말 미안해..."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "오빠가 왜 미안해!..", () => {  girl.ChangeFace(1); }),
            new DialogueFormat(Scenario.Me, Scenario.Me, "내가 어떻게 너를 잊어버릴 수가 있지.."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "에이~!"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "오히려 다시 기억해줘서 고마운걸?"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "정말 미안해.."),
            new DialogueFormat(Scenario.Me, Scenario.Me, "어떻게 여자친구를 착각할 수가 있는지..!"),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "에헴~!", () => {  girl.ChangeFace(7); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "처음엔, 정말로 당황했었다구!"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "어떻게, 날 친동생으로 착각하는거야~!"),

            new DialogueFormat(Scenario.Me, Scenario.Me, "그야..."),
            new DialogueFormat(Scenario.Me, Scenario.Me, "내가 커플일리가 없다고 생각했어."),

            new DialogueFormat(Scenario.Me, Scenario.Girl, "엣?! 그게뭐야~!", () => {  girl.ChangeFace(2); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "역시 바보라니까~!"),

            new DialogueFormat(Scenario.Me, Scenario.Me, "(소미가 웃고 있다.)"),

            new DialogueFormat(Scenario.Me, Scenario.Me, "사랑해. 앞으로도 영원하자!"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "응. 나두~ 사랑해~♥", () => {  girl.ChangeFace(7); }),
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
            time += Time.deltaTime * 0.5f;
            color.a = Mathf.Lerp(0.0f, 1f, time);
            black.color = color;
            yield return null;
        }

        girl.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        girl.ChangeCloth(0);
        girl.ChangeDeco(0);

        background.sprite = sprite;

        time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * 0.5f;
            color.a = Mathf.Lerp(1.0f, 0f, time);
            black.color = color;
            yield return null;
        }

        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(에필로그)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "근데, 왜 맨날 내 침대에 누워있던거야?"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "그건...", () => { girl.gameObject.SetActive(true); }),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "잠꼬대로는 나를 정확히 불러줬으니까!"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "비록, 꿈속이었겠지만..."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "나를 분명히 기억하고 있었는걸!!"),
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
            color.a = Mathf.Lerp(0.0f, 0.8f, time);
            black.color = color;
            yield return null;
        }

        var chat = new DialogueFormat[]
        {
            new DialogueFormat(Scenario.Me, Scenario.Me, "(지난 밤)"),
            new DialogueFormat(Scenario.Me, Scenario.Me, "(잠꼬대로) 소미야..사랑해...영원하자.."),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "응..!"),
            new DialogueFormat(Scenario.Me, Scenario.Girl, "언제까지든 기다릴게!!!"),
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
