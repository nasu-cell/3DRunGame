using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ObstacleCrush : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Collectable") && !other.gameObject.CompareTag("Ground"))
        {
            Debug.Log(other.gameObject.tag);
            gameObject.GetComponentInParent<PlayerMovement>().isGamePlaying = false;
        }
    }
}