using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using RobotsAtWar.Server.Enums;
using RobotsAtWar.Server.MoveTimers.Atack;
using RobotsAtWar.Server.MoveTimers.Defend;
using RobotsAtWar.Server.MoveTimers.Rest;
using RobotsAtWar.Server.Potions;
using RobotsAtWar.Server.Shields;
using RobotsAtWar.Server.Skills;
using RobotsAtWar.Server.Weapons;

namespace RobotsAtWar.Server
{
    public class BattleField
    {
        private readonly Dictionary<Guid, Warrior> _warriorByGuid = new Dictionary<Guid, Warrior>();
        private Guid _challengerGuid;
        private Guid _theChallengedGuid;
        private bool _bothUsersOnline;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(Warrior));

        public bool RegisterWarrior(Guid myGuid)
        {
            if (_challengerGuid == myGuid)
            {
                Logger.Error("User with same username allready exists in this room");
                return false;
            }
            _warriorByGuid.Add(
                myGuid, 
                new Warrior(
                    myGuid, 
                    new AttackSkill(new BareHandsMoveTimer(), new BareHands()),
                    new DefenceSkill(new DefenceTimer(), new LightShield()),
                    new CheckSkill(), 
                    new RestSkill(new RestTimer(), new Potion())
                    )
                );
           // _warriorByGuid.Add(myGuid, new Warrior(myGuid, new DoubleSidedAxe(), new DoubleSidedAxeMoveTimer()));

            if (_warriorByGuid.Count == 2)
            {
                _theChallengedGuid = myGuid;
                _warriorByGuid.First().Value.Enemy = _warriorByGuid.Last().Value;
                _warriorByGuid.Last().Value.Enemy = _warriorByGuid.First().Value;
                return true;
            }
                _challengerGuid = myGuid;
                return true;
        }

        public AttackOutcome Attack(Guid myGuid, Strength strength)
        {
            return GetWarriorByGuid(myGuid).Attack(strength);
        }

        public WarriorState Check(Guid myGuid)
        {
            return GetWarriorByGuid(myGuid).Check();
        }

        public DefenceOutcome Defend(Guid myGuid, int time)
        {
            return GetWarriorByGuid(myGuid).Defend(time);
        }

        public RestOutcome Rest(Guid myGuid, int time)
        {
            return GetWarriorByGuid(myGuid).Rest(time);
        }

        public bool IsBattleOver()
        {
            return !_warriorByGuid.First().Value.IsAlive() || !_warriorByGuid.Last().Value.IsAlive();
        }

        public Warrior GetWarriorByGuid(Guid myGuid)
        {
            return _warriorByGuid[myGuid];
        }

        public string CheckForWinner(Guid myGuid)
        {
            if (GetWarriorByGuid(myGuid).IsAlive())
            {
                if (Database.UserExists(myGuid))
                {
                    GiveAchievements(myGuid);

                    RatingManager.UpdateWarriorRating(_theChallengedGuid, _challengerGuid);
                }
                Console.WriteLine(_warriorByGuid[myGuid] == _warriorByGuid.First().Value
                    ? "Host has won"
                    : "Joiner has won");
                
                return "WINNER";
            }
            return "NOT WINNER";
        }

        public bool AreUsersReady()
        {
            return _bothUsersOnline;
        }

        private void GiveAchievements(Guid userGuid)
        {
            
                if (_warriorByGuid[userGuid].WarriorState.Life <= 3 && _warriorByGuid[userGuid].WarriorState.Life > 0)
                {
                    Database.SetAchievement(userGuid, "Lucky");
                }
                Database.AddWin(userGuid);

                int wins = Database.GetWinsInARow(userGuid);
                switch (wins)
                {
                    case 3:
                        Database.SetAchievement(userGuid, "Good Strategy");
                        break;
                    case 5:
                        Database.SetAchievement(userGuid, "Incredible Strategy");
                        break;
                }

                Database.ResetWins(userGuid == _challengerGuid ? _theChallengedGuid : _challengerGuid);
            
        }

        public void BothUsersJoined()
        {
            _bothUsersOnline = true;
        }

        public WarriorState MyStats(Guid myGuid)
        {
            return GetWarriorByGuid(myGuid).WarriorState;
        }
    }
}
