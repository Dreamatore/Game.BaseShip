namespace Game;

using System.ComponentModel.Design;
using System.Runtime.ExceptionServices;




public class Program
{
    private static void Main(string[] args)
    {
        var first = new Tank();
        var second = new Критовик();
        var third = new Ловкач();
        var referee = new FightReferee(first, second, third);
        referee.Fight();
    }



    public class FightReferee
    {
        private readonly BaseShip _first;
        private readonly BaseShip _second;
        private readonly BaseShip _third;
        public FightReferee(BaseShip first, BaseShip second, BaseShip third)
        {
            _first = first;
            _second = second;
            _third = third;
        }
        //private bool AttackTick(BaseShip a, BaseShip b, BaseShip c)
        
        public void Fight()
        {
            var random = new Random();
            var BeginningOrderChance = random.Next(0, 100);
            var firstAttack = BeginningOrderChance % 2 == 0;
            var secondAttack = BeginningOrderChance % 2 == 0;
            var thirdAttack = BeginningOrderChance % 2 == 0;
            var FirstAttackOrder = firstAttack;
            var SecondAttackOrder = secondAttack;
            var ThirdAttackOrder = thirdAttack;
            var FirstAttackSecondAndThirdHappened = false;
            var SecondAttackFirstAndThirdHappened = false;
            var ThirdAttackFirstAndSecondHappened = false;
            while (_first.HP >= 0 || _second.HP >= 0 || _third.HP >= 0)
            {
                if (FirstAttackOrder)
                {
                    if (firstAttack)
                    {
                        if (!FirstAttackSecondAndThirdHappened)
                        {


                            var VisionFirstOverSecond = _first.VisionRange >= (_second.VisibilityRange);
                            var VisionFirstOverThird = _first.VisionRange >= (_third.VisibilityRange);
                            var FirstKillSecond = _first.Damage >= (_second.Shield + _second.HP + _second.Armour);
                            var FirstKillThird = _first.Damage >= (_third.Shield + _third.HP + _third.Armour);
                            if (FirstKillSecond && FirstKillThird)
                            {
                                _second.HP = 0;
                                Console.WriteLine($"{_second.Name}destroyed, {_first.Name} winning");
                                _third.HP = 0;
                                Console.WriteLine($"{_third.Name}destroyed, {_first.Name} won");
                                break;
                            }

                        }

                    }
                }
                else
                {   
                    
                    var SecondhpDamage = BeginningOrderChance;
                    var ThirdhpDamage = BeginningOrderChance;
                    var SecondShieldDamage = _second.Shield - _first.Damage;
                    var SecondArmourDamage = _second.Armour - _first.Damage;
                    var ThirdArmourDamage = _third.Armour - _first.Damage;
                    var ThirdShieldDamage = _third.Shield - _first.Damage;
                    if (SecondShieldDamage <= 0 || ThirdShieldDamage <= 0)
                    {
                        SecondhpDamage = SecondArmourDamage - BeginningOrderChance;
                        ThirdhpDamage = ThirdArmourDamage - BeginningOrderChance;
                    }                                  
                    else
                    {
                        _second.Shield = SecondShieldDamage;
                        FirstAttackSecondAndThirdHappened = true;
                        continue;
                    }
                    var FirstMakeSecondDie = _second.HP - SecondhpDamage;
                    if (FirstMakeSecondDie <= 0)
                    {
                        Console.WriteLine($"{_first.Name} winning, {_second.Name} destroyed");
                    }

                    if (ThirdShieldDamage <= 0)
                        ThirdhpDamage = 0 - ThirdShieldDamage;
                    else
                    {
                        _third.Shield = ThirdShieldDamage;
                        FirstAttackSecondAndThirdHappened = true;
                        continue;
                    }
                    var FirstMakeThirdDie = _third.HP - ThirdhpDamage;
                    if (FirstMakeThirdDie <= 0)
                    {
                        Console.WriteLine($"{_first.Name} won , {_third.Name} destroyed");
                    }
                    else
                    {
                        Console.WriteLine($"{_first.Name} attack over {_second.Name} and {_third.Name} happened ,{_second.Name} HP left: {_second.HP}, Shield left {_second.Shield}, Armour left{_second.Armour}, {_third.Name} HP left {_third.HP}, Shield left {_third.Shield}, Armour left{_third.Armour}");
                    }
                }
                if (SecondAttackOrder)
                {
                    if (secondAttack)
                    {
                        if (!SecondAttackFirstAndThirdHappened)
                        {
                            var VisionSecondOverFirst = _second.VisionRange >= (_first.VisibilityRange);
                            var VisionSecondOverThird = _second.VisionRange >= (_third.VisibilityRange);
                            var SecondKillFirst = _second.Damage >= (_first.Shield + _first.Armour + _first.HP);
                            var SecondKillThird = _second.Damage >= (_third.Shield + _third.Armour + _third.HP);
                        if(SecondKillFirst || SecondKillThird)
                            {
                                _first.HP = 0;
                                Console.WriteLine($"{_first.Name} destroyed");
                                _third.HP = 0;
                                Console.WriteLine($"{_third.Name}destroyed");
                                break;
                            }
                        }
                    }
                    else
                    {
                        var FirsthpDamage = 0;
                        var ThirdhpDamage = 0;
                        var FirstShieldDamage = _first.Shield - _second.Damage;
                        var ThirdShieldDamage = _third.Shield - _second.Damage;
                        if (FirstShieldDamage <= 0)
                            FirsthpDamage = 0 - FirstShieldDamage;
                        else
                        {
                            _first.Shield = FirstShieldDamage;
                            SecondAttackFirstAndThirdHappened = true;
                            continue;
                        }
                        var SecondMakeFirstDie = _first.HP - FirsthpDamage;
                        if (SecondMakeFirstDie <= 0)
                        {
                            Console.WriteLine($"{_second.Name} winning, {_first.Name} destroyed");
                        }
                        if (ThirdShieldDamage <= 0)
                            ThirdhpDamage = 0 - ThirdShieldDamage;
                        else
                        {
                            _third.Shield = ThirdShieldDamage;
                            SecondAttackFirstAndThirdHappened = true;
                            continue;
                        }
                        var SecondMakeThirdDie = _third.HP - ThirdhpDamage;
                        if (SecondMakeThirdDie <= 0)
                        {
                            Console.WriteLine($"{_second.Name} won , {_third.Name} destroyed");
                        }                          
                        }
                    }               
                else
                {
                    Console.WriteLine($"{_second.Name} attack over {_first.Name} and {_third.Name} happened ,{_first.Name} HP left: {_first.HP}, Shield left {_first.Shield}, Armour left{_first.Armour}, {_third.Name} HP left {_third.HP}, Shield left {_third.Shield}, Armour left{_third.Armour}");
                }
                if (ThirdAttackOrder)
                {
                    if (thirdAttack)
                    {
                        if (!ThirdAttackFirstAndSecondHappened)
                        {
                            var VisionThirdOverFirst = _third.VisionRange >= (_first.VisibilityRange);
                            var VisionThirdOverSecond = _third.VisionRange >= (_second.VisibilityRange);
                            var ThirdKillFirst = _third.Damage >= (_first.HP + _first.Shield + _first.Armour);
                            var ThirdKillSecond = _third.Damage >= (_second.HP + _second.Shield + _second.Armour);
                            if (ThirdKillFirst || ThirdKillSecond)
                            {
                                _first.HP = 0;
                                Console.WriteLine($"{_first.Name}destroyed, third {_third.Name} winning");
                                _second.HP = 0;
                                Console.WriteLine($"{_second.Name}destroyed, third {_third.Name} won");
                                break;
                            }
                            else
                            {
                                var FirsthpDamage = 0;
                                var SecondhpDamage = 0;
                                var FirstShieldDamage = _first.Shield - _third.Damage;
                                var SecondShieldDamage = _second.Shield - _third.Damage;
                                if (FirstShieldDamage <= 0)
                                    FirsthpDamage = 0 - FirstShieldDamage;
                                else
                                {
                                    _first.Shield = FirstShieldDamage;
                                    ThirdAttackFirstAndSecondHappened = true;
                                    continue;
                                }
                                var ThirdMakeFirstDie = _first.HP - FirsthpDamage;
                                if (ThirdMakeFirstDie <= 0)
                                {
                                    Console.WriteLine($"{_third.Name} winning, {_first.Name} destroyed");
                                }
                                if (SecondShieldDamage <= 0)
                                    SecondhpDamage = 0 - SecondShieldDamage;
                                else
                                {
                                    _second.Shield = SecondShieldDamage;
                                    continue;
                                }
                                var ThirdMakeSecondDie = _second.HP - SecondhpDamage;
                                if (ThirdMakeSecondDie <= 0)
                                {
                                    Console.WriteLine($"{_third.Name} won , {_second.Name} destroyed");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{_third.Name} attack over {_first.Name} and {_second.Name} happened ,{_first.Name} HP left: {_first.HP}, Shield left {_first.Shield}, Armour left{_first.Armour}, {_second.Name} HP left {_second.HP}, Shield left {_second.Shield}, Armour left{_second.Armour}");
                            continue;
                          
                        }
                    }
                    }               
            }
        }
    }
}


