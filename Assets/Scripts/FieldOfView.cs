using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    private Mesh mesh;
    private Vector3 origin;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        float fov = 90;
        origin = Vector3.zero;
        int rayCount = 50;
        float angle = 0;
        float angleIncrease = fov / rayCount;
        float viewDistance = 2.5f;
        
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2 [] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];
        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <=rayCount; i++)
        {
            Vector3 vertex ; 
            RaycastHit2D raycastHit2D= Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance,layerMask);
            if (raycastHit2D.collider == null)
            { 
                vertex  = origin + GetVectorFromAngle(angle) * viewDistance;
            }else {
                vertex = raycastHit2D.point;
            }
            

            vertices[vertexIndex] = vertex;
            if(i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
    }
    
    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }
    private void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    private void SetAimDirection(Vector3 aimDirection)
    {
        aimDirection.Normalize();
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        SetAimDirection(angle);
    }
}