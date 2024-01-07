using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAPI;
using CommonAPI.Phone;
using Reptile;

namespace CommonAPISample
{
    public class AppStageSelect : CustomApp
    {
        /// <summary>
        /// Don't show in home screen.
        /// </summary>
        public override bool Available => false;

        // This app just lets us teleport to any stage. Even though it's not visible in the homescreen, we still have to register it with PhoneAPI to be able to use it.
        public static void Initialize()
        {
            PhoneAPI.RegisterApp<AppStageSelect>("stage select");
        }

        public override void OnAppInit()
        {
            base.OnAppInit();
            CreateIconlessTitleBar("Select Stage");
            ScrollView = PhoneScrollView.Create(this);

            var button = CreateStageButton(Stage.tower);
            ScrollView.AddButton(button);

            button = CreateStageButton(Stage.Prelude);
            ScrollView.AddButton(button);

            button = CreateStageButton(Stage.downhill);
            ScrollView.AddButton(button);

            button = CreateStageButton(Stage.pyramid);
            ScrollView.AddButton(button);

            button = CreateStageButton(Stage.hideout);
            ScrollView.AddButton(button);

            button = CreateStageButton(Stage.Mall);
            ScrollView.AddButton(button);

            button = CreateStageButton(Stage.osaka);
            ScrollView.AddButton(button);

            button = CreateStageButton(Stage.square);
            ScrollView.AddButton(button);
        }

        private SimplePhoneButton CreateStageButton(Stage stage)
        {
            var button = PhoneUIUtility.CreateSimpleButton(stage.ToString());
            button.OnConfirm += () =>
            {
                Core.Instance.BaseModule.StageManager.ExitCurrentStage(stage);
            };
            return button;
        }
    }
}
