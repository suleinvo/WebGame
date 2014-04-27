using System;
using System.CodeDom;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebGame.Domain;

namespace WebGame.Tests
{
    [TestClass]
    public class MechanicsTest
    {
        private GameField _field;
        private Unit _meleeUnit;
        private Unit _rangeUnit;

        [TestInitialize]
        public void InitTest()
        {
            _field = new GameField();

            _meleeUnit = new Unit()
            {
                Id = 1,
                CurrentHealth = 50,
                Damage = 20,
                Defence = 20,
                DisplayName = "Knight",
                Health = 200,
                Initiative = 20,
                UnitType = UnitType.Melee
            };

            _rangeUnit = new Unit()
            {
                Id = 2,
                CurrentHealth = 40,
                Damage = 20,
                Defence = 0,
                DisplayName = "Archer",
                Health = 120,
                Initiative = 30,
                UnitType = UnitType.Range
            };

            _field.AddUnit(_meleeUnit.GetUnitWithSide(Side.LeftSide), GamePosition.FrontCenter);
            _field.AddUnit(_meleeUnit.GetUnitWithSide(Side.LeftSide), GamePosition.FrontLeft);
            _field.AddUnit(_rangeUnit.GetUnitWithSide(Side.LeftSide), GamePosition.RearCenter);
            _field.AddUnit(_meleeUnit.GetUnitWithSide(Side.RightSide), GamePosition.FrontCenter);
            _field.AddUnit(_meleeUnit.GetUnitWithSide(Side.RightSide), GamePosition.FrontLeft);
            _field.AddUnit(_rangeUnit.GetUnitWithSide(Side.RightSide), GamePosition.RearCenter);

        }

        [TestMethod]
        public void GameCicleTest()
        {
            var battleMechanic = _field.GetTurnBattleQueue();
            var first = battleMechanic.Dequeue().Value;
            var secont = battleMechanic.Dequeue().Value;
            //first.Attack(secont);
        }
    }
}
