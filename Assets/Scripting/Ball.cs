using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// Creates a list of the tags used when the ball enters the collision of another object
    /// </summary>
    public enum CollisonTag
    {
        ScoreWall,
        BounceWall,
        Player
    }

    [SerializeField] private float speed = 8f;
    [SerializeField] private List<string> tags;
    [SerializeField] private string otherTag;
    private Vector2 direction;

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
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    /// <summary>
    /// Resets the ball back to the center of the screen, and gives the ball a new velocity in a new direction
    /// </summary>
    private void ResetBall()
    {
        transform.position = Vector2.zero;
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tags[(int) CollisonTag.ScoreWall])) //Checks to see if the ball enters the Score zone boundaries, and resets if so
        {
            ResetBall();
            GameManager.IncrementScore(other.GetComponent<ScoreWall>().scoringPlayer);
        }
        else if (other.CompareTag(tags[(int) CollisonTag.BounceWall])) //Checks to see if the ball hits the top or bottom wall, and bounce the ball in the opposite direction
        {
            direction.y = -direction.y;
        }
        else if (other.CompareTag(tags[(int) CollisonTag.Player])) //Checks to see if the ball hits a player's paddle, and bounces the ball in the opposite direction 
        {
            direction.x = -direction.x;
            direction.y = transform.position.y - other.transform.position.y;
            direction = direction.normalized;
        }
    }
}
