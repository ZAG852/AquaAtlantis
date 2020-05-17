using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private void Start()
	{
		
	}
	private void Update()
	{
        if(Grid.loaded && target != null)
		PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
        if(target == null && timer <= 0)
        {
            StopCoroutine("FollowPath");
        }
        if (timer > 0)
            timer -= Time.deltaTime;
        

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
