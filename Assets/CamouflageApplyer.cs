using UnityEngine;
using System.Collections;

public class CamouflageApplyer : MonoBehaviour
{
	public GameObject _backgroundView = null;

	void Start ()
	{
		MeshFilter meshFilter = GetComponent<MeshFilter>();

		if ( _backgroundView != null )
		{
			// apply same material as background
			renderer.material = _backgroundView.renderer.material;

			meshFilter.mesh.uv = CamouflageUV( _backgroundView.transform, meshFilter.mesh );
		}
		else
		{
			Debug.Log("Background is not set");
		}
	}

	/*
	 * Create new UV map from background transform and mesh
	 */
	private Vector2[] CamouflageUV(Transform bgTransform,  Mesh appliedMesh)
	{
		Vector3 bgLeftBottom = bgTransform.position - bgTransform.transform.localScale * 10 * 0.5f;
		float bgSizeX = bgTransform.localScale.x * 10;
		float bgSizeY = bgTransform.localScale.y * 10;

		//
		int vertexCount = appliedMesh.vertexCount;
		Vector2[] newUV = new Vector2[vertexCount];
		
		for ( int vertexiter = 0; vertexiter < vertexCount; vertexiter++ )
		{
			Vector3 localVertexPosition = appliedMesh.vertices[vertexiter];
			
			// change localVertexPosition to gloabl coordinate
			Vector3 v = transform.TransformPoint(localVertexPosition);
			
			// uv from vertex and position offset
			newUV[vertexiter] = new Vector2( (v.x - bgLeftBottom.x) / bgSizeX, (v.y - bgLeftBottom.y) / bgSizeY );
		}
		
		return newUV;
	}
}
