using Reptile;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonAPISample
{
    public class RespawnPoint
    {
        public Vector3 Position = Vector3.zero;
        public Quaternion Rotation = Quaternion.identity;
        public bool Gear = false;

        public RespawnPoint(Vector3 position, Quaternion rotation, bool gear)
        {
            Position = position;
            Rotation = rotation;
            Gear = gear;
        }

        public void ApplyToPlayer(Player player)
        {
            WorldHandler.instance.PlacePlayerAt(player, Position, Rotation);
            player.SwitchToEquippedMovestyle(Gear);
        }
    }
}
