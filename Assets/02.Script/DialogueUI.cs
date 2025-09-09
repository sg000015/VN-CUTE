using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Febucci.UI;
using TMPro;
using Febucci.UI.Core.Parsing;
using System;

public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] GameObject dialogueObject;

    [SerializeField] TypewriterByCharacter typewriter;
    // [SerializeField] TypewriterByWord typewriter;
    [SerializeField] Text dialogueName;
    [SerializeField] TMP_Text dialogueMessage;
    [SerializeField] Button dialogueNextButton;
    [SerializeField] Button dialogueBeforeButton;

    [SerializeField] RawImage thumbnailImage;


    void Awake()
    {
        StoryManager.Inst.OnEndDialogue = OnEndCallback;

        typewriter.onTextShowed.AddListener(() => OnEndDialogue());
        OnEndDialogue += () =>
        {
            dialogueNextButton.gameObject.SetActive(true);

            if (currentDialogueIndex > 1)
            {
                // dialogueBeforeButton.gameObject.SetActive(true);
            }
        };

        OnNextDialogue += ContinueSequence;
    }

    void OnEndCallback()
    {
        StoryManager.Inst.OnEndDialogue -= OnEndCallback;
        Destroy(this.gameObject);
    }


    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.RightArrow))
    //     {
    //         if (dialogueNextButton.gameObject.activeSelf)
    //         {
    //             Btn_NextDialogue();
    //         }
    //         else
    //         {
    //             Btn_SkipDialogue();
    //         }
    //     }
    // else if (Input.GetKeyDown(KeyCode.LeftArrow))
    // {
    //     if (dialogueBeforeButton.gameObject.activeSelf)
    //     {
    //         Btn_BeforeDialogue();
    //     }
    // }

    // }




    //대사, 캐릭터[종류,레이어,애니메이션]
    public DialogueFormat[] currentDialogue;
    public int currentDialogueIndex;

    Action OnNextDialogue = () => { };
    Action OnEndDialogue = () => { };


    public void Btn_NextDialogue()
    {
        SoundManager.Inst.PlaySfx(0);
        dialogueBeforeButton.gameObject.SetActive(false);
        dialogueNextButton.gameObject.SetActive(false);
        OnNextDialogue();
    }

    public void Btn_BeforeDialogue()
    {
        SoundManager.Inst.PlaySfx(0);
        dialogueBeforeButton.gameObject.SetActive(false);
        dialogueNextButton.gameObject.SetActive(false);
        currentDialogueIndex -= 2;
        OnNextDialogue();
    }

    public void Btn_SkipDialogue()
    {
        typewriter.SkipTypewriter();
    }



    public void Dialogue(DialogueFormat[] dialogue)
    {
        currentDialogue = dialogue;
        currentDialogueIndex = 0;

        ContinueSequence();
    }

    void ShowDialogue(string message)
    {
        typewriter.ShowText(message);
    }

    void ContinueSequence()
    {
        if (currentDialogueIndex < currentDialogue.Length)
        {
            dialogueName.text = currentDialogue[currentDialogueIndex].name;

            var texture = Resources.Load<Texture>(currentDialogue[currentDialogueIndex].thumbnailPath);
            if (texture != null)
            {
                thumbnailImage.gameObject.SetActive(true);
                thumbnailImage.texture = texture;
                thumbnailImage.GetComponent<RectTransform>().sizeDelta = new Vector2(thumbnailImage.texture.width, thumbnailImage.texture.height);
            }
            else
            {
                thumbnailImage.gameObject.SetActive(false);
            }

            ShowDialogue(currentDialogue[currentDialogueIndex].message);

            if (currentDialogue[currentDialogueIndex].action != null)
            {
                currentDialogue[currentDialogueIndex].action.Invoke();
            }

            //todo 캐릭터 연출
            // CharacterManager.Inst.ActiveCharacter
            // (
            //     currentDialogue[currentDialogueIndex].character.type,
            //     currentDialogue[currentDialogueIndex].character.animation
            // );

            currentDialogueIndex++;
        }
        else
        {
            // typewriter.StartDisappearingText();

            currentDialogueIndex = 0;
            StoryManager.Inst.OnEndDialogue.Invoke();
        }
    }


}
