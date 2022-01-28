using System;

namespace SurviveTheWoods
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new STWGame())
                game.Run();
        }
    }
}
