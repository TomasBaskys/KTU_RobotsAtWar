namespace RobotsAtWar.Server
{
    public class Battle
    {
        public int BattleId { get; set; }

        public string HostRobotId { get; set; }

        public string HostRobotName { get; set; }

        public string OpponentRobotId { get; set; }

        public string OpponentRobotName { get; set; }

        public string BattleName { get; set; }

        public BattleState BattleState { get; set; }

        public string WinnerRobotId { get; set; }
    }

    /*public enum BattleState
    {
        Pending = 1,
        InProgress = 2,
        Done = 3,
        Canceled = 4
    }*/
}
