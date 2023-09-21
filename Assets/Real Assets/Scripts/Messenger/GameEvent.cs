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
  public const string CLICKED_JOKERLETTER = "CLICKED_JOKERLETTER";
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
  public const string SEND_SCORE = "SEND_SCORE";

  public const string GAME_OVER = "GAME_OVER";
  public const string START_GAME = "START_GAME";
  public const string PAUSE_GAME = "PAUSE_GAME";
  
  public const string CROSS_LETTER_GENERATE = "CROSS_LETTER_GENERATE";
  public const string DESTROY_CROSS_LETTERS = "DESTROY_CROSS_LETTERS";
  public const string JOKER_LETTER_GENERATE = "JOKER_LETTER_GENERATE";

  public const string SET_LEADERBOARD_TEXT = "SET_LEADERBOARD_TEXT";
  public const string SEND_SCORE_TO_LEADERBOARD = "SEND_SCORE_TO_LEADERBOARD";

  public const string PLAY_BACKGROUND_MUSIC = "PLAY_BACKGROUND_MUSIC";
  public const string STOP_BACKGROUND_MUSIC = "STOP_BACKGROUND_MUSIC";
  public const string PLAY_SOUND_EFFECTS = "PLAY_SOUND_EFFECTS";

  public const string PLAY_CORRECT_ANSWER = "PLAY_CORRECT_ANSWER";
  public const string PLAY_CROSS_LETTERS = "PLAY_CROSS_LETTERS";
  public const string PLAY_GENERATE_LETTERS = "PLAY_GENERATE_LETTERS";
  public const string PLAY_HARFLER_BINGILDARKEN = "PLAY_HARFLER_BINGILDARKEN";
  public const string PLAY_JOKER_CALISIRKEN = "PLAY_JOKER_CALISIRKEN";
  public const string PLAY_JOKER_DUSERKEN = "PLAY_JOKER_DUSERKEN";
  public const string PLAY_SCORE_RISING = "PLAY_SCORE_RISING";
  public const string PLAY_WRONG_ANSWER = "PLAY_WRONG_ANSWER";
}
