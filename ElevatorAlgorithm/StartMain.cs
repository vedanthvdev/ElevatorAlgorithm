using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorAlgorithm
{

    class StartMain
    {
        static void Main(string[] args)
        {
			//Assigning all default values like minFloor,maxFloor,maxCapacity and number of elevators from a text file
			//this could be used but have to create the file according to the location

			List<int> termsList = new List<int>();

			String filepath = @"../../../Variables.txt";
			List<string> lines = File.ReadAllLines(filepath).ToList();

			foreach(var line in lines){

				string[] entries = line.Split('=');
                
				termsList.Add(Int32.Parse(entries[1]));

            }

			//these variables can be changed in Variables.txt file according to user preferences 

			//numOfElevators = termsList[0];
			//minFloor = termsList[1];
			//maxFloor = termsList[2];
			//maxCapacity = termsList[3];


			Elevator[] elevator = new Elevator[termsList[0]];

			// Assigning Lowest Floor
			//Highest Floor
			//Maximun Capacity 
			//to all the elevators present
			for (int i = 0; i < termsList[0]; i++)
			{ 
				elevator[i] = new Elevator(termsList[1], termsList[2], termsList[3]);

			}

			Algo a = new Algo(elevator);

			while (true)
			{

				a.elevatorStatus();
				
				a.Run();

				
			}

		}
	}
}
