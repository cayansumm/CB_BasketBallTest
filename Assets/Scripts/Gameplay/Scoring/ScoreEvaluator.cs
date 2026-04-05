using System;
using BasketBallTest.Gameplay.Items;
using UnityEngine;

namespace BasketBallTest.Gameplay.Scoring
{
    /// <summary>
    /// To be placed in any Trigger that can gives Player points when the ball passes through
    /// </summary>
    public class ScoreEvaluator : MonoBehaviour
    {
        public delegate void ScoreGiven(int score);

        public event ScoreGiven OnScoreGiven;

        [SerializeField]
        private BasketBallHoopNet basketBallHoopNet;

        //For Debug purposes to check if scores where evaluated;
        private int totalScoreEvaluated;

        private int EvaluateScore(Ball other)
        {
            return 1;
        }

        private void OnObjectEnteredNet(GameObject objectentered)
        {
            var ball = objectentered.GetComponent<Ball>();
            if (ball == null)
                return;

            var score = EvaluateScore(ball);
            if (score > 0)
            {
                OnScoreGiven?.Invoke(score);
                totalScoreEvaluated += score;
            }
        }

        private void OnEnable()
        {
            basketBallHoopNet.OnObjectEntered += OnObjectEnteredNet;
        }

        private void OnDisable()
        {
            basketBallHoopNet.OnObjectEntered -= OnObjectEnteredNet;
        }
    }
}