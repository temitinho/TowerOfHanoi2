using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegScript : MonoBehaviour
{
    public string pegName = "pegFrom";
    public GameObject[] discPositions;
    public GameObject door;
    public List<GameObject> discs;
    public int PegNumber;

    private float pegPosX;
    private float pegPosY;
    private float pegPosZ;
    
    void Start ()
    {
        discs = new List<GameObject>();
        pegPosX = transform.position.x;
        pegPosY = transform.position.y;
        pegPosZ = transform.position.z;
    }
     
    public void InitializePeg()
    {
        EmptyPegPos(discPositions);
    }
    public void InitializeFromPeg(int numDiscs)
    {
        InitializeFromPeg2(discPositions, numDiscs);
    }
    void InitializeFromPeg2(GameObject[] positions, int numDiscs)
    {
        EmptyPegPos(discPositions);
        for (int i = 0; i < numDiscs; i++)
        {
            positions[i].GetComponent<PositionScript>().isEmpty = false;
        }
    }
    void EmptyPegPos(GameObject[] positions)
    {
        foreach (var item in positions)
        {
            item.GetComponent<PositionScript>().isEmpty = true; 
        }
    }
    public void SetDiscPositionToEmpty()
    {
        if (discs.Count > 0)
        {
            discPositions[discs.Count - 1].GetComponent<PositionScript>().isEmpty = true;
        }
        else
        {
            Debug.Log("DiscPosition Count < 0");
        }
    }
    public GameObject GetDiscToMove()
    {
        GameObject disc;
        if (discs.Count > 0)
        {
            discPositions[discs.Count - 1].GetComponent<PositionScript>().isEmpty = true;
            disc = discs[discs.Count - 1];
            discs.Remove(disc);
            return disc;
        }
        else
        {
            Debug.Log("Disc Count < 0");
        }
        return null;
    }
    
    public Vector3 FindNextpPosition()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        bool isEmpty;
        if (discs.Count > 0)
        {
            for (int i = discs.Count - 1; i > -1; i--)
            {
                isEmpty = discPositions[i].GetComponent<PositionScript>().isEmpty;
                if (isEmpty)
                {
                    float posY = discPositions[i].transform.position.y;

                    pos = new Vector3(pegPosX, posY, pegPosZ);
                    discPositions[i].GetComponent<PositionScript>().isEmpty = false;
                    return pos;
                    //break;
                }
            }
        }else
        {
            float posY = discPositions[0].transform.position.y;

            pos = new Vector3(pegPosX, posY, pegPosZ);
            discPositions[0].GetComponent<PositionScript>().isEmpty = false;
            return pos;
        }
        return pos;
    }
}
