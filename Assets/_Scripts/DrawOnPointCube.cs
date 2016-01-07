using UnityEngine;
using System.Collections;

public class DrawOnPointCube : MonoBehaviour {
	
	public Transform ThisTransform;
	public Transform TargetTransform;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(ThisTransform.position, TargetTransform.localScale);
	}
}