using UnityEngine;

namespace Salkogi07.CoreSystem
{
    public class CollisionSenses : CoreComponent
    {
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
        
        public bool Ceiling {
            get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
        }

        public bool Ground {
            get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        }

        /*public bool LedgeHorizontal {
            get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
        }*/

        public bool LedgeVertical {
            get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
        }
    }
}