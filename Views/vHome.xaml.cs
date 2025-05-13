using JGualpa_TC_S5_PA.Models;
using System.Collections.Generic;
using System.Linq;

namespace JGualpa_TC_S5_PA.Views;

public partial class vHome : ContentPage
{
    private int _personIdToUpdate = 0;

    public vHome()
    {
        InitializeComponent();
    }

    private void RefreshList()
    {

        List<Models.Persona> lista = App.Repo.GetAllPerson();
        ListPersonas.ItemsSource = lista;
        statusMessage.Text = App.Repo.statusMessage;
    }

    private void SetInsertMode()
    {
        _personIdToUpdate = 0; // Reset ID
        txtNombre.Text = string.Empty; 
        btnInsertar.IsVisible = true; 
        btnActualizar.IsVisible = false; 
        btnCancelar.IsVisible = false; 
        lblModo.IsVisible = false; 
        lblModo.Text = string.Empty;
        statusMessage.Text = string.Empty; 
    }

    private void SetUpdateMode(Persona personToEdit)
    {
        _personIdToUpdate = personToEdit.Id; 
        txtNombre.Text = personToEdit.Name; 
        btnInsertar.IsVisible = false; 
        btnActualizar.IsVisible = true; 
        btnCancelar.IsVisible = true; 
        lblModo.IsVisible = true; 
        lblModo.Text = $"Editando ID: {personToEdit.Id}"; 
        statusMessage.Text = string.Empty; 
    }



    private void btnInsertar_Clicked(object sender, EventArgs e)
    {

        if (_personIdToUpdate > 0)
        {
            statusMessage.Text = "Error: Still in update mode.";
            return;
        }

        statusMessage.Text = ""; 
        App.Repo.AddNewPerson(txtNombre.Text);

        statusMessage.Text = App.Repo.statusMessage; 
        txtNombre.Text = string.Empty; 
        RefreshList(); 
    }

    private void btnListar_Clicked(object sender, EventArgs e)
    {
        RefreshList();
    }

    // *** Nuevo Manejador para el botón Eliminar de la lista ***
    private async void OnDeleteClicked(object sender, EventArgs e) 
    {
  
        var button = (Button)sender;
        var personToDelete = (Persona)button.BindingContext;

        if (personToDelete != null)
        {
     
            bool confirmed = await DisplayAlert("Confirmar Eliminación", $"¿Estás seguro de que quieres eliminar a '{personToDelete.Name}'?", "Sí", "No");

            if (confirmed)
            {
                statusMessage.Text = ""; 
                App.Repo.DeletePerson(personToDelete.Id);

                statusMessage.Text = App.Repo.statusMessage; 
                RefreshList(); 

              
                if (_personIdToUpdate == personToDelete.Id)
                {
                    SetInsertMode();
                }
            }
        }
    }


    private void OnEditClicked(object sender, EventArgs e)
    {

        var button = (Button)sender;
        var personToEdit = (Persona)button.BindingContext;

        if (personToEdit != null)
        {
            SetUpdateMode(personToEdit); 
        }
    }

    // *** Nuevo Manejador para el botón Actualizar (visible only in update mode) ***
    private void btnActualizar_Clicked(object sender, EventArgs e)
    {

        if (_personIdToUpdate <= 0)
        {
            statusMessage.Text = "Error: No hay persona seleccionada para actualzar.";
            return;
        }

        string newName = txtNombre.Text;

        if (string.IsNullOrWhiteSpace(newName))
        {
            statusMessage.Text = "El nombre no puede estar vacío para actualizar.";
            return;
        }


        Persona personToUpdate = new Persona { Id = _personIdToUpdate, Name = newName };

        statusMessage.Text = ""; 
        App.Repo.UpdatePerson(personToUpdate); 

        statusMessage.Text = App.Repo.statusMessage; 
        RefreshList(); 

        SetInsertMode();
    }

    // *** Nuevo Manejador para el botón Cancelar (visible only in update mode) ***
    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        SetInsertMode();
    }
}