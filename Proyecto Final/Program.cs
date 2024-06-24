using System.Net;

namespace ProyectoFinal_CordobaIsmael2
{
    internal class Program
    {
        static List<Alumno> alumnos = new List<Alumno>();
        static List<Materia> materias = new List<Materia>();
        public static string archivo = "Lista de Alumnos.txt";
        public static string archivo2 = "Materias Cursadas.txt";
        public static string archivo3 = "Materias.txt";
        public int Legajo = 0;
        public int indiceMateria = 0;
        public struct Alumno
        {
            public int Legajo;
            public string Apellido;
            public string Nombre;
            public string DNI;
            public DateTime fechaNacimiento;
            public string Domicilio;
            public bool estaActivo;
        }

        public struct Materia
        {
            public int indiceMateria;
            public string nombreMateria;
            public bool estaActiva;
        }

        public struct AlumnoMateria
        {
            public int IndiceAlumnoMateria;
            public int IndiceAlumno;
            public int IndiceMateria;
            public string Estado;
            public int Nota;
            public DateTime Fecha;
        }
        static int LeerEntero(string Mensaje)
        {
            int Numero = 0;
            bool esEntero = false;
            string datoEntrada;
            Console.WriteLine(Mensaje);
            while (!esEntero)
            {
                datoEntrada = Mensaje;
                if (!int.TryParse(datoEntrada, out Numero))
                {
                    Console.WriteLine("El valor ingresado no es valido. Ingreselo de nuevo: ");
                }
                else
                {
                    esEntero = true;
                }
            }
            return Numero;
        }

        static void AltaAlumno(int Legajo)
        {
            int dni = LeerEntero("Ingrese el DNI para comenzar la operación: ");
            RetornarListaAlumnos(archivo);
            bool alumnoExistente = false;
            Alumno alumnoEncontrado = new Alumno();
            alumnoEncontrado.estaActivo = true;
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (alumnos[i].DNI == Convert.ToString(dni))
                {
                    alumnoExistente = true;
                    alumnoEncontrado.Legajo = alumnos[i].Legajo;
                    alumnoEncontrado.Apellido = alumnos[i].Apellido;
                    alumnoEncontrado.Nombre = alumnos[i].Nombre;
                    alumnoEncontrado.DNI = alumnos[i].DNI;
                    alumnoEncontrado.fechaNacimiento = alumnos[i].fechaNacimiento;
                    alumnoEncontrado.Domicilio = alumnos[i].Domicilio;
                    alumnoEncontrado.estaActivo = alumnos[i].estaActivo;
                    break;
                }
            }

            if (!alumnoExistente)
            {
                Console.WriteLine("Ingrese el apellido del alumno: ");
                string apellido = Console.ReadLine();
                Console.WriteLine("Ingrese el nombre del alumno: ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese la fecha de nacimiento del alumno: ");
                DateTime fechaNacimiento = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Ingrese el domicilio del alumno: ");
                string Domicilio = Console.ReadLine();
                int nuevoIndice = Legajo + 1;
                Alumno nuevoAlumno = new Alumno
                {
                    Legajo = nuevoIndice,
                    Nombre = nombre,
                    Apellido = apellido,
                    DNI = Convert.ToString(dni),
                    fechaNacimiento = fechaNacimiento,
                    Domicilio = Domicilio,
                    estaActivo = true,
                };
                alumnos.Add(nuevoAlumno);
                GuardarAlumnosEnArchivo();
                Console.WriteLine("Alumno dado de alta exitosamente.\n -------------------------------------------------------------------");
            }
            else
            {
                if (alumnoEncontrado.estaActivo)
                {
                    Console.WriteLine("ERROR: Ya existe un alumno activo con el mismo DNI.\n ---------------------------------------------------");
                }
                else
                {
                    Console.Write("El alumno ya existe pero está inactivo. ¿Desea reactivarlo? (S/N): ");
                    string respuesta = Console.ReadLine();
                    if ((respuesta == "S") || (respuesta == "s"))
                    {
                        alumnoEncontrado.estaActivo = true;
                        GuardarAlumnosEnArchivo();
                        Console.WriteLine("Alumno reactivado exitosamente.\n --------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("El alumno no se ha activado.\n --------------------------------------------------------------");
                    }
                }
            }

        }
        static void AltaMateria(int indiceMateria)
        {
            Console.Write("Ingrese el nombre de la materia que desea agregar: ");
            string nombredemateria  = Console.ReadLine();
            RetornarListaMateria(archivo2);
            bool materiaExistente = false;
            Materia materiaEncontrada = new Materia();
            materiaEncontrada.estaActiva = true;
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (materias[i].nombreMateria == nombredemateria)
                {
                    materiaExistente = true;
                    materiaEncontrada.indiceMateria = materias[i].indiceMateria;
                    materiaEncontrada.nombreMateria = materias[i].nombreMateria;
                    materiaEncontrada.estaActiva = materias[i].estaActiva;
                    break;
                }
            }

