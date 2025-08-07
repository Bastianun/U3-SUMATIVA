-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 24-07-2025 a las 06:16:34
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `db_proyecto_redes`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalle_proyecto`
--

CREATE TABLE `detalle_proyecto` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Descripcion` varchar(500) NOT NULL,
  `ProyectoId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `detalle_proyecto`
--

INSERT INTO `detalle_proyecto` (`Id`, `Nombre`, `Descripcion`, `ProyectoId`) VALUES
(3, 'detalle 1', 'detalle 1', 3),
(4, 'caratceristica 1', 'caratceristica 1', 4),
(5, 'caratceristica 2', 'caratceristica 2', 3),
(6, 'caratceristica 3', 'caratceristica 3', 3),
(7, 'caratceristica 4', 'caratceristica 4', 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `direccionamiento_ip`
--

CREATE TABLE `direccionamiento_ip` (
  `id` int(11) NOT NULL,
  `IpNetwork` varchar(45) NOT NULL,
  `Prefijo` varchar(4) NOT NULL,
  `Mask` varchar(45) DEFAULT NULL,
  `CantIPTotales` int(11) DEFAULT NULL,
  `CantIpHost` int(11) DEFAULT NULL,
  `IpBroadcast` varchar(45) DEFAULT NULL,
  `RangoInicial` varchar(45) DEFAULT NULL,
  `RangoFinal` varchar(45) DEFAULT NULL,
  `InicioProyectoId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `direccionamiento_ip`
--

INSERT INTO `direccionamiento_ip` (`id`, `IpNetwork`, `Prefijo`, `Mask`, `CantIPTotales`, `CantIpHost`, `IpBroadcast`, `RangoInicial`, `RangoFinal`, `InicioProyectoId`) VALUES
(3, '192.168.1.0', '25', '255.255.255.128', 128, 126, '192.168.1.127', '192.168.1.1', '192.168.1.126', 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inicio_proyecto`
--

CREATE TABLE `inicio_proyecto` (
  `Id` int(11) NOT NULL,
  `EntidadInvolucrada` varchar(200) NOT NULL,
  `FechaInicio` datetime DEFAULT NULL,
  `FechaFinalizacion` datetime DEFAULT NULL,
  `Estado` int(11) DEFAULT NULL,
  `Observaciones` varchar(1000) DEFAULT NULL,
  `ProyectoId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `inicio_proyecto`
--

INSERT INTO `inicio_proyecto` (`Id`, `EntidadInvolucrada`, `FechaInicio`, `FechaFinalizacion`, `Estado`, `Observaciones`, `ProyectoId`) VALUES
(2, 'CFT', '2025-07-23 23:08:00', '2025-07-26 23:08:00', 1, 'Nada', 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proyectos`
--

CREATE TABLE `proyectos` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Descripcion` varchar(400) NOT NULL,
  `Estado` int(11) NOT NULL COMMENT '0: Finalizado ; 1: Proceso; 2: Cancelado',
  `Tipo` int(11) NOT NULL COMMENT '0: Cableado UTP; 1: Configuración Equipos; 2: Ambos',
  `UsuarioId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `proyectos`
--

INSERT INTO `proyectos` (`Id`, `Nombre`, `Descripcion`, `Estado`, `Tipo`, `UsuarioId`) VALUES
(3, 'proeycto de fabio', 'Prueba proyectos', 1, 1, 2),
(4, 'Proyecto de prueba 2', 'eeeeeeeeeeeee', 1, 1, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `trabajadores`
--

CREATE TABLE `trabajadores` (
  `Id` int(11) NOT NULL,
  `Cargo` varchar(200) DEFAULT NULL,
  `InicioproyectoId` int(11) NOT NULL,
  `UsuarioId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `trabajadores`
--

INSERT INTO `trabajadores` (`Id`, `Cargo`, `InicioproyectoId`, `UsuarioId`) VALUES
(9, 'adss777', 2, 2),
(10, 'adss777', 2, 2),
(11, 'Jefe', 2, 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Apellido` varchar(100) NOT NULL,
  `Telefono` varchar(15) DEFAULT NULL,
  `Correo` varchar(100) DEFAULT NULL,
  `Password` varchar(100) DEFAULT NULL,
  `Estado` int(11) NOT NULL COMMENT '0: Desactivado ; 1: Activado',
  `Rol` int(11) NOT NULL COMMENT '0: Administador ; 1: Trabajador'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id`, `Nombre`, `Apellido`, `Telefono`, `Correo`, `Password`, `Estado`, `Rol`) VALUES
(2, 'Fabio', 'aldj', '7878777', 'fabio@gmail.com', '1111111', 1, 1),
(3, 'edgardo', 'Cayo', '44242424', 'cayo@cftmail.cl', '$2a$11$BySqjGbMzn/KJ7/xhav8S.4roz2x2AMFXBeCXHriuffpx0djcPtmO', 1, 0);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `detalle_proyecto`
--
ALTER TABLE `detalle_proyecto`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_detalle_proyecto_proyectos1_idx` (`ProyectoId`);

--
-- Indices de la tabla `direccionamiento_ip`
--
ALTER TABLE `direccionamiento_ip`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_direccionamiento_ip_inicio_proyecto1_idx` (`InicioProyectoId`);

--
-- Indices de la tabla `inicio_proyecto`
--
ALTER TABLE `inicio_proyecto`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_inicio_proyecto_proyectos1_idx` (`ProyectoId`);

--
-- Indices de la tabla `proyectos`
--
ALTER TABLE `proyectos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_Proyectos_usuarios_idx` (`UsuarioId`);

--
-- Indices de la tabla `trabajadores`
--
ALTER TABLE `trabajadores`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_inicio_proyecto_has_usuarios_usuarios1_idx` (`UsuarioId`),
  ADD KEY `fk_inicio_proyecto_has_usuarios_inicio_proyecto1_idx` (`InicioproyectoId`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `detalle_proyecto`
--
ALTER TABLE `detalle_proyecto`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `direccionamiento_ip`
--
ALTER TABLE `direccionamiento_ip`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `inicio_proyecto`
--
ALTER TABLE `inicio_proyecto`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `proyectos`
--
ALTER TABLE `proyectos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `trabajadores`
--
ALTER TABLE `trabajadores`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `detalle_proyecto`
--
ALTER TABLE `detalle_proyecto`
  ADD CONSTRAINT `fk_detalle_proyecto_proyectos1` FOREIGN KEY (`ProyectoId`) REFERENCES `proyectos` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `direccionamiento_ip`
--
ALTER TABLE `direccionamiento_ip`
  ADD CONSTRAINT `fk_direccionamiento_ip_inicio_proyecto1` FOREIGN KEY (`InicioProyectoId`) REFERENCES `inicio_proyecto` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `inicio_proyecto`
--
ALTER TABLE `inicio_proyecto`
  ADD CONSTRAINT `fk_inicio_proyecto_proyectos1` FOREIGN KEY (`ProyectoId`) REFERENCES `proyectos` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `proyectos`
--
ALTER TABLE `proyectos`
  ADD CONSTRAINT `fk_Proyectos_usuarios` FOREIGN KEY (`UsuarioId`) REFERENCES `usuarios` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `trabajadores`
--
ALTER TABLE `trabajadores`
  ADD CONSTRAINT `fk_inicio_proyecto_has_usuarios_inicio_proyecto1` FOREIGN KEY (`InicioproyectoId`) REFERENCES `inicio_proyecto` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_inicio_proyecto_has_usuarios_usuarios1` FOREIGN KEY (`UsuarioId`) REFERENCES `usuarios` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
