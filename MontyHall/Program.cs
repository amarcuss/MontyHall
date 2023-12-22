// See https://aka.ms/new-console-template for more information
using MontyHall;

Console.WriteLine("Monty Hall Simulator\n");

int numberOfGames = 10000000;

Functions.RunRandomGames(numberOfGames);

/*
 * A multi-threaded approach to running the games. 8 threads will consume ~80% of an i7 CPU.
 * 
Task task1 = Task.Run(() => Functions.RunRandomGames(numberOfGames));
Task task2 = Task.Run(() => Functions.RunRandomGames(numberOfGames));
Task task3 = Task.Run(() => Functions.RunRandomGames(numberOfGames));
Task task4 = Task.Run(() => Functions.RunRandomGames(numberOfGames));
Task task5 = Task.Run(() => Functions.RunRandomGames(numberOfGames));
Task task6 = Task.Run(() => Functions.RunRandomGames(numberOfGames));
Task task7 = Task.Run(() => Functions.RunRandomGames(numberOfGames));
Task task8 = Task.Run(() => Functions.RunRandomGames(numberOfGames));

Task.WaitAll(task1, task2, task3, task4, task5, task6, task7, task8);
*/


