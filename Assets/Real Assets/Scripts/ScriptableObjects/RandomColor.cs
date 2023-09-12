using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pastel Colors",menuName = "ScriptableObjects/PastelColors")]

public class PastelColors : ScriptableObject
{
   public Color[] colors = new Color[11];

   public void InitializeColors()
   {
      colors[0] = ColorAssets.PASTEL_1;
      colors[1] = ColorAssets.PASTEL_2;
      colors[2] = ColorAssets.PASTEL_3;
      colors[3] = ColorAssets.PASTEL_4;
      colors[4] = ColorAssets.PASTEL_5;
      colors[5] = ColorAssets.PASTEL_6;
      colors[6] = ColorAssets.PASTEL_7;
      colors[7] = ColorAssets.PASTEL_8;
      colors[8] = ColorAssets.PASTEL_9;
      colors[9] = ColorAssets.PASTEL_10;
      colors[10] = ColorAssets.PASTEL_11;
      
   }

   public Color GenerateRandomColor()
   {
      return colors[Random.Range(0, 11)];
   }
}
