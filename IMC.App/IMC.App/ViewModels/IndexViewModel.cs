using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IMC.App.ViewModels
{
    public class IndexViewModel : BaseViewModel
    {
        #region Fields
        private double _altura;
        private double _peso;
        private double _imc;

        #endregion

        #region Properties
        public double Altura
        {
            get { return _altura; }
            set { this.SetValue(ref this._altura, value); }
        }

        public double Peso
        {
            get { return _peso; }
            set { this.SetValue(ref this._peso, value); }
        }


        public double IMC
        {
            get { return _imc; }
            set { this.SetValue(ref this._imc, value); }
        }
        #endregion

        #region  Methods

        private async void Calcular()
        {

            var peso = this.Peso;
            var altura = this.Altura / 100;


            var imc = Math.Round(peso / Math.Pow(altura, 2), 2);
            this.IMC = imc;


            var resultado = string.Empty;
            if (imc < 18.5)
                resultado = "Tienes bajo peso";

            else if (imc >= 18.5 && imc <= 24.5)
                resultado = "Tu peso es normal";


            else if (imc >= 25 && imc <= 29.9)
                resultado = "Tienes sobre peso";

            else
                resultado = "Tienes obesidad";


            await Application.Current.MainPage.DisplayAlert("Resultado", resultado, "Cerrar");

        }

        #endregion

        #region Commands
        public Command CalcularCommand { get; set; }
        #endregion

        public IndexViewModel()
        {
            this.CalcularCommand = new Command(this.Calcular);
        }
    }
}
