using Spectre.Console;
using Metronome.Controllers;

namespace Metronome.Views
{
    internal class SCInterface
    {
        static void Interface()
        {
            var table = new Table();
            table.AddColumn(new TableColumn("Screen").Centered());
            table.AddRow("left");
            table.Expand();
            AnsiConsole.Write(table);
        }
    }
}
