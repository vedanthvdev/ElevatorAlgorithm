using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorAlgorithm

{
	public class Elevator : ElevatorInterface
	{
		private int minFloor;
		private int maxFloor;
		private int maxCapacity;
		private int currentFloor;
		private int people;
		private int sum;
		private int isIdle;


		public int getSum()
		{
			return sum;
		}

		public void setSum(int sum)
		{
			this.sum += sum;
		}

		public int getPeople()
		{
			return people;
		}

		public int setPeople(int people, int z)
		{
			this.people += people;
			Console.Out.WriteLine("\nNUMBER OF PEOPLE ON ELEVATOR " + z + " NOW...>" + getSum() + "\n");
			Console.Out.WriteLine("***************MOVING TO NEXT FLOOR***************\n");
			return people;
		}

		public Elevator(int minFloor, int maxFloor, int maxCapacity)
		{
			this.minFloor = minFloor;
			this.maxFloor = maxFloor;
			this.maxCapacity = maxCapacity;

			currentFloor = minFloor;
		}

		public int getMinFloor()
		{
			return minFloor;
		}

		public int getMaxFloor()
		{
			return maxFloor;
		}

		public int getMaxCapacity()
		{
			return maxCapacity;
		}

		public int getCurrentFloor()
		{
			return currentFloor;
		}

		public void moveNext(int floor, int z)
		{

			while (floor != currentFloor)
			{
				if (currentFloor < floor)
				{
					moveUp(z);
				}
				else if (currentFloor > floor)
				{
					moveDown(z);
				}
			}

			if (currentFloor == floor)
			{
				Console.Out.WriteLine("\nELEVATOR " + z + " HAS ARRIVED AT FLOOR----> " + currentFloor);

			}
		}

		public void moveUp(int z)
		{

			currentFloor++;
			Console.Out.WriteLine("Elevator " + z + " moved up one level to : " + currentFloor + " And carrying "
					+ getPeople() + " people");

		}

		public void moveDown(int z)
		{

			currentFloor--;
			Console.Out.WriteLine("Elevator " + z + " moved down one level to : " + currentFloor + " And carrying "
					+ getPeople() + " people");


		}

		public int getIsIdle()
		{
			return isIdle;
		}

		public int setIsIdle(int a)
		{
			this.isIdle += a;
			return isIdle;
		}
	}


	public interface ElevatorInterface
	{
		int getMinFloor();
		int getMaxFloor();
		int getMaxCapacity();
		int getCurrentFloor();
		int getPeople();
		int setPeople(int people, int z);
		int getSum();
		void setSum(int sum);
		void moveUp(int z);
		void moveDown(int z);
		void moveNext(int floor, int z);
		int getIsIdle();
		int setIsIdle(int a);

	}

	
}
