## Grid
> 
##### Inherits from:
 - UnityEngine.ScriptableObject
---
### Fields
```cs
Vector2[][] cellPosition
```
```cs
Boolean[][] cellFullness
```
```cs
Int32 width
```
```cs
Int32 height
```
```cs
Single startX
```
```cs
Single startY
```
```cs
Single offset
```

### Methods
```cs
Void InitializeGrid
```
> Need two parameter: int widt and height. initialize grid base on parameters.
```cs
Void setAllPositions
```
```cs
Void setAllEmpty
```
```cs
Int32[] TargetCell
```
> Need one parameter: int column. Check the column and return top empty cell in column's index as int array

