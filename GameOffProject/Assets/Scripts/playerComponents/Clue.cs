using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue
{
    [SerializeField]
    private string clueName;
    private int clueId;
    private string clueContent;

    public string Name { get { return clueName; } }
    public string Content { get { return clueContent; } }
    public int Id { get { return clueId; } }

    public Clue(int id)
    {
        clueId = id;
        switch (id)
        {
            case -1:
                clueName = "? ? ?";
                clueContent = "? ? ?";
                break;
            case 0:
                clueName = "Clue 0";
                clueContent = "Content of Clue 0";
                break;
            case 1:
                clueName = "Clue 1";
                clueContent = "Content of Clue 1";
                break;
            default:
                clueName = "";
                clueContent = "";
                break;
        }
    }
}