            if (!materiaExistente)
            {
                
                int nuevoIndice = indiceMateria + 1;
                Materia nuevaMateria = new Materia
                {
                    indiceMateria = nuevoIndice,
                    nombreMateria = nombredemateria,
                    estaActiva = true,
                };
                materias.Add(nuevaMateria);
                GuardarMateriaEnArchivo();
                Console.WriteLine("Materia dada de alta.\n -------------------------------------------------------------------");
            }
            else
            {
                if (materiaEncontrada.estaActiva)
                {
                    Console.WriteLine("ERROR: Ya existe un alumno activo con el mismo DNI.\n ---------------------------------------------------");
                }
                else
                {
                    Console.Write("El alumno ya existe pero está inactivo. ¿Desea reactivarlo? (S/N): ");
                    string respuesta = Console.ReadLine();
                    if ((respuesta == "S") || (respuesta == "s"))
                    {
                        materiaEncontrada.estaActiva = true;
                        GuardarAlumnosEnArchivo();
                        Console.WriteLine("Alumno reactivado exitosamente.\n --------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("El alumno no se ha activado.\n --------------------------------------------------------------");
                    }
                }
            }

        }

        

        public static List<Materia> RetornarListaMateria(string archivo2)
        {
            List<Materia> listaMateria = new List<Materia>();
            using (StreamReader sr = new StreamReader(archivo2))
            {
                string? linea = sr.ReadLine();

                while (linea != null)
                {
                    string[] MateriaArchivo = linea.Split(',');
                    Materia materiaStruct = new Materia();
                    materiaStruct.indiceMateria = int.Parse(MateriaArchivo[0]);
                    materiaStruct.nombreMateria = MateriaArchivo[1];
                    materiaStruct.estaActiva = Convert.ToBoolean(MateriaArchivo[2]);
                    listaMateria.Add(materiaStruct);
                    linea = sr.ReadLine();
                }
            }
            return listaMateria;
        }

