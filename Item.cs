using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendofSparta.ItemClass
{
    enum ITEMTYPE
    {
        Weapon, Head, Armor, Shoes
    }

    enum STATSTYPE
    {
        Atk,Def
    }
    internal class Item
    {
        public  ITEMTYPE ItemType { get; set; }
        public STATSTYPE StatsType { get; set; }
        public string Name { get; set; }
        public string Stats { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public bool IsEquip { get; set; }
        public Item(ITEMTYPE itemiype,string name, STATSTYPE statstype, string stats, string description, int price, bool isequip ) 
        {
            ItemType = itemiype;
            Name = name;
            StatsType = statstype;
            Stats = stats; 
            Description = description;
            Price = price; 
            IsEquip = isequip;
        }
    }
}
