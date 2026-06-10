string jugador = "";
int operacionActual = 0;
int dificultadActual = 0;
bool partidaGuardada = false;
bool sumaDesbloqueada = true;
bool restaDesbloqueada = false;
bool multiDesbloqueada = false;
bool divDesbloqueada = false;

Dictionary<string, int> intentos = new Dictionary<string, int>();
Dictionary<string, int> aciertos = new Dictionary<string, int>();

void InicializarEstadisticas()
{
    intentos["SumaF"] = 0; intentos["SumaM"] = 0; intentos["SumaD"] = 0;
    intentos["RestaF"] = 0; intentos["RestaM"] = 0; intentos["RestaD"] = 0;
    intentos["MultiF"] = 0; intentos["MultiM"] = 0; intentos["MultiD"] = 0;
    intentos["DivF"] = 0; intentos["DivM"] = 0; intentos["DivD"] = 0;

    aciertos["SumaF"] = 0; aciertos["SumaM"] = 0; aciertos["SumaD"] = 0;
    aciertos["RestaF"] = 0; aciertos["RestaM"] = 0; aciertos["RestaD"] = 0;
    aciertos["MultiF"] = 0; aciertos["MultiM"] = 0; aciertos["MultiD"] = 0;
    aciertos["DivF"] = 0; aciertos["DivM"] = 0; aciertos["DivD"] = 0;
}

void MenuInicial()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=== APRENDE MATEMÁTICAS EN C# ===");
    Console.ResetColor();
    Console.Write("Ingrese su nombre de jugador: ");
    jugador = Console.ReadLine() ?? "";

    if (!intentos.ContainsKey("SumaF"))
        InicializarEstadisticas();

    int opcion = 0;
    do
    {
        try
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"=== MENÚ PRINCIPAL ({jugador}) ===");
            Console.ResetColor();
            Console.WriteLine("1. Nueva partida");
            Console.WriteLine("2. Continuar partida");
            Console.WriteLine("3. Estadísticas");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = LeerEntero("");

            switch (opcion)
            {
                case 1: NuevaPartida(); break;
                case 2: ContinuarPartida(); break;
                case 3: MostrarEstadisticas(); break;
                case 4: Console.WriteLine("Saliendo..."); break;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
        catch
        {
            Console.WriteLine("Entrada inválida, intente de nuevo.");
        }
    } while (opcion != 4);
}

void NuevaPartida()
{
    operacionActual = 1;
    partidaGuardada = true;
    sumaDesbloqueada = true;
    restaDesbloqueada = false;
    multiDesbloqueada = false;
    divDesbloqueada = false;
    GuardarProgreso(operacionActual, 1);
    SelectorOperacion();
}

void ContinuarPartida()
{
    if (!partidaGuardada && !System.IO.File.Exists($"{jugador}.csv"))
    {
        Console.WriteLine("No hay partida previamente guardada.");
        Console.WriteLine("Presiona una tecla para volver al menú principal...");
        Console.ReadKey();
        return;
    }
    CargarProgreso();
    SelectorOperacion();
}

void SelectorOperacion()
{
    int op = 0;
    try
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== ESCOGE OPERACIÓN ===");
        Console.ResetColor();
        Console.WriteLine("1. Suma ");
        Console.WriteLine("2. Resta " + (restaDesbloqueada ? "" : "(BLOQUEADA)"));
        Console.WriteLine("3. Multiplicación " + (multiDesbloqueada ? "" : "(BLOQUEADA)"));
        Console.WriteLine("4. División " + (divDesbloqueada ? "" : "(BLOQUEADA)"));
        Console.Write("Elige: ");
        op = LeerEntero("");

        if (op == 1)
        {
            operacionActual = 1;
        }
        else if (op == 2)
        {
            if (!restaDesbloqueada)
            {
                Console.WriteLine("La opción está BLOQUEADA. Primero completa SUMA.");
                Console.ReadKey();
                return;
            }
            operacionActual = 2;
        }
        else if (op == 3)
        {
            if (!multiDesbloqueada)
            {
                Console.WriteLine("La opción está BLOQUEADA. Primero completa RESTA.");
                Console.ReadKey();
                return;
            }
            operacionActual = 3;
        }
        else if (op == 4)
        {
            if (!divDesbloqueada)
            {
                Console.WriteLine("La opción está BLOQUEADA. Primero completa MULTIPLICACIÓN.");
                Console.ReadKey();
                return;
            }
            operacionActual = 4;
        }
        else
        {
            Console.WriteLine("Operación inválida.");
            Console.ReadKey();
            return;
        }
    }
    catch
    {
        Console.WriteLine("Entrada inválida.");
        Console.ReadKey();
        return;
    }

    SelectorDificultad();
}

