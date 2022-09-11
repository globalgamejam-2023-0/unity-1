using System.Collections.Generic;
using UnityEngine;

public class Statics
{
    public static List<(string, string)> clueGraphic = new()
    {
        ("IMG_2562", "IMG_2551"),
        ("IMG_2564", "IMG_2559"),
        ("IMG_2566", "IMG_2558")
    };

    public static (string, string) randomClueGraphics()
    {
        return Statics.clueGraphic[Random.Range(0, Statics.clueGraphic.Count - 1)];
    }
}
