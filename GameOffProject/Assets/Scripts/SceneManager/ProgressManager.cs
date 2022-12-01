using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public int date = 1;
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
        nodes[1].next[2] = 36; // boss dead
        nodes[1].next[3] = 60;
        nodes[2].next[0] = 3;
        nodes[2].next[1] = 4; // doctor dead
        nodes[2].next[2] = 5;
        nodes[2].next[3] = 6; // chef dead

        nodes[4].next[0] = 7;
        nodes[4].next[2] = 8; // lust dead
        nodes[4].next[3] = 9; // chef dead
        nodes[5].next[3] = 13;
        nodes[5].next[1] = 14;
        nodes[6].next[1] = 17;
        nodes[6].next[2] = 19;
        nodes[6].next[0] = 21;
        nodes[7].next[3] = 10; // lust dead
        nodes[8].next[2] = 11;
        nodes[9].next[3] = 12; // lust dead
        
        nodes[13].next[1] = 15;
        nodes[14].next[3] = 16;

        nodes[17].next[1] = 18;

        nodes[19].next[2] = 20;

        nodes[21].next[1] = 18;
        nodes[22].next[2] = 23;
        nodes[22].next[0] = 24;
        nodes[22].next[3] = 25;
        nodes[23].next[0] = 72;
        nodes[23].next[3] = 66;
        nodes[24].next[2] = 26;
        nodes[24].next[3] = 28;
        nodes[25].next[3] = 30;
        nodes[25].next[0] = 31;
        nodes[25].next[2] = 32;
        nodes[26].next[3] = 27;

        nodes[28].next[2] = 20;

        nodes[30].next[0] = 35;
        nodes[31].next[2] = 20;
        nodes[32].next[0] = 34;

        nodes[34].next[0] = 35;

        nodes[36].next[0] = 42;
        nodes[36].next[1] = 41;
        nodes[36].next[2] = 69;
        nodes[36].next[3] = 38;        

        nodes[38].next[0] = 39;
        nodes[38].next[1] = 40;
        nodes[39].next[0] = 52;
        nodes[40].next[0] = 55;
        nodes[41].next[0] = 46;
        nodes[41].next[2] = 44;
        nodes[42].next[0] = 43;
        nodes[43].next[0] = 48;
        nodes[43].next[2] = 49;
        nodes[44].next[0] = 45;
        
        nodes[46].next[2] = 47;

        nodes[48].next[1] = 50;
        nodes[49].next[2] = 20;

        nodes[52].next[0] = 53;
        nodes[53].next[2] = 54;

        nodes[55].next[0] = 56;

        nodes[57].next[3] = 58;

        nodes[60].next[0] = 61;
        nodes[60].next[1] = 62;
        nodes[60].next[3] = 63;
        nodes[61].next[1] = 57;
        nodes[61].next[3] = 64;
        nodes[62].next[3] = 66;
        nodes[63].next[0] = 39;
        nodes[63].next[1] = 40;
        nodes[64].next[1] = 65;

        nodes[66].next[0] = 67;

        nodes[69].next[0] = 70;
        nodes[69].next[1] = 71;
        nodes[70].next[1] = 73;
        nodes[71].next[0] = 72;
        nodes[72].next[3] = 58;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
