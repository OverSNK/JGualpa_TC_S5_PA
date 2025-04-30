namespace JGualpa_TC_S5_PA
{
    public partial class App : Application
    {
        public static Repositories.PersonRepository Repo { get; set; }
        public App(Repositories.PersonRepository personrepository)
        {   
            InitializeComponent();
            Repo = personrepository;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Views.vHome());
        }
    }
}