string jugador = "";
int mundoActual = 0;
bool partidaGuardada = false;

void MenuInicial()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=== BIENVENIDO AL JUEGO DE MATEMÁTICAS ===");
    Console.ResetColor();
    Console.Write("Ingrese su nombre de jugador: ");
    jugador = Console.ReadLine();

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
            if (partidaGuardada) Console.WriteLine("2. Continuar partida");
            Console.WriteLine("3. Cambiar dificultad");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = LeerEntero("");

            switch (opcion)
            {
                case 1: NuevaPartida(); break;
                case 2: if (partidaGuardada) ContinuarPartida(); else Console.WriteLine("No hay partida guardada."); break;
                case 3: SelectorDificultad(); break;
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
    mundoActual = 1; // empieza en suma
    partidaGuardada = true;
    GuardarProgreso(1);
    SelectorDificultad();
}

void ContinuarPartida()
{
    CargarProgreso();
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
            Console.WriteLine("¡Atención! En dificultad difícil tienes tiempo límite para completar 30 ejercicios.");
            Console.ResetColor();
            Console.WriteLine("Suma = 6 min, Resta = 6 min, Multiplicación = 8 min, División = 10 min.");
        }
    }
    catch
    {
        Console.WriteLine("Entrada inválida, se usará dificultad fácil.");
        dif = 1;
    }

    switch (mundoActual)
    {
        case 1: MundoSuma(dif); break;
        case 2: MundoResta(dif); break;
        case 3: MundoMultiplicacion(dif); break;
        case 4: MundoDivision(dif); break;
        default: Console.WriteLine("No hay mundo actual."); break;
    }
}

void MundoSuma(int dificultad)
{
    Console.Clear();
    Console.WriteLine("=== MUNDO SUMA ===");
    if (GenerarYResolver("Suma", dificultad, 30, 6)) { mundoActual = 2; GuardarProgreso(dificultad); Console.WriteLine("¡Has completado SUMA! Se desbloquea RESTA."); }
    else Console.WriteLine("No completaste los ejercicios a tiempo.");
}

void MundoResta(int dificultad)
{
    Console.Clear();
    Console.WriteLine("=== MUNDO RESTA ===");
    if (GenerarYResolver("Resta", dificultad, 30, 6)) { mundoActual = 3; GuardarProgreso(dificultad); Console.WriteLine("¡Has completado RESTA! Se desbloquea MULTIPLICACIÓN."); }
    else Console.WriteLine("No completaste los ejercicios a tiempo.");
}

void MundoMultiplicacion(int dificultad)
{
    Console.Clear();
    Console.WriteLine("=== MUNDO MULTIPLICACIÓN ===");
    if (GenerarYResolver("Multiplicación", dificultad, 30, 8)) { mundoActual = 4; GuardarProgreso(dificultad); Console.WriteLine("¡Has completado MULTIPLICACIÓN! Se desbloquea DIVISIÓN."); }
    else Console.WriteLine("No completaste los ejercicios a tiempo.");
}

void MundoDivision(int dificultad)
{
    Console.Clear();
    Console.WriteLine("=== MUNDO DIVISIÓN ===");
    if (GenerarYResolver("División", dificultad, 30, 10)) { Console.WriteLine("¡Has completado todos los mundos!"); }
    else Console.WriteLine("No completaste los ejercicios a tiempo.");
}

bool GenerarYResolver(string operacion, int dificultad, int cantidad, int minutos)
{
    Random rnd = new Random();
    Ejercicio[] lista = new Ejercicio[cantidad];

    DateTime inicio = DateTime.Now;
    TimeSpan limite = TimeSpan.FromMinutes(minutos);

    for (int i = 0; i < lista.Length; i++)
    {
        int a = 0, b = 0;
        if (dificultad == 1) { a = rnd.Next(1, 20); b = rnd.Next(1, 20); }
        else if (dificultad == 2) { a = rnd.Next(30, 300); b = rnd.Next(30, 300); }
        else if (dificultad == 3) { a = rnd.Next(200, 1500); b = rnd.Next(200, 1500); }

        int resultado = 0;
        switch (operacion)
        {
            case "Suma": resultado = a + b; break;
            case "Resta": resultado = a - b; break;
            case "Multiplicación": resultado = a * b; break;
            case "División": resultado = b != 0 ? a / b : 0; break;
        }

        lista[i].Numero1 = a;
        lista[i].Numero2 = b;
        lista[i].Resultado = resultado;
        lista[i].Operacion = operacion;
        lista[i].Dificultad = dificultad;

        Console.Write($"{a} {SimboloOperacion(operacion)} {b} = ");
        try
        {
            int respuesta = int.Parse(Console.ReadLine());
            if (respuesta == resultado)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Correcto!");
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

        if (dificultad == 3 && DateTime.Now - inicio > limite)
        {
            Console.WriteLine("¡Se acabó el tiempo!");
            return false;
        }
    }
    return true;
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

void GuardarProgreso(int dificultad)
{
    using (StreamWriter archivo = new StreamWriter($"{jugador}.csv"))
    {
        archivo.WriteLine(jugador);
        archivo.WriteLine(mundoActual);
        archivo.WriteLine(dificultad);
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
}

int LeerEntero(string mensaje)
{
    int valor;

    while (!int.TryParse(Console.ReadLine(), out valor))
    {
        Console.WriteLine("Ingrese un número válido:");
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
    public int Dificultad;
}
