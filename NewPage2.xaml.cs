using MauizAppUtn.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MauizAppUtn
{
    public partial class NewPage2 : ContentPage
    {
        private string ApiUrl2 = "https://cloudcomputingapi2.azurewebsites.net/api/Clasificaciones";

        public NewPage2()
        {
            InitializeComponent();
        }

        private async void cmdCreate_Clicked(object sender, EventArgs e)
        {
            try
            {
                var resultado = APIConsumer.CrudCrud<Clasificacion>.Create(ApiUrl2, new Clasificacion
                {
                    Id = 0,
                    Descripcion = txtClasificacion.Text
                });

                if (resultado != null)
                {
                    txtId.Text = resultado.Id.ToString();
                    await DisplayAlert("Éxito", "Clasificación creada con éxito", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async void cmdRead_Clicked(object sender, EventArgs e)
        {
            try
            {
                var resultado = APIConsumer.CrudCrud<Clasificacion>.Read_ById(ApiUrl2, int.Parse(txtId.Text));
                if (resultado != null)
                {
                    txtId.Text = resultado.Id.ToString();
                    txtClasificacion.Text = resultado.Descripcion;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async void cmdUpdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                APIConsumer.CrudCrud<Clasificacion>.Update(ApiUrl2, int.Parse(txtId.Text), new Clasificacion
                {
                    Id = int.Parse(txtId.Text),
                    Descripcion = txtClasificacion.Text
                });
                await DisplayAlert("Éxito", "Clasificación actualizada con éxito", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private async void cmdDelete_Clicked(object sender, EventArgs e)
        {
            try
            {
                APIConsumer.CrudCrud<Clasificacion>.Delete(ApiUrl2, int.Parse(txtId.Text));
                txtClasificacion.Text = "";
                txtId.Text = "";
                await DisplayAlert("Éxito", "Clasificación eliminada con éxito", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }
    }
}
