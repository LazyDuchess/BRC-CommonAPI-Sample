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
    public class AppCheats : CustomApp
    {
        private static Sprite IconSprite = null;

        public static void Initialize()
        {
            IconSprite = TextureUtility.LoadSprite(Path.Combine(SamplePlugin.Instance.Directory, "Homescreen Icon.png"));
            PhoneAPI.RegisterApp<AppCheats>("cheats", IconSprite);
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
                MyPhone.OpenApp(typeof(AppStageSelect));
            };
            ScrollView.AddButton(button);
        }
    }
}
