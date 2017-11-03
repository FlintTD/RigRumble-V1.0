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

    public class Device
    {
        protected string _name;
        protected double _baseValue;
        protected int _weight;
        protected int _maxDurability;
        protected int _currentDurability;
        protected short _size;
        protected bool _broken;

        public string Name
        {
            get { return _name; }
        }

        public double baseValue
        {
            get { return _baseValue; }
            set { _baseValue = value; }
        }

        public double Value
        {
            get { return this.baseValue * (_currentDurability / _maxDurability); }
        }

        public int Weight
        {
            get { return _weight; }
        }

        public int MaxDurability
        {
            get { return _maxDurability; }
            set { _maxDurability = value; }
        }

        public int CurrentDurability
        {
            get { return _currentDurability; }
        }

        public short Size
        {
            get { return _size; }
        }

        public bool Broken
        {
            get { return _broken; }
        }

        public void damage(int v)
        {
            this._currentDurability -= v;
            if (this._currentDurability <= 0)
            {
                this._broken = true;
            }
        }

        public void repair(int v)
        {
            this._currentDurability += v;
            if (this._currentDurability > 0)
            {
                this._broken = false;
            }
        }

        // Constructor
        public Device(string n, double v, int w, int md, int cd, short s)
        {
            this._name = n;
            this._baseValue = v;
            this._weight = w;
            this._maxDurability = md;
            this._currentDurability = cd;
            this._size = s;
            if (cd >= 0)
            {
                this._broken = false;
            }
            else
            {
                this._broken = true;
            }
        }
    }

    public class Engine : Device
    {
        private double _basePower;
        private double _efficiency;

        public double BasePower
        {
            get { return _basePower; }
        }

        public double Power
        {
            get { return _basePower + Math.Ceiling(Math.Pow((_basePower * (_currentDurability / _maxDurability)), (double)1 / 3)) ; }
        }

        public double MaxPower
        {
            get { return _basePower * 2; }
            set { _basePower = value / 2; }
        }

        public double Efficiency
        {
            get { return _efficiency; }
            set { _efficiency = value; }
        }
        
        // Constructors
        public Engine(double p, double e, string n, double v, int w, int md, int cd, short s) : base(n, v, w, md, cd, s)
        {
            this._basePower = p;
            this._efficiency = e;
            this._name = n;
            this._baseValue = v;
            this._weight = w;
            this._maxDurability = md;
            this._currentDurability = cd;
            this._size = s;
            if (cd >= 0)
            {
                this._broken = false;
            }
            else
            {
                this._broken = true;
            }
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
