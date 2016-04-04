using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class NewEnemyAI : MonoBehaviour
{

    public Transform Target;
    public float UpdateRate = 2f;
    public Path Path;
    public float Speed;
    //The max distance from the AI to waypoint for it to continue to the next waypoint
    public float NextWayPointDistance = 3;


    [HideInInspector]
    public bool PathIsEnded = false;

    private Seeker _seeker;
    private CharacterController2D _controller;
    private int _currentWayPoint = 0;
    private Vector2 _startPosition;
    private Vector2 _direction;


    public void Start()
    {
        _seeker = GetComponent<Seeker>();
        _controller = GetComponent<CharacterController2D>();
        _startPosition = transform.position;

    }

    public void Update()
    {
        _controller.SetHorizontalForce(_direction.x * Speed);
        if ((_direction.x < 0 && _controller.State.IsCollidingLeft) || (_direction.x > 0 && _controller.State.IsCollidingRight))
        {
            _direction = -_direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}