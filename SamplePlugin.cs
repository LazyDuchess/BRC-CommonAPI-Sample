using BepInEx;
using System.IO;

namespace CommonAPISample
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(CommonAPIGUID, BepInDependency.DependencyFlags.HardDependency)]
    public class SamplePlugin : BaseUnityPlugin
    {
        private const string CommonAPIGUID = "CommonAPI";
        public static SamplePlugin Instance { get; private set; }
        public string Directory => Path.GetDirectoryName(Info.Location);
        private void Awake()
        {
            Instance = this;
            AppCheats.Initialize();
            AppStageSelect.Initialize();
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
