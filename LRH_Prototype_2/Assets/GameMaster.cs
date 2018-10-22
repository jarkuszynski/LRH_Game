using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public Transform BasicFloor;
    public Transform CrossHoleFloor;
    public Transform BigCrossHoleFloor;
    public Transform SatelliteHoleFloor;
    public Transform BaseCactus;
    public Transform Perk;
    public Transform Orb_Player1;
    public Transform Orb_Player2;

    private Quaternion noRotate = new Quaternion(0, 0, 0, 0);
    List<Transform> PossibleFloors = new List<Transform>();
    Queue<Transform> ListOfFloors = new Queue<Transform>();

    private Transform orbInstancePlayer1;
    private Transform orbInstancePlayer2;

    //private Random random = new Random();
    private void PopulatePossibleFloorsList()
    {
        PossibleFloors.Add(BasicFloor);
        PossibleFloors.Add(CrossHoleFloor);
        PossibleFloors.Add(BigCrossHoleFloor);
        PossibleFloors.Add(SatelliteHoleFloor);
    }

    private Transform GetRandomBlock()
    {
        int randInt = Random.Range(0, 4);
        return PossibleFloors[randInt];
    }

    // Use this for initialization
    void Start()
    {
        orbInstancePlayer1 = Instantiate(Orb_Player1, new Vector3(-1, 0, 0), noRotate);
        orbInstancePlayer2 = Instantiate(Orb_Player2, new Vector3(1, 0, 0), noRotate);
        ListOfFloors.Enqueue(Instantiate(BasicFloor, new Vector3(0, 0, 0), noRotate));
        ListOfFloors.Enqueue(Instantiate(BasicFloor, new Vector3(0, 0, 5), noRotate));

        PopulatePossibleFloorsList();
        for (int i = 2; i < 10; i++)
        {
            ListOfFloors.Enqueue(Instantiate(GetRandomBlock(), new Vector3(0, 0, i * 5), noRotate));
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (orbInstancePlayer1 == null)
        {
            orbInstancePlayer1 = orbInstancePlayer2;    
        }

        if (orbInstancePlayer1.position.z > ListOfFloors.Peek().position.z + 20)
        {
            var firstFloor = ListOfFloors.Dequeue();
            Destroy(firstFloor.gameObject);
            ListOfFloors.Enqueue(Instantiate(GetRandomBlock(), new Vector3(0, 0, orbInstancePlayer1.position.z + 25), noRotate));
        }
    }
}