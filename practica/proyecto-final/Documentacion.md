# SGE - Sistema de Gestión de Expedientes
## Documentación de Pruebas desde Program.cs

---

## Aumnos participantes
+ Paz, Amilcar Catriel - 24232/7
+ Rebollo, Lautaro - 25880/0

---

## Cómo ejecutar el proyecto

1. Ejecutar desde la raíz de la solución.
   ```bash
   dotnet run --project SGE.Consola
   ```
2. Se mostrará el menú principal. Los datos se persisten en los archivos `expedientes.txt` y `tramites.txt` generados automáticamente en el directorio de ejecución.

---

## Menú Principal

```
====== SGE - Sistema de Gestion de Expedientes ======
   1. Agregar expediente
   2. Listar expedientes
   3. Modificar caratula de expediente
   4. Cambiar estado de expediente
   5. Eliminar expedientes
   6. Agregar tramite
   7. Eliminar tramite
   8. Modificar tramite
   9. Listar tramites por expediente
   0. Salir
```

> El sistema muestra el **Expediente activo** debajo del menú una vez que se crea o selecciona uno, evitando tener que ingresar el Id manualmente en cada operación. También está definido un `idUsuarioPrueba` que se utiliza en todos los casos de uso.

---

## Camino Feliz 1 — Ciclo completo de un expediente con trámites

### Paso 1: Crear un expediente (Opción 1)

**Código ejecutado:**
```csharp
string caratulaExp = Console.ReadLine()!; 
var reqAgregar = new AgregarExpedienteRequest(caratulaExp, idUsuarioPrueba);
var resAgregar = agregarExpedienteUseCase.Ejecutar(reqAgregar);
expedienteIdUlt = resAgregar.Id;
```

**Salida por consola:**
```
Opcion: 1
Ingrese caratula del expediente a agregar: Caratula ingresada por teclado
Expediente agregado. Id: 4602818e-6d64-449f-ac2c-8a6cf44648cd | Caratula: Caratula ingresada por teclado | Estado: RecienIniciado

Ultimo expediente activo: [2bb65754-2679-4d0f-b182-6e0bc7cb3c48]
```

> El estado inicial es siempre `RecienIniciado`, definido en la entidad `Expediente`.

---

### Paso 2: Agregar un trámite con etiqueta Resolución (Opción 6)

**Código ejecutado:**
```csharp
if (!expedienteIdUlt.HasValue)
{
    Console.WriteLine("Primero crea un expediente (Opcion 1)");
    break;  
}
Console.WriteLine($"Agregando tramite al expediente: [{expedienteIdUlt}]");
Console.Write("Ingrese contenido del tramite: ");
var contenidoTramite = Console.ReadLine()!;

Console.WriteLine("\nSeleccione la etiqueta:");
Console.WriteLine(" 1. Escrito Presentado");
Console.WriteLine(" 2. Pase a Estudio");
Console.WriteLine(" 3. Despacho");
Console.WriteLine(" 4. Resolucion");
Console.WriteLine(" 5. Notificacion");
Console.WriteLine(" 6. Pase al Archivo");
Console.Write(" Opcion: ");

EtiquetaTramite etiqueta ;
int opEtiqueta = int.Parse(Console.ReadLine()!);
etiqueta = opEtiqueta switch
{
    1 => EtiquetaTramite.EscritoPresentado,
    2 => EtiquetaTramite.PaseAEstudio,
    3 => EtiquetaTramite.Despacho,
    4 => EtiquetaTramite.Resolucion,
    5 => EtiquetaTramite.Notificacion,
    6 => EtiquetaTramite.PaseAlArchivo,
    _ => EtiquetaTramite.EscritoPresentado
};

var reqAgregarTramite = new AgregarTramiteRequest(expedienteIdUlt.Value, etiqueta, contenidoTramite, idUsuarioPrueba);
var resAgregarTramite = agregarTramiteUseCase.Ejecutar(reqAgregarTramite);
tramiteIdUlt = resAgregarTramite.Id;

Console.WriteLine($"Tramite agregado con Id: [{resAgregarTramite.Id}]");
```

