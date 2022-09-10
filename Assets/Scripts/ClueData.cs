using System;
using System.Collections.Generic;

[Serializable]
public struct ClueData
{
    public string clueName;
    public string clueText;
    public string adjective;
    public string question;
    public List<string> adjectives;
    public bool truthiness;
    public int answers;
}
