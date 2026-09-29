using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        const int MAX = 100;
        const int MAX_ATN = 300;

        string[] pacId = new string[MAX];
        string[] pacNombre = new string[MAX];
        int[] pacEdad = new int[MAX];
        string[] pacSexo = new string[MAX];
        string[] pacTelefono = new string[MAX];
        int cantPacientes = 0;

        string[] medCodigo = new string[MAX];
        string[] medNombre = new string[MAX];
        string[] medEspecialidad = new string[MAX];
        string[] medEstado = new string[MAX];
        int[] medAtendidos = new int[MAX];
        int cantMedicos = 0;

        string[] citaIdPaciente = new string[MAX];
        string[] citaCodMedico = new string[MAX];
        string[] citaFecha = new string[MAX];
        string[] citaHora = new string[MAX];
        string[] citaMotivo = new string[MAX];
        string[] citaEstado = new string[MAX];
        int cantCitas = 0;

        string[] emgIdPaciente = new string[MAX];
        int[] emgPrioridad = new int[MAX];
        int[] emgTiempoEspera = new int[MAX];
        string[] emgCodMedico = new string[MAX];
        string[] emgEstado = new string[MAX];
        int cantEmergencias = 0;

        string[] atnIdPaciente = new string[MAX_ATN];
        string[] atnCodMedico = new string[MAX_ATN];
        string[] atnDiagnostico = new string[MAX_ATN];
        string[] atnTratamiento = new string[MAX_ATN];
        string[] atnMedicamentos = new string[MAX_ATN];
        string[] atnFecha = new string[MAX_ATN];
        string[] atnEstadoPaciente = new string[MAX_ATN];
        int cantAtenciones = 0;

        int opcionPrincipal;
        do
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║        SISTEMA HOSPITALARIO          ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.WriteLine("║ 1. Pacientes                         ║");
            Console.WriteLine("║ 2. Medicos                           ║");
            Console.WriteLine("║ 3. Citas                             ║");
            Console.WriteLine("║ 4. Emergencias                       ║");
            Console.WriteLine("║ 5. Registrar atencion                ║");
            Console.WriteLine("║ 6. Historial medico                  ║");
            Console.WriteLine("║ 7. Estadisticas                      ║");
            Console.WriteLine("║ 8. Reportes                          ║");
            Console.WriteLine("║ 9. Salir                             ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            opcionPrincipal = LeerEnteroEnRango("Seleccione una opcion: ", 1, 9);

            switch (opcionPrincipal)
            {
                case 1:
                    MenuPacientes(pacId, pacNombre, pacEdad, pacSexo, pacTelefono, ref cantPacientes, MAX);
                    break;
                case 2:
                    MenuMedicos(medCodigo, medNombre, medEspecialidad, medEstado, medAtendidos, ref cantMedicos, MAX);
                    break;
                case 3:
                    MenuCitas(citaIdPaciente, citaCodMedico, citaFecha, citaHora, citaMotivo, citaEstado, ref cantCitas, pacId, cantPacientes, medCodigo, medEspecialidad, medEstado, cantMedicos, MAX);
                    break;
                case 4:
                    MenuEmergencias(emgIdPaciente, emgPrioridad, emgTiempoEspera, emgCodMedico, emgEstado, ref cantEmergencias, pacId, cantPacientes, medCodigo, medEspecialidad, medEstado, cantMedicos, MAX);
                    break;
                case 5:
                    MenuAtenciones(citaIdPaciente, citaCodMedico, citaFecha, citaHora, citaEstado, cantCitas,
                                   emgIdPaciente, emgCodMedico, emgEstado, cantEmergencias,
                                   pacId, pacNombre, cantPacientes,
                                   medCodigo, medNombre, medEstado, medAtendidos, cantMedicos,
                                   atnIdPaciente, atnCodMedico, atnDiagnostico, atnTratamiento, atnMedicamentos, atnFecha, atnEstadoPaciente, ref cantAtenciones, MAX_ATN);
                    break;
                case 6:
                    MenuHistorial(atnIdPaciente, atnCodMedico, atnDiagnostico, atnTratamiento, atnMedicamentos, atnFecha, atnEstadoPaciente, cantAtenciones,
                                  pacId, pacNombre, cantPacientes);
                    break;
                case 7:
                    MenuEstadisticas(pacId, pacEdad, cantPacientes,
                                     medCodigo, medNombre, medEspecialidad, medAtendidos, cantMedicos,
                                     emgTiempoEspera, cantEmergencias,
                                     atnIdPaciente, atnDiagnostico, atnFecha, cantAtenciones);
                    break;
                case 8:
                    MenuReportes(citaIdPaciente, citaCodMedico, citaFecha, citaHora, citaEstado, cantCitas,
                                 emgIdPaciente, emgPrioridad, emgTiempoEspera, emgCodMedico, emgEstado, cantEmergencias,
                                 pacId, pacNombre, cantPacientes,
                                 medCodigo, medNombre, medEspecialidad, medEstado, cantMedicos, cantAtenciones);
                    break;
                case 9:
                    Console.WriteLine("\nSaliendo del sistema...");
                    break;
            }
        } while (opcionPrincipal != 9);
    }

    static string LeerTextoNoVacio(string mensaje)
    {
        string entrada;
        do
        {
            Console.Write(mensaje);
            entrada = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(entrada))
            {
                Console.WriteLine(" Error: El campo no puede estar vacio.");
            }
        } while (string.IsNullOrEmpty(entrada));
        return entrada;
    }

    static int LeerEnteroEnRango(string mensaje, int min, int max)
    {
        int numero;
        bool valido;
        do
        {
            Console.Write(mensaje);
            valido = int.TryParse(Console.ReadLine(), out numero) && numero >= min && numero <= max;
            if (!valido)
            {
                Console.WriteLine($" Error: Ingrese un numero valido entre {min} y {max}.");
            }
        } while (!valido);
        return numero;
    }

    static string LeerFecha(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string s = Console.ReadLine()?.Trim();
            if (DateTime.TryParseExact(s, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d))
                return d.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine(" Error: Use el formato DD/MM/AAAA (ej: 05/03/2026).");
        }
    }

    static string LeerHora(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string s = Console.ReadLine()?.Trim();
            if (DateTime.TryParseExact(s, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime h))
                return h.ToString("HH:mm", CultureInfo.InvariantCulture);
            Console.WriteLine(" Error: Use el formato HH:MM en 24 horas (ej: 14:30).");
        }
    }

    static string FechaHoy()
    {
        return DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    }

    static void Pausa()
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    static int BuscarIndiceCadena(string[] arreglo, int limite, string busqueda)
    {
        for (int i = 0; i < limite; i++)
        {
            if (arreglo[i].Equals(busqueda, StringComparison.OrdinalIgnoreCase))
                return i;
        }
        return -1;
    }

    static int BuscarPacienteRecursivo(string[] pacId, int indiceActual, int totalPacientes, string idBuscado)
    {
        if (indiceActual >= totalPacientes) return -1;
        if (pacId[indiceActual].Equals(idBuscado, StringComparison.OrdinalIgnoreCase)) return indiceActual;

        return BuscarPacienteRecursivo(pacId, indiceActual + 1, totalPacientes, idBuscado);
    }

    static string NombrePaciente(string[] pId, string[] pNom, int cantPac, string id)
    {
        int idx = BuscarPacienteRecursivo(pId, 0, cantPac, id);
        return idx == -1 ? "(paciente no encontrado)" : pNom[idx];
    }

    static void MenuPacientes(string[] pacId, string[] pacNombre, int[] pacEdad, string[] pacSexo, string[] pacTelefono, ref int cantPacientes, int max)
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("1. Registrar paciente");
            Console.WriteLine("2. Buscar paciente (Recursivo)");
            Console.WriteLine("3. Actualizar informacion de paciente");
            Console.WriteLine("4. Regresar al menu principal");
            opcion = LeerEnteroEnRango("Seleccione una opcion: ", 1, 4);
            switch (opcion)
            {
                case 1:
                    RegistrarPaciente(pacId, pacNombre, pacEdad, pacSexo, pacTelefono, ref cantPacientes, max);
                    break;
                case 2:
                    ConsultarPacienteUI(pacId, pacNombre, pacEdad, pacSexo, pacTelefono, cantPacientes);
                    break;
                case 3:
                    ActualizarPaciente(pacId, pacNombre, pacEdad, pacSexo, pacTelefono, cantPacientes);
                    break;
            }
            if (opcion != 4) Pausa();
        } while (opcion != 4);
    }

    static void RegistrarPaciente(string[] id, string[] nombre, int[] edad, string[] sexo, string[] tel, ref int cantidad, int max)
    {
        Console.WriteLine("\nRegistrar Paciente");
        if (cantidad >= max)
        {
            Console.WriteLine(" Error: Se alcanzo el limite de pacientes registrables.");
            return;
        }

        string nuevoId = LeerTextoNoVacio("Ingrese Cedula/ID: ");
        if (BuscarPacienteRecursivo(id, 0, cantidad, nuevoId) != -1)
        {
            Console.WriteLine(" Error: Ya existe un paciente con ese ID.");
            return;
        }

        id[cantidad] = nuevoId;
        nombre[cantidad] = LeerTextoNoVacio("Ingrese Nombre Completo: ");
        edad[cantidad] = LeerEnteroEnRango("Ingrese Edad: ", 0, 120);
        sexo[cantidad] = LeerTextoNoVacio("Ingrese Sexo (M/F): ");
        tel[cantidad] = LeerTextoNoVacio("Ingrese Telefono: ");

        cantidad++;
        Console.WriteLine(" Paciente registrado exitosamente.");
    }

    static void ConsultarPacienteUI(string[] id, string[] nombre, int[] edad, string[] sexo, string[] tel, int cantidad)
    {
        Console.WriteLine("\nBuscar Paciente ");
        string idBuscado = LeerTextoNoVacio("Ingrese ID a buscar: ");
        int idx = BuscarPacienteRecursivo(id, 0, cantidad, idBuscado);

        if (idx != -1)
        {
            Console.WriteLine($"\n[ID: {id[idx]}] Nombre: {nombre[idx]} | Edad: {edad[idx]} | Sexo: {sexo[idx]} | Tel: {tel[idx]}");
        }
        else
        {
            Console.WriteLine(" Paciente no encontrado.");
        }
    }

    static void ActualizarPaciente(string[] id, string[] nombre, int[] edad, string[] sexo, string[] tel, int cantidad)
    {
        Console.WriteLine("\n--- Actualizar Paciente ---");
        string idBuscado = LeerTextoNoVacio("Ingrese ID del paciente a actualizar: ");
        int idx = BuscarPacienteRecursivo(id, 0, cantidad, idBuscado);

        if (idx != -1)
        {
            Console.WriteLine($"Actualizando a: {nombre[idx]}");
            nombre[idx] = LeerTextoNoVacio("Nuevo Nombre: ");
            edad[idx] = LeerEnteroEnRango("Nueva Edad: ", 0, 120);
            tel[idx] = LeerTextoNoVacio("Nuevo Telefono: ");
            Console.WriteLine(" Informacion actualizada correctamente.");
        }
        else
        {
            Console.WriteLine(" Paciente no encontrado.");
        }
    }

    static void MenuMedicos(string[] medCod, string[] medNom, string[] medEsp, string[] medEst, int[] medAtend, ref int cantMed, int max)
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine(" SUBMENU MEDICOS");
            Console.WriteLine("1. Registrar medico");
            Console.WriteLine("2. Consultar medicos por especialidad");
            Console.WriteLine("3. Cambiar disponibilidad de medico");
            Console.WriteLine("4. Regresar al menu principal");
            opcion = LeerEnteroEnRango("Seleccione una opcion: ", 1, 4);

            switch (opcion)
            {
                case 1:
                    RegistrarMedico(medCod, medNom, medEsp, medEst, medAtend, ref cantMed, max);
                    break;
                case 2:
                    ConsultarPorEspecialidad(medCod, medNom, medEsp, medEst, medAtend, cantMed);
                    break;
                case 3:
                    CambiarEstadoMedico(medCod, medNom, medEst, cantMed);
                    break;
            }
            if (opcion != 4) Pausa();
        } while (opcion != 4);
    }

    static void RegistrarMedico(string[] cod, string[] nom, string[] esp, string[] est, int[] atend, ref int cant, int max)
    {
        Console.WriteLine("\nRegistrar Medico");
        if (cant >= max)
        {
            Console.WriteLine(" Error: Se alcanzo el limite de medicos registrables.");
            return;
        }

        string nuevoCod = LeerTextoNoVacio("Codigo de medico: ");
        if (BuscarIndiceCadena(cod, cant, nuevoCod) != -1)
        {
            Console.WriteLine(" Error: El codigo ingresado ya existe.");
            return;
        }

        cod[cant] = nuevoCod;
        nom[cant] = LeerTextoNoVacio("Nombre Completo: ");
        esp[cant] = LeerTextoNoVacio("Especialidad (e.g., General, Pediatria, Urgencias): ");
        est[cant] = "Disponible";
        atend[cant] = 0;

        cant++;
        Console.WriteLine(" Medico registrado con exito (Estado por defecto: Disponible).");
    }

    static void ConsultarPorEspecialidad(string[] cod, string[] nom, string[] esp, string[] est, int[] atend, int cant)
    {
        string especialidad = LeerTextoNoVacio("\nEspecialidad a buscar: ");
        Console.WriteLine($"\nMedicos en la especialidad '{especialidad}':");
        bool encontrado = false;

        for (int i = 0; i < cant; i++)
        {
            if (esp[i].Equals(especialidad, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"- Cod: {cod[i]} | Dr(a). {nom[i]} | Estado: {est[i]} | Pacientes Atendidos: {atend[i]}");
                encontrado = true;
            }
        }
        if (!encontrado) Console.WriteLine("No se encontraron medicos en esa especialidad.");
    }

    static void CambiarEstadoMedico(string[] cod, string[] nom, string[] est, int cant)
    {
        string c = LeerTextoNoVacio("\nCodigo del medico: ");
        int idx = BuscarIndiceCadena(cod, cant, c);
        if (idx != -1)
        {
            Console.WriteLine($"Estado actual de {nom[idx]}: {est[idx]}");
            Console.WriteLine("1. Disponible\n2. Ocupado\n3. Fuera de Turno");
            int sel = LeerEnteroEnRango("Seleccione nuevo estado: ", 1, 3);
            est[idx] = sel switch { 1 => "Disponible", 2 => "Ocupado", _ => "Fuera de Turno" };
            Console.WriteLine(" Estado modificado correctamente.");
        }
        else Console.WriteLine(" Medico no encontrado.");
    }

    static void MenuCitas(string[] cPac, string[] cMed, string[] cFec, string[] cHor, string[] cMot, string[] cEst, ref int cantCitas, string[] pId, int cantPac, string[] mCod, string[] mEsp, string[] mEst, int cantMed, int max)
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("            SUBMENU CITAS               ");
            Console.WriteLine("1. Registrar cita");
            Console.WriteLine("2. Cancelar cita");
            Console.WriteLine("3. Consultar citas de un paciente");
            Console.WriteLine("4. Regresar al menu principal");
            opcion = LeerEnteroEnRango("Seleccione una opcion: ", 1, 4);

            switch (opcion)
            {
                case 1:
                    RegistrarCita(cPac, cMed, cFec, cHor, cMot, cEst, ref cantCitas, pId, cantPac, mCod, mEsp, mEst, cantMed, max);
                    break;
                case 2:
                    CancelarCita(cPac, cFec, cEst, cantCitas);
                    break;
                case 3:
                    ConsultarCitasPaciente(cPac, cMed, cFec, cHor, cEst, cantCitas);
                    break;
            }
            if (opcion != 4) Pausa();
        } while (opcion != 4);
    }

    static void RegistrarCita(string[] cPac, string[] cMed, string[] cFec, string[] cHor, string[] cMot, string[] cEst, ref int cantCitas, string[] pId, int cantPac, string[] mCod, string[] mEsp, string[] mEst, int cantMed, int max)
    {
        Console.WriteLine("\n--- Registrar Cita ---");
        if (cantCitas >= max)
        {
            Console.WriteLine(" Error: Se alcanzo el limite de citas registrables.");
            return;
        }

        string idP = LeerTextoNoVacio("ID del paciente: ");
        if (BuscarPacienteRecursivo(pId, 0, cantPac, idP) == -1)
        {
            Console.WriteLine(" Error: El paciente no existe.");
            return;
        }

        string espBuscada = LeerTextoNoVacio("Especialidad requerida: ");
        int idxMed = -1;
        for (int i = 0; i < cantMed; i++)
        {
            if (mEsp[i].Equals(espBuscada, StringComparison.OrdinalIgnoreCase) && mEst[i].Equals("Disponible", StringComparison.OrdinalIgnoreCase))
            {
                idxMed = i;
                break;
            }
        }

        if (idxMed == -1)
        {
            Console.WriteLine(" No hay medicos disponibles con esa especialidad en este momento.");
            return;
        }

        cPac[cantCitas] = idP;
        cMed[cantCitas] = mCod[idxMed];
        cFec[cantCitas] = LeerFecha("Fecha (DD/MM/AAAA): ");
        cHor[cantCitas] = LeerHora("Hora (HH:MM): ");
        cMot[cantCitas] = LeerTextoNoVacio("Motivo de consulta: ");
        cEst[cantCitas] = "Pendiente";

        cantCitas++;
        Console.WriteLine($" Cita registrada correctamente. Medico asignado: {mCod[idxMed]}");
    }

    static void CancelarCita(string[] cPac, string[] cFec, string[] cEst, int cantCitas)
    {
        string idP = LeerTextoNoVacio("\nID del paciente: ");
        string fec = LeerFecha("Fecha de la cita (DD/MM/AAAA): ");
        bool hallado = false;

        for (int i = 0; i < cantCitas; i++)
        {
            bool activa = cEst[i].Equals("Pendiente") || cEst[i].Equals("Confirmada");
            if (cPac[i].Equals(idP, StringComparison.OrdinalIgnoreCase) && cFec[i].Equals(fec) && activa)
            {
                cEst[i] = "Cancelada";
                Console.WriteLine(" Cita cancelada con exito.");
                hallado = true;
                break;
            }
        }
        if (!hallado) Console.WriteLine(" No se encontro una cita activa para esa fecha e ID.");
    }

    static void ConsultarCitasPaciente(string[] cPac, string[] cMed, string[] cFec, string[] cHor, string[] cEst, int cantCitas)
    {
        string idP = LeerTextoNoVacio("\nID del paciente: ");
        Console.WriteLine($"\nCitas agendadas para {idP}:");
        bool enc = false;

        for (int i = 0; i < cantCitas; i++)
        {
            if (cPac[i].Equals(idP, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"- Fecha: {cFec[i]} | Hora: {cHor[i]} | Cod. Medico: {cMed[i]} | Estado: {cEst[i]}");
                enc = true;
            }
        }
        if (!enc) Console.WriteLine("No registra citas.");
    }

    static void InsertarPorPrioridadRecursivo(string[] ePac, int[] ePrio, int[] eTiem, string[] eMed, string[] eEst, int idx)
    {
        if (idx <= 0) return;

        if (ePrio[idx] < ePrio[idx - 1])
        {
            string tempP = ePac[idx]; ePac[idx] = ePac[idx - 1]; ePac[idx - 1] = tempP;
            int tempPr = ePrio[idx]; ePrio[idx] = ePrio[idx - 1]; ePrio[idx - 1] = tempPr;
            int tempT = eTiem[idx]; eTiem[idx] = eTiem[idx - 1]; eTiem[idx - 1] = tempT;
            string tempM = eMed[idx]; eMed[idx] = eMed[idx - 1]; eMed[idx - 1] = tempM;
            string tempE = eEst[idx]; eEst[idx] = eEst[idx - 1]; eEst[idx - 1] = tempE;

            InsertarPorPrioridadRecursivo(ePac, ePrio, eTiem, eMed, eEst, idx - 1);
        }
    }

    static void MenuEmergencias(string[] ePac, int[] ePrio, int[] eTiem, string[] eMed, string[] eEst, ref int cantEmg, string[] pId, int cantPac, string[] mCod, string[] mEsp, string[] mEst, int cantMed, int max)
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("SUBMENU EMERGENCIAS");
            Console.WriteLine("1. Ingresar paciente por emergencia");
            Console.WriteLine("2. Ver cola de atencion ordenada");
            Console.WriteLine("3. Regresar al menu principal");
            opcion = LeerEnteroEnRango("Seleccione una opcion: ", 1, 3);

            switch (opcion)
            {
                case 1:
                    IngresarEmergencia(ePac, ePrio, eTiem, eMed, eEst, ref cantEmg, pId, cantPac, mCod, mEsp, mEst, cantMed, max);
                    break;
                case 2:
                    VerColaEmergencias(ePac, ePrio, eTiem, eMed, eEst, cantEmg);
                    break;
            }
            if (opcion != 3) Pausa();
        } while (opcion != 3);
    }

    static void IngresarEmergencia(string[] ePac, int[] ePrio, int[] eTiem, string[] eMed, string[] eEst, ref int cantEmg, string[] pId, int cantPac, string[] mCod, string[] mEsp, string[] mEst, int cantMed, int max)
    {
        Console.WriteLine("\nRegistro de Emergencia");
        if (cantEmg >= max)
        {
            Console.WriteLine(" Error: La cola de emergencias esta llena.");
            return;
        }

        string idP = LeerTextoNoVacio("ID del paciente: ");
        if (BuscarPacienteRecursivo(pId, 0, cantPac, idP) == -1)
        {
            Console.WriteLine(" Error: El paciente no se encuentra registrado.");
            return;
        }

        Console.WriteLine("Niveles de Prioridad:");
        Console.WriteLine("1. Critico (Riesgo inminente)");
        Console.WriteLine("2. Urgente");
        Console.WriteLine("3. Moderado");
        Console.WriteLine("4. Leve");
        int prio = LeerEnteroEnRango("Seleccione nivel (1-4): ", 1, 4);
        int tEspera = LeerEnteroEnRango("Tiempo de espera estimado (minutos): ", 0, 600);

        string espReq = LeerTextoNoVacio("Especialidad requerida: ");
        string medAsignado = "Sin Asignar";
        for (int i = 0; i < cantMed; i++)
        {
            if (mEst[i].Equals("Disponible", StringComparison.OrdinalIgnoreCase) &&
                mEsp[i].Equals(espReq, StringComparison.OrdinalIgnoreCase))
            {
                medAsignado = mCod[i];
                mEst[i] = "Ocupado";
                break;
            }
        }
        if (medAsignado == "Sin Asignar")
            Console.WriteLine(" Aviso: no hay medico disponible de esa especialidad; se asignara al momento de atender.");

        ePac[cantEmg] = idP;
        ePrio[cantEmg] = prio;
        eTiem[cantEmg] = tEspera;
        eMed[cantEmg] = medAsignado;
        eEst[cantEmg] = "En Espera";

        InsertarPorPrioridadRecursivo(ePac, ePrio, eTiem, eMed, eEst, cantEmg);

        cantEmg++;
        Console.WriteLine(" Emergencia registrada y clasificada en la cola de atencion con exito.");
    }

    static void VerColaEmergencias(string[] ePac, int[] ePrio, int[] eTiem, string[] eMed, string[] eEst, int cantEmg)
    {
        Console.WriteLine("\nCola de Atenciones de Emergencia");
        if (cantEmg == 0)
        {
            Console.WriteLine("No hay emergencias registradas.");
            return;
        }

        for (int i = 0; i < cantEmg; i++)
        {
            Console.WriteLine($"Puesto {i + 1} | Paciente: {ePac[i]} | Prioridad: {ePrio[i]} | Espera: {eTiem[i]} min | Medico: {eMed[i]} | Estado: {eEst[i]}");
        }
    }

    static void MenuAtenciones(string[] cPac, string[] cMed, string[] cFec, string[] cHor, string[] cEst, int cantCitas,
                               string[] ePac, string[] eMed, string[] eEst, int cantEmg,
                               string[] pId, string[] pNom, int cantPac,
                               string[] mCod, string[] mNom, string[] mEst, int[] mAtend, int cantMed,
                               string[] aPac, string[] aMed, string[] aDiag, string[] aTrat, string[] aMeds, string[] aFec, string[] aEstPac, ref int cantAtn, int maxAtn)
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("SUBMENU REGISTRAR ATENCION");
            Console.WriteLine("1. Atender paciente con cita");
            Console.WriteLine("2. Atender siguiente emergencia (segun prioridad)");
            Console.WriteLine("3. Regresar al menu principal");
            opcion = LeerEnteroEnRango("Seleccione una opcion: ", 1, 3);

            switch (opcion)
            {
                case 1:
                    AtenderCita(cPac, cMed, cFec, cHor, cEst, cantCitas, pId, pNom, cantPac,
                                mCod, mNom, mEst, mAtend, cantMed,
                                aPac, aMed, aDiag, aTrat, aMeds, aFec, aEstPac, ref cantAtn, maxAtn);
                    break;
                case 2:
                    AtenderSiguienteEmergencia(ePac, eMed, eEst, cantEmg, pId, pNom, cantPac,
                                mCod, mNom, mEst, mAtend, cantMed,
                                aPac, aMed, aDiag, aTrat, aMeds, aFec, aEstPac, ref cantAtn, maxAtn);
                    break;
            }
            if (opcion != 3) Pausa();
        } while (opcion != 3);
    }

    static void AtenderCita(string[] cPac, string[] cMed, string[] cFec, string[] cHor, string[] cEst, int cantCitas,
                            string[] pId, string[] pNom, int cantPac,
                            string[] mCod, string[] mNom, string[] mEst, int[] mAtend, int cantMed,
                            string[] aPac, string[] aMed, string[] aDiag, string[] aTrat, string[] aMeds, string[] aFec, string[] aEstPac, ref int cantAtn, int maxAtn)
    {
        Console.WriteLine("\n--- Atender paciente con cita ---");
        if (cantAtn >= maxAtn)
        {
            Console.WriteLine(" Error: Se alcanzo el limite de atenciones registrables.");
            return;
        }

        string idP = LeerTextoNoVacio("ID del paciente: ");
        if (BuscarPacienteRecursivo(pId, 0, cantPac, idP) == -1)
        {
            Console.WriteLine(" Error: El paciente no existe.");
            return;
        }

        int idxCita = -1;
        for (int i = 0; i < cantCitas; i++)
        {
            bool activa = cEst[i].Equals("Pendiente") || cEst[i].Equals("Confirmada");
            if (cPac[i].Equals(idP, StringComparison.OrdinalIgnoreCase) && activa)
            {
                idxCita = i;
                break;
            }
        }
        if (idxCita == -1)
        {
            Console.WriteLine(" Este paciente no tiene citas pendientes o confirmadas.");
            return;
        }

        Console.WriteLine($"Cita encontrada: {cFec[idxCita]} {cHor[idxCita]} | Medico: {cMed[idxCita]} | Paciente: {NombrePaciente(pId, pNom, cantPac, idP)}");

        GuardarAtencion(idP, cMed[idxCita], mCod, mAtend, cantMed,
                        aPac, aMed, aDiag, aTrat, aMeds, aFec, aEstPac, ref cantAtn);

        cEst[idxCita] = "Atendida";
        Console.WriteLine(" Cita marcada como Atendida.");
    }

    static void AtenderSiguienteEmergencia(string[] ePac, string[] eMed, string[] eEst, int cantEmg,
                            string[] pId, string[] pNom, int cantPac,
                            string[] mCod, string[] mNom, string[] mEst, int[] mAtend, int cantMed,
                            string[] aPac, string[] aMed, string[] aDiag, string[] aTrat, string[] aMeds, string[] aFec, string[] aEstPac, ref int cantAtn, int maxAtn)
    {
        Console.WriteLine("\n--- Atender siguiente emergencia ---");
        if (cantAtn >= maxAtn)
        {
            Console.WriteLine(" Error: Se alcanzo el limite de atenciones registrables.");
            return;
        }

        int idx = -1;
        for (int i = 0; i < cantEmg; i++)
        {
            if (eEst[i].Equals("En Espera"))
            {
                idx = i;
                break;
            }
        }
        if (idx == -1)
        {
            Console.WriteLine(" No hay emergencias en espera.");
            return;
        }

        Console.WriteLine($"Siguiente paciente: {ePac[idx]} - {NombrePaciente(pId, pNom, cantPac, ePac[idx])}");

        if (eMed[idx] == "Sin Asignar")
        {
            for (int i = 0; i < cantMed; i++)
            {
                if (mEst[i].Equals("Disponible"))
                {
                    eMed[idx] = mCod[i];
                    break;
                }
            }
            if (eMed[idx] == "Sin Asignar")
            {
                Console.WriteLine(" No hay medicos disponibles. Indique el codigo del medico que atendera.");
                string cod;
                while (true)
                {
                    cod = LeerTextoNoVacio("Codigo del medico: ");
                    if (BuscarIndiceCadena(mCod, cantMed, cod) != -1) break;
                    Console.WriteLine(" Error: Ese medico no existe.");
                }
                eMed[idx] = cod;
            }
        }
        Console.WriteLine($"Medico que atiende: {eMed[idx]}");

        GuardarAtencion(ePac[idx], eMed[idx], mCod, mAtend, cantMed,
                        aPac, aMed, aDiag, aTrat, aMeds, aFec, aEstPac, ref cantAtn);

        eEst[idx] = "Atendida";

        int idxMed = BuscarIndiceCadena(mCod, cantMed, eMed[idx]);
        if (idxMed != -1) mEst[idxMed] = "Disponible";

        Console.WriteLine(" Emergencia atendida. La cola se actualizo.");
    }

    static void GuardarAtencion(string idPac, string codMed, string[] mCod, int[] mAtend, int cantMed,
                                string[] aPac, string[] aMed, string[] aDiag, string[] aTrat, string[] aMeds, string[] aFec, string[] aEstPac, ref int cantAtn)
    {
        aPac[cantAtn] = idPac;
        aMed[cantAtn] = codMed;
        aDiag[cantAtn] = LeerTextoNoVacio("Diagnostico: ");
        aTrat[cantAtn] = LeerTextoNoVacio("Tratamiento indicado: ");
        aMeds[cantAtn] = LeerTextoNoVacio("Medicamentos recetados (o 'Ninguno'): ");
        aFec[cantAtn] = FechaHoy();
        aEstPac[cantAtn] = LeerEstadoPaciente();

        int idxMed = BuscarIndiceCadena(mCod, cantMed, codMed);
        if (idxMed != -1) mAtend[idxMed]++;

        cantAtn++;
        Console.WriteLine($" Atencion registrada con fecha {aFec[cantAtn - 1]}.");
    }

    static string LeerEstadoPaciente()
    {
        Console.WriteLine("Estado del paciente tras la atencion:");
        Console.WriteLine("1. Estable\n2. En observacion\n3. Dado de alta\n4. Remitido");
        int sel = LeerEnteroEnRango("Seleccione estado: ", 1, 4);
        return sel switch { 1 => "Estable", 2 => "En observacion", 3 => "Dado de alta", _ => "Remitido" };
    }

    static void MenuHistorial(string[] aPac, string[] aMed, string[] aDiag, string[] aTrat, string[] aMeds, string[] aFec, string[] aEstPac, int cantAtn,
                              string[] pId, string[] pNom, int cantPac)
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("SUBMENU HISTORIAL MEDICO");
            Console.WriteLine("1. Ver todas las atenciones de un paciente");
            Console.WriteLine("2. Buscar diagnostico en el historial de un paciente");
            Console.WriteLine("3. Ver tratamientos anteriores de un paciente");
            Console.WriteLine("4. Cantidad total de consultas de un paciente");
            Console.WriteLine("5. Regresar al menu principal");
            opcion = LeerEnteroEnRango("Seleccione una opcion: ", 1, 5);

            if (opcion != 5)
            {
                string idP = LeerTextoNoVacio("\nID del paciente: ");
                if (BuscarPacienteRecursivo(pId, 0, cantPac, idP) == -1)
                {
                    Console.WriteLine(" Error: El paciente no existe.");
                }
                else
                {
                    Console.WriteLine($"Paciente: {NombrePaciente(pId, pNom, cantPac, idP)}");
                    switch (opcion)
                    {
                        case 1:
                            if (ContarConsultasRecursivo(aPac, 0, cantAtn, idP) == 0)
                                Console.WriteLine(" Este paciente no tiene atenciones registradas.");
                            else
                                MostrarHistorialRecursivo(aPac, aMed, aDiag, aTrat, aMeds, aFec, aEstPac, 0, cantAtn, idP);
                            break;
                        case 2:
                            string termino = LeerTextoNoVacio("Diagnostico a buscar (palabra o frase): ");
                            int hallados = BuscarDiagnosticoRecursivo(aPac, aDiag, aFec, 0, cantAtn, idP, termino);
                            if (hallados == 0) Console.WriteLine(" No se encontraron diagnosticos con ese texto.");
                            break;
                        case 3:
                            MostrarTratamientosPaciente(aPac, aDiag, aTrat, aMeds, aFec, cantAtn, idP);
                            break;
                        case 4:
                            Console.WriteLine($"Total de consultas: {ContarConsultasRecursivo(aPac, 0, cantAtn, idP)}");
                            break;
                    }
                }
                Pausa();
            }
        } while (opcion != 5);
    }

    static void MostrarHistorialRecursivo(string[] aPac, string[] aMed, string[] aDiag, string[] aTrat, string[] aMeds, string[] aFec, string[] aEstPac,
                                          int idx, int total, string idPac)
    {
        if (idx >= total) return;

        if (aPac[idx].Equals(idPac, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"\n[{aFec[idx]}] Medico: {aMed[idx]}");
            Console.WriteLine($"  Diagnostico : {aDiag[idx]}");
            Console.WriteLine($"  Tratamiento : {aTrat[idx]}");
            Console.WriteLine($"  Medicamentos: {aMeds[idx]}");
            Console.WriteLine($"  Estado      : {aEstPac[idx]}");
        }

        MostrarHistorialRecursivo(aPac, aMed, aDiag, aTrat, aMeds, aFec, aEstPac, idx + 1, total, idPac);
    }

    static int ContarConsultasRecursivo(string[] aPac, int idx, int total, string idPac)
    {
        if (idx >= total) return 0;

        int esta = aPac[idx].Equals(idPac, StringComparison.OrdinalIgnoreCase) ? 1 : 0;
        return esta + ContarConsultasRecursivo(aPac, idx + 1, total, idPac);
    }

    static int BuscarDiagnosticoRecursivo(string[] aPac, string[] aDiag, string[] aFec, int idx, int total, string idPac, string termino)
    {
        if (idx >= total) return 0;

        int esta = 0;
        if (aPac[idx].Equals(idPac, StringComparison.OrdinalIgnoreCase) &&
            aDiag[idx].IndexOf(termino, StringComparison.OrdinalIgnoreCase) >= 0)
        {
            Console.WriteLine($"- [{aFec[idx]}] {aDiag[idx]}");
            esta = 1;
        }
        return esta + BuscarDiagnosticoRecursivo(aPac, aDiag, aFec, idx + 1, total, idPac, termino);
    }

    static void MostrarTratamientosPaciente(string[] aPac, string[] aDiag, string[] aTrat, string[] aMeds, string[] aFec, int cantAtn, string idPac)
    {
        bool hay = false;
        for (int i = 0; i < cantAtn; i++)
        {
            if (aPac[i].Equals(idPac, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"- [{aFec[i]}] Para '{aDiag[i]}': {aTrat[i]} | Medicamentos: {aMeds[i]}");
                hay = true;
            }
        }
        if (!hay) Console.WriteLine(" Este paciente no tiene tratamientos registrados.");
    }

    static void MenuEstadisticas(string[] pId, int[] pEdad, int cantPac,
                                 string[] mCod, string[] mNom, string[] mEsp, int[] mAtend, int cantMed,
                                 int[] eTiem, int cantEmg,
                                 string[] aPac, string[] aDiag, string[] aFec, int cantAtn)
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("SUBMENU ESTADISTICAS");
            Console.WriteLine("1. Medico con mas pacientes atendidos");
            Console.WriteLine("2. Especialidad con mayor demanda");
            Console.WriteLine("3. Promedio de edad de pacientes atendidos");
            Console.WriteLine("4. Pacientes atendidos por dia");
            Console.WriteLine("5. Tiempo promedio de espera en emergencias");
            Console.WriteLine("6. Diagnostico mas frecuente");
            Console.WriteLine("7. Regresar al menu principal");
            opcion = LeerEnteroEnRango("Seleccione una opcion: ", 1, 7);
            Console.WriteLine();

            switch (opcion)
            {
                case 1:
                    string med = MedicoMasAtendidos(mNom, mAtend, cantMed, out int nMed);
                    if (nMed == 0) Console.WriteLine("Aun no hay atenciones registradas.");
                    else Console.WriteLine($"Medico con mas pacientes atendidos: {med} ({nMed} atenciones)");
                    break;
                case 2:
                    string esp = EspecialidadMayorDemanda(mEsp, mAtend, cantMed, out int nEsp);
                    if (nEsp == 0) Console.WriteLine("Aun no hay atenciones registradas.");
                    else Console.WriteLine($"Especialidad con mayor demanda: {esp} ({nEsp} atenciones)");
                    break;
                case 3:
                    double prom = PromedioEdadAtendidos(pId, pEdad, cantPac, aPac, cantAtn);
                    if (prom < 0) Console.WriteLine("Aun no hay pacientes atendidos.");
                    else Console.WriteLine($"Promedio de edad de pacientes atendidos: {prom:F1} anios");
                    break;
                case 4:
                    MostrarPacientesPorDia(aFec, cantAtn);
                    break;
                case 5:
                    double espera = PromedioEsperaEmergencias(eTiem, cantEmg);
                    if (espera < 0) Console.WriteLine("No hay emergencias registradas.");
                    else Console.WriteLine($"Tiempo promedio de espera en emergencias: {espera:F1} min");
                    break;
                case 6:
                    string diag = DiagnosticoMasFrecuente(aDiag, cantAtn, out int veces);
                    if (veces == 0) Console.WriteLine("Aun no hay diagnosticos registrados.");
                    else Console.WriteLine($"Diagnostico mas frecuente: {diag} ({veces} veces)");
                    break;
            }
            if (opcion != 7) Pausa();
        } while (opcion != 7);
    }

    static string MedicoMasAtendidos(string[] mNom, int[] mAtend, int cantMed, out int maximo)
    {
        maximo = 0;
        string nombre = "";
        for (int i = 0; i < cantMed; i++)
        {
            if (mAtend[i] > maximo)
            {
                maximo = mAtend[i];
                nombre = mNom[i];
            }
        }
        return nombre;
    }

    static string EspecialidadMayorDemanda(string[] mEsp, int[] mAtend, int cantMed, out int maximo)
    {
        maximo = 0;
        string mejor = "";
        for (int i = 0; i < cantMed; i++)
        {
            int total = 0;
            for (int j = 0; j < cantMed; j++)
            {
                if (mEsp[j].Equals(mEsp[i], StringComparison.OrdinalIgnoreCase))
                    total += mAtend[j];
            }
            if (total > maximo)
            {
                maximo = total;
                mejor = mEsp[i];
            }
        }
        return mejor;
    }

    static double PromedioEdadAtendidos(string[] pId, int[] pEdad, int cantPac, string[] aPac, int cantAtn)
    {
        int suma = 0, cuenta = 0;
        for (int i = 0; i < cantPac; i++)
        {
            if (ContarConsultasRecursivo(aPac, 0, cantAtn, pId[i]) > 0)
            {
                suma += pEdad[i];
                cuenta++;
            }
        }
        return cuenta == 0 ? -1 : (double)suma / cuenta;
    }

    static int ContarAtencionesPorFecha(string[] aFec, int cantAtn, string fecha)
    {
        int n = 0;
        for (int i = 0; i < cantAtn; i++)
        {
            if (aFec[i].Equals(fecha)) n++;
        }
        return n;
    }

    static bool EsPrimeraAparicion(string[] aFec, int idx)
    {
        for (int i = 0; i < idx; i++)
        {
            if (aFec[i].Equals(aFec[idx])) return false;
        }
        return true;
    }

    static void MostrarPacientesPorDia(string[] aFec, int cantAtn)
    {
        if (cantAtn == 0)
        {
            Console.WriteLine("Aun no hay atenciones registradas.");
            return;
        }
        Console.WriteLine("Pacientes atendidos por dia:");
        for (int i = 0; i < cantAtn; i++)
        {
            if (EsPrimeraAparicion(aFec, i))
                Console.WriteLine($"- {aFec[i]}: {ContarAtencionesPorFecha(aFec, cantAtn, aFec[i])}");
        }
    }

    static double PromedioEsperaEmergencias(int[] eTiem, int cantEmg)
    {
        if (cantEmg == 0) return -1;
        int suma = 0;
        for (int i = 0; i < cantEmg; i++) suma += eTiem[i];
        return (double)suma / cantEmg;
    }

    static string DiagnosticoMasFrecuente(string[] aDiag, int cantAtn, out int veces)
    {
        veces = 0;
        string mejor = "";
        for (int i = 0; i < cantAtn; i++)
        {
            int cuenta = 0;
            for (int j = 0; j < cantAtn; j++)
            {
                if (aDiag[j].Equals(aDiag[i], StringComparison.OrdinalIgnoreCase)) cuenta++;
            }
            if (cuenta > veces)
            {
                veces = cuenta;
                mejor = aDiag[i];
            }
        }
        return mejor;
    }

    static void MenuReportes(string[] cPac, string[] cMed, string[] cFec, string[] cHor, string[] cEst, int cantCitas,
                             string[] ePac, int[] ePrio, int[] eTiem, string[] eMed, string[] eEst, int cantEmg,
                             string[] pId, string[] pNom, int cantPac,
                             string[] mCod, string[] mNom, string[] mEsp, string[] mEst, int cantMed, int cantAtn)
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("SUBMENU REPORTES");
            Console.WriteLine("1. Pacientes pendientes por atender");
            Console.WriteLine("2. Emergencias pendientes (por prioridad)");
            Console.WriteLine("3. Citas programadas para hoy");
            Console.WriteLine("4. Medicos disponibles");
            Console.WriteLine("5. Resumen general del hospital");
            Console.WriteLine("6. Regresar al menu principal");
            opcion = LeerEnteroEnRango("Seleccione una opcion: ", 1, 6);
            Console.WriteLine();

            switch (opcion)
            {
                case 1:
                    ReportePacientesPendientes(cPac, cFec, cHor, cEst, cantCitas, ePac, eEst, cantEmg, pId, pNom, cantPac);
                    break;
                case 2:
                    ReporteEmergenciasPendientes(ePac, ePrio, eTiem, eMed, eEst, cantEmg, pId, pNom, cantPac);
                    break;
                case 3:
                    ReporteCitasHoy(cPac, cMed, cFec, cHor, cEst, cantCitas, pId, pNom, cantPac);
                    break;
                case 4:
                    ReporteMedicosDisponibles(mCod, mNom, mEsp, mEst, cantMed);
                    break;
                case 5:
                    ResumenGeneral(cantPac, cantMed, cantCitas, cantEmg, cantAtn, eEst);
                    break;
            }
            if (opcion != 6) Pausa();
        } while (opcion != 6);
    }

    static void ReportePacientesPendientes(string[] cPac, string[] cFec, string[] cHor, string[] cEst, int cantCitas,
                                           string[] ePac, string[] eEst, int cantEmg,
                                           string[] pId, string[] pNom, int cantPac)
    {
        Console.WriteLine("=== PACIENTES PENDIENTES POR ATENDER ===");
        bool hay = false;

        Console.WriteLine("\nCon cita:");
        for (int i = 0; i < cantCitas; i++)
        {
            if (cEst[i].Equals("Pendiente") || cEst[i].Equals("Confirmada"))
            {
                Console.WriteLine($"- {cPac[i]} | {NombrePaciente(pId, pNom, cantPac, cPac[i])} | {cFec[i]} {cHor[i]} ({cEst[i]})");
                hay = true;
            }
        }

        Console.WriteLine("\nPor emergencia (en espera):");
        for (int i = 0; i < cantEmg; i++)
        {
            if (eEst[i].Equals("En Espera"))
            {
                Console.WriteLine($"- {ePac[i]} | {NombrePaciente(pId, pNom, cantPac, ePac[i])}");
                hay = true;
            }
        }

        if (!hay) Console.WriteLine("\nNo hay pacientes pendientes.");
    }

    static void ReporteEmergenciasPendientes(string[] ePac, int[] ePrio, int[] eTiem, string[] eMed, string[] eEst, int cantEmg,
                                             string[] pId, string[] pNom, int cantPac)
    {
        Console.WriteLine("=== EMERGENCIAS PENDIENTES (mayor prioridad primero) ===");
        int n = ListarEmergenciasPendientesRecursivo(ePac, ePrio, eTiem, eMed, eEst, 0, cantEmg, pId, pNom, cantPac);
        if (n == 0) Console.WriteLine("No hay emergencias pendientes.");
    }

    static int ListarEmergenciasPendientesRecursivo(string[] ePac, int[] ePrio, int[] eTiem, string[] eMed, string[] eEst,
                                                    int idx, int cantEmg, string[] pId, string[] pNom, int cantPac)
    {
        if (idx >= cantEmg) return 0;

        int esta = 0;
        if (eEst[idx].Equals("En Espera"))
        {
            Console.WriteLine($"Prioridad {ePrio[idx]} | {ePac[idx]} | {NombrePaciente(pId, pNom, cantPac, ePac[idx])} | Espera: {eTiem[idx]} min | Medico: {eMed[idx]}");
            esta = 1;
        }
        return esta + ListarEmergenciasPendientesRecursivo(ePac, ePrio, eTiem, eMed, eEst, idx + 1, cantEmg, pId, pNom, cantPac);
    }

    static void ReporteCitasHoy(string[] cPac, string[] cMed, string[] cFec, string[] cHor, string[] cEst, int cantCitas,
                                string[] pId, string[] pNom, int cantPac)
    {
        string hoy = FechaHoy();
        Console.WriteLine($"=== CITAS PROGRAMADAS PARA HOY ({hoy}) ===");
        bool hay = false;
        for (int i = 0; i < cantCitas; i++)
        {
            if (cFec[i].Equals(hoy) && !cEst[i].Equals("Cancelada"))
            {
                Console.WriteLine($"- {cHor[i]} | {cPac[i]} | {NombrePaciente(pId, pNom, cantPac, cPac[i])} | Medico: {cMed[i]} | {cEst[i]}");
                hay = true;
            }
        }
        if (!hay) Console.WriteLine("No hay citas programadas para hoy.");
    }

    static void ReporteMedicosDisponibles(string[] mCod, string[] mNom, string[] mEsp, string[] mEst, int cantMed)
    {
        Console.WriteLine("=== MEDICOS DISPONIBLES ===");
        bool hay = false;
        for (int i = 0; i < cantMed; i++)
        {
            if (mEst[i].Equals("Disponible", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"- {mCod[i]} | Dr(a). {mNom[i]} | {mEsp[i]}");
                hay = true;
            }
        }
        if (!hay) Console.WriteLine("No hay medicos disponibles en este momento.");
    }

    static void ResumenGeneral(int cantPac, int cantMed, int cantCitas, int cantEmg, int cantAtn, string[] eEst)
    {
        int emgPend = 0;
        for (int i = 0; i < cantEmg; i++)
        {
            if (eEst[i].Equals("En Espera")) emgPend++;
        }

        Console.WriteLine("=== RESUMEN GENERAL DEL HOSPITAL ===");
        Console.WriteLine($"Pacientes registrados : {cantPac}");
        Console.WriteLine($"Medicos registrados   : {cantMed}");
        Console.WriteLine($"Citas registradas     : {cantCitas}");
        Console.WriteLine($"Emergencias           : {cantEmg} (pendientes: {emgPend})");
        Console.WriteLine($"Atenciones realizadas : {cantAtn}");
    }
}