using UnityEngine;

namespace GameComponents.Scripts.SettingsComponents
{
    [RequireComponent(typeof(MeshRenderer))]
    public class GpuInctancingEnabler : MonoBehaviour
    {
        private void Awake()
        {
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            
            meshRenderer.GetPropertyBlock(propertyBlock);
        }
    }
}