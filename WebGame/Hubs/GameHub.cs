using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using WebGame.Domain;

namespace WebGame.Hubs
{
    static class CurrentInformation
    {
        public static int GlobalCount;
        public static Dictionary<string, KeyValuePair<string, int>> Players = new Dictionary<string, KeyValuePair<string, int>>();
        public static Dictionary<string, string> Observers = new Dictionary<string, string>();
        public static GameField Gf = new GameField();
        public static int GetSide = 1;
    }
 
    public class GameHub : Hub
    {
        public static Dictionary<string, int> Pd;
        private static int i = 0;

        public GameHub()
        {
           
        }

        public void AddUser(string name)
        {
            if (CurrentInformation.Players.Count >= 2 || String.IsNullOrEmpty(name)) return;
            var side = CurrentInformation.GetSide = - CurrentInformation.GetSide;
            CurrentInformation.Players.Add(Context.ConnectionId, new KeyValuePair<string, int>(name, CurrentInformation.GetSide));
            Groups.Add(Context.ConnectionId, "playroom");
        }

        public string Send(string name, string message)
        {
            if (CurrentInformation.Players.ContainsKey(Context.ConnectionId))
            {
                Clients.Group("playroom").addNewMessageToPage(name, message);
            }
            else
            {
                Clients.AllExcept(CurrentInformation.Players.Keys.ToArray()).addNewMessageToPage(name, message);
            }
            return "";  
        }

        public string AddUnit(string name)
        {
            if (CurrentInformation.Players.Count != 2) return "error";
            var unit = UnitDb.GetUnit(name);
            unit.Side = (Side) CurrentInformation.Players[Context.ConnectionId].Value;
            if (i < 3)
            {
                CurrentInformation.Gf.AddUnit(unit, (GamePosition) i);
                i++;
            }
            Clients.All.addNewUnitInTheMap(unit, unit.Side);
            return null;
        }

        public override Task OnDisconnected()
        {
            CurrentInformation.Players.Remove(Context.ConnectionId);
            return base.OnDisconnected();
        }
    }
}