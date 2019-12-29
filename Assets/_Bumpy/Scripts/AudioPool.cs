using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    public static AudioPool instance;

    [Header("Spatial sources")]
    public GameObject spatialSourcePrefab;
    public int spatialSourcesCapacity = 20;
    [Range(0.2f, 1f)]
    public float spatialSourceseVolume = 0.8f;
    private Queue<AudioSource> _spatialSources;

    [Header("Audio Clips")]
    [SerializeField]
    private List<AudioClip> _cubeClips;
    [SerializeField]
    private List<AudioClip> _sphereClips;
    [SerializeField]
    private List<AudioClip> _segmentClips;
    [SerializeField]
    private List<AudioClip> _floorClips;
    private Dictionary<AudioClipType, List<AudioClip>> _clipPools;
    [Header("Randomize pitch")]
    [Range(0.25f, 0.8f)]
    public float pitchMin;
    [Range(1.2f, 2f)]
    public float pitchMax;
    
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
        _spatialSources = new Queue<AudioSource>();
        for (int i = 0; i < spatialSourcesCapacity; i++)
        {
            var source = Instantiate(spatialSourcePrefab).GetComponent<AudioSource>();
            source.volume = spatialSourceseVolume;
            source.transform.parent = transform;
            _spatialSources.Enqueue(source);
        }
        _clipPools = new Dictionary<AudioClipType, List<AudioClip>>() {
            { AudioClipType.Cube, _cubeClips },
            { AudioClipType.Sphere, _sphereClips },
            { AudioClipType.Segment, _segmentClips },
            { AudioClipType.Floor, _floorClips }
        };
    }

    public void PlayClip(AudioClipType type, Vector3 position)
    {
        var source = _spatialSources.Dequeue();
        if(source.isPlaying)
        {
            source.Stop();
        }
        var randomIndex = Random.Range(0, _clipPools[type].Count);
        source.clip = _clipPools[type][randomIndex];
        source.pitch = Random.Range(pitchMin, pitchMax);
        source.transform.position = position;
        source.Play();
    }
}

public enum AudioClipType
{
    Cube,
    Sphere,
    Segment,
    Floor
}
