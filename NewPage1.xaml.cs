using Microsoft.Maui.Controls;
using MauizAppUtn.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauizAppUtn
{
    public partial class NewPage1 : ContentPage
    {
        private string ApiUrl = "https://cloudcomputingapi2.azurewebsites.net/api/Productos";
        private string ApiUrl2 = "https://cloudcomputingapi2.azurewebsites.net/api/Clasificaciones";
        private List<Clasificacion> clasificaciones; // Lista para almacenar las clasificaciones

        public NewPage1()
        {
            InitializeComponent();
            CargarClasificaciones(); // Cargar las clasificaciones al iniciar la página
        }

        private async void CargarClasificaciones()
        {
            try
            {
                clasificaciones =  APIConsumer.CrudCrud<Clasificacion>.GetAll(ApiUrl2); // Obtener todas las clasificaciones
                foreach (var clasificacion in clasificaciones)
                {
                    pickerClasificacion.Items.Add(clasificacion.Descripcion.ToString()); // Agregar la descripción de cada clasificación al Picker
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción si ocurre algún error al cargar las clasificaciones
                Console.WriteLine($"Error al cargar las clasificaciones: {ex.Message}");
            }
        }

        private async void cmdDeleteProd_Clicked(object sender, EventArgs e)
        {
            try
            {
                 APIConsumer.CrudCrud<Producto>.Delete(ApiUrl, int.Parse(txtIdProducto.Text));
                LimpiarCampos();
                await DisplayAlert("Éxito", "Producto eliminado con éxito", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al eliminar el producto: {ex.Message}", "OK");
            }
        }

        private async void cmdUpdateProd_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (pickerClasificacion.SelectedIndex == -1)
                {
                     DisplayAlert("Error", "Debe seleccionar una clasificación", "OK");
                    return;
                }

                var selectedClasificacion = clasificaciones[pickerClasificacion.SelectedIndex];

                 APIConsumer.CrudCrud<Producto>.Update(ApiUrl, int.Parse(txtIdProducto.Text), new Producto
                {
                    Id = int.Parse(txtIdProducto.Text),
                    Nombre = txtNombreProducto.Text,
                    Existencia = double.Parse(txtExistencia.Text),
                    PrecioUnitario = double.Parse(txtPrecioUnitario.Text),
                    IVA = double.Parse(txtIVA.Text),
                    ClasificacionId = selectedClasificacion.Id // Obtener la ID de la clasificación seleccionada
                });
                await DisplayAlert("Éxito", "Producto actualizado con éxito", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al actualizar el producto: {ex.Message}", "OK");
            }
        }

        private void cmdReadProd_Clicked(object sender, EventArgs e)
        {
            var prod = APIConsumer.CrudCrud<Producto>.Read_ById(ApiUrl, int.Parse(txtIdProducto.Text));
            if (prod != null)
            {
                MostrarProducto(prod);
            }
        }

        private async void cmdCreateProd_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (pickerClasificacion.SelectedIndex == -1)
                {
                    await DisplayAlert("Error", "Debe seleccionar una clasificación", "OK");
                    return;
                }

                var selectedClasificacion = clasificaciones[pickerClasificacion.SelectedIndex];

                var prod =  APIConsumer.CrudCrud<Producto>.Create(ApiUrl, new Producto
                {
                    Id = 0,
                    Nombre = txtNombreProducto.Text,
                    Existencia = double.Parse(txtExistencia.Text),
                    PrecioUnitario = double.Parse(txtPrecioUnitario.Text),
                    IVA = double.Parse(txtIVA.Text),
                    ClasificacionId = selectedClasificacion.Id // Obtener la ID de la clasificación seleccionada
                });

                if (prod != null)
                {
                    MostrarProducto(prod);
                    await DisplayAlert("Éxito", "Producto creado con éxito", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al crear el producto: {ex.Message}", "OK");
            }
        }

        private void MostrarProducto(Producto prod)
        {
            txtIdProducto.Text = prod.Id.ToString();
            txtNombreProducto.Text = prod.Nombre;
            txtExistencia.Text = prod.Existencia.ToString();
            txtPrecioUnitario.Text = prod.PrecioUnitario.ToString();
            txtIVA.Text = prod.IVA.ToString();

            // Seleccionar la clasificación correspondiente en el Picker
            pickerClasificacion.SelectedIndex = clasificaciones.FindIndex(c => c.Id == prod.ClasificacionId);
        }

        private void LimpiarCampos()
        {
            txtIdProducto.Text = "";
            txtNombreProducto.Text = "";
            txtExistencia.Text = "";
            txtPrecioUnitario.Text = "";
            txtIVA.Text = "";
            pickerClasificacion.SelectedItem = null;
        }
    }
}
