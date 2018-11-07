using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject[] Pegs;
    public GameObject disc;
    public Material[] materialsDiscs;
    public int numDiscs = 3;
    public SortedList<int, Moves> moves;
    public Transform discsHolder;
    public UImanager UImanager;
    

    Transform doorExit;
    Transform doorEntry;
    private Moves move;
    private IEnumerator currentMoveCoroutine;
    private float speed = 20;
    private GameObject discToMove;
    private Vector3 posToMoveDisc;
    private Transform tempTrans;
    private GameObject[] discs;
    private int calls;

    void Start()
    {
        instance = this;
        Pegs[0].GetComponent<PegScript>().pegName = "pegFrom";
        Pegs[1].GetComponent<PegScript>().pegName = "pegAux";
        Pegs[2].GetComponent<PegScript>().pegName = "pegTo";
        moves = new SortedList<int, Moves>();
        discs = new GameObject[10];
        //InitializePegs(Pegs);
        //SolveTowerOfHanoi(numDiscs, "pegFrom", "pegTo", "pegAux");
        //PrintMoves();
        //MoveTheDiscs();
        Debug.Log(moves.Count);
    }
    void PrintMoves()
    {
        for (int i = 1; i < moves.Count + 1; i++)
        {
            Debug.Log("move from = " + moves[i].moveFrom + " to " + moves[i].moveTo);
        }
    }
    public void InitializeGame()
    {
        //moves.Clear();
        //moves.TrimExcess();
        InitializePegs(Pegs);
        SolveTowerOfHanoi(numDiscs, "pegFrom", "pegTo", "pegAux");
        
    }
    public void StartPlay()
    {
        MoveTheDiscs();
    }
    void InitializePegs(GameObject[] pegs)
    {
        foreach (var item in pegs)
        {
            item.GetComponent<PegScript>().discs.Clear();
        }
        GameObject[] objs = GameObject.FindGameObjectsWithTag("disc");
        foreach (var item in objs)
        {
            Destroy(item);
        }
        foreach (var item in pegs)
        {
            var name =  item.GetComponent<PegScript>().pegName;
            item.GetComponent<PegScript>().InitializePeg();
            if (name == "pegFrom")
            {
                item.GetComponent<PegScript>().InitializeFromPeg(numDiscs);
                for (int i = 0; i < numDiscs; i++)
                {
                    tempTrans = item.GetComponent<PegScript>().discPositions[i].transform;
                    GameObject tempDisc = Instantiate(disc, new Vector3( tempTrans.position.x, tempTrans.position.y, tempTrans.position.z), Quaternion.identity) as GameObject;
                    tempDisc.GetComponent<DiscScript>().discSize = numDiscs - i;
                    tempDisc.GetComponentInChildren<Renderer>().material.color = materialsDiscs[i].color;
                    tempDisc.transform.localScale = new Vector3(1 -((float)i/10), 1 , 1 - ((float)i / 10));
                    item.GetComponent<PegScript>().discs.Add(tempDisc);
                }
            }
        }
    }

    void SolveTowerOfHanoi(int size, string pegFrom, string pegTo, string pegAux)
    {
        if (size == 1)
        {
            calls++;
            //print(" Call = " + calls + "( Size = " + size + " )  Move Disc from " + pegFrom + " To " + pegTo);
            move = new Moves(pegFrom, pegTo);
            moves.Add(calls, move);
        }
        else
        {
            SolveTowerOfHanoi(size - 1, pegFrom, pegAux, pegTo);
            SolveTowerOfHanoi(1, pegFrom, pegTo, pegAux);
            SolveTowerOfHanoi(size - 1, pegAux, pegTo, pegFrom);
        }
    }
    void MoveTheDiscs()
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }
        currentMoveCoroutine = StartTheMoves();
        StartCoroutine(currentMoveCoroutine);
    }
    
    IEnumerator StartTheMoves()
    {
        for (int i = 1; i < moves.Count + 1; i++)
        {
            FindDiscToMove(moves[i].moveFrom, moves[i].moveTo);
            UImanager.moves.text = "Moving Disc " + discToMove.GetComponent<DiscScript>().discSize + " " + moves[i].moveFrom + " " + moves[i].moveTo;
            yield return StartCoroutine(GoToNextPosition(discToMove, doorExit.position));
            yield return StartCoroutine(GoToNextPosition(discToMove, doorEntry.position));
            yield return  StartCoroutine(GoToNextPosition(discToMove, posToMoveDisc));
        }
        Debug.Log("complete!!");
        UImanager.HideMoves();
        UImanager.ShowCompletedPanel();
    }
    void FindDiscToMove(string fromStack, string toStack)
    {
        for (int i = 0; i < Pegs.Length; i++)
        {
            if (Pegs[i].GetComponent<PegScript>().pegName == fromStack)
            {
                discToMove = Pegs[i].GetComponent<PegScript>().GetDiscToMove();
                doorExit = Pegs[i].GetComponent<PegScript>().door.transform;
                continue;
            }
        }

        for (int i = 0; i < Pegs.Length; i++)
        {
            if (Pegs[i].GetComponent<PegScript>().pegName == toStack)
            {
                Pegs[i].GetComponent<PegScript>().discs.Add(discToMove);
                posToMoveDisc = Pegs[i].GetComponent<PegScript>().FindNextpPosition();
                doorEntry = Pegs[i].GetComponent<PegScript>().door.transform;
                continue;
            }
        }
    }
    IEnumerator GoToNextPosition(GameObject discToMove, Vector3 posToMove)
    {
        GameObject obj = discToMove;
        while (true)
        {
            obj.transform.position = Vector3.MoveTowards(discToMove.transform.position, posToMove, speed * Time.deltaTime);
            if (obj.transform.position == posToMove)
            {
                yield break;
            }
            yield return null;
        }
    }
}
