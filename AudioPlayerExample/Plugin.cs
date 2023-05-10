using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using PluginAPI.Events;
using SCPSLAudioApi;
using SCPSLAudioApi.AudioCore;
using System.IO;
using VoiceChat;

namespace Xname.AudioPlayerExample;

internal sealed class Plugin
{
    [PluginConfig]
    public static Config Config;

    [PluginPriority(LoadPriority.Medium)]
    [PluginEntryPoint("AudioPlayerExample", "1.0.0", "Bottom text", "Xname")]
    public void Load()
    {
        _pluginHandler = PluginHandler.Get(this);

        Startup.SetupDependencies();

        EventManager.RegisterEvents(this);
    }

    [PluginUnload]
    public void Unload()
    {
        EventManager.UnregisterEvents(this);
    }

    private static PluginHandler _pluginHandler;

    [PluginEvent(ServerEventType.RoundStart)]
    private void OnRoundStart()
    {
        Dummy._id = 0;
        AudioPlayerBase audioPlayer = Dummy.Spawn();
        audioPlayer.BroadcastChannel = VoiceChatChannel.Intercom; // You can change this to any channel you want so dummy can be heard by different players.

        if (!string.IsNullOrEmpty(Config.AudioToPlay))
        {
            audioPlayer.Enqueue(Path.Combine(_pluginHandler.PluginDirectoryPath, Config.AudioToPlay), -1); // -1 means that the audio file will be added to the end of the music queue.
            audioPlayer.Play(-1); // -1 means that the audio file will be added to the end of the music queue.
        }
    }
}