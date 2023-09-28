## CoinManager
> 
### Fields
```cs
GameObject prefab
```
```cs
GameObject parent
```
```cs
TMP_Text coinText
```
```cs
Queue`1 queue
```
```cs
Int32 coinCount
```

### Methods
```cs
Void CreateAndMove
```
> Need one parameter: Transform startPosition. Then Create a coin based queue, and move to parent's transform position from startPosition
```cs
Void CoinButton
```
> When click the CoinButton, check coinCount. If coinCount >=5 coin will be purchased with 5 coin
```cs
Void ResetCoins
```
> Reset cointCount and cointText

