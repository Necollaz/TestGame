using UnityEngine;

namespace GameComponents.Scripts.ResourceComponents
{
    [RequireComponent(typeof(ResourceFall))]
    public class Resource : MonoBehaviour
    {
        private ResourceFall _fall;

        private void Awake()
        {
            _fall = GetComponent<ResourceFall>();
        }
        
        public void SpawnAt(Vector3 spawnPosition)
        {
            gameObject.SetActive(true);
            _fall.StartFall(spawnPosition);
        }
        
        public void Collect()
        {
            gameObject.SetActive(false);
        }
    }
}