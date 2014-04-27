namespace WebGame.Domain
{
    public class BattleInformationResponse
    {
        public string AttackResult { get; set; }
        public double Damage { get; set; }
        public GamePosition PositionTurn { get; set; }
        public string SideTurn { get; set; }
    }
}
