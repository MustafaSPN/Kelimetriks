# Kelimetriks

## Download

- [Download the latest version here](https://play.google.com/store/apps/details?id=com.sepen.kelimetriks)


## Overview

**Genre:** Word Puzzle

**Platform:** Android, IOS

**Description:** Kelimetriks is an engaging letter-based word game where players challenge time to score points and compete on the leaderboard. The game involves creating meaningful words with randomly falling letters to prevent columns from getting filled.

## Gameplay Mechanics

### Authentication and User Profiles

- Players can register and log in using Google Firebase.
- Usernames serve as unique identifiers stored in the database.
- Continuous gaming experience with an option to stay logged in.

### Core Gameplay

- **Objective:** Eliminate letters by forming meaningful words before any column is filled.
- Randomly falling letters from a chosen word are placed above the lowest letter in their respective columns.
- Touching letters on the screen adds them to the word space.
- Players can check the word by clicking the green button. If correct, the word is scored, and letters are cleared.
- Incorrect words can be rejected by clicking the red button, making the letters usable again.

### Scoring System

- Each word's score is inversely proportional to its frequency in the database.
- Abundant words yield higher points, while less common words bring fewer points.
- The final score is added to the leaderboard when the game concludes.

### Power-Ups and Special Features

- **Joker:** Drops after three consecutive correct answers, clearing all letters in its row and column without awarding points.
- **Penalty Letters:** Drop after three consecutive wrong answers. Eliminated when the letters below it are cleared.
- **Coins:** Earned by eliminating special letters. Accumulated coins can be used to purchase a wild card.
- **Reward Ads:** Offer additional incentives to players.

### Progression

- Increasing score reduces the frequency of falling letters, making the game more challenging.
- Player usage of words is tracked in the Firebase database, adjusting the difficulty of words dynamically.

### Word Database

- Utilizes a large database of words.
- Word count database dynamically adjusts word difficulty.

### UI and Localization

- Responsive UI design for various screen sizes.
- Localization support for English and Turkish languages.

### Monetization

- Watch ads for rewards (Joker).
- Interstitial ads for additional monetization.

### Analytics

- Implementation of Google Firebase Analytics for user behavior tracking.
- Insights used to enhance gameplay and features.

## How to Play

1. **Login/Registration:**
   - Use email, username and password for login and registration.

2. **Gameplay:**
   - Tap falling letters to form meaningful words.
   - Green button to check and score the word; red button to reject.
   
3. **Power-Ups:**
   - Use joker for column and row clearance.
   - Avoid penalty letters by answering correctly.

4. **Progression:**
   - Score higher to face increased difficulty.

5. **Monetization:**
   - Watch ads for rewards and additional in-game advantages.

6. **Leaderboard:**
   - Compete for the top spot based on your scores.

## Localization

- The game supports English and Turkish languages.
