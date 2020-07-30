using System;
using System.ComponentModel.Design;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace ElevatorAlgorithm
{
	
class StartMain
    {
        static void Main(string[] args)
        {
			//Assigning all default values like minFloor,maxFloor,maxCapacity and number of elevators from a text file
			List<int> termsList = new List<int>();

			String filepath = @"C:\Users\Chint\Desktop\Admin.txt";
			List<string> lines = File.ReadAllLines(filepath).ToList();

			foreach(var line in lines){

				string[] entries = line.Split('=');
                
				termsList.Add(Int32.Parse(entries[1]));

            }
			
			//numOfElevators = termsList[0];
			//minFloor = termsList[1];
			//maxFloor = termsList[2];
			//maxCapacity = termsList[3];

			Elevator[] elevator = new Elevator[termsList[0]];

			// Assigning Lowest Floor
			//Highest Floor
			//Maximun Capacity
			for (int i = 0; i < termsList[0]; i++)
			{ 
				elevator[i] = new Elevator(termsList[1], termsList[2], termsList[3]);

			}

			//Temporary Elevator Object
			Elevator change = null;					

			while (true)
			{

				//Every  Elevator Status
				for (int j = 0; j < elevator.Length; j++)
				{
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.Out.WriteLine("\nElevator " + (j + 1) + " is at Floor----> " + elevator[j].getCurrentFloor()
							+ "\t Number of people on it.....>" + elevator[j].getSum());
					Console.ForegroundColor = ConsoleColor.White;
				}

				Console.Out.WriteLine("----------------++++++++-----------------");

				Console.Out.WriteLine("\nHow many floors are being called ");

				// Checking if the user wants Multiple floors call
				int numFloors = Int32.Parse(Console.ReadLine()); 
				int[] floor = new int[numFloors];
				int[] people = new int[numFloors];

				int z = 0;

				for (int i = 0; i < numFloors; i++) 
				{

					//User selection of desired floor where people are waiting
					while (true)
					{
						Console.Out.WriteLine("Please enter the NEXT FLOOR NUMBER where people are waiting..("
								+ elevator[1].getMinFloor() + " to " + elevator[1].getMaxFloor() + ")");

						floor[i] = Int32.Parse(Console.ReadLine());

						if (floor[i] < elevator[1].getMinFloor() || floor[i] > elevator[1].getMaxFloor())
						{
							Console.ForegroundColor = ConsoleColor.DarkRed;						//Color Coding for Error Message
							Console.Out.WriteLine("FLOOR NUMBER INVALID");
							Console.ForegroundColor = ConsoleColor.White;
						}
						else
						{
							break;
						}

					}

					// Optimizing the BEST ELEVATOR for that location selected by the user
					int[] unsorted = new int[elevator.Length];
					int[] sorted = new int[elevator.Length];
			
					for (int j = 0; j < elevator.Length; j++)
					{
						unsorted[j] = Math.Abs(floor[i] - elevator[j].getCurrentFloor());
						sorted[j] = unsorted[j];
					}

					Array.Sort(sorted);

					for (int j = 0; j < sorted.Length; j++)
					{
							for (int k = 0; k < unsorted.Length; k++)
							{

								{
									if (elevator[k].getIsIdle() == 0 && sorted[j] == unsorted[k])
									{
										change = elevator[k];									//Selecting the desired elevator into temp 
										z = k + 1;
										elevator[k].setIsIdle(1);
										Console.ForegroundColor = ConsoleColor.Cyan;
										Console.WriteLine("\nElevator " + z + " is selected\n");
										Console.ForegroundColor = ConsoleColor.White;
										goto LoopEnd;

									}
								}
							}
					}
					LoopEnd:
					
					// Registering the User input of amount of users waiting for on desired floor
					while (true)
					{
						Console.Out.WriteLine("Enter the NUMBER OF PEOPLE waiting at level " + floor[i] + "----->MAXIMUM "
								+ change.getMaxCapacity());

						people[i] = Int32.Parse(Console.ReadLine());

						change.setSum(people[i]);
						if (change.getSum() > 0 && change.getSum() <= (change.getMaxCapacity()))
						{
							break;
						}
						else if (change.getSum() <= 0)
						{
							change.setSum(-people[i]);
							Console.ForegroundColor = ConsoleColor.DarkRed;
							Console.Out.WriteLine("PLEASE ENTER A POSITIVE INTEGER");
							Console.ForegroundColor = ConsoleColor.White;
						}
						else
						{
							change.setSum(-people[i]);
							Console.ForegroundColor = ConsoleColor.DarkRed;
							Console.Out.WriteLine("SORRY WEIGHT LIMIT OF ELEVATOR " + z + " EXEEDED...\nOnly "
									+ (change.getMaxCapacity() - change.getSum()) + " CAN COME ON THIS FLOOR ");
							Console.ForegroundColor = ConsoleColor.White;

						}

					}

					// Moving to the selected floor Step by Step
					change.moveNext(floor[i], z);

					// Setting the number of people on the elevator and the elevator selected
					change.setPeople(people[i], z); 

					//Keeps looping until the people on the elevator is 0
					while (change.getSum() != 0)                    
					{
						// selecting the floor where people want to get down
						while (true)
						{
							Console.Out.WriteLine("Please enter your DESTINATION FLOOR(" + elevator[1].getMinFloor() + " to "
									+ elevator[1].getMaxFloor() + ")");

							floor[i] = Int32.Parse(Console.ReadLine());

							if (floor[i] < elevator[1].getMinFloor() || floor[i] > elevator[1].getMaxFloor())
							{
								Console.ForegroundColor = ConsoleColor.DarkRed;
								Console.Out.WriteLine("FLOOR NUMBER INVALID");
								Console.ForegroundColor = ConsoleColor.White;
							}
							else
							{
								break;
							}
						}

						// Registering the number of people getting down at that floor
						while (true)
						{
							Console.Out.WriteLine("Enter the NUMBER OF PEOPLE getting down at " + floor[i]);

							people[i] = Int32.Parse(Console.ReadLine());
							if (change.getSum() < people[i])
							{
								Console.ForegroundColor = ConsoleColor.DarkRed;
								Console.Out.WriteLine(
										"Sorry there are only " + change.getSum() + " people left on elevator " + z);
								Console.ForegroundColor = ConsoleColor.White;
							}
							else if (people[i] < 0)
							{
								Console.ForegroundColor = ConsoleColor.DarkRed;
								Console.Out.WriteLine("Please enter a positive integer...");
								Console.ForegroundColor = ConsoleColor.White;
							}
							else
							{
								change.setSum(-people[i]);

								change.moveNext(floor[i], z);
								change.setPeople(-people[i], z);
								break;
							}

						}

					}

					// forwarding the values from temporary elevator object to desired elevator object
					for (int k = 0; k < elevator.Length; k++)
					{
						if (z == k + 1)
						{
							elevator[k] = change;
							elevator[k].setIsIdle(-1);
							Console.WriteLine("\nElevator " + z + " has ended its action and is idle now \n");
							break;
						}

					}

				}

			}

		}
	}
}
