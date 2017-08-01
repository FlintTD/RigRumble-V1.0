using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RigRumble
{
    public class Rig
    {
        public Engine rigEngine;
        public string rigName = "Mobile Derreck 231";
        public int rigWeight;
        public int rigDecks;
        public int rigEngineDecks;
        public int rigPopulation;

        public int rigAmpHours = 0;
        public int rigCleanWater = 0;
        public int rigGreyWater = 0;
        public int rigBlackWater = 0;
        public int rigRawFood = 0;

        public int rigOilCrude = 0;
        public int rigOilLPG = 0;
        public int rigOilGasoline = 0;
        public int rigOilKerosene = 0;
        public int rigOilDiesel = 0;
        public int rigOilHeavy = 0;
        public int rigCarbon = 0;

        public int rigIronOre = 0;
        public int rigIron = 0;
        public int rigSteel = 0;
        public int rigSpareParts = 0;
        public int rigStructuralParts = 0;
        public int rigScrapMetal = 0;



        public List<int> getManifestValues()
        {
            return new List<int> {rigAmpHours, rigCleanWater, rigGreyWater, rigBlackWater, rigRawFood,
                                rigOilCrude, rigOilLPG, rigOilGasoline, rigOilKerosene, rigOilDiesel,
                                rigOilHeavy, rigCarbon, rigIronOre, rigIron, rigSteel, rigSpareParts,
                                rigStructuralParts, rigScrapMetal};
        }

        public List<string> getManifestLabels()
        {
            return new List<string> {"Amp Hours", "Clean Water : L", "Grey Water : L", "Black Water : L",
                                "Raw Food : Kg", "Crude Oil : L", "LPG : L", "Gasoline : L", "Kerosene : L",
                                "Diesel : L", "Heavy Oil : L", "Carbon : Kg", "Iron Ore : Kg", "Steel : Kg",
                                "Spare Parts : Kg", "Structural Parts : Kg", "Scrap Metal : Kg"};
        }

        public Rig() { }

        public Rig(string name, short engineDecks, short decks, short population)
        {
            rigName = name;
            rigEngineDecks = engineDecks;
            rigDecks = decks;
            rigPopulation = population;
        }
    }

    public class Engine
    {
        private string engineName;
        private float enginePower;
        private float engineEfficiency;
        private int engineWeight;
        private int engineDurability;
        private int engineValue;
        private short engineSize;

        public string getEngineName()
        {
            return engineName;
        }

        public float getEnginePower()
        {
            return enginePower;
        }

        public float getEngineEfficiency()
        {
            return engineEfficiency;
        }

        public int getEngineWeight()
        {
            return engineWeight;
        }

        public int getEngineDurability()
        {
            return engineDurability;
        }

        public int getEngineValue()
        {
            return engineValue;
        }

        public short getEngineSize()
        {
            return engineSize;
        }

        public void setEngineName(string name)
        {
            engineName = name;
        }

        public void setEnginePower(float power)
        {
            enginePower = power;
        }

        public void setEngineEfficiency(float efficiency)
        {
            engineEfficiency = efficiency;
        }

        public void setEngineWeight(int weight)
        {
            engineWeight = weight;
        }

        public void setEngineDurability(int durability)
        {
            engineDurability = durability;
        }

        public void setEngineValue(int value)
        {
            engineValue = value;
        }

        public void setEngineSize(short size)
        {
            engineSize = size;
        }

        public Engine() { }

        public Engine(string name, float power, float efficiency, int weight, int durability, int value, short size)
        {
            engineName = name;
            enginePower = power;
            engineEfficiency = efficiency;
            engineWeight = weight;
            engineDurability = durability;
            engineValue = value;
            engineSize = size;
        }
    }

    public class Person
    {
        public string personName;
        public string personJob;
        public string personMate;
        public short personHealth;
        public short personVigor;
        public short personHunger;
        public short personHappiness;
        public short personSanity;
        public short personLikeability;

        public short skillArchitecture;
        public short skillChemistry;
        public short skillCooking;
        public short skillFighting;
        public short skillHydroponics;
        public short skillKnowledge;
        public short skillLeadership;
        public short skillManufacture;
        public short skillMechanic;
        public short skillNavigation;
        public short skillTrading;
    }
}
