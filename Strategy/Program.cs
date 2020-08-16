using System;
using System.Collections.Generic;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prepare strategies
            var lowRisckStrategy = new LowRisckStrategy();
            var hiRisckStrategy = new HiRiskStrategy();

            var firstCustomer = new Risck(lowRisckStrategy);

            // Normal billing
            firstCustomer.Add(1.0, 1);

            // Start Happy Hour
            firstCustomer.Strategy = hiRisckStrategy;
            firstCustomer.Add(1.0, 2);

            // New Customer
            Risck secondCustomer = new Risck(hiRisckStrategy);
            secondCustomer.Add(0.8, 1);
            // The Customer pays
            firstCustomer.PrintCategories();

            // End Happy Hour
            secondCustomer.Strategy = lowRisckStrategy;
            secondCustomer.Add(1.3, 2);
            secondCustomer.Add(2.5, 1);
            secondCustomer.PrintCategories();
        }
    }

    class Risck
    {
        private IList<double> drinks;

        // Get/Set Strategy
        public IRiskStrategy Strategy { get; set; }

        public Risck(IRiskStrategy strategy)
        {
            this.drinks = new List<double>();
            this.Strategy = strategy;
        }

        public void Add(double price, int quantity)
        {
            this.drinks.Add(this.Strategy.GetActPrice(price * quantity));
        }

        // Payment of bill
        public void PrintCategories()
        {
            double sum = 0;
            foreach (var drinkCost in this.drinks)
            {
                sum += drinkCost;
            }
            Console.WriteLine($"Total due: {sum}.");
            this.drinks.Clear();
        }
    }

    interface IRiskStrategy
    {
        double GetActPrice(double rawPrice);
    }

    // Normal billing strategy (unchanged price)
    class LowRisckStrategy : IRiskStrategy
    {
        public double GetActPrice(double rawPrice) => rawPrice;
    }

    // Strategy for Happy hour (50% discount)
    class HiRiskStrategy : IRiskStrategy
    {
        public double GetActPrice(double rawPrice) => rawPrice * 0.5;
    }
}