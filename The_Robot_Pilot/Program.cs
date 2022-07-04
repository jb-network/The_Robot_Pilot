// This is challenge work for the "C# Players Guide"
// Level 32 The Robot Pilot
// This is an update to the Level 14 the Hunting the Manticore Challenge.
// This will automate Player 1's action of setting the Manticore range using the random class

bool playGame = true;

do
{
    //intial settings for a new game
    int manticore = 10;
    int city = 15;
    int round = 1;
    int damage = 0;
    string separator = "_________________________________________________________________________________";


    int attackDistance = gameSteup(separator);

    Console.WriteLine("Human Player: It is your turn.");

    while (city > 0 && manticore > 0)
    {
        Console.WriteLine(separator);
        statusMenu(round, city, manticore);
        int cannonFire = cannonInfo(damage, round);
        int targetRange = rangeSetting();
        int fireResults = fireInfo(targetRange, attackDistance, cannonFire);

        //Round updates
        city -= 1;
        manticore -= fireResults;
        round++;

    }

    winCondition(manticore, city, separator);

    playGame = playAgain();
} while (playGame);

//-----Methods-----

//Status menu method
void statusMenu(int round, int city, int manticore)
{


    Console.WriteLine($"STATUS:  Round: {round}  City: {city}/15  Manticore: {manticore}/10");

}

//Game set up method
int gameSteup(string separator)
{
    int attackRange = -1;
    Console.WriteLine(separator);
    Console.WriteLine("\nWelcome to Hunting the Manticore!\n");
    Console.WriteLine("The computer will now auto set the attack range of the Manticore");
    Console.WriteLine(separator);
    Random AutoDistance = new Random();
    attackRange = AutoDistance.Next(0, 100);
    Console.WriteLine("The range is set, press any key to start the game");
    Console.ReadKey();
    Console.Clear();
    return attackRange;
}

//Cannon fire method, returned damage to remove health from Manticore
int cannonInfo(int damage, int round)
{
    //Required exercise modifiers to set cannon damage
    if (round % 3 == 0 && round % 5 == 0) damage = 10;
    else if (round % 3 == 0 || round % 5 == 0) damage = 3;
    else damage = 1;

    Console.WriteLine($"The cannon is expected to deal {damage} damage this round.");

    return damage;
}

//Player 2 guess range 
int rangeSetting()
{
    Console.Write("Human Player: Enter the desired cannon range: ");
    int cannonRange = Convert.ToInt32(Console.ReadLine());
    return cannonRange;

}

//Status of shot taken (higher, lower, or correct guesses)
int fireInfo(int targetRange, int attackDistance, int cannonFire)
{
    int damage = 0;
    if (targetRange > attackDistance)
    {
        Console.WriteLine("That round OVERSHOT the target.");
        return damage;
    }
    else if (targetRange < attackDistance)
    {
        Console.WriteLine("That round FELL SHORT of the target");
        return damage;
    }
    else
    {
        Console.WriteLine("That round was a DIRECT HIT!");
        return damage + cannonFire;
    }
}

//End game statment based on city and manticore health
void winCondition(int manticore, int city, string separator)
{
    if (manticore <= 0 && city > 0)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The Manticore has been destoryed!  This city of Consolas has been saved!");
        Console.ResetColor();
    }
    else if (manticore > 0 && city <= 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("You failed to detory the Manticore.  The city of Consolas has fallen!");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("The Manticore and the city were destoried at the same time.  All is lost for both sides.");
        Console.ResetColor();
    }
    Console.WriteLine(separator);
}


// Replay game method
bool playAgain()
{
    int replayStatus = 0;
    //Loop to get and validate user input 
    do
    {
        Console.WriteLine("Press 1 to Play Again");
        Console.WriteLine("Press 2 to Exit");
        replayStatus = Convert.ToInt32(Console.ReadLine());
        if (replayStatus <= 0 || replayStatus > 2)
        {
            Console.Clear();
            Console.WriteLine($"You entered {replayStatus}, please try again");
        }
    }
    while (replayStatus <= 0 || replayStatus > 2);

    //either play again or not
    if (replayStatus == 1)
    {
        Console.WriteLine("Press any key to restart the game");
        Console.ReadKey();
        Console.Clear();
        return playGame = true;
    }
    else
    {
        Console.WriteLine("Press any key to exit the game");
        Console.ReadKey();
        Console.Clear();
        return playGame = false;
    }

}