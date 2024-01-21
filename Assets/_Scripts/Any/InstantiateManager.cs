using System;
using UnityEngine;

namespace _Scripts.Any
{
    public class InstantiateManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefab;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                foreach (var en in prefab)
                {
                    GameObject ob = Instantiate(en, en.transform.position, Quaternion.identity);
                    ob.transform.SetParent(transform.parent);
                }
                
                Destroy(gameObject);
                
            }
        }
    }
}