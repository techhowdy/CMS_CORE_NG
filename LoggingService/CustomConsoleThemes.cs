using System.Collections.Generic;
using Serilog.Sinks.SystemConsole.Themes;

namespace LoggingService
{
    public static class CustomConsoleThemes
    {
		public static CustomConsoleTheme VisualStudioMacLight
		{
			get;
		} = new CustomConsoleTheme(new Dictionary<ConsoleThemeStyle, string>
		{
			[ConsoleThemeStyle.Text] = "\u001b[30m",
			[ConsoleThemeStyle.SecondaryText] = "\u001b[30m",
			[ConsoleThemeStyle.TertiaryText] = "\u001b[30m",
			[ConsoleThemeStyle.Invalid] = "\u001b[30m",
			[ConsoleThemeStyle.Null] = "\u001b[30m",
			[ConsoleThemeStyle.Name] = "\u001b[30m",
			[ConsoleThemeStyle.String] = "\u001b[30m",
			[ConsoleThemeStyle.Number] = "\u001b[30m",
			[ConsoleThemeStyle.Boolean] = "\u001b[30m",
			[ConsoleThemeStyle.Scalar] = "\u001b[30m",
			[ConsoleThemeStyle.LevelVerbose] = "\u001b[30m",
			[ConsoleThemeStyle.LevelDebug] = "\u001b[44;1m\u001b[37;1m",
			[ConsoleThemeStyle.LevelInformation] = "\u001b[42;1m\u001b[37;1m",
			[ConsoleThemeStyle.LevelWarning] = "\u001b[43;1m\u001b[37;1m",
			[ConsoleThemeStyle.LevelError] = "\u001b[41;1m\u001b[37;1m",
			[ConsoleThemeStyle.LevelFatal] = "\u001b[46;1m\u001b[37;1m"
		});

	}
}

/* CUSTOM THEME CREATED BY TECHHOWDY FOR SERILOG => VISUALSTUDIOMACLIGHT
 * BLOG => https://www.techhowdy.com
 * YOUTUBE CHANNEL => https://www.youtube.com/channel/UC58AAnVqw_sF6LBAKJJES4Q
 * FOR ANSI CODES => https://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html
 */
