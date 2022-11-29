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
            next = new int[4]{-1,-1,-1,-1};
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
        nodes[1].next[1] = 22;
        nodes[1].next[2] = 36;
        nodes[1].next[3] = 60;
        nodes[2].next[0] = 3;
        nodes[2].next[1] = 4;
        nodes[2].next[2] = 5;
        nodes[2].next[3] = 6;

        nodes[4].next[0] = 7;
        nodes[4].next[1] = 8;
        nodes[4].next[2] = 9;
        nodes[5].next[0] = 13;
        nodes[5].next[1] = 14;
        nodes[6].next[0] = 17;
        nodes[6].next[1] = 19;
        nodes[6].next[2] = 21;
        nodes[7].next[0] = 10;
        nodes[8].next[0] = 11;
        nodes[9].next[0] = 12;
        
        nodes[13].next[0] = 15;
        nodes[14].next[0] = 16;

        nodes[17].next[0] = 18;

        nodes[19].next[0] = 20;

        nodes[21].next[0] = 18;
        nodes[22].next[0] = 23;
        nodes[22].next[1] = 24;
        nodes[22].next[2] = 25;

        nodes[24].next[0] = 26;
        nodes[24].next[1] = 28;
        nodes[25].next[0] = 30;
        nodes[25].next[1] = 31;
        nodes[25].next[2] = 32;
        nodes[26].next[0] = 27;

        nodes[28].next[0] = 20;

        nodes[30].next[0] = 35;
        nodes[31].next[0] = 20;
        nodes[32].next[0] = 34;

        nodes[34].next[0] = 35;

        nodes[36].next[0] = 38;
        nodes[36].next[1] = 41;
        nodes[36].next[2] = 42;
        nodes[36].next[3] = 69;        

        nodes[38].next[0] = 39;
        nodes[38].next[1] = 40;
        nodes[39].next[0] = 52;
        nodes[40].next[0] = 55;
        nodes[41].next[0] = 44;
        nodes[41].next[1] = 46;
        nodes[42].next[0] = 43;
        nodes[43].next[0] = 48;
        nodes[43].next[1] = 49;
        nodes[44].next[0] = 45;
        
        nodes[46].next[0] = 47;

        nodes[48].next[0] = 50;
        nodes[49].next[0] = 20;

        nodes[52].next[0] = 53;
        nodes[53].next[0] = 54;

        nodes[55].next[0] = 56;

        nodes[57].next[0] = 58;

        nodes[60].next[0] = 61;
        nodes[60].next[1] = 62;
        nodes[60].next[2] = 63;
        nodes[61].next[0] = 64;
        nodes[61].next[1] = 57;
        nodes[62].next[0] = 66;
        nodes[63].next[0] = 39;
        nodes[63].next[1] = 40;
        nodes[64].next[0] = 65;

        nodes[66].next[0] = 67;

        nodes[69].next[0] = 70;
        nodes[69].next[1] = 71;
        nodes[70].next[0] = 73;
        nodes[71].next[0] = 72;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
