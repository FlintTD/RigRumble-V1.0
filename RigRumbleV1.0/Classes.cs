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

        public int rigAmpHours;
        public int rigCleanWater;
        public int rigBlackWater;
        public int rigRawFood;

        public int rigOilCrude;
        public int rigOilLPG;
        public int rigOilGasoline;
        public int rigOilKerosene;
        public int rigOilDiesel;
        public int rigOilHeavy;
        public int rigCarbon;

        public int rigIronOre;
        public int rigIron;
        public int rigSteel;
        public int rigSpareParts;
        public int rigStructuralParts;
        public int rigScrapMetal;

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