void SelectorDificultad()
{
    int dif = 0;
    try
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=== SELECCIONA DIFICULTAD ===");
        Console.ResetColor();
        Console.WriteLine("1. Fácil ");
        Console.WriteLine("2. Medio ");
        Console.WriteLine("3. Difícil (con tiempo)");
        Console.Write("Elige: ");
        dif = LeerEntero("");

        if (dif == 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("¡Atención! En dificultad difícil tienes tiempo límite para completar 15 ejercicios.");
            Console.ResetColor();
            Console.WriteLine("Suma = 3 min, Resta = 3 min, Multiplicación = 4 min, División = 5 min.");
        }
    }
    catch
    {
        Console.WriteLine("Entrada inválida, se usará dificultad fácil.");
        dif = 1;
    }

    dificultadActual = dif;

    switch (operacionActual)
    {
        case 1: MundoSuma(dif); break;
        case 2: MundoResta(dif); break;
        case 3: MundoMultiplicacion(dif); break;
        case 4: MundoDivision(dif); break;
        default: Console.WriteLine("No hay operación actual."); break;
    }

    Console.WriteLine("");
    Console.WriteLine("Regresando al menú de operaciones...");
    Console.ReadKey();
    SelectorOperacion();
}

void MundoSuma(int dificultad)
{
    Console.Clear();
    Console.WriteLine("=== SUMA ===");
    var resultado = GenerarYResolver("Suma", dificultad, 15, 3);
    intentos["SumaF"] += (dificultad == 1) ? resultado.intentos : 0;
    intentos["SumaM"] += (dificultad == 2) ? resultado.intentos : 0;
    intentos["SumaD"] += (dificultad == 3) ? resultado.intentos : 0;
    aciertos["SumaF"] += (dificultad == 1) ? resultado.aciertos : 0;
    aciertos["SumaM"] += (dificultad == 2) ? resultado.aciertos : 0;
    aciertos["SumaD"] += (dificultad == 3) ? resultado.aciertos : 0;

    if (resultado.completado)
    {
        restaDesbloqueada = true;
        GuardarProgreso(2, dificultad);
        Console.WriteLine("¡Has completado SUMA con 100%! Se desbloquea RESTA.");
    }
    else
    {
        Console.WriteLine("No completaste SUMA con 100%. Intenta de nuevo.");
    }
}

void MundoResta(int dificultad)
{
    Console.Clear();
    Console.WriteLine("=== RESTA ===");
    var resultado = GenerarYResolver("Resta", dificultad, 15, 3);
    intentos["RestaF"] += (dificultad == 1) ? resultado.intentos : 0;
    intentos["RestaM"] += (dificultad == 2) ? resultado.intentos : 0;
    intentos["RestaD"] += (dificultad == 3) ? resultado.intentos : 0;
    aciertos["RestaF"] += (dificultad == 1) ? resultado.aciertos : 0;
    aciertos["RestaM"] += (dificultad == 2) ? resultado.aciertos : 0;
    aciertos["RestaD"] += (dificultad == 3) ? resultado.aciertos : 0;

    if (resultado.completado)
    {
        multiDesbloqueada = true;
        GuardarProgreso(3, dificultad);
        Console.WriteLine("¡Has completado RESTA con 100%! Se desbloquea MULTIPLICACIÓN.");
    }
    else
    {
        Console.WriteLine("No completaste RESTA con 100%. Intenta de nuevo.");
    }
}

void MundoMultiplicacion(int dificultad)
{
    Console.Clear();
    Console.WriteLine("=== MULTIPLICACIÓN ===");
    var resultado = GenerarYResolver("Multiplicación", dificultad, 15, 4);
    intentos["MultiF"] += (dificultad == 1) ? resultado.intentos : 0;
    intentos["MultiM"] += (dificultad == 2) ? resultado.intentos : 0;
    intentos["MultiD"] += (dificultad == 3) ? resultado.intentos : 0;
    aciertos["MultiF"] += (dificultad == 1) ? resultado.aciertos : 0;
    aciertos["MultiM"] += (dificultad == 2) ? resultado.aciertos : 0;
    aciertos["MultiD"] += (dificultad == 3) ? resultado.aciertos : 0;

    if (resultado.completado)
    {
        divDesbloqueada = true;
        GuardarProgreso(4, dificultad);
        Console.WriteLine("¡Has completado MULTIPLICACIÓN con 100%! Se desbloquea DIVISIÓN.");
    }
    else
    {
        Console.WriteLine("No completaste MULTIPLICACIÓN con 100%. Intenta de nuevo.");
    }
}

