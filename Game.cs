using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guessing_Game
{
    /// <summary>
    ///  This class contains Number Guessing Game methods.
    /// </summary>
    class Game
    {
        /// <summary>
        /// Player input indicating to end the game.
        /// </summary>
        public const ushort EXIT_GAME = 0;
        /// <summary>
        /// Player input specifying random number from 1 to 20.
        /// </summary>
        private const ushort ONE_TO_20 = 1;
        /// <summary>
        /// Player input specifying random number from 1 to 100.
        /// </summary>
        private const ushort ONE_TO_100 = 2;
        /// <summary>
        /// Player input specifying random number from 1 to 1000.
        /// </summary>
        private const ushort ONE_TO_1000 = 3;
        /// <summary>
        /// Contains maximum values for generating random numbers
        /// </summary>
        private int[] _max_vals = new int[4] { 0, 20, 100, 1000 };
        /// <summary>
        /// Players total number of guesses for current game
        /// </summary>
        public uint _total_number_guesses;
        /// <summary>
        /// Players current input value
        /// </summary>
        private string _player_input;
        /// <summary>
        /// Initialized on creation using default seed.
        /// </summary>
        private Random _random_number;
        /// <summary>
        /// Main menu items string
        /// </summary>
        private string _format = "Enter: {0} to guess a number between 1 and {1}";
        /// <summary>
        /// Game messages
        /// </summary>
        private string _msg;
        /// <summary>
        /// The current short value of the last user input
        /// </summary>
        private ushort _current_number;
        /// <summary>
        /// Default Constructor: initialize data members
        /// </summary>
        public Game()
        {
            _total_number_guesses = 0;
            _player_input = "";
            //See modules for proper random initialization
            _random_number = new Random();
            _msg = "Welcome to the Guessing Game\n\n";
        }
        /// <summary>
        /// Displays the games main menu.
        /// Captures the players menu choice and validates it.
        /// Calls the setupGame method with players menu choice
        /// </summary>
        /// <returns>Returns the result of call to setupGame method.</returns>
        public ushort displayMenu()
        {
            bool ok;
            do
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Clear();
                ok = false;
                Console.WriteLine(_msg);
                for (short i = 1; i < _max_vals.Length; i++)
                {
                    Console.WriteLine(_format, i, _max_vals[i]);
                }
                Console.WriteLine("\nEnter {0} to exit the game.\n", EXIT_GAME);
                _player_input = Console.ReadLine();
                if(ushort.TryParse(_player_input, out _current_number))
                {
                    if(_current_number >= 0 && _current_number <= 3) ok = true;
                }
                _msg = "The value: " + _player_input + " does not correspond with any menu choice!\nPlease Try again.\n\n";
            } while(!ok);
            return setupGame(ref _current_number);
        }
        /// <summary>
        /// This method will Setup the Game by deciding what range of random number is to be generated based on the player choice.
        /// </summary>
        /// <param name="random_number_or_exit_game"></param>
        /// <returns>A random number based on _current_number value
        /// or the constant value Game.EXIT_GAME when _current_number is Game.EXIT_GAME 
        /// </returns>
        public ushort setupGame(ref ushort menu_choice)
        {
            return EXIT_GAME == menu_choice ? EXIT_GAME : (ushort)_random_number.Next(1, _max_vals[menu_choice]);
        }
        /// <summary>
        /// All game play takes place inside this method.
        /// The player will provide a guess and the game will give feedback, based on if the guess is too high or too low or correct.
        /// </summary>
        /// <param name="random_number">A random number between 1 and a number determined by menu choice</param>
        public void playGame(ref ushort random_number)
        {
            bool ok = false;
            int max_val = _max_vals[_current_number];
            _msg = "Pick a number between 1 and " + max_val + "\nNOTE: Enter 0 to go back to main menu.";
            do
            {
                Console.WriteLine(_msg);
                _player_input = Console.ReadLine();
                _total_number_guesses++;
                if (ushort.TryParse(_player_input, out _current_number))
                {
                    if(_current_number == random_number)
                    {
                        ok = true;
                        _msg = "Congratulations " + _current_number + " is correct. It only took you " + _total_number_guesses + " guesses.\n\n";
                        Console.WriteLine(_msg + "Do you want to Continue? Enter \"n\" 0r \"N\" to exit");
                        _player_input = Console.ReadLine();
                        if(_player_input == "n" || _player_input == "N")
                        {
                            random_number = EXIT_GAME;
                        }
                    }
                    else if(_current_number == 0)
                    {
                        ok = true;
                        _msg = "Was that too hard a game for you?\n\n";
                    }
                    else if(_current_number > random_number)
                    {
                        _msg = "Too High!";
                    }
                    else
                    {
                        _msg = "Too Low!";
                    }
                }
                else
                {
                    _msg = "Sorry " + _player_input + " is not a valid choice - try again\n\n";
                }
            } while (!ok);
        }
    }
}
