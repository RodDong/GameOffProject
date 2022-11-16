using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue
{
    [SerializeField]
    private string clueName;
    private int clueId;
    private string clueContent;
    private string clueButtonSource = "Art/UI/Clues/";
    private string clueImageSource = "Art/UI/Clues/";

    public string Name { get { return clueName; } }
    public string Content { get { return clueContent; } }
    public int Id { get { return clueId; } }
    public string clueImgSrc { get { return clueImageSource; } }
    public string clueBtnSrc { get { return clueButtonSource; } }

    public Clue(int id)
    {
        clueId = id;
        switch (id)
        {
            case -1:
                clueName = "? ? ?";
                clueContent = "? ? ?";
                clueImageSource += "Clue_Not_Found";
                clueButtonSource += "Clue_Not_Found";
                break;
            case 0:
                clueName = "Clue 0";
                clueContent = "Content of Clue 0";
                clueImageSource += "Clue_0";
                clueButtonSource += "Clue_0";
                break;
            case 1:
                clueName = "Clue 1";
                clueContent = "Content of Clue 1";
                clueImageSource += "Clue_1";
                clueButtonSource += "Clue_1";
                break;
            default:
                clueName = "";
                clueContent = "";
                break;
        }
    }
}
