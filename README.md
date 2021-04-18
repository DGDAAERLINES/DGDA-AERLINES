![](https://imagizer.imageshack.com/img924/5215/nud7Jw.jpg)
## DGDA-AIRLINES

**¿Qué es DGDA-AIRLINES?**

DGDA-AIRLINES es un sistema de reservaciones para una aerolínea. El objetivo del sistema es facilitar las reservaciones en los vuelos e implementar una opción para la emisión de pasaportes.

**Pre-requisitos:**
1. Tener instalado el gestor de base de datos SQL-SERVER en su computadora

**Instalación:**
1. Clonar este repositorio y tener instalado los siguientes paquetes NuGet:
AForge y AsposePDF. Para la instalación de estos paquetes se tiene que ir al apartado de Administrador de NuGet en Visual Studio y buscarlos en la pestaña de examinar para su posterior instalación.
MaterialDesignXaml. Para más detalles de la instalación de MaterialDesignXaml da [Click Aquí](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit).

2. Crear en tu computadora la base de datos **DGDA Aerlines.Sql** que se encuentra en la rama **feature-database**

3. Asegurarse de que la cadena de conexión tenga la instancia correcta de su computadora. (Para modificarla ingrese a: Propiedades del Proyecto -> Configuración)

**Agradecimiento:**
Agradecimiento especial para el ingeniero Hectór Sabillón [@hsabillon7](https://github.com/hsabillon7) por los conocimientos impartidos que fueron necesarios para la realización de este proyecto.

**Equipo de Desarrollo:**
Denia Chavarria [@Deniachavarria](https://github.com/Deniachavarria) 
Gissel Lopez [@gisselLopez](https://github.com/gisselLopez)
Danilo Zavala [@Danzam97](https://github.com/Danzam97)
Sven Rodriguez [@svenrod09](https://github.com/svenrod09)

**Nota**
La funcion de la webcam de emisión de pasaporte funcionó mientras se probaba individualmente. Sin embargo, una vez añadida al codigo esta no funcionó sin explicación a pesar de que se utilizo el mismo codigo. Se añadio la funcion aparte en la rama feature-webcam.
