## Relaciones entre Clases

### 1) Relaciones por Composición y Agregación
- **Composición**: La relación entre `Pedidos` y `Cliente` podría ser vista como una composición, ya que un pedido no puede existir sin un cliente. El cliente es parte integral del pedido.
- **Agregación**: La relación entre `Cadetería` y `Cadete` es un ejemplo de agregación, ya que los cadetes pueden existir por sí solos, sin necesidad de estar ligados a una cadetería específica. Si una cadetería deja de existir, los cadetes aún pueden existir independientemente.

### 2) Métodos de las Clases

- **Clase `Cadetería`:**
  - `AgregarCadete(Cadete cadete)`: Para añadir un nuevo cadete a la lista de cadetes.
  - `RemoverCadete(Cadete cadete)`: Para eliminar un cadete de la lista.
  - `AsignarPedido(Cadete cadete, Pedido pedido)`: Para asignar un pedido a un cadete específico.
  - `ObtenerListadoCadetes()`: Para obtener la lista de todos los cadetes.

- **Clase `Cadete`:**
  - `AsignarPedido(Pedido pedido)`: Para añadir un pedido al listado de pedidos del cadete.
  - `CalcularJornal()`: Para calcular el jornal que se le debe pagar al cadete.
  - `MostrarInformacion()`: Para mostrar la información del cadete, incluyendo su ID, nombre, dirección, teléfono y pedidos asignados.

### 3) Atributos y Métodos Públicos y Privados

- **Clase `Cadetería`:**
  - **Públicos:**
    - `Nombre`, `Telefono`: Propiedades públicas para que se puedan consultar/modificar.
    - `AgregarCadete`, `RemoverCadete`, `AsignarPedido`, `ObtenerListadoCadetes`: Métodos públicos para gestionar los cadetes y los pedidos.
  - **Privados:**
    - `ListadoCadetes`: Debe ser privado ya que la lista de cadetes no debe ser manipulada directamente desde fuera de la clase.

- **Clase `Cadete`:**
  - **Públicos:**
    - `Id`, `Nombre`, `Direccion`, `Telefono`: Propiedades públicas para consultar la información del cadete.
    - `AsignarPedido`, `CalcularJornal`, `MostrarInformacion`: Métodos públicos para gestionar los pedidos y calcular el jornal.
  - **Privados:**
    - `ListadoPedidos`: Debe ser privado para evitar modificaciones directas desde fuera de la clase.
    - `JornalACobrar`: Este atributo debe calcularse y no debe ser modificado directamente desde fuera de la clase.

### 5) Alternativa de Diseño de Clases
Podría haberse creado una clase base `Empleado` con propiedades y métodos comunes, y luego clases derivadas como `Cadete` que añadan funcionalidades específicas. Además, se podrían utilizar interfaces para definir contratos que ciertas clases deben cumplir, como una interfaz `IPedidos` que obligue a implementar métodos relacionados con la gestión de pedidos.
