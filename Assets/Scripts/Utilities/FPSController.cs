using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        private const int MaxFramesToCalculate = 60;
        private static float _lastFPSCalculated;
        private readonly List<float> _frameTimes = new ();
 
        public static float GetCurrentFPS() => _lastFPSCalculated;
 
        private void Awake()
        {
            _lastFPSCalculated = 0f;
            _frameTimes.Clear();
        }
 
        private void Update()
        {
            AddFrame();
            _lastFPSCalculated = CalculateFPS();
            text.text = Mathf.RoundToInt(_lastFPSCalculated).ToString();
        }
 
        private void AddFrame()
        {
            _frameTimes.Add(Time.deltaTime);
            
            while (_frameTimes.Count > MaxFramesToCalculate)
                _frameTimes.RemoveAt(0);
        }
 
        private float CalculateFPS()
        {
            var newFPS = 0f;
 
            var totalTimeOfAllFrames = _frameTimes.Sum();
            newFPS = _frameTimes.Count / totalTimeOfAllFrames;
 
            return newFPS;
        }
    }
}