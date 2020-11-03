using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog.Sinks.SystemConsole.Themes;

namespace LoggingService
{
    public class CustomConsoleTheme : ConsoleTheme
    {
        private readonly IReadOnlyDictionary<ConsoleThemeStyle, string> _styles;

        public CustomConsoleTheme(IReadOnlyDictionary<ConsoleThemeStyle, string> styles)
        {
            if (styles == null)
            {
                throw new ArgumentNullException(nameof(styles));
            }

            this._styles = styles.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static CustomConsoleTheme VisualStudioMacLight { get; } = CustomConsoleThemes.VisualStudioMacLight;

        public override bool CanBuffer
        {
            get
            {
                return true;
            }
        }

        protected override int ResetCharCount { get; } = "\x001B[0m".Length;

        public override int Set(TextWriter output, ConsoleThemeStyle style)
        {
            string str;
            if (!this._styles.TryGetValue(style, out str))
                return 0;
            output.Write(str);
            return str.Length;
        }

        public override void Reset(TextWriter output)
        {
            output.Write("\x001B[0m");
        }
    }
}

/* CUSTOM THEME CREATED BY TECHHOWDY FOR SERILOG => VISUALSTUDIOMACLIGHT
 * BLOG => https://www.techhowdy.com
 * YOUTUBE CHANNEL => https://www.youtube.com/channel/UC58AAnVqw_sF6LBAKJJES4Q
 * FOR ANSI CODES => https://www.lihaoyi.com/post/BuildyourownCommandLinewithANSIescapecodes.html
 */
