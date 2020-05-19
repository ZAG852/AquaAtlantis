using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static playerBehavior;
public class Unit : MonoBehaviour
{
	public Transform target;
    [SerializeField]
	float speed = 5;
	Vector3[] path;
	int targetIndex;
    [SerializeField]
    Transform player;
    float timer = 0;
    [SerializeField]
    float memory = 0.4f;
    [SerializeField]
    float Range = 6;
    public bool isRanged = false;
    float stoppingRange = 4;
    bool lookingRight = false;
    private void Start()
	{
		if(isRanged)
        {
            stoppingRange = 4;
        }
        else
        {
            stoppingRange = 0.8f;
        }
	}
	private void Update()
	{
        if (target != null)
        {
            if (!lookingRight && target.transform.position.x - gameObject.transform.position.x > 0)
            {
                flip();
            }
            if (lookingRight && target.transform.position.x - gameObject.transform.position.x < 0)
            {
                flip();
            }
        }
        enemyMovement();

	}
    void enemyMovement()
    {
        if (Grid.loaded && target != null &&  Vector3.Distance(current.transform.position, transform.position) > stoppingRange)
            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
        if (target == null && timer <= 0)
        {
            StopCoroutine("FollowPath");
        }
        if (timer > 0)
            timer -= Time.deltaTime;
        if (target == null && Vector3.Distance(current.transform.position, transform.position) <= Range)
        {
            setTarget(playerBehavior.current.gameObject.transform);
        }
        if (target != null && Vector3.Distance(current.transform.position, transform.position) > Range)
        {
            nullTarget();
        }
        if (Vector3.Distance(current.transform.position, transform.position) < stoppingRange)
        {
            StopCoroutine("FollowPath");
        }
    }
    void flip()
    {
        lookingRight = !lookingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path[0];
		while (true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;

		}
	}
	public void OnDrawGizmos()
	{
		if(path!= null)
		{
			for(int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);
				
				if(i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}
    public void setTarget(Transform obj)
    {
        target = obj;
    }
    public void nullTarget()
    {
        target = null;
        timer = memory;
    }
    
}
