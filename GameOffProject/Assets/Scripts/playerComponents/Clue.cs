using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue
{
    private string clueName;
    private int clueId;
    private string clueDescription;
    private string clueImageSource = "Art/UI/Clues/";

    public string getClueName() {
        return clueName;
    }

    public string getClueDescription() {
        return clueDescription;
    }

    public int getClueId() {
        return clueId;
    }

    public string getClueImageSrc() {
        return clueImageSource;
    }

    public Clue(int clueId)
    {
        this.clueId = clueId;
        switch (clueId)
        {
            case -1:
                clueName = "???";
                clueDescription = "no description";
                clueImageSource += "Clue_Not_Found";
                break;
            case 0:
                clueName = "Clue 0";
                clueDescription = "Content of Clue 0";
                clueImageSource += "Clue_0";
                break;
            case 1:
                clueName = "Clue 1";
                clueDescription = "Content of Clue 1";
                clueImageSource += "Clue_1";
                break;
            case 2:
                clueName = "Clue 2";
                clueDescription = "Content of Clue 2";
                clueImageSource += "Clue_2";
                break;
            case 3:
                clueName = "Clue 3";
                clueDescription = "Content of Clue 3";
                clueImageSource += "Clue_3";
                break;
            case 4:
                clueName = "Clue 4";
                clueDescription = "Content of Clue 4";
                clueImageSource += "Clue_4";
                break;
            case 5:
                clueName = "Clue 5";
                clueDescription = "Content of Clue 5";
                clueImageSource += "Clue_5";
                break;
            case 6:
                clueName = "Clue 6";
                clueDescription = "Content of Clue 6";
                clueImageSource += "Clue_6";
                break;
            case 7:
                clueName = "Clue 7";
                clueDescription = "Content of Clue 7";
                clueImageSource += "Clue_7";
                break;
            case 8:
                clueName = "Clue 8";
                clueDescription = "Content of Clue 8";
                clueImageSource += "Clue_8";
                break;
            case 9:
                clueName = "Clue 9";
                clueDescription = "Content of Clue 9";
                clueImageSource += "Clue_9";
                break;
            case 10:
                clueName = "Clue 10";
                clueDescription = "Content of Clue 10";
                clueImageSource += "Clue_10";
                break;
            case 11:
                clueName = "Clue 11";
                clueDescription = "Content of Clue 11";
                clueImageSource += "Clue_11";
                break;
            case 12:
                clueName = "Clue 12";
                clueDescription = "Content of Clue 12";
                clueImageSource += "Clue_12";
                break;
            default:
                clueName = "";
                clueDescription = "";
                break;
        }
    }
}
