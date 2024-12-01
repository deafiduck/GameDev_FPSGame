using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    //Readonly: Bu sözlük sadece sýnýf içinde deðiþtirilebilir,
    //dýþarýdan eriþilip deðiþtirilemez.

    static readonly Dictionary<float, WaitForSeconds> WaitForSeconds = new();

    public static WaitForSeconds GetWaitForSeconds(float seconds)
    {
        /*Bu metod, verilen süreye (seconds) karþýlýk gelen bir WaitForSeconds nesnesi döndürür.
    Daha önce oluþturulmuþ bir WaitForSeconds nesnesi varsa, 
        yeni bir nesne oluþturmak yerine doðrudan onu kullanýr.*/
        if (WaitForSeconds.TryGetValue(seconds,out var forSeconds)) return forSeconds;

        var waitForSeconds = new WaitForSeconds(seconds);
        //yeni nesne oluþturur 
        WaitForSeconds.Add(seconds, waitForSeconds);
        return WaitForSeconds[seconds];

    }
}
