using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LegendofSparta.ItemClass
{
    //아이템 타입
    enum ITEMTYPE
    {
        Weapon, Head, Armor, Gold
    }

    //스탯 타입
    enum STATSTYPE
    {
        Atk,Def,Gold
    }

    //아이템 정렬 기준
    public enum ITEMSORT
    {
        Name,Equip,Atk,Def,Price
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

        public Item()
        {
            Name = " ";
            Stats =" ";
            Description = " ";
        } 
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
