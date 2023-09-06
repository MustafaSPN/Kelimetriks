using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Tilemaps;
using Random = UnityEngine.Random;

public class LetterMovement : MonoBehaviour
{
  [SerializeField] private Grid grid;
  private int randomColumn;
  private Vector2 randomPosition;
  private Vector2 targetPosition;
  private float duration;
  private static int count;
  private void Start()
  {
      if (count < 12)
      {
          MoveFirstLetters();
      }

      else
      {
          MoveLetter();      
      }
    
    count++;
  }

  private void MoveLetter()
  {
      while (targetPosition==Vector2.zero)
      {
          randomColumn = Random.Range(0, 6);
          targetPosition = grid.GenerateTargetPosition(randomColumn);
      }
     
      randomPosition = grid.cellPosition[randomColumn][9];
      transform.position = randomPosition;
      duration = 8 - ((targetPosition.y - grid.startY) / grid.offset);
      gameObject.transform.DOMoveY(targetPosition.y, duration/3);
  }

  private void MoveFirstLetters()
  {

      randomColumn = (count % 6);
      Debug.Log(randomColumn);
      targetPosition = grid.GenerateTargetPosition(randomColumn);
      randomPosition = grid.cellPosition[randomColumn][9];
      transform.position = randomPosition;
      duration = 8 - ((targetPosition.y - grid.startY) / grid.offset);
      gameObject.transform.DOMoveY(targetPosition.y, duration/3);
  }
}
