using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2 position;
    public bool walkable = true;
    public Node cameFrom;
    public List<Node> connections = new List<Node>();
    public float hScore;
    public float gScore;

    public float FScore()
    {
        return gScore + hScore;
    }

}