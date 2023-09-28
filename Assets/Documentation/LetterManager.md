## LetterManager
> 
### Fields
```cs
List`1 selectedLetter
```
```cs
List`1 jokerLetters
```
```cs
GameObject parent
```
```cs
GameObject[][] GridObjects
```
```cs
Queue`1 queue
```
```cs
Int32 totalLetterCount
```
```cs
Int32 crossLetter
```
```cs
PastelColors randomColor
```
```cs
Shape shapes
```
```cs
RandomLetters randomL
```
```cs
Grid grid
```
```cs
GameObject prefab
```
```cs
GameObject jokerPrefab
```

### Methods
```cs
Void DestroyCorrectLetter
```
> Broadcast GENERATE_LETTER 12 times. Reset and send to queue all letters in selectedLetter
```cs
Void HighlightBackLetter
```
> Set letter's color alpha 1 all letters from selectedLetter
```cs
Void StartWaitForDestroyCrossLetter
```
> Need one parameter: GameObject letter. call WaitForDestroyCrossLetter with parameter letter
```cs
IEnumerator WaitForDestroySelectedLetter
```
> Wait 0.2 second and call WaitForDestroySelectedLetter method
```cs
IEnumerator WaitForDestroyCrossLetter
```
> Need one parameter: GameObject Letter. wait 0.2 second and call DestroyCrossLetters method 
```cs
Void HighlightLetter
```
> Need one parameter: GameObject Letter. Set letter's color alpha 0.4 and add letter to selectedLetter. Then set letter's isClickable false.
```cs
Void DestroyCrossLetters
```
> Need one parameter: GameObject Cross Letter. Reset cross letter and send to queue
```cs
Void DestroySelectedLetters
```
> Reset and send to queue all letters in selectedLetter. Then call Reposition function with parameter each letter's row
```cs
Void Reposition
```
> Need one parameter: int row. Then check all letters. If lower cell is empty, move to lower cell's position 
```cs
Void GenerateLetter
```
> Take a letter from queue. Then call CreateLetter method with parameter letter. 
```cs
Void CreateLetter
```
> Need one parameter: GameObject Letter. Set letter's features and positions.
```cs
Void ShakeLetters
```
> Need one parameter: GameObject Letter. Play shake animation on letter 
```cs
Int32[] SetLetterPosition
```
> Need one parameter: GameObject Letter. Set letter positions and be made move to target position.
```cs
Single CalculateMoveDuration
```
> Need two parameters : Vector2 startPosition and Vector2 targetPosition.Calculate distance between two positions and return this result. 
```cs
Void GameOver
```
> Broadcast WAIT_FOR_ADS
```cs
Void ResetGame
```
> Call DestroyAllWords method and reset all fields.
```cs
Void GenerateCrossLetters
```
> Generate cross letters and set letter's features and positions.
```cs
IEnumerator WaitForFirstDestroyCrossLetter
```
> Need one parameter: GameObject Letter. Wait 2 seconds and call DestroyCrossLetters method with parameter letter. 
```cs
Void GenerateJokerLetter
```
> Intantiate a new object based on jokerPrefab. Then set all letter's fields.
```cs
IEnumerator JokerEffect
```
> Need one parameter: GameObject JokerLetter. Play JokerEffectAnimation and destroy all words which same row and column with jokerLetter
```cs
Void ClickedJoker
```
> call JokerEffect method with parameter jokerLetter 
```cs
Void RewardAds
```
> Call ResetGame method.
```cs
Void DestroyAllWords
```
> Reset and send to queue all words in screen.

