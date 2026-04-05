using UnityEngine;

namespace BasketBallTest.Gameplay
{
    /// <summary>
    /// Swap to a OnAirMaterial that should have no friction to let player slide while not Grounded
    /// </summary>
    public class MidairWallUnstickHandle : MonoBehaviour
    {
        [SerializeField]
        private Collider bodyCollider;
        [SerializeField]
        private GroundChecker groundChecker;
        [SerializeField]
        private PhysicsMaterial onGroundMaterial;
        [SerializeField]
        private PhysicsMaterial onAirMaterial;

        private void OnGroundCheckerUpdate(bool groundDetected)
        {
            var physicsMaterialToUse = groundDetected ? onGroundMaterial : onAirMaterial;
            bodyCollider.material = physicsMaterialToUse;
        }

        private void OnEnable()
        {
            groundChecker.OnCheckerUpdate += OnGroundCheckerUpdate;
        }

        private void OnDisable()
        {
            groundChecker.OnCheckerUpdate -= OnGroundCheckerUpdate;
        }
    }
}