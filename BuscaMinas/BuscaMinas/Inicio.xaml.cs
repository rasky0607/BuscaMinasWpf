using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//Añadido
using System.Threading;

namespace BuscaMinas
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void DificultadesTarget(object sender, RoutedEventArgs e)
        {
            const int PORCENTAJEDEDIFICULTADBAJA = 10;
            const int PORCENTAJEDEDIFICULTADMEDIA = 25;
            const int PORCENTAJEDEDIFICULTADALTA = 50;
            int numeroDeBotonesPosibles = 0;
            MainWindow miVentanaDeJuego = new MainWindow();
            Inicio ventanaDeInicioDeJuego = new Inicio();
            
            numeroDeBotonesPosibles = miVentanaDeJuego.NUMERODEFILASYCOLUMNAS * miVentanaDeJuego.NUMERODEFILASYCOLUMNAS;
            RadioButton mibotonescogido = (RadioButton)e.OriginalSource;
            
            switch (mibotonescogido.Name.ToString())
            {                 
                case "rbFacil":
                    //Cantidad de botones que pueden haber , de ahi sacamos el 10% el cual hara de tope para  la cantidad de minas que puede haber.                                      
                   
                    miVentanaDeJuego.numeroDeminasMaximas = (numeroDeBotonesPosibles * PORCENTAJEDEDIFICULTADBAJA) / 100;
                    miVentanaDeJuego.CreaGrid();
                    miVentanaDeJuego.LLenaBotones();
                    miVentanaDeJuego.ShowDialog();
                  
                    break;
                case "rbMedio":
                    miVentanaDeJuego.numeroDeminasMaximas = (numeroDeBotonesPosibles * PORCENTAJEDEDIFICULTADMEDIA) / 100;
                    miVentanaDeJuego.CreaGrid();
                    miVentanaDeJuego.LLenaBotones();
                    miVentanaDeJuego.ShowDialog();
                  
                    break;
                case "rbDificil":
                    miVentanaDeJuego.numeroDeminasMaximas = (numeroDeBotonesPosibles * PORCENTAJEDEDIFICULTADALTA) / 100;
                    miVentanaDeJuego.CreaGrid();
                    miVentanaDeJuego.LLenaBotones();
                    miVentanaDeJuego.ShowDialog();
                  
                    break;
                                    
            }
           


        }

        private void rbFacil_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
