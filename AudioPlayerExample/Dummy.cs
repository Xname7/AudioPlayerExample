using Mirror;
using PlayerRoles;
using SCPSLAudioApi.AudioCore;
using System;

namespace Xname.AudioPlayerExample;

internal static class Dummy
{
    public static ushort _id = 0;

    public static AudioPlayerBase Spawn()
    {
        var playerObject = UnityEngine.Object.Instantiate(NetworkManager.singleton.playerPrefab);
        var fakeConnection = new FakeConnection(_id++);
        var dummy = playerObject.GetComponent<ReferenceHub>();

        try
        {
            dummy.nicknameSync.SetNick($"Dummy-{_id}");
        }
        catch
        {
        }

        NetworkServer.AddPlayerForConnection(fakeConnection, playerObject);
        dummy.roleManager.ServerSetRole(RoleTypeId.Tutorial, RoleChangeReason.RemoteAdmin, RoleSpawnFlags.UseSpawnpoint);
        return AudioPlayerBase.Get(dummy);
    }

    public class FakeConnection : NetworkConnectionToClient
    {
        public FakeConnection(int connectionId) : base(connectionId)
        {
        }

        public override string address
        {
            get
            {
                return "localhost";
            }
        }

        public override void Send(ArraySegment<byte> segment, int channelId = 0)
        {
        }

        public override void Disconnect()
        {
        }
    }
}
