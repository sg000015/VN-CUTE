using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


[Flags]
[SerializeField]
enum Efx
{
    Alpha = 1,
    Scale = 2,
    Position = 4,
    Rotation = 8
};

public class UIAnimationEfx : MonoBehaviour
{

    [SerializeField] private Efx efx;

    [SerializeField] private bool autoPlay = true;
    [SerializeField] private Image[] images;
    [SerializeField] private Text[] texts;
    [SerializeField] private TMPro.TMP_Text[] tmpTexts;

    delegate void InitHandler();
    InitHandler initHandler = () => { };

    InitHandler exitHandler = () => { };



    int initCount = 0;
    public UnityEvent efxCallback = new UnityEvent();

    int endCount = 0;
    public Action endcallback = () => { };



    #region Effect Setting

    [SerializeField] private bool alphaFadeLoop;
    [SerializeField] private float alphaFadeLoopDelay;
    [SerializeField] private float alphaFadeIntensity = 1;
    [SerializeField] private AnimationCurve alphaFadeCurve;

    [SerializeField] private bool scaleLoop;
    [SerializeField] private float scaleLoopDelay;
    [SerializeField] private float scaleIntensity = 1;
    [SerializeField] private Vector3 startScale;
    [SerializeField] private Vector3 endScale;
    [SerializeField] private AnimationCurve scaleCurve;

    [SerializeField] private bool positionLoop;
    [SerializeField] private float positionLoopDelay;
    [SerializeField] private float positionIntensity = 1;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private AnimationCurve positionCurve;

    [SerializeField] private bool rotationLoop;
    [SerializeField] private float rotationLoopDelay;
    [SerializeField] private float rotationIntensity = 1;
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private Vector3 endRotation;
    [SerializeField] private AnimationCurve rotationCurve;



    #endregion


    WaitForSeconds ws = new WaitForSeconds(0.01f);


    //에디터에서 실행하는 함수
    public void AutoSetUp()
    {
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<Text>();
        tmpTexts = GetComponentsInChildren<TMP_Text>();
        Debug.Log("AutoSetUp");
    }

