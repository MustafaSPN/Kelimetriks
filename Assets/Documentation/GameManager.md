## GameManager
> 
### Fields
```cs
Grid grid
```
```cs
PastelColors randomColor
```
```cs
Words wordAsset
```
```cs
RandomLetters randomLetters
```
```cs
GameObject redCircle1
```
```cs
GameObject redCircle2
```
```cs
GameObject redCircle3
```
```cs
GameObject greenCircle1
```
```cs
GameObject greenCircle2
```
```cs
GameObject greenCircle3
```
```cs
AdsInitializer initializerAds
```
```cs
Int32 clickCount
```
```cs
Single timer
```
```cs
Boolean isGameContinue
```
```cs
Int32 wrongAnswers
```
```cs
Int32 score
```
```cs
Int32 correctWordCount
```
```cs
Int32 wrongWordCount
```
```cs
Int32 letterCount
```
```cs
Int32 jokerCount
```
```cs
Int32 crossLetterCount
```
```cs
Boolean isAdsShowed
```

### Methods
```cs
Void Initializer
```
> Initialize Scriptable Objects
```cs
IEnumerator GenerateFirstLetters
```
> Broadcast GENERATE_LETTER 12 times.
```cs
Void ClickedLetter
```
> Need two parameter: char letter and GameObject letter object. Check clickCount. if less than 6, Then broadcast ADD_LETTER_TO_WORD with parameter letter, broadcastMOVE_CLICKED_LETTER_HIDE with parameter letter object. Then increase one letterCount and clickCount, else Broadcast MOVE_CLICKED_LETTER_HIDE with parameter letter object.
```cs
Void CrossButtonPressed
```
> Empty word and be moved clicked letter back. Then set clickCount 0 
```cs
Void CheckWordIsExist
```
> Need one parameter: string word. Send this word to Wordasset's SearchWord functions. if this word exist destroy letters and increase score and wrongAnswerselse be moved clicked letter back and decrease one wrongAnswers
```cs
Void CheckButtonPressed
```
> if clickCount is suitable, Request word from word manager..
```cs
Void GameOver
```
> Set isGameContinue false, Call sendAnaltytics method and Initialize Ads.
```cs
Void SendAnalytics
```
> Send all parameters to Analytics
```cs
Void StartGame
```
> Set all fields. and call GenerateFirstLetters method
```cs
Void SetScore
```
> Set score and wordAsset's Segment based on score.
```cs
Void ShowAd
```
```cs
Void PauseGame
```
> Set isGameContinue bool false
```cs
Void Rewarded
```
> reset click count and wrong answers. Then call GenerateFirstLetters method.
```cs
Void StartContinueGame
```
> Call ContinueGame method
```cs
IEnumerator ContinueGame
```
> Wait 1 second and set isGameContinue bool true

