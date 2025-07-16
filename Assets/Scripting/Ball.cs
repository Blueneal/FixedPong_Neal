using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private List<string> tags;
    [SerializeField] private string otherTag;
    private Vector2 velocity;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioScore;
    [SerializeField] private AudioClip audioPlayer;
    [SerializeField] private AudioClip audioWall;

    /// <summary>
    /// gives the ball velocity, and finds a direction on a random point on the two axis and normalizing the speed to keep the movement clean
    /// </summary>
    void Start()
    {
        transform.position = Vector2.zero;
        velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreWall")) //Checks to see if the ball enters the Score zone boundaries, and resets if so
        {
            ResetBall();
        }
        else if (other.CompareTag("BounceWall")) //Checks to see if the ball hits the top or bottom wall, and bounce the ball in the opposite direction
        {
            velocity.y = -velocity.y;
        }
        else if (other.CompareTag("Player")) //Checks to see if the ball hits a player's paddle, and bounces the ball in the opposite direction 
        {
            velocity.x = -velocity.x;
            velocity.y = transform.position.y - other.transform.position.y;
            velocity = velocity.normalized;
        }
    }
}
