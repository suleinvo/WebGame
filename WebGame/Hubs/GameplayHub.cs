using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebGame.Domain;
using Unit = WebGame.Domain.Unit;

namespace WebGame.Hubs
{
    public class GameplayHub : Hub
    {
        public void AddUnit(string unitName)
        {
            var unit = UnitDb.GetUnit(unitName);
            Clients.All.addNewUnitInTheMap(unitName);
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}