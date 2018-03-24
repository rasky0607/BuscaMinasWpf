﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BuscaMinas
{
    //10% facil 60 botones
    //25% normal 40 botones
    //50% dificil 25 botones
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class VentanaDeJuego : Window
    {     
       public int NUMERODEFILASYCOLUMNAS = 5;
        Button[,] misbotones;//20 filas 20 columnas por cada fila y columna un boton       
        int[] posicionButtonActual = new int[2];//Poscion del botton actualmente pulsado para comprobar si alrededor ahi minas.
        List<Button> botonesSinMinas = new List<Button>();
        List<Button> botonesConMinas = new List<Button>();
        List<Button> botonesPulsados = new List<Button>();
        int contadorBotonesSinMinas = 0;
        int contadorBotonesConMinas = 0;
        public int numeroDeminasMaximas = 0;
        public VentanaDeJuego()
        {
            InitializeComponent();           
           
        }

        public void CreaGrid()
        {
            RowDefinition mifila;
            ColumnDefinition micolum;

            //Limpia las columnas y filas del Grid para el reinicio de la partida
            gridTablero.RowDefinitions.Clear();
            gridTablero.ColumnDefinitions.Clear();
            //------------------------------------//

            for (int i = 0; i < NUMERODEFILASYCOLUMNAS; i++)//Filas
            {
                mifila = new RowDefinition();              
                gridTablero.RowDefinitions.Add(mifila);
                micolum = new ColumnDefinition();
                gridTablero.ColumnDefinitions.Add(micolum);
               
            }

        }

        /// <LLenaBotones>
        /// Pinta un boton por cada posicion de X y de Y por cada cuadrante
        /// que es la posicion de una fila y una columna
        /// </LLenaBotones>
        /// <param name="array"></param>
        public void LLenaBotones()
        {           
            int contadorMinasCreadas = 0;
            misbotones = new Button[NUMERODEFILASYCOLUMNAS, NUMERODEFILASYCOLUMNAS];//20 filas 20 columnas por cada fila y columna un boton   
            int contadorNombreBotones =1;
            int nColum = gridTablero.ColumnDefinitions.Count;
            int nFilas = gridTablero.RowDefinitions.Count;
            Button mibutton;
            SolidColorBrush micolor;
            Random rnd = new Random();
            //Limpia las lista para los contadores de minas
            botonesConMinas.Clear();
            botonesSinMinas.Clear();
            //-----------------------------//
            for (int i = 0; i < nFilas; i++)
            {
                for (int j = 0; j < nColum; j++)
                {                
                    mibutton = new Button();
                    micolor = new SolidColorBrush(Colors.Orange);
                    mibutton.Background = micolor;
                    //añade un valor aleatorio entre 0 y 1 al tag del boton definiendo si hay mina o no(1-> no mina 0->si mina)
                    mibutton.Tag = rnd.Next(0, 2);
                    if (contadorMinasCreadas <= numeroDeminasMaximas)
                    {
                        if (mibutton.Tag is 0)
                        {
                            contadorMinasCreadas++;
                        }
                    }
                    else
                        mibutton.Tag = 1;

                    mibutton.Name = "boton" + contadorNombreBotones;
                    int variable = int.Parse(mibutton.Tag.ToString());
                    if ( variable == 0)
                        botonesConMinas.Add(mibutton);//Botones que si contienen minas
                    else
                        botonesSinMinas.Add(mibutton);//Botones que no contienen minas
                    //Posiciones en el grid
                    Grid.SetRow(mibutton, i);
                    Grid.SetColumn(mibutton, j);
                    //Guarda cada boton en el array de buttons
                    misbotones[i, j] = mibutton;
                    //Agrega el evento al boton
                    mibutton.Click += new RoutedEventHandler(EventoBoton);
                    //Añade el boton al panel
                    gridTablero.Children.Add(mibutton);
                    contadorNombreBotones++;
                }
            }
            contadorBotonesSinMinas = botonesSinMinas.Count;
            contadorBotonesConMinas = botonesConMinas.Count;
            lbNumerodeBotonesSinMinas.Content = contadorBotonesSinMinas;
            lbNumerodeBotonesConMinas.Content = contadorBotonesConMinas;
            
        }

        /// <Comprueba si hay minas>
        /// Comprueba en las celdas colindantes si hay alguna en la que encuentre minas
        /// </Comprueba si hay minas>
        /// <returns>Devuelve la posicion del elemento clicado</returns>
        public bool BuscaMinasCercanas(int posX, int posY)
        {
           
            int contadorMinasCercanas = 0;
            //1 -No mina
            //0 Si mina


            #region Minas alrededores

            #region si faltan botones con la combinacion de ambas( arriba izquierda, abajo izquierda ..etc..)
            if (posX == 0 && posY == 0)//Parte superior izquierda
            {
                if ((int)misbotones[posX + 1, posY].Tag == 0)
                    contadorMinasCercanas++;
                if ((int)misbotones[posX, posY + 1].Tag == 0)
                    contadorMinasCercanas++;
                if ((int)misbotones[posX + 1, posY + 1].Tag == 0)
                    contadorMinasCercanas++;
            }
            if (posX == 0 && posY == gridTablero.RowDefinitions.Count - 1)//Parte  inferior izquierda
            {
                if ((int)misbotones[posX, posY - 1].Tag == 0)
                    contadorMinasCercanas++;
                if ((int)misbotones[posX + 1, posY - 1].Tag == 0)
                    contadorMinasCercanas++;
                if ((int)misbotones[posX + 1, posY].Tag == 0)
                    contadorMinasCercanas++;


            }
            if (posX == gridTablero.ColumnDefinitions.Count - 1 && posY == 0)//Parte  superior derecha
            {
                if ((int)misbotones[posX - 1, posY].Tag == 0)
                    contadorMinasCercanas++;
                if ((int)misbotones[posX - 1, posY + 1].Tag == 0)
                    contadorMinasCercanas++;
                if ((int)misbotones[posX, posY + 1].Tag == 0)
                    contadorMinasCercanas++;

            }
            if (posX == gridTablero.ColumnDefinitions.Count - 1 && posY == gridTablero.RowDefinitions.Count - 1)//Parte  inferior derecha
            {

                if ((int)misbotones[posX - 1, posY - 1].Tag == 0)
                    contadorMinasCercanas++;
                if ((int)misbotones[posX, posY - 1].Tag == 0)
                    contadorMinasCercanas++;
                if ((int)misbotones[posX - 1, posY].Tag == 0)
                    contadorMinasCercanas++;
            }

            #endregion
            else
            {
                #region si faltan botones por la izqueirda solo
                if (posX == 0 && posY != 0 && posY != gridTablero.RowDefinitions.Count-1)
                {
                    if ((int)misbotones[posX, posY - 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX + 1, posY - 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX + 1, posY].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX, posY + 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX + 1, posY + 1].Tag == 0)
                        contadorMinasCercanas++;
                }
                #endregion
                #region si faltan botones por la derecha solo
                if (posX == gridTablero.ColumnDefinitions.Count - 1 && posY != gridTablero.RowDefinitions.Count-1 && posY != 0)//REVISAR****
                {
                    if ((int)misbotones[posX - 1, posY - 1].Tag == 0)//REVISAR***
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX, posY - 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX - 1, posY].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX - 1, posY + 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX, posY + 1].Tag == 0)
                        contadorMinasCercanas++;

                }
                #endregion

                #region si faltan botones por arriba solo
                if (posX != 0 && posY == 0 && posX !=gridTablero.ColumnDefinitions.Count-1)
                {
                    if ((int)misbotones[posX - 1, posY].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX + 1, posY].Tag == 0)
                        contadorMinasCercanas++;

                    if ((int)misbotones[posX - 1, posY + 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX, posY + 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX + 1, posY + 1].Tag == 0)
                        contadorMinasCercanas++;
                }
                #endregion
                #region si faltan botones por la abajo solo
                if (posX != 0 && posX != gridTablero.ColumnDefinitions.Count - 1 && posY == gridTablero.RowDefinitions.Count - 1)
                {

                    if ((int)misbotones[posX - 1, posY - 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX, posY - 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX + 1, posY - 1].Tag == 0)
                        contadorMinasCercanas++;

                    if ((int)misbotones[posX - 1, posY].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX + 1, posY].Tag == 0)
                        contadorMinasCercanas++;
                }
                #endregion


                #region Si hay botones 8 alrededor
                else if (posX != 0 && posY != 0 && posY != gridTablero.RowDefinitions.Count - 1 && posX != gridTablero.ColumnDefinitions.Count-1)
                {

                    if ((int)misbotones[posX - 1, posY - 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX, posY - 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX + 1, posY - 1].Tag == 0)
                        contadorMinasCercanas++;

                    if ((int)misbotones[posX - 1, posY].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX + 1, posY].Tag == 0)
                        contadorMinasCercanas++;

                    if ((int)misbotones[posX - 1, posY + 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX, posY + 1].Tag == 0)
                        contadorMinasCercanas++;
                    if ((int)misbotones[posX + 1, posY + 1].Tag == 0)
                        contadorMinasCercanas++;
                }
                
                
                #endregion


            }

            misbotones[posX, posY].Content = contadorMinasCercanas;
            #endregion


            #region Mina en el boton

            SolidColorBrush micolor = new SolidColorBrush(Colors.Red);
            SolidColorBrush micolor2 = new SolidColorBrush(Colors.Green);       
            if ((int)misbotones[posX, posY].Tag == 0)
            {
                MessageBox.Show("hay mina!");            
                misbotones[posX, posY].Background = micolor;
                lbNumerodeBotonesConMinas.Content = --contadorBotonesConMinas;
                return true;
            }
            else
            {
                MessageBox.Show("NO hay mina");              
                misbotones[posX, posY].Background = micolor2;             
                lbNumerodeBotonesSinMinas.Content = --contadorBotonesSinMinas;//Revisar cuando llega a 0 minas " el cual se pone en negativo" 
                return false;
            }
            #endregion

         

        }


        private void EventoBoton(object sender, RoutedEventArgs e)
        {


            for (int i = 0; i < misbotones.GetLength(0); i++)
            {
                for (int j = 0; j < misbotones.GetLength(1); j++)
                {
                    //Si el boton pulsado es el mismo que uno de los del array de botones guardo la posicion en la que se encuentra en dicho array
                    if (misbotones[i, j].Equals(sender))
                    {
                        bool sisepulso = true;
                        for (int k = 0; k < botonesPulsados.Count; k++)//Recorre la lista de botones ya pulsados
                        {
                            if (botonesPulsados[k] == e.OriginalSource)//si el boton pulsado ,es uno que ya se pulso antes
                                sisepulso = false;

                        }
                        if (sisepulso)
                        {
                            botonesPulsados.Add(misbotones[i, j]);//añado el boton pulsado a una lista de descartados , para que en caso de que se pulse de nuevo se ignore , sin deshabilitarlo                   
                            BuscaMinasCercanas(i, j);
                        }


                    }
                }
            }
            if (contadorBotonesSinMinas == 0)
            {
                MessageBoxResult respuesta = MessageBox.Show("¿Quieres comenzar una nueva partida?", "Felicidades has ganado!", MessageBoxButton.YesNo);

                switch (respuesta)
                {
                    case MessageBoxResult.Yes:
                        
                        CreaGrid();                      
                        LLenaBotones();
                        break;
                    case MessageBoxResult.No:
                        Close();
                        break;
                }

            }

            if (contadorBotonesConMinas == 0)
            {
                MessageBoxResult respuesta = MessageBox.Show("¿Quieres intentarlo de nuevo?", "Oh, losiento has perdido!", MessageBoxButton.YesNo);
                switch (respuesta)
                {
                    case MessageBoxResult.Yes:                      
                        CreaGrid();                      
                        LLenaBotones();
                        break;
                    case MessageBoxResult.No:
                        Close();
                        break;
                }


            }
        }

        private void SalirPrograma(object sender, RoutedEventArgs e)
        {
            Close();         
        }

        private void ButtonDificultades(object sender, RoutedEventArgs e)
        {
            const int PORCENTAJEDEDIFICULTADBAJA = 10;
            const int PORCENTAJEDEDIFICULTADMEDIA = 25;
            const int PORCENTAJEDEDIFICULTADALTA = 50;            
            int numeroDeBotonesPosibles = 0;
            numeroDeBotonesPosibles = NUMERODEFILASYCOLUMNAS * NUMERODEFILASYCOLUMNAS;
            MenuItem mibotonescogido =(MenuItem) e.OriginalSource;
            switch (mibotonescogido.Name.ToString())
            { 
                case "rbtFacil":
                    //Cantidad de botones que pueden haber , de ahi sacamos el 10% el cual hara de tope para  la cantidad de minas que puede haber.                
                    numeroDeminasMaximas = (numeroDeBotonesPosibles * PORCENTAJEDEDIFICULTADBAJA) / 100;
                    CreaGrid();
                    LLenaBotones();
                    break;
                case "rbNormal":
                    numeroDeminasMaximas = (numeroDeBotonesPosibles * PORCENTAJEDEDIFICULTADMEDIA) / 100;
                    CreaGrid();
                    LLenaBotones();
                    break;
                case "rbDificil":
                    numeroDeminasMaximas = (numeroDeBotonesPosibles * PORCENTAJEDEDIFICULTADALTA) / 100;
                    CreaGrid();
                    LLenaBotones();
                    break;                
            }

          
        }

       
    }
}
