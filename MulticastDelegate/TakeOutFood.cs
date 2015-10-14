using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MulticastDelegate
{
    // decclare the delegate object to hold the OrderTotalDelegate()
    public delegate void OrderTotalDelegate(List<FoodItem> customerOrder, ref decimal orderCost);
    
    public struct FoodItem
    {
        public string foodName;
        public decimal baseCost;
        public FoodItem(string name, decimal cost)
        {
            foodName = name;
            baseCost = cost;
        }
        public override string ToString()
        {
            return "Food: " + this.foodName + "Base Cost: " + this.baseCost;
        }
    }

    // foundation class used for each of the take out types by food grade
    abstract class TakeOutFood
    {
        public virtual void calcOrderTotal(List<FoodItem> customerOrder, ref decimal orderrCost) { }
        public virtual void printOrder(List<FoodItem> customerOrder, ref decimal orderTotal) { }
        public abstract List<FoodItem> GetFoodItems();

        public string steak = "steak";
        public string shrimp = "shrimp";
        public string lobster = "lobster";
        public string vegetables = "vegatables";
        public decimal steakCost = 21.00m;
        public decimal shrimpCost = 22.00m;
        public decimal lobsterCost = 27.00m;
        public decimal vegeCost = 5.00m;
        public bool priceOverride;
        public decimal foodSurcharge = 0m;

        public static TakeOutFood getFoodObject(string foodType)
        {
            if (foodType.Equals("P"))
            {
                return new PrimeFood();
            }
            if (foodType.Equals("C"))
            {
                return new ChoiceFood();
            }
            if (foodType.Equals("E"))
            {
                return new EconomyFood();
            }
            return null;
        }

    }


    class PrimeFood : TakeOutFood
    {
        List<FoodItem> foodList;
        
        public PrimeFood()
        {
            Console.WriteLine("*** PrimeFood Constructor ***\n");
            foodSurcharge = .25m; 
        }

        public override List<FoodItem> GetFoodItems()
        {
            this.foodList = new List<FoodItem>();
            foodList.Add(new FoodItem(steak, steakCost));
            foodList.Add(new FoodItem(shrimp, shrimpCost));
            foodList.Add(new FoodItem(lobster, lobsterCost));
            foodList.Add(new FoodItem(vegetables, vegeCost));
            return this.foodList;
        }


        public override void calcOrderTotal(List<FoodItem> customerOrder, ref decimal orderTotal)
        {
            for (int i = 0; i < customerOrder.Count(); i++)
            {
                FoodItem f = customerOrder[i];
                f.baseCost = f.baseCost + (f.baseCost * foodSurcharge);
                orderTotal += f.baseCost;
            }
        }

        public override void printOrder(List<FoodItem> customerOrder, ref decimal orderTotal)
        {
            Console.WriteLine("\nPrime Quality - Food Order includes Price Override of {0:P}", foodSurcharge);
            foreach (FoodItem f in customerOrder)
            {
                Console.WriteLine("{0} - {1:C}", f.foodName, f.baseCost );
            }
            Console.WriteLine("Total Food Order {0:C}", orderTotal);
        }

    }

    class ChoiceFood : TakeOutFood
    {
        List<FoodItem> foodList;
        public ChoiceFood()
        {
            Console.WriteLine("*** ChoiceFood Constructor ***\n");
            foodSurcharge = .10m;
        }

        public override List<FoodItem> GetFoodItems()
        {
            this.foodList = new List<FoodItem>();
            foodList.Add(new FoodItem(steak, steakCost));
            foodList.Add(new FoodItem(shrimp, shrimpCost));
            foodList.Add(new FoodItem("burger", 7.50m));
            foodList.Add(new FoodItem("Fries", 2.75m));
            foodList.Add(new FoodItem(vegetables, vegeCost));
            return this.foodList;
        }

        public override void calcOrderTotal(List<FoodItem> customerOrder, ref decimal orderTotal)
        {
            for (int i = 0; i < customerOrder.Count(); i++)
            {
                FoodItem f = customerOrder[i];
                f.baseCost = f.baseCost + (f.baseCost * foodSurcharge);
                orderTotal += f.baseCost;
            }
        }

        public override void printOrder(List<FoodItem> customerOrder, ref decimal orderTotal)
        {
            Console.WriteLine("\nChoice Quality - Food Order includes Price Override of {0:P}\n", foodSurcharge);
            foreach (FoodItem f in customerOrder)
            {
                Console.WriteLine("{0} - {1:C}", f.foodName, f.baseCost);
            }
            Console.WriteLine("Total Food Order {0:C}", orderTotal);
        }
    }

    class EconomyFood : TakeOutFood
    {
        List<FoodItem> foodList;
        public EconomyFood()
        {
            Console.WriteLine("*** EconomyFood Constructor ***\n");
            foodSurcharge = 0.0m;
        }

        public override List<FoodItem> GetFoodItems()
        {
            this.foodList = new List<FoodItem>();
            foodList.Add(new FoodItem("mac and cheese", 5.75m));
            foodList.Add(new FoodItem("chicken soup", 3.50m));
            foodList.Add(new FoodItem("burger", 7.50m));
            foodList.Add(new FoodItem("Fries", 2.75m));
            foodList.Add(new FoodItem(vegetables, vegeCost));
            return this.foodList;
        }

        public override void calcOrderTotal(List<FoodItem> customerOrder, ref decimal orderTotal)
        {
            for (int i = 0; i < customerOrder.Count(); i++)
            {
                FoodItem f = customerOrder[i];
                f.baseCost = f.baseCost + (f.baseCost * foodSurcharge);
                orderTotal += f.baseCost;
            }
        }

        public override void printOrder(List<FoodItem> customerOrder, ref decimal orderTotal)
        {
            Console.WriteLine("\nEconomy Food Order - includes Price Override of {0:P}\n", foodSurcharge);
            foreach (FoodItem f in customerOrder)
            {
                Console.WriteLine("{0} - {1:C}", f.foodName, f.baseCost);
            }
            Console.WriteLine("Total Food Order {0:C}", orderTotal);
        }

    }
}
