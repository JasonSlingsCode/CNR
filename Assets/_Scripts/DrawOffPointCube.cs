using UnityEngine;
using System.Collections;

public class DrawOffPointCube : MonoBehaviour {
	
	public Transform ThisTransform;
	public Transform TargetTransform;
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(ThisTransform.position, TargetTransform.localScale);
	}
}
