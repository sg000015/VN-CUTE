using UnityEngine;
using UnityEngine.UI;

public class Girl : MonoBehaviour
{


    [SerializeField] Sprite[] face;
    [SerializeField] Sprite[] cloth;
    [SerializeField] Sprite[] deco;

    [SerializeField] Image faceImage;
    [SerializeField] Image clothImage;
    [SerializeField] Image decoImage;


    public void ChangeFace(int index)
    {
        if (index >= 0 && index < face.Length)
        {
            faceImage.sprite = face[index];
        }
    }

    public void ChangeCloth(int index)
    {
        if (index >= 0 && index < cloth.Length)
        {
            clothImage.sprite = cloth[index];
        }
    }

    public void ChangeDeco(int index)
    {
        if (index >= 0 && index < deco.Length)
        {
            decoImage.sprite = deco[index];
        }
    }

}
