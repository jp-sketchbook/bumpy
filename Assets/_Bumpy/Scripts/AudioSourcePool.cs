using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public static AudioSourcePool instance;

    [Header("Audio Sources")]
    public GameObject sourcePrefab;
    public int capacity = 20;
    [Range(0.2f, 1f)]
    public float sourceVolume = 0.8f;
    private Queue<AudioSource> _sources;

    void Start()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        _sources = new Queue<AudioSource>();
        for (int i = 0; i < capacity; i++)
        {
            var source = Instantiate(sourcePrefab).GetComponent<AudioSource>();
            source.volume = sourceVolume;
            source.transform.parent = transform;
            _sources.Enqueue(source);
        }
    }

    public void PlayClip(AudioClip clip, float pitch, Vector3 position)
    {
        var source = _sources.Dequeue();
        if(source.isPlaying)
        {
            source.Stop();
        }
        source.clip = clip;
        source.pitch = pitch;
        source.transform.position = position;
        source.Play();
        _sources.Enqueue(source);
    }
}
