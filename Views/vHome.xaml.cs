namespace JGualpa_TC_S5_PA.Views;

public partial class vHome : ContentPage
{
	public vHome()
	{
		InitializeComponent();
	}

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        App.Repo.AddNewPerson(txtNombre.Text);

        statusMessage.Text = App.Repo.statusMessage;
    }

    private void btnListar_Clicked(object sender, EventArgs e)
    {
        statusMessage.Text = "";
        List<Models.Persona> lista = App.Repo.GetAllPerson();
        ListPersonas.ItemsSource = lista;
    }
}