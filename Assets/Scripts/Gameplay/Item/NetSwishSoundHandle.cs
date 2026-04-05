using BasketBallTest.Gameplay.Items;
using BasketBallTest.Gameplay.Scoring;
using UnityEngine;

namespace BasketBallTest.Gameplay
{
    public class NetSwishSoundHandle : MonoBehaviour
    {
        [SerializeField]
        private BasketBallHoopNet scoreEvaluator;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip audioClip;
        
        private void OnObjectEnteredNet(GameObject objectentered)
        {
            audioSource.PlayOneShot(audioClip);
        }
        
        private void Start()
        {
            scoreEvaluator.OnObjectEntered += OnObjectEnteredNet;
        }
    }
}