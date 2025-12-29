using UnityEngine;

namespace Salkogi07.CoreSystem
{
    public class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected Core core;

        protected virtual void Awake()
        {
            core = transform.parent.GetComponent<Core>();

            if (core == null)
            {
                Debug.LogError("해당 부모에는 코어가 없습니다.");
            }

            core.AddComponent(this);
        }
        
        public virtual void LogicUpdate() { }
    }
}