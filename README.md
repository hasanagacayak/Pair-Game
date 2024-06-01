# Pair-Game
# Project Overview
Pair Game Project was one of the tasks given to me during my internship at ASELSAN and I wrote it through C# Forum Applications. The Pair Game is a classic memory matching game where players find pairs of identical pictures by selecting boxes with the mouse. The player who matches all the pictures in the shortest time wins the game. This project includes features such as reading/writing data to an Excel file, working with multiple forms, and transitioning between these forms.

# Features
Multiple Forms: The project consists of two different form designs for the game interface and user data entry.
Data Management: Capable of writing player data to an Excel file and a text file.
Leaderboard: Displays a leaderboard of previous players.
Randomized Icons: Icons are randomly assigned to boxes each game session.
# Technical Details
# Form 1
Game Interface: The main interface where the game is played and the leaderboard is displayed.
Icon Assignment: Uses the “Webdings” font to create shapes inside the boxes. Shapes are assigned randomly to boxes.
Click Handling: Manages user clicks to reveal or hide icons, ensuring the third click is prevented until necessary actions are completed.
Game Logic: Checks for matched pairs and keeps them visible, while unmatched pairs are hidden after a delay.
Timer and Score: Tracks the player's time to complete the game and displays the score upon completion.
# Form 2
User Data Entry: Interface where the user enters their name and starts the game.
Data Storage: Transfers user data to the game interface and writes to log files.
# Key Functions
AssignIconsToSquare: Randomly distributes shapes into boxes.
CheckForWinner: Verifies if all pairs are matched and ends the game, displaying the player's time.
Leaderboard Management: Reads data from Excel files and displays the top scores on the leaderboard.
