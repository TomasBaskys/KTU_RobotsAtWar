using System;
using System.Threading;
using RobotsAtWar.Server.Enums;
using Action = RobotsAtWar.Server.Enums.Action;

namespace RobotsAtWar.Server
{
    public class RobotActions : IReceiver
    {
        private static Handler _h1;

        private readonly Robot _robot;
        private readonly ActionStrength _actionStrength;

        private static Action _action;

        public RobotActions(Robot robot, ActionStrength actionStrength)
        {
            if (robot == null) throw new ArgumentNullException(nameof(robot));

            _robot = robot;
            _actionStrength = actionStrength;

            InitializeChainOfResponsibility();
        }

        private void InitializeChainOfResponsibility()
        {
            _h1 = new AttackHandler();
            Handler h2 = new DefenceHandler();
            Handler h3 = new RestHandler();
            _h1.SetSuccessor(h2);
            h2.SetSuccessor(h3);
        }

        public void SetAction(Action action)
        {
            _action = action;
        }

        public int GetResult()
        {
            int result;

            switch (_action)
            {
                case Action.Attack:
                    {
                        result = Attack(_robot, _actionStrength);
                        _h1.HandleRequest(Action.Attack, result);
                    }
                    break;
                case Action.Defence:
                    {
                        result = Defence(_robot, _actionStrength);
                        _h1.HandleRequest(Action.Defence, result);
                    }
                    break;
                case Action.Rest:
                    {
                        result = Rest(_robot, _actionStrength);
                        _h1.HandleRequest(Action.Rest, result);
                    }
                    break;
                default:
                    throw new ArgumentException();
            }

            return result;
        }

        private static int Attack(Robot robot, ActionStrength attackStrength)
        {
            int damage;

            robot.Status.RobotState = RobotState.Attacking;

            Thread.Sleep((int)attackStrength * 1000);

            if (robot.Status.RobotState == RobotState.Attacking)
            {
                damage = MakeDamage(robot.Enemy, attackStrength);
                robot.Status.RobotState = RobotState.Idle;
            }
            else
            {
                damage = -1; //interupted
            }

            return damage;
        }

        private static int MakeDamage(Robot enemy, ActionStrength attackStrength)
        {
            var damage = 0;

            if (enemy.Status.RobotState != RobotState.Defending)
            {
                damage = CalculateInpact(attackStrength, 10);

                enemy.Status.Life -= damage;
                enemy.Status.RobotState = IsAlive(enemy) ? RobotState.Interrupted : RobotState.Dead;
                enemy.Status.LastReceivedDamage = damage;
            }

            return enemy.Status.RobotState != RobotState.Dead ? damage : -99; //dead;
        }

        private static int Defence(Robot robot, ActionStrength defenceStrength)
        {
            robot.Status.RobotState = RobotState.Defending;

            Thread.Sleep((int)defenceStrength * 1000);

            robot.Status.RobotState = RobotState.Idle;

            return 0;
        }

        private static int Rest(Robot robot, ActionStrength restStrength)
        {
            int healthToRestore = 0;

            robot.Status.RobotState = RobotState.Resting;

            Thread.Sleep((int)restStrength * 1000);

            if (robot.Status.RobotState == RobotState.Resting)
            {
                //Magic formula
                healthToRestore = CalculateInpact(restStrength, 5);
                robot.Status.Life += healthToRestore;

                if (robot.Status.Life > 500)
                {
                    robot.Status.Life = 500;
                }
            }

            return healthToRestore;
        }

        private static int CalculateInpact(ActionStrength actionStrength, int number)
        {
            return (number + ((int)actionStrength - 1) * 2) * (int)actionStrength;
        }

        private static bool IsAlive(Robot robot) => robot.Status.Life > 0;
    }

    public interface IReceiver
    {
        void SetAction(Action action);

        int GetResult();
    }

    public abstract class ActionCommand
    {
        protected IReceiver Receiver = null;

        public ActionCommand(IReceiver receiver)
        {
            Receiver = receiver;
        }

        public abstract int Execute();
    }

    public class AttackAction : ActionCommand
    {
        public AttackAction(IReceiver receiver) : base(receiver)
        {
        }

        public override int Execute()
        {
            Receiver.SetAction(Action.Attack);
            return Receiver.GetResult();
        }
    }

    public class DefenceAction : ActionCommand
    {
        public DefenceAction(IReceiver receiver) : base(receiver)
        {
        }

        public override int Execute()
        {
            Receiver.SetAction(Action.Defence);
            return Receiver.GetResult();
        }
    }

    public class RestAction : ActionCommand
    {
        public RestAction(IReceiver receiver) : base(receiver)
        {
        }

        public override int Execute()
        {
            Receiver.SetAction(Action.Rest);
            return Receiver.GetResult();
        }
    }

    public abstract class Handler
    {
        protected Handler Successor;

        public void SetSuccessor(Handler successor)
        {
            Successor = successor;
        }

        public abstract void HandleRequest(Action action, int result);
    }

    public class AttackHandler : Handler
    {
        public override void HandleRequest(Action action, int result)
        {
            if (action == Action.Attack)
            {
                Console.WriteLine($"Robot made {result} damage.");
            }
            else if (Successor != null)
            {
                Successor.HandleRequest(action, result);
            }
        }
    }

    public class DefenceHandler : Handler
    {
        public override void HandleRequest(Action action, int result)
        {
            if (action == Action.Defence)
            {
                Console.WriteLine("Robot switched to the defence position.");
            }
            else if (Successor != null)
            {
                Successor.HandleRequest(action, result);
            }
        }
    }

    public class RestHandler : Handler
    {
        public override void HandleRequest(Action action, int result)
        {
            if (action == Action.Rest)
            {
                Console.WriteLine($"Robot restored {result} healpoints.");
            }
            else
            {
                Console.WriteLine("Unexpected robot behavior...");
            }
        }
    }
}