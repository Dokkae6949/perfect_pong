using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;
using test.app.data;
using test.ball.logic;

namespace test.app.logic;

public partial class AppLogic
{
    public partial record State
    {
        [Meta, Id("app_logic_state_splash_screen")]
        public partial record SplashScreen : State, IGet<Input.SplashScreenFadedIn>
        {
            public SplashScreen()
            {
                this.OnEnter(() => Output(new Output.ShowSplashScreen()));
                
                OnAttach(() => Get<IAppRepo>().SplashScreenSkipped += OnSplashScreenSkipped);
                OnDetach(() => Get<IAppRepo>().SplashScreenSkipped -= OnSplashScreenSkipped);
            }

            public Transition On(in Input.SplashScreenFadedIn input) => To<MainMenu>();

            private void OnSplashScreenSkipped()
            {
                Output(new Output.HideSplashScreen());
                Input(new Input.SplashScreenFadedIn());
            }
        }
    }
}