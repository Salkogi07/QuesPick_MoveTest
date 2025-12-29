using System.Collections.Generic;
using System.Linq;
using Salkogi07.CoreSystem;
using UnityEngine;

namespace Salkogi07.CoreSystem
{
    public class Core : MonoBehaviour
    {
        [field: SerializeField] public GameObject Root { get; private set;}
        private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();
        
        private void Awake()
        {
            Root = Root ? Root : transform.parent.gameObject;
        }
    
        public void LogicUpdate()
        {
            foreach (CoreComponent component in CoreComponents)
            {
                component.LogicUpdate();
            }
        }
        
        public void AddComponent(CoreComponent component)
        {
            if (!CoreComponents.Contains(component))
            {
                CoreComponents.Add(component);
            }
        }
    
        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var comp = CoreComponents.OfType<T>().FirstOrDefault();

            if (comp)
                return comp;

            comp = GetComponentInChildren<T>();
            
            if(comp)
                return comp;
            
            Debug.LogWarning($"{transform.parent.name}에서 {typeof(T)} 컴포넌트를 찾지 못했습니다.");
            return null;
        }

        public T GetCoreComponent<T>(ref T value) where T : CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }
    }
}

