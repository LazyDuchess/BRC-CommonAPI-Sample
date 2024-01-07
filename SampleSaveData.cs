using CommonAPI;
using Reptile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CommonAPISample
{
    public class SampleSaveData : CustomSaveData
    {
        public static SampleSaveData Instance { get; private set; }
        private readonly Dictionary<Stage, RespawnPoint> respawnPoints = [];
        public SampleSaveData() : base(PluginInfo.PLUGIN_NAME, "{0}.data")
        {
            Instance = this;
        }

        public void SetRespawnPoint(Stage stage, Vector3 position, Quaternion rotation, bool gear)
        {
            var respawnPoint = new RespawnPoint(position, rotation, gear);
            respawnPoints[stage] = respawnPoint;
            // Custom save data gets saved alongside the base savedata, so it makes sense to call this right after changing custom data.
            Core.Instance.SaveManager.SaveCurrentSaveSlot();
        }

        public RespawnPoint GetRespawnPoint(Stage stage)
        {
            if (respawnPoints.TryGetValue(stage, out var respawnPoint)) return respawnPoint;
            return null;
        }

        // Starting a new save - start from zero.
        public override void Initialize()
        {
            respawnPoints.Clear();
        }

        public override void Read(BinaryReader reader)
        {
            var version = reader.ReadByte();
            var respawns = reader.ReadInt32();
            for(var i = 0; i < respawns; i++)
            {
                var stage = (Stage)reader.ReadInt32();

                var positionX = reader.ReadSingle();
                var positionY = reader.ReadSingle();
                var positionZ = reader.ReadSingle();

                var rotationX = reader.ReadSingle();
                var rotationY = reader.ReadSingle();
                var rotationZ = reader.ReadSingle();
                var rotationW = reader.ReadSingle();

                var gear = reader.ReadBoolean();

                var position = new Vector3(positionX, positionY, positionZ);
                var rotation = new Quaternion(rotationX, rotationY, rotationZ, rotationW);

                var respawnPoint = new RespawnPoint(position, rotation, gear);
                respawnPoints[stage] = respawnPoint;
            }
        }

        public override void Write(BinaryWriter writer)
        {
            // Version
            writer.Write((byte)0);
            writer.Write(respawnPoints.Count);
            foreach(var respawnPair in respawnPoints)
            {
                writer.Write((int)respawnPair.Key);
                var respawnPoint = respawnPair.Value;

                writer.Write(respawnPoint.Position.x);
                writer.Write(respawnPoint.Position.y);
                writer.Write(respawnPoint.Position.z);

                writer.Write(respawnPoint.Rotation.x);
                writer.Write(respawnPoint.Rotation.y);
                writer.Write(respawnPoint.Rotation.z);
                writer.Write(respawnPoint.Rotation.w);

                writer.Write(respawnPoint.Gear);
            }
        }
    }
}
