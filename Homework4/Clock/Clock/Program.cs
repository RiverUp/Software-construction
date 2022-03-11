using System;

namespace Clock
{
    public delegate void TickHandler(object sender);
    public delegate void ClockHandler(object sender);
    public class Clock
    {
        public Date d;
        public Clock()
        {
            d = new Date();
        }
        public DateTime Alarm { set { d.Dalarm = value; d.onClock += new ClockHandler(d_onClock); } }
        public void Run()
        {
            try
            {
                if (d.Dt > d.Dalarm)
                {
                    throw new Exception();
                }
                d.onTick += new TickHandler(d_Tick);
                d.onClock += new ClockHandler(d_onClock);
                while (d.Dt.Hour != d.Dalarm.Hour || d.Dt.Minute != d.Dalarm.Minute || d.Dt.Second != d.Dalarm.Second)
                {
                    d.Tick();
                }
                d.Clock();
            }
            catch
            {
                Console.WriteLine("the clock time input is wrong");
            }
        }
        void d_Tick(object sender)
        {
            Console.WriteLine(d.Dt);
        }
        void d_onClock(object sender)
        {
            Console.WriteLine("it's time!!!!!");
        }
        
    }
    public class Date
    {
        DateTime dt;
        DateTime dalarm;
        public DateTime Dalarm { get { return dalarm;} set { dalarm = value; } }
        public DateTime Dt { get { return dt; } }
        public Date()
        {
            dt = DateTime.Now;
        }

        public event TickHandler onTick;
        public event ClockHandler onClock;
        public void Tick()
        {
            DateTime temp = DateTime.Now;
            if (temp.Hour> dt.Hour||temp.Minute>dt.Minute||temp.Second>dt.Second)
            {
                dt = temp;
                onTick(this);
            }
        }
        public void Clock()
        {
            if (DateTime.Now != dalarm)
            {
                onClock(this);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Clock c = new Clock();
            Console.WriteLine("please input your clock time:(xx.xx.xx)");
            String s=Console.ReadLine();
            String[] s2 = new String[3];
            s2 = s.Split(".");
            double hour, min, sec;
            DateTime date = DateTime.Now;
            hour = Double.Parse(s2[0]) - date.Hour;
            min = Double.Parse(s2[1]) - date.Minute;
            sec = Double.Parse(s2[2]) - date.Second;
            c.d.Dalarm = date.AddHours(hour).AddMinutes(min).AddSeconds(sec);
            c.Run();
        }
    }
}
