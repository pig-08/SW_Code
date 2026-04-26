using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomObject : MonoBehaviour
{
    [SerializeField] private Vector3 createSize;
    [SerializeField] private int createCount;
    [SerializeField] private GameObject[] newcreateObjectList;

    private List<GameObject> createObjectList = new List<GameObject>();

    [ContextMenu("CreateObject")]
    public void CreateObject()
    {
        DestroyObject();

        int randomObjectIndex = 0;
        Vector3 objectPoint;

        for (int i = 0; i < createCount; i++)
        {
            randomObjectIndex = Random.Range(0, newcreateObjectList.Length);
            objectPoint = new Vector3(
               /*Mathf.Abs*/(Random.Range(transform.position.x, createSize.x)),
               /*Mathf.Abs*/(Random.Range(transform.position.y, createSize.y)),
               /*Mathf.Abs*/(Random.Range(transform.position.z, createSize.z)));

            GameObject newObject = Instantiate(newcreateObjectList[randomObjectIndex],transform);
            newObject.transform.position = objectPoint;
            createObjectList.Add(newObject);
        }
    }

    [ContextMenu("DestroyObject")]
    public void DestroyObject()
    {
        if (createObjectList.Count > 0)
        {
            for (int i = 0; i < createObjectList.Count; ++i)
                DestroyImmediate(createObjectList[i]);
        }

        createObjectList.Clear();
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 oldMatrix = Gizmos.matrix;

        // 부모의 Transform 영향 제거

        Gizmos.color = Color.yellow;


        Vector3 center = (transform.localPosition + createSize) * 0.5f;

        Vector3 size = new(
            Mathf.Abs(createSize.x - transform.localPosition.x),
            Mathf.Abs(createSize.y - transform.localPosition.y),
            Mathf.Abs(createSize.z - transform.localPosition.z));

        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.DrawWireCube(center, size);
        Gizmos.matrix = oldMatrix;
    }
}
