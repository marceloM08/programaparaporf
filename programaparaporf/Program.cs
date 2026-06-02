Random rnd = new Random();
int opcionPrincipal = 0;

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

                int[,] ejercicios = new int[10, 2];
                for (int i = 0; i < 10; i++)
                {
                    ejercicios[i, 0] = rnd.Next(1, 50);
                    ejercicios[i, 1] = rnd.Next(1, 50);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nResuelve estos ejercicios de suma:");
                Console.ResetColor();


                for (int i = 0; i < 3; i++)
                {
                    int idx = rnd.Next(0, 10);
                    int a = ejercicios[idx, 0];
                    int b = ejercicios[idx, 1];

                    Console.Write($"{a} + {b} = ");
                    int respuesta = int.Parse(Console.ReadLine());

                    if (respuesta == a + b)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Correcto!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Incorrecto. La respuesta era {a + b}");
                    }
                    Console.ResetColor();
                }
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
