using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace WebGame.Domain
{
    public enum UnitType
    {
        Melee,
        Range,
        Mage
    }

    public enum Side
    {
        LeftSide = 1,
        RightSide = -1
    }

    public class Unit
    {
        [BsonId]
        [BsonRequired]
        public string Name { get; set; }
        [BsonRequired]
        public double Health { get; set; }
        [BsonRequired]
        public double Damage { get; set; }
        [BsonRequired]
        public double Defence { get; set; }
        [BsonRequired]
        public string DisplayName { get; set; }
        [BsonRequired]
        public UnitType UnitType { get; set; }
        [BsonRequired]
        public int Initiative { get; set; }
        [BsonRequired]
        public Dictionary<string, List<double>> SpritePoints { get; set; }
        [BsonRequired]
        public string SpriteLink { get; set; }

        [BsonIgnore]
        private double _currentHealth;

        [BsonIgnore]
        public double CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                if (CurrentHealth < 0)
                    IsDead = true;
                _currentHealth = value;
            }
        }

        [BsonIgnore]
        public bool IsDead { get; private set; }

        [BsonIgnore]
        public Side Side { get; set; }

        public Unit()
        {
            _currentHealth = Health;
            IsDead = false;
        }

        public Unit GetUnitWithSide(Side side)
        {
            var newUnitSide = MemberwiseClone() as Unit;
            if (newUnitSide != null)
            {
                newUnitSide.Side = side;
            }
            return newUnitSide;
        }

        public void Attack(IEnumerable<Unit> defenders)
        {
            foreach (var defender in defenders)
            {
                if (defender.Side == Side) return;
                defender.CurrentHealth -= Damage - (Damage/100*Defence);
                if (defender.CurrentHealth <= 0)
                {
                    defender.IsDead = true;
                }
            }
        }

        private IEnumerable<Unit> GetTargets(GamePosition pDefender, GamePosition dAttacker, IDictionary<GamePosition, Unit> preTargets)
        {
            if (UnitType == UnitType.Mage) return preTargets.Values.ToList();
            if (UnitType == UnitType.Range)
            {
                Unit preUnit;
                var units = new List<Unit>();
                if (preTargets.TryGetValue(pDefender, out preUnit))
                {
                    units.Add(preUnit);
                }
                return units;
            }
            if (UnitType == UnitType.Melee)
            {
                var units = new List<Unit>();
                Unit preUnit;
                if (dAttacker == GamePosition.FrontLeft)
                {
                    if ((pDefender == GamePosition.FrontLeft || pDefender == GamePosition.FrontCenter) && preTargets.TryGetValue(pDefender, out preUnit))
                    {
                        units.Add(preUnit);
                    }
                }
                else if (dAttacker == GamePosition.FrontCenter)
                {
                    if ((pDefender == GamePosition.FrontLeft || pDefender == GamePosition.FrontCenter || pDefender == GamePosition.FrontRight) && preTargets.TryGetValue(pDefender, out preUnit))
                    {
                        units.Add(preUnit);
                    }
                }
                else if (dAttacker == GamePosition.FrontRight)
                {
                    if ((pDefender == GamePosition.FrontCenter || pDefender == GamePosition.FrontRight) && preTargets.TryGetValue(pDefender, out preUnit))
                    {
                        units.Add(preUnit);
                    }
                }
                return units;
            }
            return null;
        }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
