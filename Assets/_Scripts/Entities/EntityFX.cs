using System.Collections;
using UnityEngine;

namespace _Scripts.Entities
{
    public class EntityFX : MonoBehaviour
    {
        public SpriteRenderer[] spriteRenderers;

        [Header("Efek Kedip")]
        [SerializeField] private Material hitMat;

        [SerializeField] private float durasiKedip = 0.1f;
        private Material originMat;


        private void Start()
        {
            if (spriteRenderers != null)
            {
                foreach (var sr in spriteRenderers)
                {
                    originMat = sr.material;
                }
            }
        
        }


        IEnumerator KedipFX()
        {
            if (spriteRenderers != null)
            {
                foreach (var sr in spriteRenderers)
                {
                    sr.material = hitMat;
                    yield return new WaitForSeconds(durasiKedip);
                    sr.material = originMat;
                }
            }
        }
    }
}
