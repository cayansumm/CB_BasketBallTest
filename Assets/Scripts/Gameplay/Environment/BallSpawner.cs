using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BasketBallTest.Gameplay.Environment
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject ball;

        [SerializeField]
        private Transform spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            //Improve player check when you have time. Use Physics Layers
            var isPlayer = other.attachedRigidbody?.CompareTag("Player");
            var isPlayerBodyCollider = other.isTrigger == false;
            if (isPlayer == false || isPlayerBodyCollider == false)
                return;

            //Use Object when you have the time to change this
            var spawnedBall = Instantiate(ball, spawnPoint.position, Quaternion.identity);

            //This is to prevent the balls from stacking
            var randomTorque = Random.Range(-100, 100);
            spawnedBall.GetComponent<Rigidbody>().AddTorque(Vector3.right *randomTorque, ForceMode.Impulse);
        }
    }
}