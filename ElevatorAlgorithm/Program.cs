using System;
using System.ComponentModel.Design;
using System.Threading;

namespace ElevatorAlgorithm
{
	
class Program
    {
        static void Main(string[] args)
        {

			int numOfElevators = 4; // Setting Number of Elevators in Building
			int minFloor = 0;
			int maxFloor = 10;
			int maxCapacity = 5;
			Elevator[] elevator = new Elevator[numOfElevators];

			for (int i = 0; i < numOfElevators; i++)
			{ // Assigning MinFloors, MaxFloors and MaxCapacity
				elevator[i] = new Elevator(minFloor, maxFloor, maxCapacity);

			}

			Elevator change = null;					// Temporary Object

			while (true)
			{

				// Elevators Status
				for (int j = 0; j < elevator.Length; j++)
				{
					Console.Out.WriteLine("\nElevator " + (j + 1) + " is at Floor----> " + elevator[j].getCurrentFloor()
							+ "\t Number of people on it.....>" + elevator[j].getSum());
				}

				Console.Out.WriteLine("----------------++++++++-----------------");

				Console.Out.WriteLine("\nHow many floors are being called ");

				int numFloors = Int32.Parse(Console.ReadLine()); // Checking if the user wants Multiple floors call
				int[] floor = new int[numFloors];
				int[] people = new int[numFloors];

				int z = 0;

				for (int i = 0; i < numFloors; i++) 
				{

					// Selecting the floor where people are waiting
					while (true)
					{
						Console.Out.WriteLine("Please enter the NEXT FLOOR NUMBER where people are waiting..("
								+ elevator[1].getMinFloor() + " to " + elevator[1].getMaxFloor() + ")");

						floor[i] = Int32.Parse(Console.ReadLine());

						if (floor[i] < elevator[1].getMinFloor() || floor[i] > elevator[1].getMaxFloor())
						{
							Console.Out.WriteLine("Enter valid floor");
						}
						else
						{
							break;
						}

					}

					// Choosing the BEST ELEVATOR for that location
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
							for (int k = 0; k < elevator.Length; k++)
							{

								{
									if (elevator[k].getIsIdle() == 0 && sorted[j] == unsorted[k])
									{
										change = elevator[k];
										z = k + 1;
										elevator[k].setIsIdle(1);
										Console.WriteLine("\nElevator " + z + " is selected\n");
										goto LoopEnd;

									}
								}
							}
					}
					LoopEnd:
					//sfs
					// Acknowledging the number of people waiting for the elevator on selected floor
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
							Console.Out.WriteLine("PLEASE ENTER A POSITIVE INTEGER");
						}
						else
						{
							change.setSum(-people[i]);
							Console.Out.WriteLine("SORRY WEIGHT LIMIT OF ELEVATOR " + z + " EXEEDED...\n"
									+ (change.getMaxCapacity() - change.getSum()) + " CAN COME ON THIS FLOOR ");

						}

					}

					change.moveNext(floor[i], z); // Moving to the selected floor
					change.setPeople(people[i], z); // Defining the number of people present on the elevator

					// checking if the elevator still has people on it
					while (change.getSum() != 0)
					{
						// selecting the floor to Unload
						while (true)
						{
							Console.Out.WriteLine("Please enter your DESTINATION FLOOR(" + elevator[1].getMinFloor() + " to "
									+ elevator[1].getMaxFloor() + ")");

							floor[i] = Int32.Parse(Console.ReadLine());

							if (floor[i] < elevator[1].getMinFloor() || floor[i] > elevator[1].getMaxFloor())
							{
								Console.Out.WriteLine("Enter valid floor");
							}
							else
							{
								break;
							}
						}

						// Unloading the elevator
						while (true)
						{
							Console.Out.WriteLine("Enter the NUMBER OF PEOPLE getting down at " + floor[i]);

							people[i] = Int32.Parse(Console.ReadLine());
							if (change.getSum() < people[i])
							{
								Console.Out.WriteLine(
										"Sorry there are only " + change.getSum() + " people left on elevator " + z);
							}
							else if (people[i] < 0)
							{
								Console.Out.WriteLine("Please enter a positive integer...");
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
