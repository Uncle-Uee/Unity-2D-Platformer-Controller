using UnityEngine;

namespace PlatCtrl2D
{
    public class ResetPlayer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                collider.transform.position = new Vector3(0, 3, 0);
            }
        }
    }
}