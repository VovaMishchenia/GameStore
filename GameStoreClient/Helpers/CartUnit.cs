using GameStoreClient.Models;
using GameStoreDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStoreClient.Helpers
{
    public class CartUnit
    {
        public GameViewModel Game { get; set; }
        public int Amount { get; set; }
        public double GetAllPrice()
        {
            return Amount * Game.Price;
        }
    }
}