USE [master]
GO
/****** Object:  Database [TIENDABD_QA]    Script Date: 21/04/2023 16:41:23 ******/
CREATE DATABASE [TIENDABD_QA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TIENDABD_QA', FILENAME = N'D:\Datos1\TIENDABD_QA.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TIENDABD_QA_log', FILENAME = N'D:\Log\TIENDABD_QA_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TIENDABD_QA] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TIENDABD_QA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TIENDABD_QA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET ARITHABORT OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TIENDABD_QA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TIENDABD_QA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TIENDABD_QA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TIENDABD_QA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET RECOVERY FULL 
GO
ALTER DATABASE [TIENDABD_QA] SET  MULTI_USER 
GO
ALTER DATABASE [TIENDABD_QA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TIENDABD_QA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TIENDABD_QA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TIENDABD_QA] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TIENDABD_QA] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TIENDABD_QA', N'ON'
GO
USE [TIENDABD_QA]
GO
/****** Object:  Table [dbo].[PERSONA]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERSONA](
	[PersonaId] [int] IDENTITY(1,1) NOT NULL,
	[Dni] [varchar](50) NULL,
	[Nombres] [varchar](50) NULL,
	[Apellidos] [varchar](50) NULL,
	[Edad] [int] NULL,
	[Estado] [bit] NULL,
	[Genero] [char](1) NULL,
	[FlagEnviado] [bit] NULL,
	[Celular] [nvarchar](20) NULL,
 CONSTRAINT [PK__PERSONA__C645EB7EA38D4249] PRIMARY KEY CLUSTERED 
(
	[PersonaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCTO]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCTO](
	[intProductoId] [int] IDENTITY(1,1) NOT NULL,
	[strProductoDesc] [varchar](50) NULL,
	[intCantidad] [int] NULL,
	[decPrecio] [decimal](12, 4) NULL,
	[bitEstado] [bit] NULL,
 CONSTRAINT [PK__PRODUCTO__3213E83FBAE4C4EA] PRIMARY KEY CLUSTERED 
(
	[intProductoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PERSONA] ADD  CONSTRAINT [DF_PERSONA_bitEstado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[PRODUCTO] ADD  CONSTRAINT [DF_PRODUCTO_estado]  DEFAULT ((1)) FOR [bitEstado]
GO
/****** Object:  StoredProcedure [dbo].[usp_persona_actualizar]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_persona_actualizar]  
(  
@PersonaId int,  
@Dni varchar(50),  
@Nombre varchar(50),  
@Apellidos varchar(50),  
--@Estado BIT,  
@Genero CHAR(1),  
@FlagEnviado BIT,  
@Edad INT,
@Celular NVARCHAR(20)   
)  
as  
BEGIN  
  
 UPDATE dbo.PERSONA   
 SET Dni = @Dni,  
  Nombres = @Nombre,  
  Apellidos = @Apellidos,  
  Edad=@Edad,  
  --Estado=@Estado,  
  Genero=@Genero,  
  FlagEnviado=@FlagEnviado,
  Celular=@Celular 
  WHERE PersonaId = @PersonaId  
  
   
end  
GO
/****** Object:  StoredProcedure [dbo].[usp_persona_anulacionLogica]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[usp_persona_anulacionLogica]
(
@PersonaId int 
)
as
begin
	UPDATE [dbo].[PERSONA]
	SET Estado = 0
	where PersonaId = @PersonaId
end
GO
/****** Object:  StoredProcedure [dbo].[usp_persona_eliminar]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_persona_eliminar]
(
@PersonaId int
)
as
begin
 DELETE FROM [dbo].[PERSONA]
 WHERE PersonaId = @PersonaId;
 end
GO
/****** Object:  StoredProcedure [dbo].[usp_persona_insert]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE proc [dbo].[usp_persona_insert]  
(  
@Dni varchar(50),  
@Nombre varchar(50),  
@Edad INT,  
@Apellidos VARCHAR(50),  
@Genero CHAR(1),
@Celular NVARCHAR(20)
)  
as  
begin  
   
 INSERT INTO dbo.PERSONA  
     (  
         Dni,  
         Nombres,  
         Apellidos,  
         Edad,  
         Estado,  
         Genero,  
         FlagEnviado,
		 Celular 
     )  
 VALUES  
     (  
         @Dni,   -- Dni - varchar(50)  
         @Nombre,   -- Nombres - varchar(50)  
         @Apellidos,   -- Apellidos - varchar(50)  
         @Edad,    -- Edad - int  
         1, -- Estado - bit  
         @Genero,   -- Genero - char(1)  
         0,  -- FlagEnviado - bit  
		 @Celular
     )  
  
   
end  
GO
/****** Object:  StoredProcedure [dbo].[usp_persona_listar]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_persona_listar]  
as  
begin  
  select PersonaId,  
               Dni,  
               Nombres,  
               Apellidos,  
               Edad,  
               Estado,  
               Genero,  
               FlagEnviado,
			   Celular from [dbo].[PERSONA]  
  where Estado = 'true'  
end  
GO
/****** Object:  StoredProcedure [dbo].[usp_persona_ListarById]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_persona_ListarById]  
(  
@personaId int  
)  
as  
begin  
  select PersonaId,  
               Dni,  
               Nombres,  
               Apellidos,  
               Edad,  
               Estado,  
               Genero,  
               FlagEnviado,
			   Celular
			    from [dbo].[PERSONA]  
  where Estado = 1 and PersonaId=@personaId  
end  
GO
/****** Object:  StoredProcedure [dbo].[usp_prod_actualizar]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_prod_actualizar]
(
@intProductoId int,
@strProductoDesc varchar(50),
@intCantidad int ,
@decPrecio decimal(12,4)
)
as
begin
	UPDATE [dbo].[PRODUCTO]
	SET strProductoDesc = @strProductoDesc,
		intCantidad = @intCantidad,
		decPrecio = @decPrecio
		WHERE intProductoId = @intProductoId
end
GO
/****** Object:  StoredProcedure [dbo].[usp_prod_anulacionLogica]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_prod_anulacionLogica]
(
@intProductoId int 
)
as
begin
	UPDATE [dbo].[PRODUCTO]
	SET bitEstado = 'false'
	where intProductoId = @intProductoId
end
GO
/****** Object:  StoredProcedure [dbo].[usp_prod_eliminar]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[usp_prod_eliminar]
(
@intProductoId int
)
as
begin
 DELETE FROM PRODUCTO 
 WHERE intProductoId = @intProductoId;
 end
GO
/****** Object:  StoredProcedure [dbo].[usp_prod_insert]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[usp_prod_insert]
(
@strProductoDesc varchar(50),
@intCantidad int,
@decPrecio decimal(12,4)
)
as
begin
	
	insert into [dbo].[PRODUCTO]
			(
			strProductoDesc,
			intCantidad,
			decPrecio
			
			)
			values
			(
			@strProductoDesc,
			@intCantidad,
			@decPrecio
			
			)
end
GO
/****** Object:  StoredProcedure [dbo].[usp_prod_listar]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_prod_listar]
as
begin
		select intProductoId,strProductoDesc,intCantidad,decPrecio,bitEstado from [dbo].[PRODUCTO]
		where bitEstado = 'true'
end
GO
/****** Object:  StoredProcedure [dbo].[usp_prod_ListarById]    Script Date: 21/04/2023 16:41:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[usp_prod_ListarById]
(
@intProductoId int
)
as
begin
		select intProductoId,strProductoDesc,intCantidad,decPrecio,bitEstado from [dbo].[PRODUCTO]
		where bitEstado='true' and intProductoId=@intProductoId
end
GO
USE [master]
GO
ALTER DATABASE [TIENDABD_QA] SET  READ_WRITE 
GO
