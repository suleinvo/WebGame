using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace WebGame.Domain
{
    public enum GamePosition
    {
        FrontLeft, FrontCenter, FrontRight, RearLeft, RearCenter, RearRight
    }

    public class GameField
    {
        private readonly Dictionary<GamePosition, Unit> _leftSideUnits;
        private readonly Dictionary<GamePosition, Unit> _rightSideUnits;

        public void AddUnit(Unit unit, GamePosition position)
        {
            if (unit.Side == Side.LeftSide)
            {
                _leftSideUnits.Add(position, unit);
                return;
            }
            if (unit.Side == Side.RightSide)
            {
                _rightSideUnits.Add(position, unit);
            }
        }

        public void DeleteLeftUnit(GamePosition position)
        {
            _leftSideUnits.Remove(position);
        }

        public void DeleteRightUnit(GamePosition position)
        {
            _rightSideUnits.Remove(position);
        }

        public Queue<KeyValuePair<GamePosition, Unit>> GetTurnBattleQueue()
        {
            var list = _leftSideUnits.ToList();
            list.AddRange(_rightSideUnits.ToList());
            return new Queue<KeyValuePair<GamePosition, Unit>>(list.OrderByDescending(t => t.Value.Initiative)
                                                                   .Where(t=>!t.Value.IsDead));
        }

        public GameField()
        {
            _leftSideUnits = new Dictionary<GamePosition, Unit>();
            _rightSideUnits = new Dictionary<GamePosition, Unit>();
        }
    }
}
