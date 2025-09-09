using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;



[CustomEditor(typeof(UIAnimationEfx))]
public class UIAnimationEditor : Editor
{

    static GUIStyle labelMenuStyle, labelBoldStyle, labelNormalStyle, sectionHeaderBoldStyle, sectionHeaderNormalStyle, sectionHeaderIndentedStyle;


    #region Default Settings

    SerializedProperty _efx;
    SerializedProperty _efxCallback;
    SerializedProperty _autoPlay;
    SerializedProperty _images;
    SerializedProperty _texts;
    SerializedProperty _tmpTexts;

    #endregion


    private bool alphaFold;
    SerializedProperty _alphaLoop;
    SerializedProperty _alphaIntensity;
    SerializedProperty _alphaDelay;
    SerializedProperty _alphaCurve;

    private bool scaleFold;
    SerializedProperty _scaleLoop;
    SerializedProperty _scaleIntensity;
    SerializedProperty _scaleDelay;
    SerializedProperty _scaleStart;
    SerializedProperty _scaleEnd;
    SerializedProperty _scaleCurve;

    private bool positionFold;
    SerializedProperty _positionLoop;
    SerializedProperty _positionIntensity;
    SerializedProperty _positionDelay;
    SerializedProperty _positionStart;
    SerializedProperty _positionEnd;
    SerializedProperty _positionCurve;

    private bool rotateFold;
    SerializedProperty _rotationLoop;
    SerializedProperty _rotationIntensity;
    SerializedProperty _rotationDelay;
    SerializedProperty _rotationStart;
    SerializedProperty _rotationEnd;
    SerializedProperty _rotationCurve;



