using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAPI;
using CommonAPI.Phone;
using Reptile;
using UnityEngine;

namespace CommonAPISample
{
    // Cheats app - Shows up in homescreen. Lets us do misc things.
    public class AppCheats : CustomApp
    {
        private static Sprite IconSprite = null;
        private PhoneButton goToRespawnButton = null;

        // Load the icon for this app and register it with the PhoneAPI, so that it shows up on the homescreen.
        public static void Initialize()
        {
            IconSprite = TextureUtility.LoadSprite(Path.Combine(SamplePlugin.Instance.Directory, "Homescreen Icon.png"));
            PhoneAPI.RegisterApp<AppCheats>("cheats", IconSprite);
        }

        // Add or remove the Go to respawn button from the menu, depending on if the current stage has a respawn set or not.
        public void UpdateGoToRespawnButton()
        {
            var stage = Core.Instance.BaseModule.CurrentStage;
            var respawnPoint = SampleSaveData.Instance.GetRespawnPoint(stage);
            if (respawnPoint == null)
            {
                if (ScrollView.HasButton(goToRespawnButton))
                    ScrollView.RemoveButton(goToRespawnButton);
            }
            else
            {
                if (!ScrollView.HasButton(goToRespawnButton))
                    ScrollView.AddButton(goToRespawnButton);
            }
        }

        public override void OnAppEnable()
        {
            base.OnAppEnable();
            UpdateGoToRespawnButton();
        }

        public override void OnAppInit()
        {
            base.OnAppInit();
            CreateTitleBar("Cheats", IconSprite);
            ScrollView = PhoneScrollView.Create(this);

            var button = PhoneUIUtility.CreateSimpleButton("Give Max Boost");
            button.OnConfirm += () => {
                var player = WorldHandler.instance.GetCurrentPlayer();
                player.boostCharge = player.maxBoostCharge;
            };
            ScrollView.AddButton(button);

            button = PhoneUIUtility.CreateSimpleButton("Go to Stage...");
            button.OnConfirm += () => {
                // Launch our stage select app.
                MyPhone.OpenApp(typeof(AppStageSelect));
            };
            ScrollView.AddButton(button);

            button = PhoneUIUtility.CreateSimpleButton("Set Respawn");
            button.OnConfirm += () =>
            {
                var stage = Core.Instance.BaseModule.CurrentStage;
                var position = MyPhone.player.transform.position;
                var rotation = MyPhone.player.transform.rotation;
                var gear = MyPhone.player.usingEquippedMovestyle;
                SampleSaveData.Instance.SetRespawnPoint(stage, position, rotation, gear);
                UpdateGoToRespawnButton();
            };
            ScrollView.AddButton(button);

            // We conditionally add this button depending on whether the stage has a respawn saved or not.
            goToRespawnButton = PhoneUIUtility.CreateSimpleButton("Go to Respawn");
            goToRespawnButton.OnConfirm += () =>
            {
                var stage = Core.Instance.BaseModule.CurrentStage;
                var respawnPoint = SampleSaveData.Instance.GetRespawnPoint(stage);
                if (respawnPoint == null) return;
                respawnPoint.ApplyToPlayer(MyPhone.player);
            };
        }
    }
}
