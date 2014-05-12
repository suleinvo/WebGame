using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebGame.Domain;
using Unit = WebGame.Domain.Unit;
using UnitType = WebGame.Domain.UnitType;

namespace WebGame.Tests
{
    [TestClass]
    public class MongoDbTests
    {
        [TestMethod]
        public void InsertTest()
        {
            var spritePoints = new Dictionary<string, List<double>>
            {
                {"idle", new List<double>() {62, 134, 62, 112}},
                {"attack", new List<double>()
                {
                    2, 2, 66, 130
                }},
                {"die", new List<double>() {2, 134, 58, 46}}
            };
            var unit = new Unit
            {
                Name = "Archer",
                Damage = 40,
                Defence = 0,
                DisplayName = "Archer",
                Initiative = 40,
                Health = 100,
                UnitType = UnitType.Range,
                SpritePoints = spritePoints,
                SpriteLink = "http://i64.fastpic.ru/big/2014/0511/c4/6fd702ed5d5545762ec8e160b83b17c4.png"
            };
            UnitDb.AddUnit(unit);
        }

        [TestMethod]
        public void FindTest()
        {
            var unit = UnitDb.GetUnit("Angel");
            Assert.IsNotNull(unit);
        }
    }
}
