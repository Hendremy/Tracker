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

        protected string AskString(string message)
        {
            Console.WriteLine(message);
            Console.Write(">");
            return Console.ReadLine();
        }

        protected int AskInt(string message)
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

        protected void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        protected void WriteInternalError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n /!\\ {message} /!\\ \n");
            ResetColors();
        }

        protected void WriteUserError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n *** {message} *** \n");
            ResetColors();
        }

        protected void WriteWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n {message} \n");
            ResetColors();
        }

        private void ResetColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
