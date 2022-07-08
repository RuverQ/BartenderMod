using System.Reflection;

// The title of your mod, as displayed in menus
[assembly: AssemblyTitle("BartenderMod")]

// The author of the mod
[assembly: AssemblyCompany("RuverQ, Sandwinq")]

// The description of the mod
[assembly: AssemblyDescription("Adds a couple of things. But more importanly adds BEER!")]

// The mod's version
[assembly: AssemblyVersion("0.1")]

namespace DuckGame.BartenderMod
{
    public class BartenderMod : Mod
    {
        // The mod's priority; this property controls the load order of the mod.
        public override Priority priority
        {
            get { return base.priority; }

        }

        // This function is run before all mods are finished loading.
        protected override void OnPreInitialize()
        {
            base.OnPreInitialize();
        }

        // This function is run after all mods are loaded.
        protected override void OnPostInitialize()
        {
            base.OnPostInitialize();

            if (configuration.isWorkshop)
            {
                DevConsole.Log("Workshop version of Bartender Mod is loaded", Color.DarkGray);
            }
            else
            {
                DevConsole.Log("Local version of Bartender Mod is loaded", Color.Yellow);
            }
        }
    }
}
