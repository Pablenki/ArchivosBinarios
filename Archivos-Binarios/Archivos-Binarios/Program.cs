using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Archivos_Binarios
{
    class ArchivoBinarioEmpleados
    {
        // Deckaracion de flujos
        BinaryWriter bw = null; // flujo salida - escritura de datos
        BinaryReader br = null; // flujo entrada - lectura de datos

        // campos de la clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;

        public void CrearArchivo (string Archivo)
        {
            // Variable local metodo
            char resp;

                try
            {
                // Creacion del flujo poara escribir datos del archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                //  captura de datos
                do
                {
                    Console.Clear();
                    Console.Write("Numero del Empleado: ");
                    NumEmp = Int32.Parse(Console.ReadLine());
                    Console.Write("Nombre del Empleado: ");
                    Nombre = Console.ReadLine();
                    Console.Write("Dirección del Empleado: ");
                    Direccion = Console.ReadLine();
                    Console.Write("Telefono del empleado: ");
                    Telefono = Int64.Parse(Console.ReadLine());
                    Console.Write("Dias Trabajados del Empleado: ");
                    DiasTrabajados = Int32.Parse(Console.ReadLine());
                    Console.Write("Salario Diario del Empleado: ");
                    SalarioDiario = Single.Parse(Console.ReadLine());

                    // Escribe los datos del archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);

                    Console.Write("\n\nDeseas Almacenar otro Registro (s/n)?");

                        resp = Char.Parse(Console.ReadLine());
                } while ((resp == 's') || (resp == 'S'));
            }
            catch (IOException e)
            {
                Console.WriteLine("\nError :" + e.Message);
                Console.WriteLine("\nRuta :" + e.StackTrace);
            }
            finally
            {
                if (bw != null) bw.Close(); // Cierra el flujo - escritura
                Console.Write("\nPresione <enter> para terminar la Escritura de Datos y regresar al Menu.");
                Console.ReadKey();
            }
        }

        public void MostrarArchivo(string Archivo)
        {
            try
            {
                // Verifica si existe el archivo
                if (File.Exists(Archivo))
                {
                    // Creacion flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                    //despliegue de datos en pantalla
                    Console.Clear();
                    do
                    {
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();

                        // muestra los datos
                        Console.WriteLine("Numero del empleado: " + NumEmp);
                        Console.WriteLine("Nombre del empleado: " + Nombre);
                        Console.WriteLine("Direccion del empleado: " + Direccion);
                        Console.WriteLine("Telefono del empleado: " + Telefono);
                        Console.WriteLine("Dias Trabajados del empleado: " + DiasTrabajados);
                        Console.WriteLine("Salario Diario del Empleado: " + SalarioDiario);
                        Console.WriteLine("SUELDO TOTAL del Empleado: {0:C}", DiasTrabajados * SalarioDiario);
                        Console.WriteLine("\n");
                    } while (true);                                    
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nEl Archivo " + Archivo + "No existe en el Disco!!");
                    Console.Write("\nPresione <enter> para Continuar...");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del listado de Empleados");
                Console.Write("\nPresione <enter> para continuar...");
                Console.ReadKey();
            }

            finally
            {
                if (br != null) br.Close(); // cierra flujo
                Console.Write("\nPresione <enter> para terminar la lectura de Datos y regresar al Menu.");
                Console.ReadKey();
            }
        }
        static void Main(string[] args)
        {
            string Arch = null;
            int opcion;

            // creacion del objeto
            ArchivoBinarioEmpleados AL = new ArchivoBinarioEmpleados();
            do
            {
                // Menu de Opciones
                Console.Clear();
                Console.WriteLine("\n ♣♦♥ ARCHIVO BINARIO EMPLEADOS ♥♦♣");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del programa.");
                Console.Write("\nQue opcion deseas: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        // bloque de escritura 
                        try
                        {
                            // captura nombre del archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: ");
                            Arch = Console.ReadLine();

                            // Verifica si existe el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Deseas sobreescribirlo (s/n) ? ");
                                resp = Char.Parse(Console.ReadLine());

                            }
                            if ((resp == 's') || (resp == 'S'))
                                AL.CrearArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError :" + e.Message);
                            Console.WriteLine("\nRuta :" + e.StackTrace);
                        }
                        break;

                    case 2:
                        // Bloque de lectura
                        try
                        {
                            // captura nombre archivos
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas Leer: ");
                            Arch = Console.ReadLine();
                            AL.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para salir del programa.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Write("\nEsa opcion no existe!!, Presione <enter> para continuar...");
                        Console.ReadKey();
                        break;

                }
            } while (opcion != 3);
        }
    }
}
