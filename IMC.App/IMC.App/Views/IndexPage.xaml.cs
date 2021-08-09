using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IMC.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPage : ContentPage
    {
        public IndexPage()
        {
            InitializeComponent();
        }

        private void Calcular_Clicked(object sender, EventArgs e)
        {

            var peso = double.Parse(Peso.Text);
            var altura = double.Parse(Altura.Text) / 100;


            var imc = Math.Round(peso / Math.Pow(altura, 2), 2);
            IMC.Text = imc.ToString();


            var resultado = string.Empty;
            if (imc < 18.5)
                resultado = "Tienes bajo peso";

            else if (imc >= 18.5 && imc <= 24.5)
                resultado = "Tu peso es normal";


            else if (imc >= 25 && imc <= 29.9)
                resultado = "Tienes sobre peso";

            else
                resultado = "Tienes obesidad";


            DisplayAlert("Resultado", resultado, "Cerrar");


        }
    }
}