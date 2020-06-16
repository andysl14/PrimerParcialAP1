using PrimerParcial.BLL;
using PrimerParcial.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrimerParcial.UI.Registros
{
    /// <summary>
    /// Interaction logic for rArticulos.xaml
    /// </summary>
    public partial class rArticulos : Window
    {
        private Articulos Articulo = new Articulos();
        public rArticulos()
        {
            InitializeComponent();

            //Asignando instancia para poder hacer BINDINGS en el formulario
            this.DataContext = Articulo;
        }

        private void Limpiar()
        {
            this.Articulo = new Articulos();
            this.DataContext = Articulo;
        }

        private bool Validar()
        {
            bool esValido = true;

            if (DescripcionTextBox.Text.Length == 0 || ArticuloIdTextBox.Text.Length == 0 || ExistenciaTextBox.Text.Length == 0 || CostoTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Hay Campos vacìos, Revise los campos y vuelva a intentarlo", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return esValido;

        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var articulo = ArticulosBLL.Buscar(Utilidades.Toint(ArticuloIdTextBox.Text));

            if (articulo != null)
                this.Articulo = articulo;
            else
                this.Articulo = new Articulos();

            this.DataContext = this.Articulo;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var paso = ArticulosBLL.Guardar(Articulo);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado con exitosamente!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transacciòn Fallida", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ArticulosBLL.Eliminar(Utilidades.Toint(ArticuloIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("No pudo ser posible eliminar el registro", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CalcularValorInventario_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal Existencia, Costo, Total;

            decimal.TryParse(ExistenciaTextBox.Text, out Existencia);
            decimal.TryParse(CostoTextBox.Text, out Costo);

            Total = Existencia * Costo;

            ValorInventarioTextBox.Text = Total.ToString();

           

        }

        private void NumberValidation_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^[0 - 9](?:\.[0 - 9])?$");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
