using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    string sceneName;
     Scene m_Scene;
     float song1;
    private void Awake() {
        if(instance==null){
            instance=this;
        }
        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds){
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip=s.clip;
            s.source.volume=s.volume;
            s.source.pitch=s.pitch;
            s.source.loop=s.loop;
        }
    }
    private void Start() {
        Play("Theme");
    }
    public void Play(string name){
        Sound s =Array.Find(sounds,sound=>sound.name==name);
        if(s==null)
            return;
        s.source.Play();
    }
    public void StopPlaying (string name)
    {
    Sound s = Array.Find(sounds, sound => sound.name == name);
    if (s == null)
    {
    Debug.LogWarning("Sound: " + name + " not found!");
    return;
    }

    s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume / 2f));
    s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitch / 2f, s.pitch / 2f));

    s.source.Stop ();
    }

}
