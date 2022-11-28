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
                clueName = "Chef's Phone";
                clueDescription = "A black, clean design. Probably going to cost many times your monthly paycheck.";
                clueImageSource += "Clue_0";
                break;
            case 1:
                clueName = "Chef's Guest List";
                clueDescription = "A list of names. An invitation letter, maybe? Those last names…some seem familiar, you’ve seen them in newspapers before, those big, power families. The boss's name is in it.";
                clueImageSource += "Clue_1";
                break;
            case 2:
                clueName = "Chef's Supply List";
                clueDescription = "A supply list, sent from the doctor.\n...Lung, from a 27 years old male...\n...left hand, from a 67 years old male...\n...Intestine, from a 42 years old female....";
                clueImageSource += "Clue_2";
                break;
            case 3:
                clueName = "List of Patient Information";
                clueDescription = "“XXX, 25 years old … healthy\nXXX, 31 years old … healthy\nXXX, 29 years old … healthy";
                clueImageSource += "Clue_3";
                break;
            case 4:
                clueName = "Some Pictures of Organs";
                clueDescription = "heart? Lung? brain? What are these for?";
                clueImageSource += "Clue_4";
                break;
            case 5:
                clueName = "Feast Menu";
                clueDescription = "The one that he told me before. But the menu looks not like a usual restaurant.";
                clueImageSource += "Clue_5";
                break;
            case 6:
                clueName = "Record of Surgeries";
                clueDescription = "Surgeries? This signature looks familiar.";
                clueImageSource += "Clue_6";
                break;
            case 7:
                clueName = "Supervisor's Notebook";
                clueDescription = "Content of Clue 7";
                clueImageSource += "Clue_7";
                break;
            case 8:
                clueName = "Supervisor's Phone";
                clueDescription = "Phone is locked. There is an unread message: ";
                clueImageSource += "Clue_8";
                break;
            case 9:
                clueName = "Account Book";
                clueDescription = "It is a ledger that records the embezzlement of public funds by Mr. L to subsidize the doctor.";
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
