namespace RobotsAtWar.Server.Models
{
    class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public int WinsInARow { get; set; }
        public int Rank { get; set; }
        public bool GoodTiming { get; set; }
        public bool BallsOfSteel { get; set; }
        public bool GoodStrategy { get; set; }
        public bool IncredibleStrategy { get; set; }
        public bool Lucky { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
