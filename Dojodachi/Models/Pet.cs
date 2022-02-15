using System;

namespace Dojodachi.Models
{
    public class Pet
    {
        private int? happiness;
        private int? fullness;
        private int? energy;
        private int? meals;
        // constants
        private static int feedCost = 1;
        private static int playCost = 5;
        private static int workCost = 5;
        private static int sleepCost = 5;

        // public accessors
        // setters can deal with casting
        public int Happiness
        {
            get
            {
                if (happiness != null)
                    return (int)happiness;
                return 0;
            }
            set { happiness = (int?)value; }
        }
        public int Fullness
        {
            get
            {
                if (fullness != null)
                    return (int)fullness;
                return 0;
            }
            set { fullness = (int?)value; }
        }
        public int Energy
        {
            get
            {
                if (energy != null)
                    return (int)energy;
                return 0;
            }
            set { energy = (int?)value; }
        }
        public int Meals
        {
            get
            {
                if (meals != null)
                    return (int)meals;
                return 0;
            }
            set
            {
                if (value < 0)
                    meals = 0;
                else
                    meals = (int?)value;
            }
        }
        public bool DidWin
        {
            get
            {
                return
                (
                    energy > 100 &&
                    fullness > 100 &&
                    happiness > 100
                );
            }
        }
        public bool DidLose
        {
            get
            {
                return
                (
                    fullness < 0 ||
                    happiness < 0
                );

            }
        }
        public bool DidLike
        {
            get
            {
                Random rand = new Random();
                return rand.Next(100) > 25;
            }
        }
        // returns a message
        public string Play(string gameType)
        {
            Random rand = new Random();
            bool didLike = DidLike;

            if (gameType == "feed")
            {
                int addedFullness = rand.Next(5, 11);
                Meals -= feedCost;
                if (Meals > 0 && didLike)
                {
                    Fullness += addedFullness;
                }
                return (didLike)
                    ? $"You fed your Dojodachi. Fullness: +{addedFullness}, Meals -{feedCost}"
                    : $"You tried to feed your Dojodachi, but it didn't like it! Meals -{feedCost}";

            }
            if (gameType == "play")
            {
                Energy -= playCost;
                int addedHappiness = rand.Next(5, 11);
                if (didLike)
                {
                    Happiness += addedHappiness;
                    return $"You played with your Dojodachi. Happiness: +{addedHappiness}, Energy: -{playCost}";
                }
                return $"You played with your Dojodachi, but it didn't like :( Energy: -{playCost}";
            }
            else if (gameType == "work")
            {
                int addedMeals = rand.Next(1, 4);
                Energy -= workCost;
                Meals += addedMeals;
                return $"You worked your Dojodachi. Meals: +{addedMeals}, Energy: -{workCost}";
            }
            else
            {
                Energy += 15;
                Fullness -= sleepCost;
                Happiness -= sleepCost;
                return $"You rested your Dojodachi. Energy: +{15}, Fullness: -{sleepCost} Happiness -{sleepCost}";
            }
        }

        public Pet()
        {
            // set defaults
            happiness = 20;
            fullness = 20;
            energy = 50;
            meals = 3;
        }
        // constructor for passing session values
        public Pet(int? happiness, int? fullness, int? energy, int? meals)
        {
            this.happiness = happiness;
            this.fullness = fullness;
            this.energy = energy;
            this.meals = meals;
        }
    }
}