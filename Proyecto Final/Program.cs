using System;
using System.Net;

namespace ProyectoFinal
{
    internal class Program
    {
        static List<Alumno> alumnos = new List<Alumno>();
        static List<Materia> materias = new List<Materia>();
        static List<AlumnoMateria> alumnosEnMaterias = new List<AlumnoMateria>();
        public static string archivo = "Lista de Alumnos.txt";
        public static string archivo2 = "Materias Cursadas.txt";
        public static string archivo3 = "Materias.txt";
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
            public string Nota;
            public DateTime Fecha;
        }
        static int LeerEntero(string Mensaje)
        {
            int Numero = 0;
            bool esEntero = false;
            string datoEntrada;
            Console.Write(Mensaje);
            while (!esEntero)
            {
                datoEntrada = Console.ReadLine();
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

        static bool ExisteAlumno(int dni)
        {
            bool alumnoExistente = false;
            int i = 0;
            while (!alumnoExistente && i < alumnos.Count)
            {
                if (int.Parse(alumnos[i].DNI) == dni)
                {
                    alumnoExistente = true;
                }
                i++;
            }
            return alumnoExistente;
        }

        static Alumno BuscarAlumno(int dni)
        {
            Alumno alumnoEncontrado = new Alumno();
            alumnoEncontrado.estaActivo = true;
            bool alumnoExistente = false;
            int i = 0;
            while (!alumnoExistente && i < alumnos.Count)
            {
                if (int.Parse(alumnos[i].DNI) == dni)
                {
                    alumnoExistente = true;
                    alumnoEncontrado.Legajo = alumnos[i].Legajo;
                    alumnoEncontrado.Apellido = alumnos[i].Apellido;
                    alumnoEncontrado.Nombre = alumnos[i].Nombre;
                    alumnoEncontrado.DNI = alumnos[i].DNI;
                    alumnoEncontrado.fechaNacimiento = alumnos[i].fechaNacimiento;
                    alumnoEncontrado.Domicilio = alumnos[i].Domicilio;
                    alumnoEncontrado.estaActivo = alumnos[i].estaActivo;
                }
                i++;
            }
            return alumnoEncontrado;
        }

        static bool ExisteMateria(string nombreDeMateria)
        {
            bool materiaExistente = false;
            int i = 0;
            while (!materiaExistente && i < materias.Count)
            {
                if (materias[i].nombreMateria == nombreDeMateria)
                {
                    materiaExistente = true;
                }
                i++;
            }
            return materiaExistente;
        }

        
        static Materia BuscarMateria(string nombreDeMateria)
        {
            Materia materiaEncontrada = new Materia();
            materiaEncontrada.estaActiva = true;
            bool materiaExistente = false;
            int i = 0;
            while (!materiaExistente && i < materias.Count)
            {
                if (materias[i].nombreMateria == nombreDeMateria)
                {
                    materiaExistente = true;
                    materiaEncontrada.indiceMateria = materias[i].indiceMateria;
                    materiaEncontrada.nombreMateria = materias[i].nombreMateria;
                    materiaEncontrada.estaActiva = materias[i].estaActiva;
                }
                i++;
            }
            return materiaEncontrada;
        }

        static bool ExisteAlumnoMateria(int dni, string nombreDeMateria)
        {
            bool alumnomMateriaExistente = false;
            int i = 0;
            while (!alumnomMateriaExistente && i < materias.Count)
            {
                if (int.Parse(alumnos[i].DNI) == dni && materias[i].nombreMateria == nombreDeMateria)
                {
                    alumnomMateriaExistente = true;
                }
                i++;
            }
            return alumnomMateriaExistente;
        }


        static void AltaAlumno()
        {
            int dni = LeerEntero("Ingrese el DNI para comenzar la operación: ");
            CargarAlumnosDesdeArchivo();


            if (!ExisteAlumno(dni))
            {

                Console.WriteLine("Ingrese el apellido del alumno: ");
                string apellido = Console.ReadLine();
                Console.WriteLine("Ingrese el nombre del alumno: ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese la fecha de nacimiento del alumno, colocando las barras. Ej = dd/mm/aaaa: ");
                DateTime fechaNacimiento = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Ingrese el domicilio del alumno: ");
                string Domicilio = Console.ReadLine();
                int nuevoIndice = alumnos.Count + 1;
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
                Alumno alumnoEncontrado = BuscarAlumno(dni);
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
                        for (int i = 0; i < alumnos.Count; i++)
                        {
                            Alumno alumno = alumnos[i];
                            alumno.estaActivo = true;
                            alumnos[i] = alumno;
                            GuardarAlumnosEnArchivo();
                        }
                        Console.WriteLine("Alumno reactivado exitosamente.\n --------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("El alumno no se ha reactivado.\n --------------------------------------------------------------");
                    }
                }
            }
            Console.WriteLine();
        }
        static void AltaMateria()
        {
            Console.Write("Ingrese el nombre de la materia que desea agregar: ");
            string nombreDeMateria = Console.ReadLine();
            CargarMateriaDesdeArchivo();
            if (!ExisteMateria(nombreDeMateria))
            {
                int nuevoIndice = materias.Count + 1;
                Materia nuevaMateria = new Materia
                {
                    indiceMateria = nuevoIndice,
                    nombreMateria = nombreDeMateria,
                    estaActiva = true,
                };
                materias.Add(nuevaMateria);
                GuardarMateriaEnArchivo();
                Console.WriteLine("Materia dada de alta.\n -------------------------------------------------------------------");
            }
            else
            {
                Materia materiaEncontrada = BuscarMateria(nombreDeMateria);
                if (materiaEncontrada.estaActiva)
                {
                    Console.WriteLine("ERROR: Ya existe una materia activa con el mismo nombre.\n ---------------------------------------------------");
                }
                else
                {
                    Console.Write("La materia ya existe pero esta inactiva. ¿Desea reactivarla? (S/N): ");
                    string respuesta = Console.ReadLine();
                    if ((respuesta == "S") || (respuesta == "s"))
                    {
                        for (int i = 0; i < materias.Count; i++)
                        {
                            Materia materia = materias[i];
                            materia.estaActiva = true;
                            materias[i] = materia;
                            GuardarMateriaEnArchivo();
                        }
                        Console.WriteLine("Materia reactivada exitosamente.\n --------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("La materia no se ha reactivado.\n --------------------------------------------------------------");
                    }
                }
            }
            Console.WriteLine();
        }



        public static List<Materia> RetornarListaMateria(string archivo2)
        {
            List<Materia> listaMateria = new();
            using (StreamReader sr = new StreamReader(archivo2))
            {
                string? linea = sr.ReadLine();

                while (linea != null)
                {
                    string[] MateriaArchivo = linea.Split(',');
                    Materia materiaStruct = new();
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
            using (StreamWriter writer = new(archivo, false))
            {
                foreach (var alumno in alumnos)
                {
                    writer.WriteLine($"{alumno.Legajo}, {alumno.Nombre}, {alumno.Apellido}, {alumno.DNI}, {alumno.fechaNacimiento:dd/MM/yyyy}, {alumno.Domicilio}, {alumno.estaActivo}");
                }
            }
        }
        static void GuardarAlumnoMateriaEnArchivo()
        {
            using (StreamWriter writer = new StreamWriter(archivo2, true))
            {
                foreach (var indice in alumnosEnMaterias)
                {
                    writer.WriteLine($"{indice.IndiceAlumno}, {indice.IndiceMateria}, {indice.IndiceAlumnoMateria}, {indice.Estado}, {indice.Nota}, {indice.Fecha}");
                }
            }
        }

        static void CargarAlumnosDesdeArchivo()
        {
            alumnos.Clear();
            if (File.Exists(archivo))
            {
                string[] lineas = File.ReadAllLines(archivo);
                foreach (var linea in lineas)
                {
                    string[] separador = linea.Split(',');
                    Alumno alumno = new()
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

        static void CargarMateriaDesdeArchivo()
        {
            materias.Clear();
            if (File.Exists(archivo2))
            {
                string[] lineas = File.ReadAllLines(archivo2);
                foreach (var linea in lineas)
                {
                    string[] separador = linea.Split(',');
                    Materia materia = new()
                    {
                        indiceMateria = int.Parse(separador[0]),
                        nombreMateria = separador[1],
                        estaActiva = bool.Parse(separador[2])
                    };
                    materias.Add(materia);
                }
            }
        }

        static void BajaAlumno()
        {
            CargarAlumnosDesdeArchivo();
            int dni = LeerEntero("Ingrese el DNI del alumno que desea dar de baja: ");

            if (!ExisteAlumno(dni))
            {
                Console.WriteLine("ERROR: No existe alumno con ese dni.\n------------------------------------------------------------------------------------");
            }
            else
            {
                Alumno alumoEncontrado = BuscarAlumno(dni);
                Console.Write($"El alumno que desea modificar es{alumoEncontrado.Nombre}{alumoEncontrado.Apellido}? S/N: ");
                string respuesta = Console.ReadLine();
                if (respuesta == "s" || respuesta == "S")
                {
                    for (int i = 0; i < alumnos.Count; i++)
                    {
                        if (int.Parse(alumnos[i].DNI) == (dni))
                        {
                            Alumno alumno = alumnos[i];
                            alumno.estaActivo = false;
                            alumnos[i] = alumno;
                            Console.WriteLine("Alumno dado de baja correctamente.\n----------------------------------------------------------------------------");

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Se ha cancelado la operación.\n------------------------------------------------------------------------------");
                }
                GuardarAlumnosEnArchivo();
            }
            Console.WriteLine();
        }

        static void BajaMateria()
        {
            Console.Write("Ingrese el nombre de la materia que quiere dar de baja");
            string nombreDeMateria = Console.ReadLine();
            CargarMateriaDesdeArchivo();
            if (!ExisteMateria(nombreDeMateria))
            {
                Console.WriteLine("ERROR: No existe una materia con ese nombre.\n------------------------------------------------------------------------------------");
            }
            else
            {
                Materia materiaEncontrada = BuscarMateria(nombreDeMateria);
                Console.Write($"La materia que desea modificar es{materiaEncontrada.nombreMateria}? S/N: ");
                string respuesta = Console.ReadLine();
                if (respuesta == "s" || respuesta == "S")
                {
                    for (int i = 0; i < materias.Count; i++)
                    {
                        if (materias[i].nombreMateria == nombreDeMateria)
                        {
                            Materia materia = materias[i];
                            materia.estaActiva = false;
                            materias[i] = materia;
                            Console.WriteLine("Materia dada de baja correctamente.\n----------------------------------------------------------------------------");

                        }
                    }
                }
                else
                {
                    Console.WriteLine("Se ha cancelado la operación.\n------------------------------------------------------------------------------");
                }
                GuardarMateriaEnArchivo();
            }
            Console.WriteLine();
        }


        static void ModificarAlumno()  //Como modificar un alumno, como transcribirlo
        {
            int numerodni = LeerEntero("Ingrese el numero de dni del alumno que desea modificar: ");

            CargarAlumnosDesdeArchivo();
            Alumno alumnos2 = BuscarAlumno(numerodni);
            if (ExisteAlumno(numerodni))
            {
                Console.Write($"El alumno a modificar es {alumnos2.Apellido} {alumnos2.Nombre}? S/N: ");
                string respuesta = Console.ReadLine();
                if ((respuesta == "s") || (respuesta == "S"))
                {
                    /*alumnos2.Legajo = BuscarAlumno(numerodni).Legajo;*/
                    Console.Write("Reingrese el nombre del alumno: ");
                    alumnos2.Nombre = Console.ReadLine();
                    Console.Write("Reingrese el apellido: ");
                    alumnos2.Apellido = Console.ReadLine();
                    alumnos2.DNI = Convert.ToString(LeerEntero("Reingrese el dni del alumno: "));
                    Console.Write("Reingrese el domicilio: ");
                    alumnos2.Domicilio = Console.ReadLine();
                    Console.Write("Reingrese la fecha de nacimiento (Recuerde ingresar las barras. Ej: 00/00/0000): ");
                    alumnos2.fechaNacimiento = Convert.ToDateTime(Console.ReadLine());
                    alumnos.Remove(BuscarAlumno(numerodni));
                    alumnos.Add(alumnos2);
                    GuardarAlumnosEnArchivo();
                    Console.WriteLine($"Alumno modificado exitosamente.\n----------------------------------------------------------------");
                }
                else
                {
                    alumnos.Add(alumnos2);
                    GuardarAlumnosEnArchivo();
                    Console.WriteLine($"El alumno no se ha modificado.\n----------------------------------------------------------------------------------");
                }

            }
            Console.WriteLine();
        }
    
        


        static void ModificarMateria()
        {
            Console.WriteLine("Ingrese el nombre de la materia que desea modificar: ");
            string nombreMateria = Console.ReadLine();
            CargarMateriaDesdeArchivo();
            Materia materia2 = BuscarMateria(nombreMateria);
            if (ExisteMateria(nombreMateria))
            {
                Console.Write($"La materia a modificar es {materia2.nombreMateria}? S/N: ");
                string respuesta = Console.ReadLine();
                if ((respuesta == "s") || (respuesta == "S"))
                {
                    Console.Write("Reingrese el nombre de la materia: ");
                    materia2.nombreMateria = Console.ReadLine();
                    materias.Remove(BuscarMateria(nombreMateria));
                    materias.Add(materia2);
                    GuardarMateriaEnArchivo();
                    Console.WriteLine($"Materia modificada exitosamente.\n----------------------------------------------------------------");
                }
                else
                {
                    materias.Add(materia2);
                    GuardarMateriaEnArchivo();
                    Console.WriteLine($"La materia no se ha modificado.\n----------------------------------------------------------------------------------");
                }
            }
            Console.WriteLine();
        }


        /*public static List<Alumno> RetornarListaAlumnos(string archivo)
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
        }*/

        public static void MostrarAlumnosActivos()
        {
            CargarAlumnosDesdeArchivo();
            Console.WriteLine("Los alumnos activos actualmente son: ");
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (alumnos[i].estaActivo)
                {
                    Console.WriteLine(alumnos[i].Legajo + ", " + alumnos[i].Nombre + ", " + alumnos[i].Apellido);
                }
            }
            Console.WriteLine($"--------------------------------------------------------------------------------------\n");
        }


        public static void MostrarAlumnosInactivos()
        {
            CargarAlumnosDesdeArchivo();
            Console.WriteLine("Los alumnos activos actualmente son: ");
            for (int i = 0; i < alumnos.Count; i++)
            {
                if (alumnos[i].estaActivo == false)
                {
                    Console.WriteLine(alumnos[i].Legajo + ", " + alumnos[i].Nombre + ", " + alumnos[i].Apellido);
                }
            }
            Console.WriteLine($"--------------------------------------------------------------------------------------\n");

        }


        static void CargarAlumnosEnMaterias()
        {
            alumnosEnMaterias.Clear();
            if (File.Exists(archivo3))
            {
                string[] lineas = File.ReadAllLines(archivo3);
                foreach (var linea in lineas)
                {
                    string[] separador = linea.Split(',');
                    AlumnoMateria materiaAlumno = new()
                    {
                        IndiceAlumnoMateria = Convert.ToInt32(separador[0]),
                        IndiceAlumno = Convert.ToInt32(separador[1]),
                        IndiceMateria = Convert.ToInt32(separador[2]),
                        Estado = separador[3],
                        Nota = separador[4],
                        Fecha = Convert.ToDateTime(separador[5]),
                        
                    };
                    alumnosEnMaterias.Add(materiaAlumno);
                }
            }
        }

        static void AnotarAlumnoMateria()
        {
            string nombreMateria, estadoMateria, respuesta, respuesta2;
            int nota = 0;
            DateTime fechaExamen;
            int dni = LeerEntero("ingrese el dni del alumno que desea anotar en una materia: ");
            CargarAlumnosDesdeArchivo();
            CargarMateriaDesdeArchivo();
            CargarAlumnosEnMaterias();
            while (!ExisteAlumno(dni))
            {
                Console.Write("ERROR: No existe un alumno inscripto con ese DNI. Intente de nuevo: ");
            }
            if (BuscarAlumno(dni).estaActivo == false)
            {
                Console.WriteLine("El alumno que desea anotar esta inactivo. Pr favor, activelo e intente nuevamente.\n -------------------------------------------------------------------------\n");
            }

            Console.Write("Ingrese el nombre de la materia en la que desea anotar al alumno: ");
            nombreMateria = Console.ReadLine();
            while (!ExisteMateria(nombreMateria))
            {
                Console.WriteLine("No existe una materia registrada con ese nombre. Por favor, intente de nuevo: ");
            }
            if (BuscarMateria(nombreMateria).estaActiva == false)
            {
                Console.WriteLine("La materia que ingresó está inactiva. Por favor, activela e intente nuevamente.\n-------------------------------------------------------------------------------\n");
            }
            
            for (int i = 0; i < alumnosEnMaterias.Count; i++)
            {
                if (BuscarAlumno(dni).Legajo == alumnosEnMaterias[i].IndiceAlumno && BuscarMateria(nombreMateria).indiceMateria == alumnosEnMaterias[i].IndiceMateria)
                {
                    Console.WriteLine($"El alumno ya esta inscripto en la materia. Que accion desea realizar?");
                    Console.WriteLine("1) Modificar.");
                    Console.WriteLine("2) Eliminar.");
                    Console.WriteLine("0) Cancelar.");
                    int leerRespuesta = LeerEntero("Elija una opción: ");
                    if (leerRespuesta >= 0 && leerRespuesta <= 2)
                    {
                        if (leerRespuesta == 1)
                        {
                            AlumnoMateria alumno2 = alumnosEnMaterias[i];
                            Console.Write("Reingrese el estado del alumno (Aprobado, desaprobado, anotado, cursado): ");
                            alumno2.Estado = Console.ReadLine();
                            Console.Write("Reingrese la nota del alumno (Si no rindió examen final, ingrese un guion [-]): ");
                            alumno2.Nota = Console.ReadLine();
                            Console.Write("Reingrese la fecha de examen, recuerde usar las barras. Ej: dd/mm/aaaa. (Si no rindió examen final, ingrese [00/00/0000]): ");
                            alumno2.Fecha = Convert.ToDateTime(Console.ReadLine());
                            alumnosEnMaterias.Add(alumno2);
                            alumnosEnMaterias.Remove(alumnosEnMaterias[i]);
                            GuardarAlumnoMateriaEnArchivo();
                        }
                    }

                }

            }


            Console.Write("Cual es el estado del alumno en cuanto a la materia? (Anotado, Cursado, Aprobado, Desaprobado): ");
            estadoMateria = Console.ReadLine();
            Console.Write("El alumno rindió examen final? S/N: ");
            respuesta2 = Console.ReadLine();
            if (respuesta2 == "s" || respuesta2 == "S")
            {
                nota = LeerEntero("Ingrese la nota del examen: ");
                Console.Write("Ingrese la fecha en la que rindió el examen (Recuerde usar las barras. (Ej: 00/00/0000): ");
                fechaExamen = Convert.ToDateTime(Console.ReadLine());
                int nuevoIndice = alumnosEnMaterias.Count + 1;
                AlumnoMateria nuevoAlumno = new()
                {
                    IndiceAlumno = BuscarAlumno(dni).Legajo,
                    IndiceMateria = BuscarMateria(nombreMateria).indiceMateria,
                    IndiceAlumnoMateria = nuevoIndice,
                    Estado = estadoMateria,
                    Nota = Convert.ToString(nota),
                    Fecha = fechaExamen,
                };
                alumnosEnMaterias.Add(nuevoAlumno);
                Console.WriteLine();
                GuardarAlumnoMateriaEnArchivo();
                Console.WriteLine("Alumno anotado correctamente en la materia.\n---------------------------------------------------------------------------------------");
            }
            else
            {
                DateTime fecha = Convert.ToDateTime("00/00/0000");
                string sinNota = "-";
                int nuevoIndice = alumnosEnMaterias.Count + 1;
                AlumnoMateria nuevoAlumno = new()
                {
                    IndiceAlumno = BuscarAlumno(dni).Legajo,
                    IndiceMateria = BuscarMateria(nombreMateria).indiceMateria,
                    IndiceAlumnoMateria = nuevoIndice,
                    Estado = estadoMateria,
                    Nota = sinNota,
                    Fecha = fecha,
                };
                alumnosEnMaterias.Add(nuevoAlumno);
                Console.WriteLine();
                GuardarAlumnoMateriaEnArchivo();
                Console.WriteLine("Alumno anotado correctamente en la materia.\n---------------------------------------------------------------------------------------");
            }
            Console.WriteLine();
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
        static void Main(string[] args)
        {
            int numEj = 0;
            CargarAlumnosDesdeArchivo();
            do
            {
                numEj = ValidarNumeroMenu();
                if (numEj == 1)
                {
                    AltaAlumno();
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
                    AltaMateria();
                }
                
                else if (numEj == 7)
                {
                    BajaMateria();
                }
               
                else if (numEj == 8)
                {
                    ModificarMateria();
                } 
                else if (numEj == 9)
                {
                    AnotarAlumnoMateria();
                }
            } while (numEj != 0);
        }

    }
}
