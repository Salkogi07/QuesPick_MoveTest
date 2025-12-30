using Salkogi07.Interaction;
using UnityEngine;

namespace Salkogi07.Utilities
{
    public static class ComponentUtilities
    {
        public static bool IsInteractable(this Component component, out IInteractable interactable)
        {
            return component.TryGetComponent(out interactable);
        }
    }
}