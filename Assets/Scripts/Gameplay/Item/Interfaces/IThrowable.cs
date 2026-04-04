using UnityEngine;

namespace BasketBallTest.Gameplay.Items
{
    public interface IThrowable
    {
        Transform transform { get; }

        void Throw(Vector3 linearForce, Vector3 torque);
        void ResetState();
        void SetStateToHeld();
    }
}