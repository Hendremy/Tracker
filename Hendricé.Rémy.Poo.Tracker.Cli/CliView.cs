using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hendricé.Rémy.Poo.Tracker.Cli
{
    public abstract class CliView
    {
        protected readonly Presenter _presenter;
        protected Thread _thread;

        public CliView()
        {
            _presenter = Presenter.GetInstance();
        }

        protected void StartThread()
        {
            _thread.Start();
        }

        public string AskString(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        public int AskInt(string message)
        {
            try
            {
                string choice = AskString(message);
                return Int32.Parse(choice);
            }
            catch (FormatException ex)
            {
                return -1;
            }
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteInternalError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n /!\\ {message} /!\\ \n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteUserError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n *** {message} *** \n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
