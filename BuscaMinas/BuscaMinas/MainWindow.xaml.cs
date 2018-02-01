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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BuscaMinas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int NUMERODEFILASYCOLUMNAS = 5;
        Button[,] misbotones = new Button[NUMERODEFILASYCOLUMNAS, NUMERODEFILASYCOLUMNAS];//20 filas 20 columnas por cada fila y columna un boton
        int[] posicionButtonActual = new int[2];//Poscion del botton actualmente pulsado para comprobar si alrededor ahi minas.
        public MainWindow()
        {
            InitializeComponent();
            CreaGrid();          
            LLenaBotones();         
           
           
        }

        public void CreaGrid()
        {
            RowDefinition mifila;
            ColumnDefinition micolum;

            for (int i = 0; i < NUMERODEFILASYCOLUMNAS; i++)//Filas
            {
                mifila = new RowDefinition();
                gridTablero.RowDefinitions.Add(mifila);
                micolum = new ColumnDefinition();
                gridTablero.ColumnDefinitions.Add(micolum);
                //como guardar  las posicines de el grid creado
            }

        }

        /// <LLenaBotones>
        /// Pinta un boton por cada posicion de X y de Y por cada cuadrante
        /// que es la posicion de una fila y una columna
        /// </LLenaBotones>
        /// <param name="array"></param>
        private void LLenaBotones()
        {
           
           int nColum = gridTablero.ColumnDefinitions.Count;
            int nFilas = gridTablero.RowDefinitions.Count;
            Button mibutton;
            SolidColorBrush micolor;
            Random rnd = new Random();
            for (int i = 0; i < nFilas; i++)
            {
                for (int j = 0; j < nColum; j++)
                {                
                    mibutton = new Button();
                    micolor = new SolidColorBrush(Colors.Orange);
                    mibutton.Background = micolor;
                    //añade un valor aleatorio entre 0 y 1 al tag del boton definiendo si hay mina o no(1-> no mina 0->si mina)
                    mibutton.Tag = rnd.Next(0, 2);
                    //Posiciones en el grid
                    Grid.SetRow(mibutton, i);
                    Grid.SetColumn(mibutton, j);
                    //Guarda cada boton en el array de buttons
                    misbotones[i, j] = mibutton;
                    //Agrega el evento al boton
                    mibutton.Click += new RoutedEventHandler(EventoBoton);
                    //Añade el boton al panel
                    gridTablero.Children.Add(mibutton);
                    
                }
            }
            
        }

        /// <Comprueba si hay minas>
        /// Comprueba en las celdas colindantes si hay alguna en la que encuentre minas
        /// </Comprueba si hay minas>
        /// <returns>Devuelve la posicion del elemento clicado</returns>
        public bool BuscaMinasCercanas(int posX, int posY)// <----RVISAR ESQUINAS!
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
                    if ((int)misbotones[posX, posY + 1].Tag == 0)//REVISAR ***
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
                return true;
            }
            else
            {
                MessageBox.Show("NO hay mina");
                misbotones[posX, posY].Background = micolor2;
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
                        BuscaMinasCercanas(i, j);
                       //Si es asi colocamos el boton  en rojo, lo desactivamos y añadimos una imagen de una mina
                      
                    }
                }
            }        
        }
      

        
    }
}
