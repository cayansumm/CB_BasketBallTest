using System.Collections.Generic;
using BasketBallTest.Gameplay.Items;
using UnityEngine;

namespace BasketBallTest.Gameplay.Audio
{
    public class CollisionHitSoundHandle : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody rigidbody;

        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private CollisionHitSoundData soundData;

        [SerializeField, Tooltip("For Threshold Calculation will use velocity of other gameObject instead of self")]
        private bool useOtherVelocity;

        [SerializeField,
         Tooltip("Only colliders listed here will trigger the sound, IF Empty, filter will not be use.")]
        private List<Collider> selfColliderFilter;

        private void OnCollisionEnter(Collision collision)
        {
            if (selfColliderFilter.Count > 0)
            {
                if (selfColliderFilter.Contains(collision.GetContact(0).thisCollider) == false)
                    return;
            }

            float force = 0f;
            if (useOtherVelocity)
            {
                force = collision.rigidbody.linearVelocity.sqrMagnitude;
            }
            else
            {
                force = rigidbody.linearVelocity.sqrMagnitude;
            }

            Debug.Log($"Force: {force}", gameObject);

            if (force > soundData.MinHitSoundForceThreshold)
            {
                audioSource.PlayOneShot(soundData.HitSound);
                audioSource.volume = Mathf.Clamp(force / soundData.MaxHitSoundForceThreshold, 0f, 1f);
            }
        }
    }
}