        static void GuardarMateriaEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(archivo2, true))
            {
                foreach (var materia in materias)
                {
                    writer.WriteLine($"{materia.indiceMateria}, {materia.nombreMateria}, {materia.estaActiva}");
                }
            }
        }

        static void GuardarAlumnosEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(archivo, true))
            {
                foreach (var alumno in alumnos)
                {
                    writer.WriteLine($"{alumno.Legajo}, {alumno.Nombre}, {alumno.Apellido}, {alumno.DNI}, {alumno.fechaNacimiento:dd/MM/yyyy}, {alumno.Domicilio}, {alumno.estaActivo}");
                }
            }
        }

        static void CargarAlumnosDesdeArchivo()
        {
            if (File.Exists(archivo))
            {
                string[] lineas = File.ReadAllLines(archivo);
                foreach (var linea in lineas)
                {
                    string[] separador = linea.Split(',');
                    Alumno alumno = new Alumno
                    {
                        Legajo = int.Parse(separador[0]),
                        Nombre = separador[1],
                        Apellido = separador[2],
                        DNI = separador[3],
                        fechaNacimiento = DateTime.Parse(separador[4]),
                        Domicilio = separador[5],
                        estaActivo = bool.Parse(separador[6])
                    };
                    alumnos.Add(alumno);
                }
            }
        }

        static void BajaAlumno()
        {
            int dni = LeerEntero("Ingrese el DNI del alumno que desea dar de baja: ");
            bool concatenar = true;
            using (StreamWriter escritor = new StreamWriter(archivo, concatenar))
            {
                List<Alumno> alumnos = RetornarListaAlumnos(archivo);
                bool existe = false;
                Alumno alumnos2 = new Alumno();
                for (int i = 0; i < alumnos.Count; i++)
                {
                    if (alumnos[i].DNI == Convert.ToString(dni))
                    {
                        existe = true;
                    }
                }
                if (existe)
                {
                    alumnos2.estaActivo = false;
                }

                EscribirAlumnos(alumnos);

            }
        }

        static void BajaMateria()
        {
            Console.Write("Ingrese el nombre de la materia que quiere dar de baja");
            string nombreMateria = Console.ReadLine();
            
            using (StreamWriter escritor = new StreamWriter(archivo2, true))
            {
                List<Materia> Materias = RetornarListaMateria(archivo2);
                bool existe = false;
                Materia Materia2 = new Materia();
                for (int i = 0; i < Materias.Count; i++)
                {
                    if (materias[i].nombreMateria == nombreMateria)
                    {
                        existe = true;
                    }
                }
                if (existe)
                {
                    Materia2.estaActiva = false;
                }

                EscribirMateria(Materias);

            }
        }

        static int EstadoAlumno()
        {
            int numero = 0;

            return numero;
        }

        static void ModificarAlumno()  //Como modificar un alumno, como transcribirlo
        {
            int numerodni = LeerEntero("Ingrese el numero de dni del alumno que desea eliminar: ");
            using (StreamWriter escritor = new StreamWriter(archivo, true))
            {
                List<Alumno> alumnos = RetornarListaAlumnos(archivo);
                bool existe = false;
                Alumno alumnos2 = new Alumno();
                for (int i = 0; i < alumnos.Count; i++)
                {
                    if (alumnos[i].DNI == Convert.ToString(numerodni))
                    {
                        alumnos2.Nombre = alumnos[i].Nombre;
                        alumnos2.Apellido = alumnos[i].Apellido;
                        alumnos2.DNI = Convert.ToString(numerodni);
                        alumnos2.Domicilio = alumnos[i].Domicilio;
                        alumnos2.fechaNacimiento = alumnos[i].fechaNacimiento;
                        alumnos2.estaActivo = alumnos[i].estaActivo;
                        existe = true;
                    }
                }
                

                EscribirAlumnos(alumnos);

            }

        }





        public static List<Alumno> RetornarListaAlumnos(string archivo)
        {
            List<Alumno> listaAlumnos = new List<Alumno>();
            using (StreamReader sr = new StreamReader(archivo))
            {
                string? linea = sr.ReadLine();

                while (linea != null)
                {
                    string[] alumnoArchivo = linea.Split(',');
                    Alumno alumnoStruct = new Alumno();
                    alumnoStruct.Legajo = int.Parse(alumnoArchivo[0]);
                    alumnoStruct.Nombre = alumnoArchivo[1];
                    alumnoStruct.Apellido = alumnoArchivo[2];
                    alumnoStruct.DNI = (alumnoArchivo[3]);
                    alumnoStruct.fechaNacimiento = Convert.ToDateTime(alumnoArchivo[4]);
                    alumnoStruct.Domicilio = (alumnoArchivo[4]);
                    alumnoStruct.estaActivo = Convert.ToBoolean(alumnoArchivo[6]);
                    listaAlumnos.Add(alumnoStruct);
                    linea = sr.ReadLine();
                }
            }
            return listaAlumnos;
        }

        public static void MostrarAlumnosActivos()
        {
            RetornarListaAlumnos(archivo);
            Console.WriteLine("Los alumnos activos actualmente son: ");
            for (int i = 0; i < alumnos.Count; i++)
            {
                bool alumnoEncontrado = alumnos[i].estaActivo;
                if (alumnoEncontrado)
                {
                    Console.WriteLine(alumnos[i].Legajo + ", " + alumnos[i].Nombre + ", " + alumnos[i].Apellido);
                }

            }
            Console.WriteLine($"--------------------------------------------------------------------------------------\n");
        }


        public static void MostrarAlumnosInactivos()
        {
            RetornarListaAlumnos(archivo);
            Console.WriteLine("Los alumnos activos actualmente son: ");
            for (int i = 0; i < alumnos.Count; i++)
            {
                bool alumnoEncontrado = alumnos[i].estaActivo;
                if (!alumnoEncontrado)
                {
                    EscribirAlumnos(alumnos);
                }

            }
            Console.WriteLine($"--------------------------------------------------------------------------------------\n");

        }

        public static void EscribirMateria(List<Materia> materias)
        {
            using (StreamWriter escritor = new StreamWriter(archivo2, true))
            {
                foreach (var materia in materias)
                {
                    escritor.WriteLine(materia.indiceMateria + ", " + materia.nombreMateria + ", " + materia.estaActiva);
                }

            }
        }

        public static void EscribirAlumnos(List<Alumno> alumnos)
        {
            using (StreamWriter escritor = new StreamWriter(archivo, true))
            {
                foreach (var alumno in alumnos)
                {
                    escritor.WriteLine(alumno.Legajo + ", " + alumno.Nombre + ", " + alumno.Apellido);
                }

            }
        }

        static int ValidarNumeroMenu()
        {
            bool esValido = false;
            int Numero = 0;
            do
            {
                Console.WriteLine("==== MENÚ ====");
                Console.WriteLine("1. Alta de alumno");
                Console.WriteLine("2. Baja de alumno");
                Console.WriteLine("3. Modificación de alumno");
                Console.WriteLine("4. Mostrar alumnos activos");
                Console.WriteLine("5. Mostrar alumnos inactivos");
                Console.WriteLine("6. Alta de materia");
                Console.WriteLine("7. Baja de materia");
                Console.WriteLine("8. Modificación de materia");
                Console.WriteLine("9. Anotar alumno a materia");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                esValido = int.TryParse(Console.ReadLine(), out Numero);
                if (esValido)
                {
                    if ((Numero < 0) || (Numero > 9))
                    {
                        Console.WriteLine("\nOpción inválida. Intente nuevamente: ");
                    }
                }
            } while (!esValido);
            return Numero;
        }
        static void Main(string[] args, int Legajo, int indiceMateria)
        {
            int numEj = 0;

            do
            {
                numEj = ValidarNumeroMenu();
                if (numEj == 1)
                {
                    AltaAlumno(Legajo);
                }
                else if (numEj == 2)
                {
                    BajaAlumno();
                }
                else if (numEj == 3)
                {
                    ModificarAlumno();
                }
                else if (numEj == 4)
                {
                    MostrarAlumnosActivos();
                }
                else if (numEj == 5)
                {
                    MostrarAlumnosInactivos();
                }
                else if (numEj == 6)
                {
                    AltaMateria(indiceMateria);
                }
                
                else if (numEj == 7)
                {
                    BajaMateria();
                }
               
                else if (numEj == 8)
                {
                    ModificarMateria();
                } /*
                else if (numEj == 9)
                {
                    AnotarAlumnoMateria();
                }*/
            } while (numEj != 0);
        }

    }
}
