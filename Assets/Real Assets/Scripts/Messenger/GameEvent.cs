using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
  public const string LOG_IN = "LOG_IN";
  public const string REGISTER = "REGISTER";
  public const string REQUEST_USER = "REQUEST_USER";
  public const string SENDING_USER = "SENDING_USER";
  public const string SENDING_USERNAME = "SENDING_USERNAME";
  public const string REGISTER_COMPLATED = "REGISTER_COMPLATED";
  public const string LOG_OUT = "LOG_OUT";

  public const string GENERATE_LETTER = "GENERATE_LETTER";
  public const string CLICKED_LETTER = "CLICKED_LETTER";
  public const string DESTROY_CORRECT_LETTER = "DESTROY_CORRECT_LETTER";
  public const string RE_POSITION = "RE_POSITION";

  public const string ADD_LETTER_TO_WORD = "ADD_LETTER_TO_WORLD";
  public const string MOVE_CLICKED_LETTER_HIDE = "MOVE_CLICKED_LETTER_HIDE";
  public const string MOVE_CLICKED_LETTER_BACK = "MOVE_CLICKED_LETTER_BACK";

  public const string CROSS_BUTTON_PRESSED = "CROSS_BUTTON_PRESSED";
  public const string CHECK_BUTTON_PRESSED = "CHECK_BUTTON_PRESSED";

  public const string EMPTY_WORD = "EMPTY_WORD";
  public const string SHAKE_LETTERS = "SHAKE_LETTERS";

  public const string REQUEST_WORD = "REQUEST_WORD";
  public const string RETURN_WORD = "RETURN_WORD";

  public const string ADD_SCORE = "ADD_SCORE";

  public const string GAME_OVER = "GAME_OVER";
  public const string START_GAME = "START_GAME";

  public const string CROSS_LETTER_GENERATE = "CROSS_LETTER_GENERATE";
  public const string DESTROY_CROSS_LETTERS = "DESTROY_CROSS_LETTERS";


  public const string SET_LEADERBOARD_TEXT = "SET_LEADERBOARD_TEXT";
  public const string SEND_SCORE_TO_LEADERBOARD = "SEND_SCORE_TO_LEADERBOARD";
}