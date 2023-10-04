using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioClipController : MonoBehaviour
    {
        public List<AudioClip> clips = new ();
        
        private readonly List<int> _priority = new ();
        private int _clipIndex;

        private void Awake()
        {
            for(var i = 0; i < clips.Count; i++)
                _priority.Add(3);
            _clipIndex = -1;
        }

        public AudioClip GetNextClip(bool shuffle = true)
        {
            AudioClip nextClip;

            if (!shuffle)
            {
                nextClip = _clipIndex == clips.Count - 1
                    ? clips[0]
                    : clips[_clipIndex + 1];
            }
            else
            {
                var randomList = new List<int>();

                for (var i = 0; i < _priority.Count; i++)
                {
                    if (_priority[i] == 0) continue;

                    for (var j = 0; j < _priority[i]; j++)
                        randomList.Add(i);
                }

                nextClip = clips[randomList[Random.Range(0, randomList.Count - 1)]];

                for (var i = 0; i < _priority.Count; i++)
                    _priority[i] = clips[i] == nextClip ? 0 : _priority[i] + 1;
            }

            _clipIndex = clips.IndexOf(nextClip);
            return nextClip;
        }
    }
}
