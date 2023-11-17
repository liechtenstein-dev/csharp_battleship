# Battalla Naval Sockets con Python y C#

Esta combinación poderosa y versátil permite establecer conexiones robustas y bidireccionales entre diferentes plataformas.


## Descripción

Este projecto fue creado para un trabajo practico en la universidad (UAI), bajo la idea de crear un videojuego en Winforms y tener una arquitectura en capas.

Además de esta arquitectura en capas se encuentra en el projecto un modelo cliente-servidor donde la comunicación sigue el protocolo de TCP entre sockets. Donde el cliente (C#) se conecta a un servidor (Python) y juega contra este una partida de batalla naval.


## Requisitos previos

- Python 3.x instalado.
- Entorno de desarrollo para C# (Visual Studio, Visual Studio Code con extensión C#, etc.).

## Guía de inicio rápido

Clona este repositorio en tu máquina local.

```bash
git clone https://github.com/lichadev/csharp_battleship.git`
```

Abre la solución C# en tu entorno de desarrollo y genera un build del projecto.

Ejecuta el script de Python para iniciar el servidor.

```bash
cd csharp_battleship/server
python server.py
```

Ejecuta la aplicación WinForms para conectarte al servidor y comenzar la comunicación.
