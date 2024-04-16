using System;
using System.Text.RegularExpressions;
class BattleshipGame
{
    static char[,] tablero;
    static int contadorBarco = 4;
    static int intentos = 8;

    static BattleshipGame()
    {
        // Inicia tablero predeterminado 3x3 para evitar valor null
        tablero = new char[3, 3];
    }

    static void Main()
    {
        string inicio;
        Console.WriteLine("|...........................................................................................|");
        Console.WriteLine("|                  * * * BIENVENIDO AL JUEGO BATTLESHIP * * *                               |");
        Console.WriteLine("|   Estas son las reglas:                                                                   |");
        Console.WriteLine("|   Regla 1. tienes 8 intentos para atacar 4 barcos.                                        |");
        Console.WriteLine("|   Regla 2. si aciertas en derrotar un barco, el intento no es tomado en cuenta.           |");
        Console.WriteLine("|   Regla 3. si fallas en derrotar un barco, el intento si es tomado en cuenta.             |");
        Console.WriteLine("|   Regla 4. Si atacas en donde ya habías eliminado el barco el intento no cuenta.          |");
        Console.WriteLine("|   Regla 5. Si atacas en donde ya habías fallado. el intento si cuenta.                    |");
        Console.WriteLine("|                         * * * ¡BUENA SUERTE!* * *                                         |");
        Console.WriteLine("|...........................................................................................|");
        do
        {
            ComenzarJuego();
            VerTablero(false); // Muestra el tablero ocultando donde la posicion de los barcos
            ModoJuego();
            do
            {
                Console.WriteLine("\n¿Quieres seguir jugando? Escribe '1' para jugar de nuevo o '2' para salir");
                inicio = Console.ReadLine()!;
            }
            while (inicio != "1" && inicio != "2");
        }
        while (inicio == "1");
    }
    static void ComenzarJuego()
    {

        int filas, columnas;
        do
        {
            Console.WriteLine("\n* * * EL TAMAÑO DEL TABLERO DEBER SER ENTRE 3x3 AL 10x10 * * *\n");
            Console.WriteLine("Ingresa un numero de fila para el tablero:");
            filas = Convert.ToInt32(Console.ReadLine()!);
            Console.WriteLine("Ingrese un número de columna para el tablero:");
            columnas = Convert.ToInt32(Console.ReadLine()!);

            if (filas < 3 || columnas < 3 || filas > 10 || columnas > 10)
            {
                Console.WriteLine("\nNo ingresaste el tamaño admitido, intenta de nuevo\n");
            }
        } while (filas < 3 || columnas < 3 || filas > 10 || columnas > 10);

        tablero = new char[filas, columnas];

        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j < columnas; j++)
            {
                tablero[i, j] = '-'; // '-' esto es agua
            }
        }
        AreaBarco();
    }

    static void AreaBarco()
    {
        Random aleatorio = new Random();
        for (int i = 0; i < contadorBarco; i++)
        {
            int filas, columnas;
            do
            {
                filas = aleatorio.Next(tablero.GetLength(0));
                columnas = aleatorio.Next(tablero.GetLength(1));
            } while (tablero[filas, columnas] == 'B'); // 'B' representa al barco de forma interna

            tablero[filas, columnas] = 'B'; // agrega un barco en el tablero interno
        }
    }

    static void VerTablero(bool mostrarBarcos)
    {

        Console.Write("       ");
        for (int i = 0; i < tablero.GetLength(1); i++)
        {
            Console.Write(i + "  ");
        }

        Console.WriteLine();

        for (int i = 0; i < tablero.GetLength(0); i++)
        {
            Console.Write("     " + i + " ");
            for (int j = 0; j < tablero.GetLength(1); j++)
            {
                char verCaracter = tablero[i, j];
                if (!mostrarBarcos && verCaracter == 'B')
                {  // Oculta los barcos si revealShips es falso

                    verCaracter = '-';
                }
                Console.Write(verCaracter + "  ");
            }
            Console.WriteLine();
        }
    }
    static void ModoJuego()
    {
        int golpe = 0;
        int intentoActual = 0;

        while (intentoActual < intentos)
        {
            Console.WriteLine($"\n         | Intento {intentoActual + 1} de {intentos} |\n");
            Console.WriteLine("Ingresa un numero de fila:");
            int filas = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingresa un numero de columna:");
            int columnas = Convert.ToInt32(Console.ReadLine());

            if (tablero[filas, columnas] == 'X')
            {

                Console.WriteLine("\nYa has acertado en esta posición antes. Intento no tomado en cuenta.");
                continue; // No va a incrementar el intentoActual
            }

            if (tablero[filas, columnas] == '*')
            {

                Console.WriteLine("Ya has fallado en esta posición antes. Intento tomado en cuenta.\n");
                intentoActual++; // Incrementa el intentoActual
            }
            else if (tablero[filas, columnas] == 'B')
            {
                Console.WriteLine("\n            ¡Acertaste!\n");
                tablero[filas, columnas] = 'X'; // 'X' representa un barco hundido
                golpe++;
                if (golpe == contadorBarco)
                {
                    Console.WriteLine("¡Felicidades, has eliminado todos los barcos!" +
                        "");
                    break;
                }
            }
            else
            {
                Console.WriteLine("\n            ¡Fallaste!\n");
                tablero[filas, columnas] = '*'; // '*' representa un intento fallido
                intentoActual++; // Incrementa el intentoActual
            }
            VerTablero(false); // Actualiza y muestra el tablero después de cada intento sin revelar los barcos
        }

        if (golpe < contadorBarco)
        {
            Console.WriteLine("\nJuego terminado. No has eliminado todos los barcos.");
            Console.WriteLine("..................................................");
            Console.WriteLine("\n * * * Barco/s faltante/s a elimnar * * *\n");
            VerTablero(true);
            Console.WriteLine("..................................................");
        }
    }
}