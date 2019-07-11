using System;

namespace Program
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            using var engine = new Engine(800, 400);
            engine.Run();
        }
    }
}