**Salida por consola:**
```
Opcion: 6
Agregando tramite al expediente: [2bb65754-2679-4d0f-b182-6e0bc7cb3c48]
Ingrese contenido del tramite: Contenido ingresado por teclado

Seleccione la etiqueta:
 1. Escrito Presentado
 2. Pase a Estudio
 3. Despacho
 4. Resolucion
 5. Notificacion
 6. Pase al Archivo
 Opcion: 4
Tramite agregado con Id: [99b72ed2-5760-4691-99ca-f5d884a33113]
```

> Al agregar el trámite, `ActualizacionEstadoExpedienteService` detecta que la etiqueta del último trámite es `Resolucion` y actualiza automáticamente el estado del expediente a `ConResolucion`.

---

### Paso 3: Listar expedientes y verificar el cambio de estado automático (Opción 2)

**Código ejecutado:**
```csharp
var resLista = listarExpedientesUseCase.Ejecutar();

foreach (var e in resLista)
{
   Console.WriteLine($"Id: {e.Id} | Caratula: {e.Caratula} | Estado: {e.Estado}");
}
```

**Salida por consola:**
```
Opcion: 2
------------- Listado de expedientes -------------
Id: 2bb65754-2679-4d0f-b182-6e0bc7cb3c48 | Caratula: Caratula ingresada por teclado | Estado: ConResolucion
--------------------------------------------------
```

> El estado cambió automáticamente de `RecienIniciado` a `ConResolucion`.

---

### Paso 4: Listar trámites por expediente (Opción 9)

**Código ejecutado:**
```csharp
var reqListarTramites = new ListarTramitesPorExpedienteRequest(expedienteIdUlt.Value);
var resListaTramites = listarTramitesPorExpediente.Ejecutar(reqListarTramites);

foreach (var t in resListaTramites)
{
   Console.WriteLine($"Id: {t.Id} | Etiqueta: {t.Etiqueta} | Contenido: {t.Contenido}");
}
```

**Salida por consola:**
```
Opcion: 9
------------- Tramites del Exp: [2bb65754-2679-4d0f-b182-6e0bc7cb3c48] -------------
Id: 99b72ed2-5760-4691-99ca-f5d884a33113 | Etiqueta: Resolucion | Contenido: Contenido ingresado por teclado
--------------------------------------------------
```

---

### Paso 5: Modificar la carátula del expediente (Opción 3)

**Código ejecutado:**
```csharp
var nuevaCaratula = Console.ReadLine()!;
var reqModificar = new ModificarCaratulaExpedienteRequest(expedienteIdUlt.Value, nuevaCaratula, idUsuarioPrueba);
var resModificar = modificarCaratulaExpedienteUseCase.Ejecutar(reqModificar);
```

**Salida por consola:**
```
Opcion: 3
Id del expediente activo: [2bb65754-2679-4d0f-b182-6e0bc7cb3c48]
Ingrese nueva caratula: Nueva caratula ingresada por teclado
Caratula modificada.
```

---

### Paso 6: Cambiar el estado manualmente a "En Notificación" (Opción 4)

**Código ejecutado:**
```csharp
var reqCambiarEstado = new CambiarEstadoExpedienteRequest(expedienteIdUlt.Value, nuevoEstado, idUsuarioPrueba);
var resCambiarEstado = cambiarEstadoExpedienteUseCase.Ejecutar(reqCambiarEstado);
```

**Salida por consola:**
```
Opcion: 4
[Id del expediente activo: 2bb65754-2679-4d0f-b182-6e0bc7cb3c48]

Ingrese un nuevo estado:
   1. Recien iniciado
   2. Para resolver
   3. Con resolucion
   4. En notificacion
   5. Finalizado
  Opcion: 4
Estado modificado.
```

> Esto demuestra el cambio de estado manual, independiente de los trámites. El expediente tenía el último trámite con etiqueta `Resolucion`, pero el usuario puede forzar cualquier otro estado.

---

### Paso 7: Modificar el trámite (Opción 8)