    void OnEnable()
    {
        if (autoPlay)
        {
            Init();
            StartCoroutine(InitCoroutine());
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
        initHandler = () => { };
        exitHandler = () => { };
        endCount = 0;
    }

    public void Play()
    {
        Stop();
        Init();
        StartCoroutine(InitCoroutine());
    }

    public void Stop()
    {
        StopAllCoroutines();
        initHandler = () => { };
    }


    void Init()
    {
        initCount = 0;
        if (efx.HasFlag(Efx.Alpha))
        {
            initHandler += InitAlphaEfx;
        }
        if (efx.HasFlag(Efx.Scale))
        {
            initHandler += InitScaleEfx;
        }
        if (efx.HasFlag(Efx.Position))
        {
            initHandler += InitPositionEfx;
        }
        if (efx.HasFlag(Efx.Rotation))
        {
            initHandler += InitRotationEfx;
        }
        initHandler();
    }

    IEnumerator InitCoroutine()
    {
        //Procedural Images 때문에 1프레임 뒤에 동작
        yield return new WaitForEndOfFrame();

        if (efx.HasFlag(Efx.Alpha))
        {
            StartCoroutine(AlphaEfx());
        }
        if (efx.HasFlag(Efx.Scale))
        {
            StartCoroutine(ScaleEfx());
        }
        if (efx.HasFlag(Efx.Position))
        {
            StartCoroutine(PositionEfx());
        }
        if (efx.HasFlag(Efx.Rotation))
        {
            StartCoroutine(RotationEfx());
        }
    }


    void InitAlphaEfx()
    {
        Color color = new Color();

        float alpha = alphaFadeCurve.Evaluate(0);
        foreach (var img in images)
        {
            color = img.color;
            color.a = alpha;
            img.color = color;
        }

        foreach (var text in texts)
        {
            color = text.color;
            color.a = alpha;
            text.color = color;
        }

        foreach (var text in tmpTexts)
        {
            color = text.color;
            color.a = alpha;
            text.color = color;
        }

        if (!alphaFadeLoop)
        {
            initCount++;
        }
    }

    IEnumerator AlphaEfx()
    {
        float maxTime = alphaFadeCurve.keys[alphaFadeCurve.length - 1].time;
        float timer = 0;

        Color color = new Color();
        float alpha = 0;

        while (timer < maxTime)
        {
            timer += Time.deltaTime * alphaFadeIntensity;
            alpha = alphaFadeCurve.Evaluate(timer);

            for (int i = 0; i < images.Length; i++)
            {
                color = images[i].color;
                color.a = alpha;
                images[i].color = color;
            }
            for (int i = 0; i < texts.Length; i++)
            {
                color = texts[i].color;
                color.a = alpha;
                texts[i].color = color;
            }
            for (int i = 0; i < tmpTexts.Length; i++)
            {
                color = tmpTexts[i].color;
                color.a = alpha;
                tmpTexts[i].color = color;
            }
            yield return null;
        }

        if (alphaFadeLoop)
        {
            yield return new WaitForSeconds(alphaFadeLoopDelay);
            StartCoroutine(AlphaEfx());
        }
        else
        {
            CheckEndCallback();
        }

    }

    void InitScaleEfx()
    {
        transform.localScale = startScale;

        if (!scaleLoop)
        {
            initCount++;
        }
    }
    IEnumerator ScaleEfx()
    {
        float maxTime = scaleCurve.keys[scaleCurve.length - 1].time;
        float timer = 0;
        while (timer < maxTime)
        {
            timer += Time.deltaTime * scaleIntensity;
            transform.localScale = Vector3.Lerp(startScale, endScale, scaleCurve.Evaluate(timer));
            yield return null;
        }
        if (scaleLoop)
        {
            yield return new WaitForSeconds(scaleLoopDelay);
            StartCoroutine(ScaleEfx());
        }
        else
        {
            CheckEndCallback();
        }
    }

    void InitPositionEfx()
    {
        transform.position = startPosition;
        if (!positionLoop)
        {
            initCount++;
        }
    }
    IEnumerator PositionEfx()
    {
        float maxTime = positionCurve.keys[positionCurve.length - 1].time;
        float timer = 0;
        while (timer < maxTime)
        {
            timer += Time.deltaTime * positionIntensity;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, positionCurve.Evaluate(timer));
            yield return null;
        }
        if (positionLoop)
        {
            yield return new WaitForSeconds(positionLoopDelay);
            StartCoroutine(PositionEfx());
        }
        else
        {
            CheckEndCallback();
        }
    }

