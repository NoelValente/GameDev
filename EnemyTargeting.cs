using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        public Transform targetSprite; // Assign the target sprite's transform in the editor

    void Update()
    {
        if (targetSprite != null)
        {
            Vector3 direction = targetSprite.position - transform.position; // Calculate the direction vector
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculate the angle in degrees
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Rotate the sprite to face the target
        }
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
