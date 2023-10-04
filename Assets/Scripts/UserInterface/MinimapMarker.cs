using System;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    [Serializable]
    public enum MinimapMarkerType
    {
        None,
        Player,
        QuestGiver,
        CustomWaypoint,
        QuestWaypoint,
        Enemy,
        BossEnemy,
        SpecialItem,
    }
    public class MinimapMarker : MonoBehaviour
    {
        [SerializeField] private MinimapMarkerType minimapMarkerType;
        [SerializeField] private RawImage icon;

        [Header("Texture References")]
        [SerializeField] private Texture playerIcon;
        [SerializeField] private Texture questGiverIcon;

        private void OnEnable() => SetMinimapMarker();

        public void SetMinimapMarker(MinimapMarkerType type)
        {
            minimapMarkerType = type;
            SetMinimapMarker();
        }

        private void SetMinimapMarker()
        {
            icon.texture = minimapMarkerType switch
            {
                MinimapMarkerType.Player => playerIcon,
                MinimapMarkerType.QuestGiver => questGiverIcon,
                _ => icon.texture
            };
        }
    }
}