    void OnEnable()
    {
        _efxCallback = serializedObject.FindProperty("efxCallback");
        _efx = serializedObject.FindProperty("efx");
        _autoPlay = serializedObject.FindProperty("autoPlay");
        _images = serializedObject.FindProperty("images");
        _texts = serializedObject.FindProperty("texts");
        _tmpTexts = serializedObject.FindProperty("tmpTexts");


        alphaFold = false;
        _alphaLoop = serializedObject.FindProperty("alphaFadeLoop");
        _alphaIntensity = serializedObject.FindProperty("alphaFadeIntensity");
        _alphaDelay = serializedObject.FindProperty("alphaFadeLoopDelay");
        _alphaCurve = serializedObject.FindProperty("alphaFadeCurve");

        scaleFold = false;
        _scaleLoop = serializedObject.FindProperty("scaleLoop");
        _scaleIntensity = serializedObject.FindProperty("scaleIntensity");
        _scaleDelay = serializedObject.FindProperty("scaleLoopDelay");
        _scaleStart = serializedObject.FindProperty("startScale");
        _scaleEnd = serializedObject.FindProperty("endScale");
        _scaleCurve = serializedObject.FindProperty("scaleCurve");

        positionFold = false;
        _positionLoop = serializedObject.FindProperty("positionLoop");
        _positionIntensity = serializedObject.FindProperty("positionIntensity");
        _positionDelay = serializedObject.FindProperty("positionLoopDelay");
        _positionStart = serializedObject.FindProperty("startPosition");
        _positionEnd = serializedObject.FindProperty("endPosition");
        _positionCurve = serializedObject.FindProperty("positionCurve");

        rotateFold = false;
        _rotationLoop = serializedObject.FindProperty("rotationLoop");
        _rotationIntensity = serializedObject.FindProperty("rotationIntensity");
        _rotationDelay = serializedObject.FindProperty("rotationLoopDelay");
        _rotationStart = serializedObject.FindProperty("startRotation");
        _rotationEnd = serializedObject.FindProperty("endRotation");
        _rotationCurve = serializedObject.FindProperty("rotationCurve");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // setup styles
        if (labelMenuStyle == null)
        {
            labelMenuStyle = new GUIStyle(EditorStyles.label); // GUI.skin.label);
            labelMenuStyle.fontStyle = FontStyle.Bold;
            labelMenuStyle.fontSize = 15;
        }
        if (labelBoldStyle == null)
        {
            labelBoldStyle = new GUIStyle(EditorStyles.label); // GUI.skin.label);
            labelBoldStyle.fontStyle = FontStyle.Bold;
        }
        if (labelNormalStyle == null)
        {
            labelNormalStyle = new GUIStyle(EditorStyles.label); // GUI.skin.label);
        }
        if (sectionHeaderNormalStyle == null)
        {
            sectionHeaderNormalStyle = new GUIStyle(EditorStyles.foldout);
        }
        sectionHeaderNormalStyle.margin = new RectOffset(12, 0, 0, 0);
        if (sectionHeaderBoldStyle == null)
        {
            sectionHeaderBoldStyle = new GUIStyle(sectionHeaderNormalStyle);
        }
        sectionHeaderBoldStyle.fontStyle = FontStyle.Bold;
        if (sectionHeaderIndentedStyle == null)
        {
            sectionHeaderIndentedStyle = new GUIStyle(EditorStyles.foldout);
        }
        sectionHeaderIndentedStyle.margin = new RectOffset(24, 0, 0, 0);


        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(_efx, new GUIContent("Effect Type"));
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(_autoPlay, new GUIContent("Play on Enable"));
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(_efxCallback, new GUIContent("Efx Callback"));
        EditorGUILayout.Space(10);


        if (EditorGUILayout.LinkButton("Auto Match (Image & Text)", GUILayout.Height(20)))
        {
            target.GetType().GetMethod("AutoSetUp").Invoke(target, null);
        }

        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(_images, new GUIContent("images"));
        EditorGUILayout.PropertyField(_texts, new GUIContent("texts"));
        EditorGUILayout.PropertyField(_tmpTexts, new GUIContent("tmpTexts"));
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("❣ Effect ❣ (curve must be in range 0~1)", labelMenuStyle);
        EditorGUILayout.Space(5);

        GUIStyle labelStyle;
        labelStyle = alphaFold ? sectionHeaderBoldStyle : sectionHeaderNormalStyle;
        alphaFold = EditorGUILayout.Foldout(alphaFold, new GUIContent("Alpha", "Alpha Effect"), labelStyle);

        if (alphaFold)
        {
            EditorGUILayout.PropertyField(_alphaLoop, new GUIContent("Refeat"));
            if (_alphaLoop.boolValue)
            {
                EditorGUILayout.PropertyField(_alphaDelay, new GUIContent("Refeat Delay"));
            }
            EditorGUILayout.PropertyField(_alphaIntensity, new GUIContent("Simulation Speed"));
            EditorGUILayout.PropertyField(_alphaCurve, new GUIContent("Curve"));
        }
        EditorGUILayout.Space(5);

        labelStyle = scaleFold ? sectionHeaderBoldStyle : sectionHeaderNormalStyle;
        scaleFold = EditorGUILayout.Foldout(scaleFold, new GUIContent("Scale", "Scale Effect"), labelStyle);
        if (scaleFold)
        {
            EditorGUILayout.PropertyField(_scaleLoop, new GUIContent("Refeat"));
            if (_scaleLoop.boolValue)
            {
                EditorGUILayout.PropertyField(_scaleDelay, new GUIContent("Refeat Delay"));
            }
            EditorGUILayout.PropertyField(_scaleIntensity, new GUIContent("Simulation Speed"));
            EditorGUILayout.PropertyField(_scaleStart, new GUIContent("Start"));
            EditorGUILayout.PropertyField(_scaleEnd, new GUIContent("End"));
            EditorGUILayout.PropertyField(_scaleCurve, new GUIContent("Curve"));
        }
        EditorGUILayout.Space(5);

        labelStyle = positionFold ? sectionHeaderBoldStyle : sectionHeaderNormalStyle;
        positionFold = EditorGUILayout.Foldout(positionFold, new GUIContent("Position", "Position Effect"), labelStyle);
        if (positionFold)
        {
            EditorGUILayout.PropertyField(_positionLoop, new GUIContent("Refeat"));
            if (_positionLoop.boolValue)
            {
                EditorGUILayout.PropertyField(_positionDelay, new GUIContent("Refeat Delay"));
            }
            EditorGUILayout.PropertyField(_positionIntensity, new GUIContent("Simulation Speed"));
            EditorGUILayout.PropertyField(_positionStart, new GUIContent("Start"));
            EditorGUILayout.PropertyField(_positionEnd, new GUIContent("End"));
            EditorGUILayout.PropertyField(_positionCurve, new GUIContent("Curve"));
        }
        EditorGUILayout.Space(5);

        labelStyle = rotateFold ? sectionHeaderBoldStyle : sectionHeaderNormalStyle;
        rotateFold = EditorGUILayout.Foldout(rotateFold, new GUIContent("Rotate", "Rotate Effect"), labelStyle);
        if (rotateFold)
        {
            EditorGUILayout.PropertyField(_rotationLoop, new GUIContent("Refeat"));
            if (_rotationLoop.boolValue)
            {
                EditorGUILayout.PropertyField(_rotationDelay, new GUIContent("Refeat Delay"));
            }
            EditorGUILayout.PropertyField(_rotationIntensity, new GUIContent("Simulation Speed"));
            EditorGUILayout.PropertyField(_rotationStart, new GUIContent("Start"));
            EditorGUILayout.PropertyField(_rotationEnd, new GUIContent("End"));
            EditorGUILayout.PropertyField(_rotationCurve, new GUIContent("Curve"));
        }
        EditorGUILayout.Space(5);

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
    }

}

#endif