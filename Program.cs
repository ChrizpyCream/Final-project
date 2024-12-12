using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace BowlingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // game's welcome message
            Console.WriteLine("Welcome to the Ultimate Bowling Game!");
            Console.WriteLine("Create your player profiles (name, nickname, age):");

            // List to store player details
            List<Player> players = new List<Player>();

            // Get the number of players, ensuring it's between 1 and 4
            int playerCount = GetValidInt("Enter number of players (1-4):", 1, 4);

            // Loop to gather player details and add to the list
            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine($"Player {i + 1}:");
                string name = GetValidString("Enter name:");
                string nickname = GetValidString("Enter nickname:");
                int age = GetValidInt("Enter age (10-100):", 10, 100);
                players.Add(new Player(name, nickname, age));
            }

            // Initialize a new game with the list of players and start the game
            Game game = new Game(players);
            game.Start();

            // Display final scores and update high scores
            Console.WriteLine("\nGame Over! Final Scores:");
            foreach (Player player in players)
            {
                Console.WriteLine($"{player.Name} ({player.Nickname}) - Age {player.Age}: {player.GetScore()}");
                player.UpdateHighScore();
            }

            // Save the scores to a file
            SaveScores(players);

            // Optionally display saved scores
            Console.WriteLine("\nSaved scores to 'scores.txt'. Would you like to view the saved scores? (y/n)");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                DisplaySavedScores();
            }
        }

        // Method to validate integer input within a specified range
        static int GetValidInt(string prompt, int min, int max)
        {
            int value;
            Console.WriteLine(prompt);
            while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max)
            {
                Console.WriteLine($"Invalid input. Enter a number between {min} and {max}:");
            }
            return value;
        }

        // Method to validate non-empty string input
        static string GetValidString(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine()?.Trim();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty. Try again:");
                input = Console.ReadLine()?.Trim();
            }
            return input;
        }

        // Method to save players' scores to a file
        static void SaveScores(List<Player> players)
        {
            using (StreamWriter writer = new StreamWriter("scores.txt", true))
            {
                writer.WriteLine($"Game played on {DateTime.Now}");
                foreach (Player player in players)
                {
                    writer.WriteLine($"{player.Name} ({player.Nickname}) - High Score: {player.HighScore}");
                }
                writer.WriteLine(new string('-', 30));
            }
        }

        // Method to display saved scores from the file
        static void DisplaySavedScores()
        {
            Console.WriteLine("\n--- Saved Scores ---");
            using (StreamReader reader = new StreamReader("scores.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }

    // Represents a player in the bowling game
    class Player // stores player details, keeps tack of roll and caluclates using boweling rules given
    {
        public string Name { get; private set; }
        public string Nickname { get; private set; }
        public int Age { get; private set; }
        public int HighScore { get; private set; }

        private int[] rolls = new int[21]; // Stores all rolls for a game
        private int rollIndex = 0;

        public Player(string name, string nickname, int age)
        {
            Name = name;
            Nickname = nickname;
            Age = age;
            HighScore = 0; // Initialize high score
        }

        // Adds the pins knocked down in a roll
        public void AddRoll(int pins)
        {
            rolls[rollIndex++] = pins;
        }

        // Calculates the total score for the game
        public int GetScore()
        {
            int score = 0;
            int index = 0;

            for (int frame = 0; frame < 10; frame++)
            {
                if (rolls[index] == 10) // Strike
                {
                    score += 10 + rolls[index + 1] + rolls[index + 2];
                    index++;
                }
                else if (rolls[index] + rolls[index + 1] == 10) // Spare
                {
                    score += 10 + rolls[index + 2];
                    index += 2;
                }
                else
                {
                    score += rolls[index] + rolls[index + 1];
                    index += 2;
                }
            }
            return score;
        }

        // updates the player's high score if the current game score is higher
        public void UpdateHighScore()
        {
            int currentScore = GetScore();
            if (currentScore > HighScore)
            {
                HighScore = currentScore;
            }
        }
    }

    // represents the bowling game and handles game logic
    class Game
    {
        private List<Player> players;

        public Game(List<Player> players)
        {
            this.players = players;
        }

        // starts the bowling game with 10 frames
        public void Start()
        {
            Console.WriteLine("\nStarting the game!");
            Console.WriteLine("Get ready for 10 exciting frames!");

            for (int frame = 1; frame <= 10; frame++)
            {
                foreach (Player player in players)
                {
                    Console.Clear();
                    Console.WriteLine($"Frame {frame}: {player.Name}'s turn ({player.Nickname})");
                    DisplayBowlingTip(frame);

                    // First roll
                    int firstRoll = GetRoll("Enter pins knocked down for first roll:");
                    player.AddRoll(firstRoll);

                    if (firstRoll == 10) // Strike
                    {
                        Console.WriteLine("STRIKE! 🎉");
                        DisplayStrikeAnimation();
                        continue;
                    }

                    // Second roll
                    int secondRoll = GetRoll("Enter pins knocked down for second roll:", firstRoll);
                    player.AddRoll(secondRoll);

                    if (firstRoll + secondRoll == 10) // Spare
                    {
                        Console.WriteLine("SPARE! 🎉");
                        DisplaySpareAnimation();
                    }

                    DisplayFrameScore(player);
                }
            }
        }

        // Validates and retrieves the number of pins knocked down
        private int GetRoll(string prompt, int previousRoll = 0)
        {
            Console.WriteLine(prompt);
            int pins;
            while (!int.TryParse(Console.ReadLine(), out pins) || pins < 0 || pins > (10 - previousRoll))
            {
                Console.WriteLine($"Invalid input. Enter a number between 0 and {10 - previousRoll}:");
            }
            return pins;
        }

        // Displays motivational tips during the game
        private void DisplayBowlingTip(int frame)
        {
            string[] tips = {
                "Focus on your form to hit the sweet spot!",
                "Aim for the center pin for better results.",
                "Relax and take a deep breath before your roll.",
                "Consistency is key—stick to your routine."
            };
            Console.WriteLine($"Bowling Tip: {tips[frame % tips.Length]}");
        }

        // simulates a strike animation
        private void DisplayStrikeAnimation()
        {
            Console.WriteLine("Rolling... 🎳");
            Thread.Sleep(500);
            Console.WriteLine("Pins flying everywhere! STRIKE! 🎯");
            Thread.Sleep(500);
        }

        // simulates a spare animation
        private void DisplaySpareAnimation()
        {
            Console.WriteLine("Carefully rolling... 🎳");
            Thread.Sleep(500);
            Console.WriteLine("All pins down! SPARE! 🔥");
            Thread.Sleep(500);
        }

        // displays the current frame's score for a player
        private void DisplayFrameScore(Player player)
        {
            Console.WriteLine($"\n{player.Name}'s current score: {player.GetScore()}\n");
            Thread.Sleep(1000); //equals one second so the player can see there score before moving to the next player
        }
    }
}
