namespace DiagnosticoMotoresElectricos
{
    // ==========================================
    // CLASE: DiagnosticoMotor
    // Gestiona las lecturas de corriente y el diagnóstico técnico de un motor
    // ==========================================
    public class DiagnosticoMotor
    {
        // PROPIEDADES (con sus respectivos tipos de datos)
        public string Identificador { get; set; }
        public double[] MedicionesCorriente { get; set; } // Arreglo de 4 lecturas en Amperios (A)

        // Constructor
        public DiagnosticoMotor(string identificador, double[] mediciones)
        {
            Identificador = identificador;
            MedicionesCorriente = mediciones;
        }

        // MÉTODOS DE LA CLASE

        /// <summary>
        /// Calcula el promedio de corriente procesando el arreglo de mediciones mediante un ciclo for.
        /// </summary>
        /// <returns>Promedio de corriente en Amperios (A)</returns>
        public double CalcularPromedioCorriente()
        {
            // Validar que el arreglo no sea nulo ni esté vacío para evitar divisiones entre cero
            if (MedicionesCorriente == null || MedicionesCorriente.Length == 0)
            {
                return 0.0;
            }

            double suma = 0.0;

            // Uso de ciclo for obligatorio para la captura/procesamiento de lecturas
            for (int i = 0; i < MedicionesCorriente.Length; i++)
            {
                suma += MedicionesCorriente[i];
            }

            return suma / MedicionesCorriente.Length;
        }

        /// <summary>
        /// Evalúa el promedio de corriente y determina el estado del motor.
        /// </summary>
        /// <returns>"NORMAL" o "REQUIERE MANTENIMIENTO"</returns>
        public string ObtenerEstadoDiagnostico()
        {
            double promedio = CalcularPromedioCorriente();

            // Criterio del problema: Promedio <= 5 A es NORMAL, mayor es REQUIERE MANTENIMIENTO
            if (promedio <= 5.0)
            {
                return "NORMAL";
            }
            else
            {
                return "REQUIERE MANTENIMIENTO";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Diagnóstico de Motores Eléctricos - Mantenimiento";

            // Ciclo WHILE / DO-WHILE para permitir procesar una cantidad desconocida de motores
            char opcionContinuar;
            do
            {
                Console.Clear();
                Console.WriteLine("=================================================");
                Console.WriteLine("      DIAGNÓSTICO MANTENIMIENTO DE MOTORES      ");
                Console.WriteLine("=================================================\n");

                // Captura del identificador
                Console.Write("Ingrese el identificador/código del motor: ");
                string id = Console.ReadLine();

                // Arreglo para almacenar las 4 mediciones
                double[] lecturas = new double[4];

                Console.WriteLine("\n--- Captura de 4 Lecturas de Corriente ---");

                // Captura mediante ciclo FOR
                for (int i = 0; i < 4; i++)
                {
                    lecturas[i] = LeerCorrienteValida($"Ingrese la medición de corriente #{i + 1} (A): ");
                }

                // CREACIÓN DEL OBJETO a partir de la clase DiagnosticoMotor
                DiagnosticoMotor motor = new DiagnosticoMotor(id, lecturas);

                // CÁLCULO Y DIAGNÓSTICO
                double promedio = motor.CalcularPromedioCorriente();
                string estado = motor.ObtenerEstadoDiagnostico();

                // DESPLIEGUE DE RESULTADOS CON UNIDADES DE MEDIDA
                Console.WriteLine("\n=================================================");
                Console.WriteLine("              INFORME DE DIAGNÓSTICO             ");
                Console.WriteLine("=================================================");
                Console.WriteLine($"Motor Identificador:   {motor.Identificador}");
                Console.WriteLine($"Lecturas registradas:  {string.Join(" A, ", motor.MedicionesCorriente)} A");
                Console.WriteLine($"Promedio de Corriente: {promedio:F2} A");
                Console.WriteLine($"Estado del Motor:      {estado}");
                Console.WriteLine("=================================================\n");

                // Preguntar al usuario si desea registrar otro motor (control del ciclo while/do-while)
                Console.Write("¿Desea realizar el diagnóstico de otro motor? (S/N): ");
                string entradaOpcion = Console.ReadLine().Trim().ToUpper();
                opcionContinuar = (entradaOpcion.Length > 0) ? entradaOpcion[0] : 'N';

            } while (opcionContinuar == 'S');

            Console.WriteLine("\nProceso de diagnóstico finalizado. Presione cualquier tecla para salir...");
            Console.ReadKey();
        }

        /// <summary>
        /// Método de validación que garantiza la lectura de corrientes numéricas no negativas (>= 0 A),
        /// evitando valores incoherentes o errores de formato.
        /// </summary>
        static double LeerCorrienteValida(string mensaje)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (double.TryParse(entrada, out valor) && valor >= 0)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [ERROR] Entrada inválida. La corriente debe ser un número mayor o igual a cero (0 A).");
                Console.ResetColor();
            }
            return valor;
        }
    }
}
