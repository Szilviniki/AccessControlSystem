USE [master]
GO
/****** Object:  Database [acs]    Script Date: 2024. 04. 22. 8:09:41 ******/
CREATE DATABASE [acs]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'acs_Data', FILENAME = N'/var/opt/mssql/data\acs.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'acs_Log', FILENAME = N'/var/opt/mssql/data\acs.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [acs] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [acs].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [acs] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [acs] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [acs] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [acs] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [acs] SET ARITHABORT OFF 
GO
ALTER DATABASE [acs] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [acs] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [acs] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [acs] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [acs] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [acs] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [acs] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [acs] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [acs] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [acs] SET  ENABLE_BROKER 
GO
ALTER DATABASE [acs] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [acs] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [acs] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [acs] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [acs] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [acs] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [acs] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [acs] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [acs] SET  MULTI_USER 
GO
ALTER DATABASE [acs] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [acs] SET DB_CHAINING OFF 
GO
ALTER DATABASE [acs] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [acs] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [acs] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [acs] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [acs] SET QUERY_STORE = ON
GO
ALTER DATABASE [acs] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [acs]
GO
/****** Object:  Table [dbo].[GateLogs]    Script Date: 2024. 04. 22. 8:09:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GateLogs](
	[cardId] [int] NOT NULL,
	[isGuest] [bit] NULL,
	[stamp] [datetime] NOT NULL,
 CONSTRAINT [PK_gate_logs_1] PRIMARY KEY CLUSTERED 
(
	[cardId] ASC,
	[stamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 2024. 04. 22. 8:09:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DayOfWeek] [int] NULL,
	[StudentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parents]    Script Date: 2024. 04. 22. 8:09:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parents](
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [varchar](13) NOT NULL,
	[Email] [varchar](100) NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_parents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personnel]    Script Date: 2024. 04. 22. 8:09:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personnel](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[email] [varchar](100) NULL,
	[isPresent] [bit] NULL,
	[phone] [varchar](13) NULL,
	[Role] [int] NOT NULL,
	[cardId] [int] NOT NULL,
	[password] [nvarchar](255) NULL,
	[canLogIn] [bit] NOT NULL,
 CONSTRAINT [faculty_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 2024. 04. 22. 8:09:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[name] [nvarchar](50) NOT NULL,
	[isPresent] [bit] NOT NULL,
	[phone] [varchar](13) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[id] [uniqueidentifier] NOT NULL,
	[cardId] [int] NOT NULL,
	[birthDate] [date] NOT NULL,
	[parentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_students_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-21T08:22:15.817' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T17:38:01.333' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T17:38:25.410' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T17:39:49.357' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T17:40:28.590' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T20:19:36.890' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T20:21:13.993' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T20:38:15.367' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T20:46:00.193' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T20:47:19.403' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T21:53:01.400' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T21:56:57.863' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T21:57:12.060' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T21:57:16.033' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T21:57:30.937' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T21:58:06.133' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T21:58:17.017' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T21:58:27.987' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T21:59:53.853' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T22:00:00.343' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T22:00:02.900' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T22:00:10.437' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (0, 0, CAST(N'2024-02-22T22:00:26.500' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (111111, 0, CAST(N'2024-02-21T08:27:01.897' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (111111, 0, CAST(N'2024-02-29T15:07:04.063' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (111111, 0, CAST(N'2024-02-29T15:10:17.503' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (111111, 0, CAST(N'2024-02-29T15:12:43.147' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (111111, 0, CAST(N'2024-02-29T15:13:09.440' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (200003, 0, CAST(N'2024-04-11T01:10:08.300' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (200003, 0, CAST(N'2024-04-11T01:11:19.700' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (200003, 0, CAST(N'2024-04-11T23:02:26.213' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (200003, 0, CAST(N'2024-04-12T00:47:05.787' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (200003, 0, CAST(N'2024-04-18T16:08:09.510' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (200003, 0, CAST(N'2024-04-18T17:20:14.593' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (200003, 0, CAST(N'2024-04-18T20:34:11.873' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (200003, 0, CAST(N'2024-04-18T20:46:40.577' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (200003, 0, CAST(N'2024-04-21T12:16:53.257' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (200003, 0, CAST(N'2024-04-21T12:20:22.467' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (874215, 0, CAST(N'2024-02-22T20:26:55.607' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (874215, 0, CAST(N'2024-02-22T20:35:16.227' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (874215, 0, CAST(N'2024-02-22T20:47:52.493' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (874215, 0, CAST(N'2024-02-22T20:50:10.587' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (874215, 0, CAST(N'2024-02-22T21:28:53.217' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (874215, 0, CAST(N'2024-02-22T21:29:07.167' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (874215, 0, CAST(N'2024-02-22T21:30:36.063' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (874215, 0, CAST(N'2024-02-23T08:17:32.537' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (874215, 0, CAST(N'2024-03-18T09:07:46.993' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (1000001, 0, CAST(N'2024-04-11T00:44:17.657' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (1000001, 0, CAST(N'2024-04-11T01:11:18.770' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (1000001, 0, CAST(N'2024-04-11T23:02:25.153' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (1000001, 0, CAST(N'2024-04-12T00:47:04.820' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (1000001, 0, CAST(N'2024-04-18T16:08:08.580' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (1000001, 0, CAST(N'2024-04-18T17:20:13.650' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (1000001, 0, CAST(N'2024-04-18T20:34:10.790' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (1000001, 0, CAST(N'2024-04-18T20:46:39.603' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (1000001, 0, CAST(N'2024-04-21T12:16:52.463' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (1000001, 0, CAST(N'2024-04-21T12:20:21.137' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (33445566, 0, CAST(N'2024-02-20T22:56:54.000' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (33445566, 0, CAST(N'2024-02-22T22:17:39.937' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (33445566, 0, CAST(N'2024-02-22T22:19:15.163' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (33445566, 0, CAST(N'2024-02-22T22:19:31.523' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (33445566, 0, CAST(N'2024-03-13T14:29:47.430' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (33445566, 0, CAST(N'2024-03-13T14:29:48.810' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (33445566, 0, CAST(N'2024-03-13T14:31:00.803' AS DateTime))
GO
INSERT [dbo].[GateLogs] ([cardId], [isGuest], [stamp]) VALUES (33445566, 0, CAST(N'2024-03-13T14:36:33.150' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Notes] ON 
GO
INSERT [dbo].[Notes] ([Id], [Name], [DayOfWeek], [StudentId]) VALUES (6, N'jnkm,', 5, N'68ec2ff0-6961-45db-87d1-142b0370de7c')
GO
INSERT [dbo].[Notes] ([Id], [Name], [DayOfWeek], [StudentId]) VALUES (7, N'nljm,', 3, N'68ec2ff0-6961-45db-87d1-142b0370de7c')
GO
INSERT [dbo].[Notes] ([Id], [Name], [DayOfWeek], [StudentId]) VALUES (8, N'Csütörtöki note', 4, N'68ec2ff0-6961-45db-87d1-142b0370de7c')
GO
INSERT [dbo].[Notes] ([Id], [Name], [DayOfWeek], [StudentId]) VALUES (9, N'teszt', 5, N'68ec2ff0-6961-45db-87d1-142b0370de7c')
GO
INSERT [dbo].[Notes] ([Id], [Name], [DayOfWeek], [StudentId]) VALUES (10, N'Foci edzés 17:00-tól', 4, N'd082e2af-aa06-45af-a1d9-26be2362fbe0')
GO
INSERT [dbo].[Notes] ([Id], [Name], [DayOfWeek], [StudentId]) VALUES (11, N'Kemoterápia', 5, N'457287dd-6a61-404d-85b5-34aa37323c6e')
GO
INSERT [dbo].[Notes] ([Id], [Name], [DayOfWeek], [StudentId]) VALUES (32, N'Keddi kecske jóga', 2, N'e765e27b-e07d-42af-998b-2611275fcfc0')
GO
SET IDENTITY_INSERT [dbo].[Notes] OFF
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Erős István', N'+36601002000', N'eros.pista@hungarikum.hu', N'311b47ee-555c-4cfe-9d15-16a9feb9d43e')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Kellby Whitewood', N'+36106361692', N'kwhitewooda@ocn.ne.jp', N'bdd620dc-293a-4b88-bb74-16defa57533e')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Kerek Károly', N'+36503223443', N'kkaroly@te.mp', N'0283f721-4db4-42f7-8d24-1d9e87aaf2c8')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Armand Yong', N'+36203681814', N'ayongc@ameblo.jp', N'13853841-38a0-4c4f-8f8a-240b2020dae5')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Larry Toderi', N'+36209680324', N'ltoderif@ning.com', N'16bf16f3-05eb-49e5-8644-33feab699dc5')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Ula Petrishchev', N'+36504699516', N'upetrishchevh@latimes.com', N'4709b05a-afff-4830-b0e1-5042c1d84238')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Parent1', N'+36301234567', N'mock.mark@mockify.eu', N'4a7c7d89-f600-4f6a-9ec8-55602d7a1979')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Aaren Romero', N'+36802498577', N'aromerob@unc.edu', N'41c63122-e2c7-4ffa-903c-5661e22ccd9e')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Gran Henriques', N'+36700050513', N'ghenriques9@pcworld.com', N'b00f1514-a9cf-470e-9aa0-5813917ed734')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Reamonn O''Nolan', N'+36303533249', N'ronolani@fastcompany.com', N'9b23e982-6632-40d0-ab23-620f4baff9bd')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Rina Burbank', N'+36809984175', N'rburbank2@irs.gov', N'1a3bf97f-ba5f-42f6-93c9-67105e526909')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Rurik Tracey', N'+36807009547', N'rtracey4@scribd.com', N'3944b53c-954c-4008-830a-794b1f0f0ae8')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Szülő Szülő', N'+36308579258', N'szilviniki9@gmail.com', N'b6f6e4c9-41d5-42fb-840c-8482290bacf5')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Dulcia O''Heyne', N'+36300469765', N'doheynee@phpbb.com', N'7f03c5a8-9b04-47ee-a158-979ecd31001b')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Szülő Szülő2', N'+36308579244', N'szilviniki449@gmail.com', N'bc87ebc2-6a1a-449c-b763-9a9e26f3248a')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Keene Thornewill', N'+36906070658', N'kthornewill7@miibeian.gov.cn', N'89fb9862-7515-4a38-8018-9ca4481ebbac')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Jerrie Beachamp', N'+36109950379', N'jbeachamp8@usnews.com', N'b44f5761-fa85-4b7f-8fe3-a3674cae035a')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Rozanna Catterell', N'+36309598535', N'rcatterellg@wunderground.com', N'd879e22f-1c08-4b0e-9d17-b3f1a1802df1')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Cherey Scamaden', N'+36403644786', N'cscamadend@cam.ac.uk', N'6c212947-5529-4548-b19c-d1304f4a0e1d')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Caspar Legate', N'+36101086791', N'clegate5@washingtonpost.com', N'0c9d01c7-3451-4c92-995f-d1fffcb92be5')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Gaylord Joe', N'+36907293905', N'gjoe1@nydailynews.com', N'da8b6efd-c4e1-47c2-8c7c-dd50cb7914c9')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Frederick Boneham', N'+36600414032', N'fboneham0@nps.gov', N'6ddb8b11-995d-4561-ab2a-e0b2198f4a42')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Fayina Hunnawill', N'+36401857499', N'fhunnawill6@weather.com', N'79320273-d621-4147-abd3-ecdf1a8919c6')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Brandais Gladebeck', N'+36903201749', N'bgladebeck3@purevolume.com', N'78609ec1-44c5-4d00-b5b6-f62933b37524')
GO
INSERT [dbo].[Parents] ([Name], [Phone], [Email], [Id]) VALUES (N'Starlene McEwan', N'+36700823021', N'smcewanj@apple.com', N'eeb143b1-c5b1-448f-8d78-ffde83d3091f')
GO
INSERT [dbo].[Personnel] ([id], [name], [email], [isPresent], [phone], [Role], [cardId], [password], [canLogIn]) VALUES (N'1481019b-7595-4ca1-8bb7-077a2c059286', N'Frissített-Postás Ferenc VI.', N'friss.feri@tst.com', 0, N'+36505105052', 3, 767747, N'$2a$11$e0vGzVag7vHVzf55Yrg3weP6QagL64Zprtebee.9AQAbp9iLfdtjS', 0)
GO
INSERT [dbo].[Personnel] ([id], [name], [email], [isPresent], [phone], [Role], [cardId], [password], [canLogIn]) VALUES (N'c19ad25e-9465-4bab-9bef-66c83fc6bc86', N'Nincsen Noémi', N'no.nono@tst.com', 0, N'+36505005050', 4, 874215, N'$2a$11$rd88aOFn6vlpqvHxomUS1.dpfdj3Xn0sYMwF2pQkyvo.coAmM.D7O', 1)
GO
INSERT [dbo].[Personnel] ([id], [name], [email], [isPresent], [phone], [Role], [cardId], [password], [canLogIn]) VALUES (N'e07d8690-09c7-4dfd-88b5-73e644c23f61', N'Nevelő Nikolett', N'niki@nevi.com', 1, N'+36603334033', 3, 439423, N'$2a$11$g9xlWK2aH49Ku5BR8oJSXudRAv75cjojZUIt9lAt2wLHO7CKHIDu6', 1)
GO
INSERT [dbo].[Personnel] ([id], [name], [email], [isPresent], [phone], [Role], [cardId], [password], [canLogIn]) VALUES (N'd66027ad-29eb-4277-8bf7-9ef62e0ee182', N'Webes-Tesztes Viktor', N'web.viktor@teszt.com', 0, N'+36505150515', 4, 352524, N'$2a$11$BxJ9W/ZuiAt3/yvLGMsy8.H3C/8EX96H4Fv/LL23fN3O3Ya.RmhLO', 0)
GO
INSERT [dbo].[Personnel] ([id], [name], [email], [isPresent], [phone], [Role], [cardId], [password], [canLogIn]) VALUES (N'd3396b05-6d5e-4583-8702-b9bee04da9ed', N'Jelszó Jónás', N'jelszo@teszt.com', 1, N'+36703004054', 1, 431747, N'$2a$11$AVJ3fQem/rhzUEUKAcG13.HrRs.kjcKUHGda09n/8hhcZ3CWTYo26', 1)
GO
INSERT [dbo].[Personnel] ([id], [name], [email], [isPresent], [phone], [Role], [cardId], [password], [canLogIn]) VALUES (N'9ae500e8-5f28-4569-98d0-bba3e3c51a5e', N'Dolgozó Dénes Dávid', N'dolgi.dani@te.mps', 0, N'+36505005013', 4, 598487, N'$2a$11$1zbLcjX8ieq74C2VLMZRRuUULAVK2yyra7n/4a8pDT/VxTka0bBfW', 0)
GO
INSERT [dbo].[Personnel] ([id], [name], [email], [isPresent], [phone], [Role], [cardId], [password], [canLogIn]) VALUES (N'b502f8b3-77e8-44c5-bd5a-d352982633d1', N'Admin Ármin', N'armin@admin.com', 1, N'+36603334044', 2, 701650, N'$2a$11$OdAenivi08TqSBxKc.x7ZO8k0BOMVEjL1ySpK1neKRgkM7LogHgBy', 1)
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Jessey McCurrie', 0, N'+36103076664', N'jmccurrie21@usnews.com', N'68ec2ff0-6961-45db-87d1-142b0370de7c', 97165, CAST(N'2005-12-14' AS Date), N'6c212947-5529-4548-b19c-d1304f4a0e1d')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Kerek Kiara', 0, N'+36505005321', N'kkiara@te.mp', N'e765e27b-e07d-42af-998b-2611275fcfc0', 494739, CAST(N'2007-05-02' AS Date), N'0283f721-4db4-42f7-8d24-1d9e87aaf2c8')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Jodie Holhouse', 1, N'+36508002285', N'jholhouseg@addthis.com', N'd082e2af-aa06-45af-a1d9-26be2362fbe0', 96395, CAST(N'2009-05-21' AS Date), N'd879e22f-1c08-4b0e-9d17-b3f1a1802df1')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Aland Tapton', 1, N'+36901206003', N'atapton1i@xing.com', N'457287dd-6a61-404d-85b5-34aa37323c6e', 37658, CAST(N'2008-02-10' AS Date), N'7f03c5a8-9b04-47ee-a158-979ecd31001b')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Sibylle Till', 0, N'+36401056716', N'stillk@un.org', N'5f472c3f-981a-4d91-bb72-35f7ebd1b1d3', 81882, CAST(N'2006-08-19' AS Date), N'6ddb8b11-995d-4561-ab2a-e0b2198f4a42')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Anthea Ericsson', 0, N'+36603966742', N'aericssone@barnesandnoble.com', N'8869de61-c813-4932-8647-3f0215afba76', 32197, CAST(N'2005-01-04' AS Date), N'7f03c5a8-9b04-47ee-a158-979ecd31001b')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Jeremie Jakubowski', 1, N'+36401492479', N'jjakubowski8@hatena.ne.jp', N'e0d26d53-ecb3-4a4b-bc00-4077497936ab', 65005, CAST(N'2005-05-14' AS Date), N'b44f5761-fa85-4b7f-8fe3-a3674cae035a')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Nina Menguy', 1, N'+36801903011', N'nmenguy7@hostgator.com', N'b9936622-5284-46f2-ac52-426b909a1387', 82444, CAST(N'2006-06-11' AS Date), N'89fb9862-7515-4a38-8018-9ca4481ebbac')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Marijo Posnett', 1, N'+36903440995', N'mposnett1n@reddit.com', N'642cd2fc-dd70-414d-bec5-4695d6e5d726', 87179, CAST(N'2009-07-20' AS Date), N'eeb143b1-c5b1-448f-8d78-ffde83d3091f')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Pauli Tutin', 1, N'+36908706305', N'ptutino@webs.com', N'08e9c349-de76-400d-93a8-47017c29aedc', 77652, CAST(N'2008-02-10' AS Date), N'3944b53c-954c-4008-830a-794b1f0f0ae8')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Sher Corps', 1, N'+36709660410', N'scorpsa@pagesperso-orange.fr', N'91301af5-8569-4c9d-a348-52a652d20968', 12942, CAST(N'2009-10-26' AS Date), N'bdd620dc-293a-4b88-bb74-16defa57533e')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Mayne Steet', 1, N'+36502923780', N'msteet1k@pinterest.com', N'352ff403-eb67-4844-ab3b-52e5206f5e0c', 11603, CAST(N'2009-06-19' AS Date), N'd879e22f-1c08-4b0e-9d17-b3f1a1802df1')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Alaric Bravery', 0, N'+36606175611', N'abravery1h@cnbc.com', N'be2b77ac-7e8c-49ba-81cf-5337b3213c77', 54767, CAST(N'2008-06-05' AS Date), N'6c212947-5529-4548-b19c-d1304f4a0e1d')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Kipp Gabbott', 0, N'+36405987221', N'kgabbott1j@ted.com', N'97a07184-7331-4f46-af22-543d3338d5a5', 72586, CAST(N'2008-06-27' AS Date), N'16bf16f3-05eb-49e5-8644-33feab699dc5')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Alexi Lyes', 0, N'+36708526656', N'alyes27@cornell.edu', N'58b3a482-e3ff-415d-8779-552ef85aacb4', 24389, CAST(N'2007-06-03' AS Date), N'eeb143b1-c5b1-448f-8d78-ffde83d3091f')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Pavia Patey', 1, N'+36800423644', N'ppatey13@ustream.tv', N'b89879e1-98a9-40c0-bd4d-57fb93efdf61', 69457, CAST(N'2006-08-31' AS Date), N'eeb143b1-c5b1-448f-8d78-ffde83d3091f')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Riccardo Pacquet', 1, N'+36602722689', N'rpacquet1l@utexas.edu', N'84adf7f1-0346-463c-87a0-58a4319783a5', 56582, CAST(N'2009-09-01' AS Date), N'4709b05a-afff-4830-b0e1-5042c1d84238')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Newton Pentelow', 1, N'+36708456548', N'npentelow24@skyrock.com', N'0ee62dde-1199-4c9f-92e8-5f9bd0fdd4f7', 85976, CAST(N'2009-05-01' AS Date), N'd879e22f-1c08-4b0e-9d17-b3f1a1802df1')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Pinchas Parmley', 1, N'+36403773403', N'pparmley14@icio.us', N'1dc01ce1-a1db-45b0-b716-60f6579c74da', 52165, CAST(N'2006-09-16' AS Date), N'6ddb8b11-995d-4561-ab2a-e0b2198f4a42')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Madelle Whittier', 1, N'+36501400749', N'mwhittier1b@wired.com', N'43a71d03-0988-490c-b7fa-62957a4c3b9e', 53314, CAST(N'2009-02-19' AS Date), N'89fb9862-7515-4a38-8018-9ca4481ebbac')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Mel Pulfer', 1, N'+36803701384', N'mpulfer1c@hibu.com', N'dcf4d212-8b1b-4361-b89f-63466e82b75e', 25034, CAST(N'2005-03-13' AS Date), N'b44f5761-fa85-4b7f-8fe3-a3674cae035a')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Mock Martin', 0, N'+36301234566', N'mock.martin@mock.dat', N'a2bcbfc4-ff96-4cdb-97e3-641bfb790919', 1000001, CAST(N'2000-01-01' AS Date), N'4a7c7d89-f600-4f6a-9ec8-55602d7a1979')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Frannie Cruikshanks', 0, N'+36507064189', N'fcruikshanks1r@squidoo.com', N'1de18bb3-9d7b-4377-89f3-66787e51750b', 74378, CAST(N'2005-02-11' AS Date), N'78609ec1-44c5-4d00-b5b6-f62933b37524')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Alena Jeanequin', 0, N'+36609937519', N'ajeanequin25@ucsd.edu', N'df088e88-1fc8-466b-add2-6bb61ad99aa5', 50778, CAST(N'2005-10-13' AS Date), N'4709b05a-afff-4830-b0e1-5042c1d84238')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Rori Stothert', 0, N'+36708126785', N'rstothertc@addthis.com', N'd741da29-87d1-4f18-8a2f-6dfac279411d', 51208, CAST(N'2005-12-02' AS Date), N'13853841-38a0-4c4f-8f8a-240b2020dae5')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Paulo Beceril', 0, N'+36308297651', N'pbecerilx@tinyurl.com', N'db9bcb77-2c4f-4662-83f7-7ca88266ed4d', 30694, CAST(N'2006-07-21' AS Date), N'6c212947-5529-4548-b19c-d1304f4a0e1d')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Gilli Ridgedell', 1, N'+36603620499', N'gridgedelli@51.la', N'798a91a8-a3d8-4843-af8f-7cbe7e739e9f', 62032, CAST(N'2006-06-22' AS Date), N'9b23e982-6632-40d0-ab23-620f4baff9bd')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Beverly Saterweyte', 1, N'+36702457873', N'bsaterweytep@bizjournals.com', N'7df4d483-30f3-4a3c-a44c-7ef1f6a93584', 89485, CAST(N'2008-07-14' AS Date), N'0c9d01c7-3451-4c92-995f-d1fffcb92be5')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Jorgan O''Mailey', 1, N'+36501958138', N'jomailey1u@naver.com', N'd6c4c4e1-3b4e-49a5-a168-7ffd362bcb24', 88463, CAST(N'2009-03-29' AS Date), N'79320273-d621-4147-abd3-ecdf1a8919c6')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Bonny Madre', 1, N'+36807421540', N'bmadre0@google.com.au', N'8a420f2f-2341-4b94-a7e6-80212e79cdd7', 72027, CAST(N'2008-06-20' AS Date), N'6ddb8b11-995d-4561-ab2a-e0b2198f4a42')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Mikkel Papen', 1, N'+36805230991', N'mpapen16@hhs.gov', N'afe60a7c-31ff-4898-8551-84d1fca7e4c0', 21051, CAST(N'2008-08-26' AS Date), N'1a3bf97f-ba5f-42f6-93c9-67105e526909')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Sascha MacCole', 0, N'+36401154371', N'smaccole1q@accuweather.com', N'89a1de9c-ab18-4b5d-b242-85b39c04d763', 50429, CAST(N'2007-08-04' AS Date), N'1a3bf97f-ba5f-42f6-93c9-67105e526909')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Abigail Vasnev', 1, N'+36604898110', N'avasnevr@shinystat.com', N'eccdf0e0-a143-4d18-a37c-85f53255fe2e', 99656, CAST(N'2007-03-21' AS Date), N'89fb9862-7515-4a38-8018-9ca4481ebbac')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Leilah Ruse', 0, N'+36308173316', N'lruse1g@taobao.com', N'1683e23d-6d43-42d8-9b30-86ee903feba1', 61465, CAST(N'2005-06-30' AS Date), N'13853841-38a0-4c4f-8f8a-240b2020dae5')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Diandra Bufton', 0, N'+36106984704', N'dbuftonj@storify.com', N'bacf6368-0dcb-4940-8e47-8e58801f6467', 97855, CAST(N'2009-12-03' AS Date), N'eeb143b1-c5b1-448f-8d78-ffde83d3091f')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Felice Dmitr', 1, N'+36806241058', N'fdmitr1v@printfriendly.com', N'84943fd6-07b9-49ca-bf01-99f067bb296e', 27643, CAST(N'2005-01-09' AS Date), N'89fb9862-7515-4a38-8018-9ca4481ebbac')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Vivianne Sibbe', 1, N'+36606083324', N'vsibbe1w@comsenz.com', N'c7053de1-f7e7-4dec-8a85-a0d87e03ee58', 28692, CAST(N'2005-05-02' AS Date), N'b44f5761-fa85-4b7f-8fe3-a3674cae035a')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Henrik Purcer', 0, N'+36808144009', N'hpurcer20@elegantthemes.com', N'74a836cb-1abc-46f4-ab23-a137f520e76b', 52993, CAST(N'2006-01-25' AS Date), N'13853841-38a0-4c4f-8f8a-240b2020dae5')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Dedra Viegas', 0, N'+36404512093', N'dviegasq@google.nl', N'8ef3ae06-7e66-4436-bfab-a2535b68fcec', 15765, CAST(N'2005-05-20' AS Date), N'79320273-d621-4147-abd3-ecdf1a8919c6')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Chilton Debney', 0, N'+36407144002', N'cdebney1e@apple.com', N'd48026f5-6a04-4242-8540-a506c7dc81c8', 69246, CAST(N'2005-03-28' AS Date), N'bdd620dc-293a-4b88-bb74-16defa57533e')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Corri Othick', 1, N'+36300287200', N'cothick1s@redcross.org', N'b8e01cf2-c9d6-4a12-914e-a5c8de3f3ce3', 98383, CAST(N'2009-02-16' AS Date), N'3944b53c-954c-4008-830a-794b1f0f0ae8')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Misti Turone', 1, N'+36308162790', N'mturone1z@wikimedia.org', N'2f1c6992-df7f-472f-8ac9-ab04cda60608', 73082, CAST(N'2005-12-25' AS Date), N'41c63122-e2c7-4ffa-903c-5661e22ccd9e')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Venita Checklin', 1, N'+36108936103', N'vchecklin1t@sun.com', N'20a6c44e-9560-4302-8f3f-b373c7b24484', 16228, CAST(N'2006-08-23' AS Date), N'0c9d01c7-3451-4c92-995f-d1fffcb92be5')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Renault Wabe', 0, N'+36306885451', N'rwabes@sun.com', N'9ee19387-fe8a-4231-8931-b885c4434db8', 52788, CAST(N'2009-06-25' AS Date), N'b44f5761-fa85-4b7f-8fe3-a3674cae035a')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Lillis Cassedy', 0, N'+36105841976', N'lcassedy1@wunderground.com', N'99191b85-f0e0-484d-8c57-bbbdce1e6044', 58393, CAST(N'2008-11-10' AS Date), N'da8b6efd-c4e1-47c2-8c7c-dd50cb7914c9')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Teszt Elek II.', 0, N'+36808009000', N'tesztelek.elek@teszt.abc', N'2981cc39-5dab-4549-b3d1-bd6d8fe5d967', 236988, CAST(N'2002-04-10' AS Date), N'311b47ee-555c-4cfe-9d15-16a9feb9d43e')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Constanta Sidary', 0, N'+36203487074', N'csidaryw@google.nl', N'121e27ec-d56f-407f-bc08-bf63f9752f45', 75937, CAST(N'2005-08-20' AS Date), N'13853841-38a0-4c4f-8f8a-240b2020dae5')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Hamnet Leith-Harvey', 0, N'+36202745858', N'hleithharvey3@ask.com', N'8d63ff46-90e4-4e4d-88f4-d03ffacaa182', 16207, CAST(N'2007-05-29' AS Date), N'78609ec1-44c5-4d00-b5b6-f62933b37524')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Kiersten Bellhouse', 1, N'+36804392557', N'kbellhouse1y@phoca.cz', N'72319857-7246-4aef-824a-d12e4845391b', 60127, CAST(N'2007-01-19' AS Date), N'bdd620dc-293a-4b88-bb74-16defa57533e')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Katleen Scimoni', 1, N'+36208259905', N'kscimoni1o@wisc.edu', N'dff47af9-7db3-45ef-bd7b-d1715e9bc980', 35227, CAST(N'2005-09-02' AS Date), N'6ddb8b11-995d-4561-ab2a-e0b2198f4a42')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Griffith Stredder', 1, N'+36104606225', N'gstredder18@harvard.edu', N'7c520750-a09c-435d-8413-d4140413cd79', 68846, CAST(N'2007-03-13' AS Date), N'3944b53c-954c-4008-830a-794b1f0f0ae8')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Kele Cordell', 1, N'+36909581973', N'kcordell2@photobucket.com', N'69161d9b-6efe-4033-ba63-d717537f75af', 92724, CAST(N'2009-08-01' AS Date), N'1a3bf97f-ba5f-42f6-93c9-67105e526909')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Katheryn MacNamara', 1, N'+36405406857', N'kmacnamaraz@telegraph.co.uk', N'6ff06fb1-949e-4e03-b803-d76783cf572d', 96506, CAST(N'2008-04-16' AS Date), N'16bf16f3-05eb-49e5-8644-33feab699dc5')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Cleon Karim', 1, N'+36509560200', N'ckarimy@usa.gov', N'3262311d-c297-483a-9092-ddb86ac37f80', 53521, CAST(N'2006-06-07' AS Date), N'7f03c5a8-9b04-47ee-a158-979ecd31001b')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Mallissa Heynel', 1, N'+36100791762', N'mheynelh@ezinearticles.com', N'ba24abfc-d487-4354-b6d7-deb9603cb3d2', 30877, CAST(N'2007-07-17' AS Date), N'4709b05a-afff-4830-b0e1-5042c1d84238')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Giorgio Byfford', 0, N'+36104598353', N'gbyffordm@ameblo.jp', N'09295184-95b1-4351-8364-e32f261282ae', 88266, CAST(N'2009-02-12' AS Date), N'1a3bf97f-ba5f-42f6-93c9-67105e526909')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Sue Pickworth', 1, N'+36800635306', N'spickworth17@themeforest.net', N'f33a258c-f3e8-4c2d-bbef-e3ed0898d82f', 77835, CAST(N'2006-12-01' AS Date), N'78609ec1-44c5-4d00-b5b6-f62933b37524')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Rodrique Halpin', 1, N'+36304652873', N'rhalpinu@upenn.edu', N'4e323055-1e1c-4788-b4c9-e97bbfe753db', 45340, CAST(N'2005-05-20' AS Date), N'bdd620dc-293a-4b88-bb74-16defa57533e')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Marwin Ormistone', 0, N'+36700157258', N'mormistonev@nsw.gov.au', N'ed57a869-0319-4a1c-bcfd-ebfd5d50f753', 18430, CAST(N'2007-02-03' AS Date), N'41c63122-e2c7-4ffa-903c-5661e22ccd9e')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Carl Locksley', 0, N'+36908279416', N'clocksleyl@dailymail.co.uk', N'9133f8eb-642d-4117-b294-ec4fabe92bbf', 77992, CAST(N'2005-03-03' AS Date), N'da8b6efd-c4e1-47c2-8c7c-dd50cb7914c9')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Fanya Jaskowicz', 0, N'+36308567289', N'fjaskowicz4@dedecms.com', N'f2bb06e9-ff59-4ed2-92c6-ed5e3abec064', 49137, CAST(N'2005-08-10' AS Date), N'3944b53c-954c-4008-830a-794b1f0f0ae8')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Mikel Girvin', 1, N'+36409976362', N'mgirvin9@bloomberg.com', N'db67211f-74c9-48ac-8da7-ee316ec678d2', 12853, CAST(N'2007-03-01' AS Date), N'b00f1514-a9cf-470e-9aa0-5813917ed734')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Cam Joynes', 0, N'+36606911326', N'cjoynes1x@addtoany.com', N'94943f7a-42cc-4a4b-9a66-f070cba56a2e', 68265, CAST(N'2006-04-26' AS Date), N'b00f1514-a9cf-470e-9aa0-5813917ed734')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Harald Ber', 0, N'+36700066868', N'hber1m@hostgator.com', N'38b7993f-aabb-4325-935a-f4136fbec0c7', 64966, CAST(N'2008-08-26' AS Date), N'9b23e982-6632-40d0-ab23-620f4baff9bd')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Suzy Orsman', 0, N'+36508056247', N'sorsman15@w3.org', N'35dca20a-81c6-4c36-83ad-f64feb0f35fe', 20872, CAST(N'2008-08-24' AS Date), N'da8b6efd-c4e1-47c2-8c7c-dd50cb7914c9')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Szép Keresznév', 1, N'+36602003000', N'nemtudomhogymilegyen@az.email.cim', N'98b6ec87-9962-4fd5-b62b-f83bb2a9cc9c', 759935, CAST(N'2010-04-10' AS Date), N'311b47ee-555c-4cfe-9d15-16a9feb9d43e')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Reynold Giraudou', 1, N'+36106980837', N'rgiraudoub@theatlantic.com', N'e95c06ef-8e4b-4baf-984c-fa10b156ecd6', 15459, CAST(N'2009-11-08' AS Date), N'41c63122-e2c7-4ffa-903c-5661e22ccd9e')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Jerry Etherington', 0, N'+36105143133', N'jetherington10@acquirethisname.com', N'5ec55f7b-1d09-43c2-8bbb-fa9641a005d7', 10417, CAST(N'2006-10-25' AS Date), N'd879e22f-1c08-4b0e-9d17-b3f1a1802df1')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Nettie Dives', 0, N'+36102933311', N'ndives1d@edublogs.org', N'09114e4a-a7d4-413e-bab8-fb7d1d0c4fd0', 89803, CAST(N'2007-04-21' AS Date), N'b00f1514-a9cf-470e-9aa0-5813917ed734')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Godart Bernardet', 1, N'+36500811770', N'gbernardet22@meetup.com', N'5c3e0985-3031-4e50-ac5d-fc801b2c2760', 35872, CAST(N'2008-06-02' AS Date), N'7f03c5a8-9b04-47ee-a158-979ecd31001b')
GO
INSERT [dbo].[Students] ([name], [isPresent], [phone], [email], [id], [cardId], [birthDate], [parentId]) VALUES (N'Kissie Chislett', 0, N'+36109115090', N'kchislett26@sfgate.com', N'067ffac4-e3cf-4d23-95e0-fccd7bfc036c', 79448, CAST(N'2009-04-08' AS Date), N'9b23e982-6632-40d0-ab23-620f4baff9bd')
GO
/****** Object:  Index [IX_faculty]    Script Date: 2024. 04. 22. 8:09:59 ******/
ALTER TABLE [dbo].[Personnel] ADD  CONSTRAINT [IX_faculty] UNIQUE NONCLUSTERED 
(
	[cardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Personnel]    Script Date: 2024. 04. 22. 8:09:59 ******/
CREATE NONCLUSTERED INDEX [IX_Personnel] ON [dbo].[Personnel]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Students_Phone]    Script Date: 2024. 04. 22. 8:09:59 ******/
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [Students_Phone] UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [students_pk]    Script Date: 2024. 04. 22. 8:09:59 ******/
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [students_pk] UNIQUE NONCLUSTERED 
(
	[cardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GateLogs] ADD  CONSTRAINT [DF_gate_logs_is_guest]  DEFAULT ((0)) FOR [isGuest]
GO
ALTER TABLE [dbo].[Parents] ADD  CONSTRAINT [DF_parents_id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Personnel] ADD  CONSTRAINT [DF__faculty__id__4AB81AF0]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Personnel] ADD  DEFAULT ((4)) FOR [Role]
GO
ALTER TABLE [dbo].[Personnel] ADD  CONSTRAINT [DF_Faculty_password]  DEFAULT (N'test') FOR [password]
GO
ALTER TABLE [dbo].[Personnel] ADD  CONSTRAINT [DF_Personnel_canLogIn]  DEFAULT ((0)) FOR [canLogIn]
GO
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_students_is_present]  DEFAULT ((0)) FOR [isPresent]
GO
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_students_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([id])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Students]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Parents] FOREIGN KEY([parentId])
REFERENCES [dbo].[Parents] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Parents]
GO
USE [master]
GO
ALTER DATABASE [acs] SET  READ_WRITE 
GO
