## WordCountData
> 
### Fields
```cs
DataSnapshot snapshot
```

### Methods
```cs
Void AddWordToWordDataDictionary
```
> Need one parameter: string word, then add wordData Dictionary and call SaveWordCountDataToDatabase
```cs
Void SaveWordCountDataToDatabase
```
> Need one parameter: string word, then update database with word count
```cs
Void PullWordCountDataFromDatabase
```
> Pull all word count data from data base and call Words class's AddWordCountDataSegment based on word count 

