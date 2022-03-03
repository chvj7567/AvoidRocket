using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Audio.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Audio");
        if (root == null)
        {
            root = new GameObject { name = "@Audio" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Audio));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Audio.Bgm].loop = true;
        }
    }

    public void Play(string path, Define.Audio type, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);

        if (type == Define.Audio.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Audio.Bgm];

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Audio.Explosion];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, Define.Audio type)
    {
        if (path.Contains("Audio/") == false)
            path = $"Audio/{path}";

        AudioClip audioClip = null;

        if (type == Define.Audio.Bgm)
        {
            audioClip = MasterManager.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = MasterManager.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing! {path}");

        return audioClip;
    }
}