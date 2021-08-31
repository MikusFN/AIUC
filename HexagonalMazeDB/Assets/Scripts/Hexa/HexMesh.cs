using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Hexa;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{

    public GameObject prefab;


    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;
    List<Vector3> wallPositions;

    MeshCollider meshCollider;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        wallPositions = new List<Vector3>();
    }

    public void Triangulate(HexCell[] cells, Wall[] walls)
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        int j = 0;
        for (int i = 0; i < cells.Length; i++, j++)
        {
            Triangulate(cells[i], walls, ref j);
        }
        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();
        meshCollider.sharedMesh = hexMesh;
    }

    void Triangulate(HexCell cell, Wall[] wall, ref int j)
    {
        Vector3 center = cell.transform.localPosition;
        float angle = -30;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + HexMetrics.corners[i],
                center + HexMetrics.corners[i + 1]
            );
            int pos = 0;
            foreach (var item in wallPositions)
            {
                if (item == (center + (HexMetrics.corners[i] - HexMetrics.corners[i + 1] * 0.5f) + Vector3.up * 2.5f))
                    pos++;
            }
            if (pos == 0)
            {
                if (cell.hexaDirections.Count != 0 && cell.visited)
                {

                    switch (cell.hexaDirections.Count)
                    {
                        case 3:
                            Debug.Log("im here 3!");
                            foreach (var item in wallPositions)
                            {
                                if (item == (center + (HexMetrics.corners[i] - HexMetrics.corners[i + 1] * 0.5f) + Vector3.up * 2.5f))
                                    pos++;
                            }
                            if ((cell.hexaDirections[1] != (HexaDirection)i && cell.hexaDirections[0] != (HexaDirection)i) && (cell.hexaDirections[2] != (HexaDirection)i && pos == 0))
                            {
                                GameObject gbWall = Instantiate(prefab, center + (HexMetrics.corners[i] - HexMetrics.corners[i + 1] * 0.5f) + Vector3.up * 2.5f,
                    Quaternion.AngleAxis(angle, transform.up));
                                Wall newWall = new Wall();
                                newWall.prefab = gbWall;
                                wall[i] = newWall;
                                wallPositions.Add(gbWall.transform.position);
                            }
                            break;
                        case 2:
                            Debug.Log("im here 2!");
                            foreach (var item in wallPositions)
                            {
                                if (item == (center + (HexMetrics.corners[i] - HexMetrics.corners[i + 1] * 0.5f) + Vector3.up * 2.5f))
                                    pos++;
                            }
                            if ((cell.hexaDirections[1] != (HexaDirection)i && cell.hexaDirections[0] != (HexaDirection)i) && pos == 0)
                            {
                                GameObject gbWall = Instantiate(prefab, center + (HexMetrics.corners[i] - HexMetrics.corners[i + 1] * 0.5f) + Vector3.up * 2.5f,
                    Quaternion.AngleAxis(angle, transform.up));
                                Wall newWall = new Wall();
                                newWall.prefab = gbWall;
                                wall[i] = newWall;
                                wallPositions.Add(gbWall.transform.position);
                            }
                            break;
                        case 1:
                            Debug.Log("im here 1!");
                            if (cell.hexaDirections[0] != (HexaDirection)i)
                            {
                                GameObject gbWall = Instantiate(prefab, center + (HexMetrics.corners[i] - HexMetrics.corners[i + 1] * 0.5f) + Vector3.up * 2.5f,
                         Quaternion.AngleAxis(angle, transform.up));
                                Wall newWall = new Wall();
                                newWall.prefab = gbWall;
                                wall[i] = newWall;
                                wallPositions.Add(gbWall.transform.position);
                            }
                            break;
                    }
                }
            }
            angle += 60;
            j++;
        }

    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }
}