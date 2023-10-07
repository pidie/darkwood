using TMPro;
using UnityEngine;

namespace Utilities
{
    [RequireComponent(typeof(SpinInPlace))]
    public class SolarClock : MonoBehaviour
    {
        [Tooltip("One minute of real time is equal to this many minutes of in-game time")]
        [SerializeField] private float timeScale = 30f;
        [SerializeField] private int startTimeOfDay = 6;
        [SerializeField] private TMP_Text timeDisplay;

        private SpinInPlace _spinner;

        private float SpinRatio => _spinner.GetSpeed.x;

        private void Awake() => _spinner = GetComponent<SpinInPlace>();

        private void Start()
        {
            Globals.TIMESCALE = timeScale;
            _spinner.SetSpeed(timeScale / 12000f);
            var eulerAngles = transform.eulerAngles;
            eulerAngles =
                new Vector3(15 * (startTimeOfDay - 6), eulerAngles.y, eulerAngles.z);
            transform.eulerAngles = eulerAngles;
        }

        private void Update()
        {
            var eulerAngles = transform.eulerAngles;

            transform.eulerAngles = eulerAngles.x switch
            {
                >= 360 => new Vector3(eulerAngles.x - 360, eulerAngles.y, eulerAngles.z),
                < 0 => new Vector3(eulerAngles.x + 360, eulerAngles.y, eulerAngles.z),
                _ => eulerAngles
            };

            timeDisplay.text = GetCurrentTime();
        }

        private string GetCurrentTime()
        {
            var xAngle = transform.eulerAngles.x;
            var currentHour = (int)Mathf.Floor(xAngle / 15);
            currentHour = currentHour + 6 > 23
                ? currentHour - 18
                : currentHour + 6;
            var currentMinute = (int)Mathf.Floor(xAngle % 15 * 4);
            
            return $"{currentHour:D2}:{currentMinute:D2}";
        }
    }
}