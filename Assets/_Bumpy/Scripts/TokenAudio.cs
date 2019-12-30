using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenAudio : MonoBehaviour
{
    [Header("Collision clips")]
    public List<AudioClip> hitCubeClips;
    public List<AudioClip> hitSphereClips;
    public List<AudioClip> hitSegmentClips;
    public List<AudioClip> hitFloorClips;

    [Header("Randomize pitch")]
    [Range(0f, 1f)]
    public float randomizePitchCube;
    [Range(0f, 1f)]
    public float randomizePitchSphere;
    [Range(0f, 1f)]
    public float randomizePitchSegment;
    [Range(0f, 1f)]
    public float randomizePitchFloor;
    [Range(0f, 0.8f)]
    public float pitchRangeDown;
    [Range(0f, 2f)]
    public float pitchRangeUp;

    private Dictionary<CollisionTag, List<AudioClip>> _hitClips;
    private Dictionary<CollisionTag, float> _randomizePitch;
    private Dictionary<string, CollisionTag> _tags;
    
    // Start is called before the first frame update
    void Start()
    {
        _hitClips = new Dictionary<CollisionTag, List<AudioClip>>()
        {
            { CollisionTag.Cube, hitCubeClips },
            { CollisionTag.Sphere, hitSphereClips },
            { CollisionTag.Segment, hitSegmentClips },
            { CollisionTag.Floor, hitFloorClips }
        };
        _randomizePitch = new Dictionary<CollisionTag, float>()
        {
            { CollisionTag.Cube, randomizePitchCube },
            { CollisionTag.Sphere, randomizePitchSphere },
            { CollisionTag.Segment, randomizePitchSegment },
            { CollisionTag.Floor, randomizePitchFloor }
        };
        _tags = new Dictionary<string, CollisionTag>() {
            { "Cube", CollisionTag.Cube },
            { "Sphere", CollisionTag.Sphere },
            { "Segment", CollisionTag.Segment },
            { "Floor", CollisionTag.Floor }
        };
    }

    public void PlayCollisionClip(string tagString, Vector3 position)
    {
        var tag = _tags[tagString];
        var clips = _hitClips[tag];
        var randomize = _randomizePitch[tag];

        var clip = clips[Random.Range(0, clips.Count)];
        var pitch = Random.Range(1f - pitchRangeDown * randomize, 1f + pitchRangeUp * randomize);

        AudioSourcePool.instance.PlayClip(clip, pitch, position);
    }
}
