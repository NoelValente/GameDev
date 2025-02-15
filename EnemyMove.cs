using UnityEngine;
public class EnemyMove : MonoBehaviour
{
    private Transform target;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {//Looks through gameobjects and finds one with the tag "player" and retreives its position by the Transform
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
