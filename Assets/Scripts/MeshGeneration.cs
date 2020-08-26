using UnityEditor;
using UnityEngine;

public class MeshGeneration : MonoBehaviour
{
   private Mesh generatedMesh;
   [SerializeField] private Vector2Int subdivisions;

   [SerializeField] private Shape shape;
   private void Start()
   {
      generateMesh();
   }
   public void generateMesh() 
   {
      generatedMesh = new Mesh();
      subdivisions = new Vector2Int(20, 240); //for helix 10 x 200
      var vertexSize = subdivisions + new Vector2Int(1, 1);

      var vertices = new Vector3[vertexSize.x * vertexSize.y];
      var uvs = new Vector2[vertices.Length];

      for (var y = 0; y < vertexSize.y; y++)
      {
        var v = (1f/ subdivisions.y) * y;
         
         for (var x = 0; x < vertexSize.x; x++)
         {
            var u = (1f / subdivisions.x) * x;
            var vertex = Form(u, v); //vertex postion is dependant on parametric function
           //you can choose between the functions in the Form method 
            var uv = new Vector2(u, v);

            var arrayIndex = x + y * vertexSize.x;
            vertices[arrayIndex] = vertex;
            uvs[arrayIndex] = uv;
         }
      }

      var triangles = new int[subdivisions.x * subdivisions.y * 6];

      for (var i = 0; i < subdivisions.x * subdivisions.y; i += 1)
      {
         var triangleIndex = (i % subdivisions.x) + (i / subdivisions.x) * vertexSize.x;
         var indexer = i * 6;

         triangles[indexer + 0] = triangleIndex;
         triangles[indexer + 1] = triangleIndex + subdivisions.x + 1;
         triangles[indexer + 2] = triangleIndex + 1;

         triangles[indexer + 3] = triangleIndex + 1;
         triangles[indexer + 4] = triangleIndex + subdivisions.x + 1;
         triangles[indexer + 5] = triangleIndex + subdivisions.x + 2;
         
      }

      generatedMesh.vertices = vertices;
      generatedMesh.uv = uvs;
      generatedMesh.triangles = triangles;

      generatedMesh.RecalculateBounds();
      generatedMesh.RecalculateNormals();
      generatedMesh.RecalculateTangents();
      
      //only works in Editor, obstructs Build
      /*
      AssetDatabase.CreateAsset(generatedMesh, "Assets/Meshes/hyperboloid.asset"); //Serialisierung des Mesh assets
      AssetDatabase.SaveAssets();*/
      GetComponent<MeshFilter>().mesh = generatedMesh;
   }
   
   private Vector3 Form (float u, float v)
   {
      if (shape == Shape.Rocket)
      {
         //Abtastung nach umin,umax und vmin, vmax
         u = u * 2 * Mathf.PI - Mathf.PI;
         v = v * 2 * Mathf.PI - Mathf.PI;
         // gibt die richtig berechneten xyz Werte zurück 
         return new Vector3(
            u,
            v,
            Mathf.Pow(u, 2) + Mathf.Pow(v, 2)
         );
      }
      if (shape == Shape.Helix)
      {
         u = u * 3 - 1; // u * umax - umin
         v = v * 4 * Mathf.PI - (-4 * Mathf.PI); // v * vmax - vmin
         return new Vector3(
            u * Mathf.Cos(v),
            u * Mathf.Sin(v),
            v / 4f
         );
      }
      if (shape == Shape.Torus)
      {
         u = u * 2 * Mathf.PI;
         v = v * 2 * Mathf.PI;
         return new Vector3(
            (4 + Mathf.Cos(u)) * Mathf.Cos(v),
            (4 + Mathf.Cos(u)) * Mathf.Sin(v),
            Mathf.Sin(u)
         );
      }
      if (shape == Shape.Hyperboloid)
      {
         u = u * 2 * Mathf.PI;
         v = v  * 4 ;//* 2 - (-2);
         return new Vector3(
            Mathf.Sqrt(v * v ) * Mathf.Cos(u),
            Mathf.Sqrt(v * v ) * Mathf.Sin(u),
            v
         );
      }
      if (shape == Shape.Parabelschnecke)
      {
         u = u * 2;
         v = v * 8 * Mathf.PI;
         return new Vector3(
            (-1 * u*u + 2)* Mathf.Exp(0.17f*v) * Mathf.Cos(v),
            (-1 * u*u + 2)* Mathf.Exp(0.17f*v) * Mathf.Sin(v),
            30 * u
         );
      }
      return new Vector3();
   }
   
}
