# Final-project

Bowling game 
Aspirations:
I wanted to make a mini traditional bowling game from stracth that folllows the rules of consisting 10 rounds, up to 4 players, 10 pin In which the player enters how many pins are knocked down in each round. once the 10 rounds are up the program adds up all the scores for each player then determines who won that match. 

Interest: 
The project was interesting to me since it gives a thrill and anticpeation on which player will win since they can input any score they want 


Reflection: 
it was challenging to representing the player score during eaxch round for multple, putting players Validates and retrieveing the number of pins knocked down

Flowchart:
Start
1) Program Starts
Display: "Welcome to the Ultimate Bowling Game!"

Create Players

2) Prompt for Player Count
Input: Enter number of players (1-4).
Validate input: Repeat if input is invalid.

3) Loop Through Players
For each player:

Input: Name, Nickname, and Age (validated).
Add player to the players list.

Initialize Game
4) Start the Game
Create Game instance with the list of players.
Call the Start method of the game.

Gameplay (10 Frames)
5) Outer Loop: 10 Frames
For each frame (1 to 10):
For each player:
Display the player's turn.
First Roll: Input and validate the number of pins.
If strike, display animation and skip to the next player.
Second Roll: Input and validate remaining pins (if needed).
Display score for the current frame.

6) Calculate and Display Scores
After 10 Frames
Calculate the total score for each player.
Update the player's high score if necessary.
Display all players' final scores.

7) Save Scores
Write Scores to File
Append scores to scores.txt with the current date.

8) Optional: Display Saved Scores
Prompt: "Would you like to view the saved scores? (y/n)"
If yes, read and display the scores from the file.

9) End
Program Ends

10) Additional Details
Validation Subprocesses:

For player count, age, and rolls, ensure input is within valid ranges. Re-prompt for invalid inputs.
Animations:

Display animations for strikes and spares during gameplay for added user experience.
File Operations:

Use SaveScores to append data to a file.
Use DisplaySavedScores to read and show data from the file.