    void InitRotationEfx()
    {
        transform.eulerAngles = startRotation;
        if (!rotationLoop)
        {
            initCount++;
        }
    }
    IEnumerator RotationEfx()
    {
        float maxTime = rotationCurve.keys[rotationCurve.length - 1].time;
        float timer = 0;
        while (timer < maxTime)
        {
            timer += Time.deltaTime * rotationIntensity;
            transform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, rotationCurve.Evaluate(timer));
            yield return null;
        }
        if (rotationLoop)
        {
            yield return new WaitForSeconds(rotationLoopDelay);
            StartCoroutine(RotationEfx());
        }
        else
        {
            CheckEndCallback();
        }
    }



    void CheckEndCallback()
    {
        initCount--;
        if (initCount == 0)
        {
            efxCallback.Invoke();
        }
    }




    public void Exit()
    {
        Stop();

        if (efx.HasFlag(Efx.Alpha))
        {
            exitHandler += ExitAlphaEfx;
            endCount++;
        }
        if (efx.HasFlag(Efx.Scale))
        {
            exitHandler += ExitScaleEfx;
            endCount++;
        }
        if (efx.HasFlag(Efx.Position))
        {
            exitHandler += ExitPositionEfx;
            endCount++;
        }
        if (efx.HasFlag(Efx.Rotation))
        {
            exitHandler += ExitRotationEfx;
            endCount++;
        }
        exitHandler();

        StartCoroutine(ExitCoroutine());
    }


    IEnumerator ExitCoroutine()
    {
        //Procedural Images 때문에 1프레임 뒤에 동작
        yield return new WaitForEndOfFrame();

        if (efx.HasFlag(Efx.Alpha))
        {
            StartCoroutine(ExitAlphaEfxCo());
        }
        if (efx.HasFlag(Efx.Scale))
        {
            StartCoroutine(ExitScaleEfxCo());
        }
        if (efx.HasFlag(Efx.Position))
        {
            StartCoroutine(ExitPositionEfxCo());
        }
        if (efx.HasFlag(Efx.Rotation))
        {
            StartCoroutine(ExitRotationEfxCo());
        }
    }


    void ExitAlphaEfx()
    {
        Color color = new Color();

        float alpha = alphaFadeCurve.Evaluate(alphaFadeCurve.keys[alphaFadeCurve.length - 1].time);
        foreach (var img in images)
        {
            color = img.color;
            color.a = alpha;
            img.color = color;
        }

        foreach (var text in texts)
        {
            color = text.color;
            color.a = alpha;
            text.color = color;
        }

        foreach (var text in tmpTexts)
        {
            color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }

    IEnumerator ExitAlphaEfxCo()
    {
        float maxTime = alphaFadeCurve.keys[alphaFadeCurve.length - 1].time;
        float timer = 0;

        Color color = new Color();
        float alpha = 0;

        while (timer < maxTime)
        {
            maxTime -= Time.deltaTime * alphaFadeIntensity;
            alpha = alphaFadeCurve.Evaluate(maxTime);

            for (int i = 0; i < images.Length; i++)
            {
                color = images[i].color;
                color.a = alpha;
                images[i].color = color;
            }
            for (int i = 0; i < texts.Length; i++)
            {
                color = texts[i].color;
                color.a = alpha;
                texts[i].color = color;
            }
            for (int i = 0; i < tmpTexts.Length; i++)
            {
                color = tmpTexts[i].color;
                color.a = alpha;
                tmpTexts[i].color = color;
            }
            yield return null;
        }
        endCount--;
        if (endCount == 0)
        {
            endcallback();
        }
    }

    void ExitScaleEfx()
    {
        transform.localScale = endScale;
    }
    IEnumerator ExitScaleEfxCo()
    {
        float maxTime = scaleCurve.keys[scaleCurve.length - 1].time;
        float timer = 0;
        while (timer < maxTime)
        {
            maxTime -= Time.deltaTime * scaleIntensity;
            transform.localScale = Vector3.Lerp(startScale, endScale, scaleCurve.Evaluate(maxTime));
            yield return null;
        }
        endCount--;
        if (endCount == 0)
        {
            endcallback();
        }
    }

    void ExitPositionEfx()
    {
        transform.position = endPosition;
    }
    IEnumerator ExitPositionEfxCo()
    {
        float maxTime = positionCurve.keys[positionCurve.length - 1].time;
        float timer = 0;
        while (timer < maxTime)
        {
            maxTime -= Time.deltaTime * positionIntensity;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, positionCurve.Evaluate(maxTime));
            yield return null;
        }
        endCount--;
        if (endCount == 0)
        {
            endcallback();
        }
    }

    void ExitRotationEfx()
    {
        transform.eulerAngles = endRotation;
    }
    IEnumerator ExitRotationEfxCo()
    {
        float maxTime = rotationCurve.keys[rotationCurve.length - 1].time;
        float timer = 0;
        while (timer < maxTime)
        {
            maxTime -= Time.deltaTime * rotationIntensity;
            transform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, rotationCurve.Evaluate(maxTime));
            yield return null;
        }
        endCount--;
        if (endCount == 0)
        {
            endcallback();
        }
    }





}
