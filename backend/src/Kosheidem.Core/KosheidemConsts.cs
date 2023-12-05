using Kosheidem.Debugging;

namespace Kosheidem
{
    public class KosheidemConsts
    {
        public const string LocalizationSourceName = "Kosheidem";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "ea29f9268f6642b6abc3fd4ac32a0e32";
    }
}
