using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    //Readonly: Bu s�zl�k sadece s�n�f i�inde de�i�tirilebilir,
    //d��ar�dan eri�ilip de�i�tirilemez.

    static readonly Dictionary<float, WaitForSeconds> WaitForSeconds = new();

    public static WaitForSeconds GetWaitForSeconds(float seconds)
    {
        /*Bu metod, verilen s�reye (seconds) kar��l�k gelen bir WaitForSeconds nesnesi d�nd�r�r.
    Daha �nce olu�turulmu� bir WaitForSeconds nesnesi varsa, 
        yeni bir nesne olu�turmak yerine do�rudan onu kullan�r.*/
        if (WaitForSeconds.TryGetValue(seconds,out var forSeconds)) return forSeconds;

        var waitForSeconds = new WaitForSeconds(seconds);
        //yeni nesne olu�turur 
        WaitForSeconds.Add(seconds, waitForSeconds);
        return WaitForSeconds[seconds];

    }
}
