using UnityEngine;

namespace Salkogi07.CoreSystem
{
    public class CollisionSenses : CoreComponent
    {
        private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

		private Movement movement;
        
        #region Check Transform

        public Transform GroundCheck {
            get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
            private set => groundCheck = value;
        }
        public Transform LedgeCheckHorizontal {
            get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
            private set => ledgeCheckHorizontal = value;
        }
        public Transform LedgeCheckVertical {
            get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
            private set => ledgeCheckVertical = value;
        }
        public Transform CeilingCheck {
            get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
            private set => ceilingCheck = value;
        }
        
        public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
        public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform ledgeCheckHorizontal;
        [SerializeField] private Transform ledgeCheckVertical;
        [SerializeField] private Transform ceilingCheck;

        [SerializeField] private float groundCheckRadius;
        [SerializeField] private float wallCheckDistance;

        [SerializeField] private LayerMask whatIsGround;
        
        #endregion
        
        [Header("Gizmos Settings")]
        [SerializeField] private bool showGizmos = true;
        [SerializeField] private Color gizmoColor = Color.red;
        
        public bool Ceiling {
            get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
        }

        public bool Ground {
            get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        }

        public bool LedgeHorizontal {
            get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
        }

        public bool LedgeVertical {
            get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
        }
        
        private void OnDrawGizmos()
        {
            if (!showGizmos) return;

            Gizmos.color = gizmoColor;

            // 1. Ground Check (Circle)
            if (groundCheck != null)
            {
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }

            // 2. Ceiling Check (Circle)
            if (ceilingCheck != null)
            {
                Gizmos.DrawWireSphere(ceilingCheck.position, groundCheckRadius);
            }

            // 3. Ledge Horizontal Check (Ray)
            if (ledgeCheckHorizontal != null && core != null)
            {
                // FacingDirection을 알기 위해 Movement 컴포넌트 참조 시도
                int direction = (Movement != null) ? Movement.FacingDirection : 1;
                Vector3 endPos = ledgeCheckHorizontal.position + (Vector3)(Vector2.right * direction * wallCheckDistance);
                Gizmos.DrawLine(ledgeCheckHorizontal.position, endPos);
            }

            // 4. Ledge Vertical Check (Ray)
            if (ledgeCheckVertical != null)
            {
                Vector3 endPos = ledgeCheckVertical.position + (Vector3)(Vector2.down * wallCheckDistance);
                Gizmos.DrawLine(ledgeCheckVertical.position, endPos);
            }
        }
    }
}