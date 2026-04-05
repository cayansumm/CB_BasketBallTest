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

        //For Debug purposes to check if scores where evaluated;
        private int totalScoreEvaluated;

        private int EvaluateScore(Ball other)
        {
            return 1;
        }
        
        private bool IsBallGoingInFromTheAbove(Ball ball, Rigidbody ballBody)
        {
            var toBallDirection = (ball.transform.position - transform.position).normalized;
            var toBallDotProduct = Vector3.Dot(transform.up, toBallDirection);
            Debug.Log(
                $"transform.up: {transform.up},toBallDirection {toBallDirection}, ToBallDotProduct: {toBallDotProduct}",
                gameObject);
            var isBallFromTheTop = toBallDotProduct > 0;
            if (isBallFromTheTop == false)
                return false;

            var ballNormalLinearVelocity = ballBody.linearVelocity.normalized;
            var ballVelocityDotProduct = Vector3.Dot(transform.up, ballNormalLinearVelocity);
            var isBallGoingToTheBottom = ballVelocityDotProduct < 0;
            Debug.Log($"ToBallVelocityDotProduct: {ballVelocityDotProduct}", gameObject);
            return isBallGoingToTheBottom;
        }

        public void OnTriggerEnter(Collider other)
        {
            var ball = other.attachedRigidbody.GetComponent<Ball>();
            if (ball == null)
                return;

            if (IsBallGoingInFromTheAbove(ball, other.attachedRigidbody) == false)
                return;

            var score = EvaluateScore(ball);
            if (score > 0)
            {
                OnScoreGiven?.Invoke(score);
                totalScoreEvaluated += score;
            }
        }
    }
}