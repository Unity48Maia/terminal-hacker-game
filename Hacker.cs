﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    // Game configuration data
    const string menuHint = "You may type menu at any time.";
    string[] level1Passwords = { "Oh Canada", "Donuts", "Maple Syrup", "Loony", "Eh", "Vancouver" };
    string[] level2Passwords = { "prisoner", "handcuffs", "firearm", "murderer", "gang" };
    string[] level3Passwords = { "Fortnite", "Scar", "Season 6", "Item Shop", "Pump" };

    // Game state
    int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    string password;

    // Use this for initialization
    void Start ()
    {
        ShowMainMenu ();
    }

    void ShowMainMenu ()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for Canada");
        Terminal.WriteLine("Press 2 for a Maximum Security Prison");
        Terminal.WriteLine("Press 3 for Epic Games!");
        Terminal.WriteLine("Enter your selection:");
    }

    void OnUserInput(string input)
    {
        if (input == "menu") // we can always go direct to main menu
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "007") // easter egg
        {
            Terminal.WriteLine("Please select a level Mr Bond!");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level");
            Terminal.WriteLine(menuHint);
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Oh Canada");
                Terminal.WriteLine(@"
  --------------
  |             |
  |   ------    |
  |   |    |    |
  |   |    |    |
  |    ----     |  
  |             |
  --------------|
"
                );
                break;
            case 2:
                Terminal.WriteLine("You got the prison key!");
                Terminal.WriteLine(@"
 __
/0 \_______
\__/-=' = '         
"
                );
                Terminal.WriteLine("Play again for a greater challenge.");
                break;
            case 3:
                Terminal.WriteLine(@"
|---------
|        
|---------
|
|
|
"
                );
                Terminal.WriteLine("Welcome to the Epic Games console!");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
    }
}
