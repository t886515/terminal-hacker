using System;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Game state

    enum Screen { SignIn, MainMenu, InGame, Win };
    Screen currentScreen;
    string playerName = "anonymous";
    string password;
    int level;

    string[] level1Passwords = { "idk", "wtf", "gg", "ftw", "btw" };
    string[] level2Passwords = { "idontreallyknow", "whatthefuck", "goodgame", "forthewin", "bytheway" };

    int GenerateRandomNumber(int length)
    {
        int rn = UnityEngine.Random.Range(0, length);
        //System.Random random = new System.Random();
        //int rn = random.Next(0, 4);
        return rn;
    }


    // Pre-defined methods
    // Use this for initialization
    void Start()
    {
        currentScreen = Screen.SignIn;
        Terminal.WriteLine("What is your name?");
    }

    void OnUserInput(string input)
    {
        // Easter Egg
        if (input == "teddy!")
        {
            GenerateTdddyBear();
        }

        if (currentScreen == Screen.SignIn)
        {
            if (!string.IsNullOrEmpty(input))
            {
                GetPlayerName(input);
            }
            else
            {
                Terminal.WriteLine("Invalid Name. Please try again.");
            }
        }
        else if (currentScreen == Screen.MainMenu)
        {
            DetermineLevel(input);

        }
        else if (currentScreen == Screen.InGame)
        {
            CheckPassword(input);
        }
        else if (currentScreen == Screen.Win)
        {
            Restart(input);
        }

    }

    void Restart(string input)
    {
        if (input.ToLower() == "yes" || input.ToLower() == "y")
        {
            ShowStartMenu();
        }
        else
        {
            Terminal.WriteLine("Guess not. Bye!");
        }
    }

    // Methods

    void GetPlayerName(string input)
    {
        playerName = input;

        ShowStartMenu();

    }

    void ShowStartMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hi, " + playerName + ". Welcome!");
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for the local librarry");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Enter your selection:");
    }


    void DetermineLevel(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            StartGame();
        }
        else
        {
            Terminal.WriteLine("Invalid selection. Please try again.");
        }


    }

    void StartGame()
    {
        currentScreen = Screen.InGame;
        Terminal.ClearScreen();
        SetRandomPassword();
        string hint = password;
        Terminal.WriteLine("Guess the password! Hint:" + hint.Anagram());


    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[GenerateRandomNumber(level1Passwords.Length)];
                break;

            case 2:
                password = level2Passwords[GenerateRandomNumber(level2Passwords.Length)];
                break;
            case 3:
                password = "awesomeplace";
                break;
            default:
                Debug.LogError("Should never be here...");
                break;

        }

    }



    void CheckPassword(string input)
    {

        (input == password ? (Action)WinGame : StartGame)();
    }


    void WinGame()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        Terminal.WriteLine("YOU GUESSED THE PASSWORD!");
        Terminal.WriteLine(@"
                 __________
                /       / /
               /       / /
              /       / /   yay!
             /       / /
            (_______(_/
            "
           );

        Terminal.WriteLine("Do you want to play again, " + playerName + "?");

    }

    void GenerateTdddyBear()
    {
        Terminal.WriteLine(@"
                 ___   .--.
            .--.-'   '-' .- |
           / .-,`          .'
           \   `           \
            '.            ! \
              | !    .--.    |
              \      '--'    /.____
             /`-.     \__/  .'      `\
          __ /   \`-.____.- ' `\      /
          | `---`'-'._ / -`     \----'    _ 
          |, -'`  /             |    _.-' `\
         .'     /              |--'`     / |
        /      /\              `         | |
        |   .\/  \      .--.__           \ |
         '-'      '._       /  `\         /
                     `\    '     |------'`
                       \  |      |
                        \        /
                         '._  _.'

        ");
    }


}



