using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameName3
{
    public class Inventory
    {
        public List<Item> items;
        public int size;
        public Inventory()
        {
            size = 10;
        }
    }


    public class Item
    {
        public int itemID;
        public Item()
        {
            itemID = 0;
        }

        public Item( int id )
        {
            itemID = id;
        }
    }
}


