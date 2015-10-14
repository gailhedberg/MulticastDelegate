using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* This project demonstrates the creation and usage of a Multicast Delegates.
* 
* The console based app simulates collecting food orders for a takeout restaurant
*  the class TakeOutFood has a base class and 3 derived classes
*  PrimeFood class, ChoiceFood class, and EconomyFood class
*  the user is prompted to get started on their order by first selecting a 
*  food type : prime, choice, economy
*  is then presented with menu options
*  their order is totaled using the method assigned to the OrderTotalDelegate
*  which is unique for each food type.
*/


namespace MulticastDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Delegates Drill ***\n");

            //declare local variables
            List<FoodItem> foodItems = new List<FoodItem>();
            List<FoodItem> customerOrder = new List<FoodItem>();
            OrderTotalDelegate orderTotalDel;
            TakeOutFood takeOutObj;
            string result;
            bool keepLooping = true;


            //greet user and loop until a valid character is entered
            Console.WriteLine("Welcome to Take Out Time!\n");

            do
            {
                Console.Write("Choose Food Type: P = Prime, C = Choice, E = Economy, or press 'X' to exit.");
                result = Console.ReadLine();
                result = result.ToUpper();
                if (result.Equals("P") || result.Equals("C") || result.Equals("E") || result.Equals("X"))
                {
                    keepLooping = false;
                }
            } while (keepLooping);

            // create an object to represent the food type, get it's base food list and 
            // store it's calcTotal method in a delegate
            if (!result.Equals("X"))
            {
                takeOutObj = TakeOutFood.getFoodObject(result);

                if (takeOutObj != null)
                {
                    foodItems = takeOutObj.GetFoodItems();
                    orderTotalDel = takeOutObj.calcOrderTotal;
                    orderTotalDel += takeOutObj.printOrder;
                    
                      
                    // loop to get food order, then call the calc method assigned to the delegate
                    // and finally, present a total to the user
                    Console.WriteLine("Press 'Y' to accept menu choice.");

                    foreach (FoodItem f in foodItems)
                    {
                        Console.Write("Would you like {0} at {1:C} ", f.foodName, f.baseCost);
                        result = Console.ReadLine();
                        result = result.ToUpper();
                        if (result.Equals("Y"))
                        {
                            customerOrder.Add(new FoodItem(f.foodName, f.baseCost));
                        }
                    }


                    decimal orderTotal = 0.0m;
                    orderTotalDel(customerOrder, ref orderTotal);
 //                   Console.WriteLine("Your order total is: {0:C}", orderTotal);
                }
            } 
            if (result.Equals("X"))
            {
                Console.WriteLine("Bye.");
            }

            Console.ReadKey();

        }

    }
}

