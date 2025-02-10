# Evaluador-Expresiones-Matematicas

Este proyecto es una aplicación de Windows Forms que permite evaluar expresiones matemáticas ingresadas por el usuario. La aplicación analiza la expresión, construye un árbol de análisis sintáctico y muestra el resultado de la evaluación junto con el recorrido del árbol.

## Características

- Evaluación de expresiones matemáticas básicas (+, -, *, /).
- Manejo de errores como división por cero.
- Visualización del recorrido del árbol de análisis sintáctico en preorden.

## Requisitos

- .NET Framework 4.8
- Visual Studio 2019 o superior

## Instalación

1. Clona este repositorio:
    ```sh
    git clone https://github.com/tu-usuario/Evaluador-Expresiones-Matematicas.git
    ```

2. Abre el archivo de solución `Practica 4.sln` en Visual Studio.

3. Restaura los paquetes NuGet necesarios.

## Uso

1. Ejecuta la aplicación desde Visual Studio (`F5`).

2. Ingresa una expresión matemática en el [RichTextBox](http://_vscodecontentref_/1).

3. Haz clic en el botón "Iniciar".

4. El resultado de la evaluación se mostrará en el [Label](http://_vscodecontentref_/2) correspondiente, y el recorrido del árbol de análisis sintáctico se mostrará en otro [Label](http://_vscodecontentref_/3).

## Capturas de Pantalla

### Interfaz Principal

![Interfaz Principal](ruta/a/la/captura1.png)

### Ejemplo de Evaluación

![Ejemplo de Evaluación](ruta/a/la/captura2.png)

## Estructura del Proyecto

- [Form1.cs](http://_vscodecontentref_/4): Contiene la lógica principal de la aplicación, incluyendo la evaluación de expresiones y la construcción del árbol de análisis sintáctico.
- [Form1.Designer.cs](http://_vscodecontentref_/5): Contiene el diseño de la interfaz de usuario.
- [Program.cs](http://_vscodecontentref_/6): Punto de entrada principal para la aplicación.
- [Properties/](http://_vscodecontentref_/7): Contiene archivos de configuración y recursos del proyecto.

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue o un pull request para discutir cualquier cambio que desees realizar.