**Código ejecutado:**
```csharp
var reqModificarTramite = new ModificarTramiteRequest(tramiteIdUlt.Value, nuevaEtiqueta, nuevoContenido, idUsuarioPrueba);
modificarTramiteUseCase.Ejecutar(reqModificarTramite);
```

**Salida por consola:**
```
Opcion: 8
Id del trámite activo: [99b72ed2-5760-4691-99ca-f5d884a33113]
Ingrese el nuevo contenido del tramite: Nuevo contenido ingresado por teclado

Seleccione la nueva etiqueta:
 1. Escrito Presentado
 2. Pase a Estudio
 3. Despacho
 4. Resolucion
 5. Notificacion
 6. Pase al Archivo
 Opcion: 2
Tramite modificado
```

> Al modificar la etiqueta a `PaseAEstudio`, `ActualizacionEstadoExpedienteService` actualiza automáticamente el estado del expediente a `ParaResolver`. Y si se ingresa un nuevo contenido también se modifica en el tramite.

---

## Camino Feliz 2 — Eliminación en cascada

### Eliminar un expediente (Opción 5)

**Código ejecutado:**
```csharp
var reqEliminarExpediente = new EliminarExpedienteRequest(expedienteIdUlt.Value, idUsuarioPrueba);
eliminarExpedienteUseCase.Ejecutar(reqEliminarExpediente);
Console.WriteLine("Expediente eliminado correctamente.");
expedienteIdUlt = null;
```

**Salida por consola:**
```
Opcion: 5
Id del expediente a eliminar [3f2504e0-4f89-11d3-9a0c-0305e82c3301]
Expediente eliminado correctamente.
```

> El `EliminarExpedienteUseCase` elimina primero todos los trámites asociados y luego el expediente, tal como se indica en la consigna.

### Verificar que la lista quedó vacía (Opción 2)

```
Opcion: 2
------------- Listado de expedientes -------------
--------------------------------------------------
```

---

## Caminos de Error

### Error de Dominio — Carátula vacía

**Código ejecutado:**
```csharp
Console.Write("Ingrese caratula del expediente a agregar: ");
string caratulaExp = Console.ReadLine()!;
var reqAgregar = new AgregarExpedienteRequest(caratulaExp, idUsuarioPrueba);
var resAgregar = agregarExpedienteUseCase.Ejecutar(reqAgregar);
```
> Si se presiona Enter sin ingresar texto, `CaratulaExpediente` lanza `DominioException` al instanciarse con string vacío.

**Salida por consola:**
```
Opcion: 1
Ingrese caratula del expediente a agregar:
[Error de Dominio]: La caratula no puede estar vacia
```

---

### Error de Autorización — Usuario sin permisos

Para probar este camino, cambiar temporalmente `AutorizacionProvisionalService`:
```csharp
// SGE.Infraestructura/Autorizacion/AutorizacionProvisionalService.cs
public bool PoseeElPermiso(Guid idUsuario, Permiso permiso) => false;
```

**Salida por consola:**
```
Opcion: 1
Ingrese caratula del expediente a agregar: Expediente de prueba
[Error de permisos]: El usuario no tiene permiso para crear expedientes.
```

> Restaurar a `return => true` luego de la prueba.

---

### Error de Repositorio — Entidad no encontrada

Para probar este camino se adicionó una opcion extra (Opcion 99) que fuerce la `RepositorioException`

**Código ejecutado:**
```csharp
// atacamos directo al repositorio con un id que no existe
expedienteRepositorio.Eliminar(Guid.NewGuid());
```

**Salida por consola:**
```
Opcion: 99
Forzando error de repositorio...
[Error de datos]: No se encontro el expediente a eliminar
```

---

## Resumen de capas y responsabilidades

| Capa | Proyecto | Responsabilidad |
|---|---|---|
| Dominio | SGE.Dominio | Entidades, Value Objects, reglas de negocio, enumerativos |
| Aplicación | SGE.Aplicacion | Casos de Uso, DTOs, interfaces de repositorios y servicios |
| Infraestructura | SGE.Infraestructura | Repositorios TXT, servicio de autorización provisional |
| Presentación | SGE.Consola | Menú interactivo, Composition Root |