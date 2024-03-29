using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SoundType {
    CLICK = 0,
    SOLVED = 1,
}

public struct Range {
    private readonly float min;
    private readonly float max;

    public Range(float min, float max) {
        this.min = min;
        this.max = max;
    }

    public float GetRandValue() {
        return Random.Range(min, max);
    }
}

public class SoundCollection {
    private AudioClip[] clips;
    private int lastClipIndex;

    public SoundCollection(params string[] audioNames) {
        clips = new AudioClip[audioNames.Length];
        for (int i = 0; i < clips.Length; i++) {
            clips[i] = Resources.Load(audioNames[i]) as AudioClip;
            if (clips[i] == null) {
                Debug.LogWarning($"Couldn't find clip {audioNames[i]}");
            }
        }
        lastClipIndex = -1;
    }

    public AudioClip GetRandClip() {
        if (clips.Length == 0) {
            Debug.LogWarning($"No clips found");
            return null;
        }
        else if (clips.Length == 1) {
            return clips[0];
        }
        else {
            int index = Random.Range(0, clips.Length);
            return clips[index];
        }
    }
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
    private Dictionary<SoundType, SoundCollection> sounds;
    private AudioSource audioSrc;
    private Range rangeVol;
    private Range rangePitch;

    public static SoundManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
    void Start() {
        audioSrc = GetComponent<AudioSource>();
        sounds = new()
        {
            {SoundType.CLICK, new("clicks/click_1", "clicks/click_2", "clicks/click_3", "clicks/click_4") },
            {SoundType.SOLVED, new("solved")}
        };
        rangeVol = new(0.75f, 1.0f);
        rangePitch = new(0.75f, 1.25f);
    }

    public void Play(SoundType type, AudioSource audioSrc = null) {
        if (sounds.ContainsKey(type)) {
            if (audioSrc == null) {
                this.audioSrc.volume = rangeVol.GetRandValue();
                this.audioSrc.pitch = rangePitch.GetRandValue();
                this.audioSrc.clip = sounds[type].GetRandClip();
                this.audioSrc.Play();
            }
            else {
                audioSrc.volume = rangeVol.GetRandValue();
                audioSrc.pitch = rangePitch.GetRandValue();
                audioSrc.clip = sounds[type].GetRandClip();
                audioSrc.Play();
            }
        }
    }
    public void Play(string type, AudioSource audioSrc) {
        SoundType soundType = Enum.Parse<SoundType>(type, true);
        Play(soundType, audioSrc);
    }
    public void Play(string type) {
        Play(type, audioSrc);
    }
    public void Play(int type) {
        Play((SoundType)type, audioSrc);
    }
}

