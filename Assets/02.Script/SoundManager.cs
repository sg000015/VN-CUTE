using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Inst;

    void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    public static AudioSource SFX
    {
        get
        {
            return Inst.sfxSouces;
        }
    }




    [SerializeField] private AudioSource sfxSouces;
    [SerializeField] private AudioSource bgmSouces;


    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] sfxClips;

    [SerializeField] private AudioClip[] efxClips;


    public void PlayBGM(AudioClip clip)
    {
        bgmSouces.clip = clip;
        bgmSouces.Play();
    }


    public void PlayBGM(int index)
    {
        bgmSouces.clip = bgmClips[index];
        bgmSouces.Play();
    }

    public void PlayBGM()
    {
        bgmSouces.Play();
    }

    public void StopBGM()
    {
        bgmSouces.Stop();
    }


    public void PlayBGMOnce(int index)
    {
        bgmSouces.PlayOneShot(bgmClips[index]);
    }


    public void PlaySfx(int index, float volume = 1)
    {
        if (index == 2)
        {
            volume *= 0.8f;
        }
        sfxSouces.PlayOneShot(sfxClips[index], volume);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSouces.PlayOneShot(clip);
    }


    public void PlayEfx(int index, float volume = 1)
    {
        sfxSouces.PlayOneShot(efxClips[index], volume);
    }

}
