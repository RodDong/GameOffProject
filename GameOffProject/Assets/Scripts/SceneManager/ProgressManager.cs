using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public int currentProgress = 0;
    // Start is called before the first frame update
    public Node[] nodes;
    public class Node {
        int ID;
        public int[] next;
        public Node(int i) {
            ID = i;
            next = new int[4];
        }
    }

    public void transitionToNextState(int i) {
        currentProgress = nodes[currentProgress].next[i];
    }

    void Start()
    {
        // currentProgress to be read from json
        nodes = new Node[100];
        for (int i = 0; i < nodes.Length; i++) {
            nodes[i] = new Node(i);
        }
        nodes[0].next[0] = 1;
        nodes[1].next[0] = 2;
        nodes[1].next[0] = 3;
        nodes[1].next[0] = 4;
        nodes[1].next[0] = 5;
        nodes[2].next[0] = 1;
        nodes[2].next[0] = 1;
        nodes[0].next[0] = 1;
        nodes[0].next[0] = 1;
        nodes[0].next[0] = 1;
        nodes[0].next[0] = 1;
        nodes[0].next[0] = 1;
        nodes[0].next[0] = 1;
        nodes[0].next[0] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
