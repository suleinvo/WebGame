using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebGame.Domain;
using Unit = WebGame.Domain.Unit;

namespace WebGame.Hubs
{
    public class GameHub : Hub
    {
        public string Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
            return "";
        }

        public void AddUnit(string name)
        {
            var unit = UnitDb.GetUnit(name);
            Clients.All.addNewUnitInTheMap(unit);
        }
    }
}