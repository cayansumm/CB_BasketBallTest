using UnityEngine;

namespace BasketBallTest.Gameplay.Audio
{
    [CreateAssetMenu(fileName = "CollisionHitSoundData", menuName = "Project/Sounds/Collision Hit")]
    public class CollisionHitSoundData : ScriptableObject
    {
        [SerializeField]
        private AudioClip hitSound;

        [SerializeField]
        private float minHitSoundForceThreshold;
        [SerializeField]
        private float maxHitSoundForceThreshold;
        
        public AudioClip HitSound => hitSound;
        public float MinHitSoundForceThreshold => minHitSoundForceThreshold;
        public float MaxHitSoundForceThreshold => maxHitSoundForceThreshold;
    }
}