
Random rnd = new Random();
int opcionPrincipal = 0;

Ejercicio[] GenerarEjercicios(int cantidad)
{ 
    Ejercicio[] lista = new Ejercicio[cantidad];
    for (int i = 0; i < cantidad; i++)
    {
        lista[i].Numero1 = rnd.Next(1, 50);
        lista[i].Numero2 = rnd.Next(1, 50);
        lista[i].Resultado = lista[i].Numero1 + lista[i].Numero2;
    }
    return lista;
}


void ResolverEjercicios(Ejercicio[] lista, int cantidadMostrar)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\nResuelve estos ejercicios de suma:");
    Console.ResetColor();

    for (int i = 0; i < cantidadMostrar; i++)
    {
        int idx = rnd.Next(0, lista.Length);
        int a = lista[idx].Numero1;
        int b = lista[idx].Numero2;

        Console.Write($"{a} + {b} = ");
        int respuesta = int.Parse(Console.ReadLine());

        if (respuesta == lista[idx].Resultado)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Correcto!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Incorrecto. La respuesta era {lista[idx].Resultado}");
        }
        Console.ResetColor();
    }
}

do
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=== MENÚ DE MATEMÁTICAS BÁSICAS ===");
    Console.ResetColor();
    Console.WriteLine("1. Suma");
    Console.WriteLine("2. Resta");
    Console.WriteLine("3. Multiplicación");
    Console.WriteLine("4. División");
    Console.WriteLine("5. Salir");
    Console.Write("Elige una opción: ");
    opcionPrincipal = int.Parse(Console.ReadLine());

    if (opcionPrincipal == 1)
    {
        int opcionSub = 0;
        do
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- SUMA ---");
            Console.ResetColor();
            Console.WriteLine("1. Resolver ejercicios");
            Console.WriteLine("2. Volver al menú principal");
            Console.Write("Elige una opción: ");
            opcionSub = int.Parse(Console.ReadLine());

            if (opcionSub == 1)
            {
                Ejercicio[] ejercicios = GenerarEjercicios(10);
                ResolverEjercicios(ejercicios, 5);
            }
            else if (opcionSub == 2)
            {
                Console.WriteLine("Volviendo al menú principal...\n");
            }
            else
            {
                Console.WriteLine("Opción inválida");
            }

        } while (opcionSub != 2);
    }
    else if (opcionPrincipal == 2)
    {
        Console.WriteLine("\n--- RESTA --- (pendiente de implementar)\n");
    }
    else if (opcionPrincipal == 3)
    {
        Console.WriteLine("\n--- MULTIPLICACIÓN --- (pendiente de implementar)\n");
    }
    else if (opcionPrincipal == 4)
    {
        Console.WriteLine("\n--- DIVISIÓN --- (pendiente de implementar)\n");
    }
    else if (opcionPrincipal == 5)
    {
        Console.WriteLine("Saliendo del programa...");
    }
    else
    {
        Console.WriteLine("Opción inválida");
    }

} while (opcionPrincipal != 5);


struct Ejercicio
{
    public int Numero1;
    public int Numero2;
    public int Resultado;
}