void MundoDivision(int dificultad)
{
    Console.Clear();
    Console.WriteLine("=== DIVISIÓN ===");
    var resultado = GenerarYResolver("División", dificultad, 15, 5);
    intentos["DivF"] += (dificultad == 1) ? resultado.intentos : 0;
    intentos["DivM"] += (dificultad == 2) ? resultado.intentos : 0;
    intentos["DivD"] += (dificultad == 3) ? resultado.intentos : 0;
    aciertos["DivF"] += (dificultad == 1) ? resultado.aciertos : 0;
    aciertos["DivM"] += (dificultad == 2) ? resultado.aciertos : 0;
    aciertos["DivD"] += (dificultad == 3) ? resultado.aciertos : 0;

    if (resultado.completado)
    {
        Console.WriteLine("¡Felicitaciones! Completaste todo el juego con 100%!");
        Console.WriteLine("Eres un maestro de las matemáticas!");
    }
    else
    {
        Console.WriteLine("No completaste DIVISIÓN con 100%. Intenta de nuevo.");
    }
}

(int intentos, int aciertos, bool completado) GenerarYResolver(string operacion, int dificultad, int cantidad, int minutos)
{
    Random rnd = new Random();

    DateTime inicio = DateTime.Now;
    TimeSpan limite = TimeSpan.FromMinutes(minutos);

    int totalIntentos = 0;
    int totalAciertos = 0;

    for (int i = 0; i < cantidad; i++)
    {
        if (dificultad == 3 && DateTime.Now - inicio > limite)
        {
            Console.WriteLine("¡Se acabó el tiempo!");
            return (totalIntentos, totalAciertos, false);
        }

        int a = 0, b = 0;
        if (dificultad == 1) { a = rnd.Next(1, 51); b = rnd.Next(1, 51); }
        else if (dificultad == 2) { a = rnd.Next(50, 201); b = rnd.Next(50, 201); }
        else if (dificultad == 3) { a = rnd.Next(200, 501); b = rnd.Next(200, 501); }

        int resultado = 0;
        switch (operacion)
        {
            case "Suma": resultado = a + b; break;
            case "Resta":
                if (a < b) { int t = a; a = b; b = t; }
                resultado = a - b;
                break;
            case "Multiplicación": resultado = a * b; break;
            case "División":
                b = rnd.Next(1, 21);
                a = b * rnd.Next(1, 21);
                resultado = a / b;
                break;
        }

        Console.Write($"{a} {SimboloOperacion(operacion)} {b} = ");

        string? entrada = Console.ReadLine();
        if (entrada != null && entrada.ToLower() == "salir")
        {
            Console.WriteLine("Volviendo al menú...");
            return (totalIntentos, totalAciertos, false);
        }

        try
        {
            int respuesta = int.Parse(Console.ReadLine());
            if (respuesta == resultado)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correcto!");
                totalAciertos++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Incorrecto. La respuesta era {resultado}");
            }
            Console.ResetColor();
        }
        catch
        {
            Console.WriteLine("Entrada inválida.");
        }
    }

    bool completado = (totalAciertos == cantidad);
    if (completado)
    {
        Console.WriteLine("¡Completaste con 100% de precisión!");
    }

    return (totalIntentos, totalAciertos, completado);
}

string SimboloOperacion(string op)
{
    switch (op)
    {
        case "Suma": return "+";
        case "Resta": return "-";
        case "Multiplicación": return "*";
        case "División": return "/";
        default: return "?";
    }
}

void GuardarProgreso(int operacion, int dificultad)
{
    using (StreamWriter archivo = new StreamWriter($"{jugador}.csv"))
    {
        archivo.WriteLine(jugador);
        archivo.WriteLine(operacion);
        archivo.WriteLine(dificultad);
        archivo.WriteLine(restaDesbloqueada ? "1" : "0");
        archivo.WriteLine(multiDesbloqueada ? "1" : "0");
        archivo.WriteLine(divDesbloqueada ? "1" : "0");
    }
}

void CargarProgreso()
{
    if (System.IO.File.Exists($"{jugador}.csv"))
    {
        using (StreamReader archivo = new StreamReader($"{jugador}.csv"))
        {
            jugador = archivo.ReadLine();
            mundoActual = int.Parse(archivo.ReadLine());
            int dificultad = int.Parse(archivo.ReadLine());
            partidaGuardada = true;
        }
    }

    return valor;
}

MenuInicial();

struct Ejercicio
{
    public int Numero1;
    public int Numero2;
    public int Resultado;
    public string Operacion;
    public 