using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
using UnityEngine.Networking;

public class StoryPlayer : MonoBehaviour
{

    [Header("Default")]
    public VideoPlayer videoPlayer;
    public string videoUrl = "file://E:/UnityProject/My-Hero-Build/Assets/StreamingAssets/001.mp4";
    public GameObject loading;
    public Button playButton;


    [ContextMenu("Play")]
    public virtual void Play()
    {
        if (string.IsNullOrEmpty(videoUrl))
        {
            loading.SetActive(false);
            playButton.gameObject.SetActive(true);

            playButton.onClick.AddListener(() =>
            {
                playButton.gameObject.SetActive(false);
                Invoke("Excute", 1.0f);
            });
            return;
        }

        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoUrl);
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (a) =>
        {
            loading.SetActive(false);
            playButton.gameObject.SetActive(true);

            playButton.onClick.AddListener(() =>
            {
                playButton.gameObject.SetActive(false);
                Invoke("Excute", 1.0f);
            });
        };
    }


    protected virtual void Excute()
    {

    }


    protected IEnumerator LoadImageFromStreamingAssets(string fileName, System.Action<Sprite> sprite)
    {
        string url = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);

                Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                sprite(s);
            }
            else
            {
                Debug.LogError($"Failed to load image: {uwr.error}");
            }
        }

    }
}