    public class BaseShip
    {
    public void SpecialAbility()
    {
        
    }
    public void Move()
    {
        VisibilityRange += Speed;
        VisionRange += Speed;
    }
        public string Name{ get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public int HP { get; set; }
        public int Shield { get; set; }
        public int Armour { get; set; }
        public int VisionRange { get; set; }
        public int VisibilityRange { get; set; }
        public int Accuracy { get; set; }
        public int HPRecovery { get; set; }
        public int ShieldRecovery { get; set; } 
    }
public class Tank : BaseShip
{
    public Tank()
    {
        Name = "Танк";
        Speed = 25;
        Damage = 40;
        HP = 1500;
        Shield = 500;
        Armour = 200;
        VisionRange = 100;
        VisibilityRange = 100;
    }
    public void Block()
    {

    }
}
public class Критовик : BaseShip
{     
    public Критовик()
    {
        Name = "Костолом";
        Speed = 50;
        Damage = 85;
        HP = 1000;
        Shield = 300;
        Armour = 100;
        VisionRange = 100;
        VisibilityRange = 75;
    }
    public void Crucial()
    {

    }
}
public class Ловкач : BaseShip
{
    public Ловкач()
    {
        Name = "Ловкач";
        Speed = 100;
        Damage = 70;
        HP = 850;
        Shield = 200;
        Armour = 50;
        VisionRange = 100;
        VisibilityRange = 25;
    }
    public void Evasion()
    {

    }
}